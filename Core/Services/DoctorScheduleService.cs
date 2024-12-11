using Core.Models.DoctorSchedule;
using Core.Models.ResponseDtos.DoctorSchedule;
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
            var daysOff = await GetDaysOffAsync(doctorId);

            return daysOff.DaysOff.Any(d => d.Date == date) || daysOff.WeeklyDaysOff.Any(d => d == date.DayOfWeek);
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
                        .Where(m => m.Date.Date == date)
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

        public async Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(int doctorId, int month, int year)
        {
            var busyDays = await GetBusyDaysAsync(doctorId, month, year);
            var daysOff = await GetDaysOffAsync(doctorId);
            int daysInMonthNum = DateTime.DaysInMonth(year, month);
            var daysInMonth = new List<MonthScheduleResponse>();
            bool isAvailable;

            for (int day = 1; day <= daysInMonthNum; day++)
            {
                var date = new DateTime(year, month, day);

                isAvailable =
                    daysOff.DaysOff.Contains(date) ||
                    daysOff.WeeklyDaysOff.Contains(date.DayOfWeek) ||
                    busyDays.Contains(date)
                    ? false : true;

                daysInMonth.Add(new MonthScheduleResponse(date.ToString("yyyy-MM-dd"), isAvailable));
            }

            return daysInMonth;
        }

        public async Task<bool> IsDateValidAsync(int doctorId, DateTime date)
        {
            var busyDays = await GetBusyDaysAsync(doctorId, date.Month, date.Year);
            var daysOff = await GetDaysOffAsync(doctorId);

            return daysOff.DaysOff.Contains(date) ||
                    daysOff.WeeklyDaysOff.Contains(date.DayOfWeek) ||
                    busyDays.Contains(date)
                    ? false : true;
        }

        private async Task<UnavailableDaysResponse> GetDaysOffAsync(int doctorId)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new UnavailableDaysResponse()
                {
                    DaysOff = d.DaysOff
                        .Select(doff => doff.Date)
                        .ToList(),
                    WeeklyDaysOff = d.WorkWeek
                        .Where(wk => !wk.IsWorkDay)
                        .Select(wk => wk.Day)
                        .ToList()
                })
                .FirstAsync();
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
