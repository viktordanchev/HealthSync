using Core.Models.DoctorSchedule;
using Core.Services.Contracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly HealthSyncDbContext _context;

        public DoctorScheduleService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsDayOffAsync(int doctorId, DateTime date)
        {
            var daysOff = await GetDaysOffAsync(doctorId, date.Month, date.Year);

            return daysOff.Any(d => d.Date == date);
        }

        public async Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date)
        {
            var workTime = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorWorkTimeModel()
                {
                    MeetingTimeMinutes = d.MeetingTimeMinutes,
                    WorkDayStart = d.WorkWeek.First(wd => wd.Day == date.DayOfWeek).Start,
                    WorkDayEnd = d.WorkWeek.First(wd => wd.Day == date.DayOfWeek).End,
                    Meetings = d.Meetings
                        .Where(m => m.Date.Date == date.Date)
                        .Select(m => m.Date.TimeOfDay)
                        .ToList()
                })
                .FirstAsync();

            var availableMeetings = new List<string>();

            while (workTime.WorkDayStart <= workTime.WorkDayEnd)
            {
                if (!workTime.Meetings.Contains(workTime.WorkDayStart))
                {
                    availableMeetings.Add($"{workTime.WorkDayStart.Hours:D2} : {workTime.WorkDayStart.Minutes:D2}");
                }

                workTime.WorkDayStart = workTime.WorkDayStart.Add(TimeSpan.FromMinutes(workTime.MeetingTimeMinutes));
            }

            return availableMeetings;
        }

        public async Task<IEnumerable<DateTime>> GetMonthUnavailableDaysAsync(int doctorId, int month, int year)
        {
            var busyDays = await GetBusyDaysAsync(doctorId, month, year);
            var daysOff = await GetDaysOffAsync(doctorId, month, year);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var unavailableDays = new List<DateTime>();

            if (daysInMonth != daysOff.Count())
            {
                unavailableDays.AddRange(busyDays);
                unavailableDays.AddRange(daysOff);
            }

            return unavailableDays;
        }

        public async Task<bool> IsDateValidAsync(int doctorId, DateTime date)
        {
            var busyDays = await GetBusyDaysAsync(doctorId, date.Month, date.Year);
            var daysOff = await GetDaysOffAsync(doctorId, date.Month, date.Year);
            var isDateValid = daysOff.Contains(date) || busyDays.Contains(date) ? false : true;

            return isDateValid;
        }

        private async Task<IEnumerable<DateTime>> GetDaysOffAsync(int doctorId, int month, int year)
        {
            var datesOffTask = _context.DaysOff
                .AsNoTracking()
                .Where(d => d.DoctorId == doctorId && d.Month == month)
                .Select(d => new DateTime(DateTime.Now.Year, d.Month, d.Day))
                .ToListAsync();

            var weeklyDaysOffTask = _context.WeekDays
                .AsNoTracking()
                .Where(wd => wd.DoctorId == doctorId & !wd.IsWorkDay)
                .Select(wd => wd.Day)
                .ToListAsync();

            await Task.WhenAll(datesOffTask, weeklyDaysOffTask);

            var datesOff = datesOffTask.Result;
            var weeklyDaysOff = weeklyDaysOffTask.Result;
            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            var weeklyDaysOffDates = Enumerable.Range(0, (lastDateOfMonth - firstDateOfMonth).Days + 1)
                             .Select(day => firstDateOfMonth.AddDays(day))
                             .Where(date => weeklyDaysOff.Contains(date.DayOfWeek))
                             .ToList();

            var datesOffResult = new List<DateTime>();
            datesOffResult.AddRange(datesOff);
            datesOffResult.AddRange(weeklyDaysOffDates);

            return datesOffResult;
        }

        private async Task<IEnumerable<DateTime>> GetBusyDaysAsync(int doctorId, int month, int year)
        {
            var shedule = await _context.Doctors
                    .AsNoTracking()
                    .Where(d => d.Id == doctorId)
                    .Select(d => new MonthScheduleModel()
                    {
                        MeetingTimeMinutes = d.MeetingTimeMinutes,
                        AllMeetings = d.Meetings
                            .Where(m => m.Date.Month == month && m.Date.Year == year)
                            .Select(m => m.Date),
                        WeekDays = d.WorkWeek
                            .Where(wk => wk.IsWorkDay)
                            .Select(wk => new WeekDayModel()
                            {
                                Day = wk.Day,
                                WorkDayStart = wk.Start,
                                WorkDayEnd = wk.End
                            })
                    })
                    .FirstAsync();

            var meetingDays = shedule.AllMeetings
                .Select(am => am.Date)
                .ToHashSet();

            foreach (var meetingDay in meetingDays)
            {
                var allMeetingsByDay = shedule.AllMeetings.Where(am => am.Date == meetingDay).Count();
                var dayOfWeek = shedule.WeekDays.FirstOrDefault(ww => ww.Day == meetingDay.DayOfWeek);
                var count = (dayOfWeek.WorkDayEnd - dayOfWeek.WorkDayStart).TotalMinutes / shedule.MeetingTimeMinutes + 1;

                if (allMeetingsByDay != count)
                {
                    meetingDays.Remove(meetingDay);
                }
            }

            return meetingDays;
        }
    }
}
