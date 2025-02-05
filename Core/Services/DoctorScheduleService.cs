﻿using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.ResponseDtos.DoctorSchedule;

namespace Core.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _repository;

        public DoctorScheduleService(IDoctorScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsDayOffAsync(int doctorId, DateTime date)
        {
            var daysOff = await GetDaysOffAsync(doctorId, date.Month, date.Year);
            var busyDays = await GetBusyDaysAsync(doctorId, date.Month, date.Year);
            var isDayOff = daysOff.Contains(date) || busyDays.Contains(date) ? true : false;

            return isDayOff;
        }

        public async Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date)
        {
            var workTime = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new
                {
                    WorkDay = d.WorkWeek.FirstOrDefault(w => w.WeekDay == date.DayOfWeek),
                    Meetings = d.Meetings
                        .Where(m => m.DateAndTime.Date == date.Date)
                        .Select(m => m.DateAndTime.TimeOfDay)
                        .ToList()
                })
                .FirstAsync();

            var availableMeetings = new List<string>();

            while (workTime.WorkDay.Start <= workTime.WorkDay.End)
            {
                if (!workTime.Meetings.Contains(workTime.WorkDay.Start))
                {
                    availableMeetings.Add($"{workTime.WorkDay.Start.Hours:D2} : {workTime.WorkDay.Start.Minutes:D2}");
                }

                workTime.WorkDay.Start = workTime.WorkDay.Start.Add(TimeSpan.FromMinutes(workTime.WorkDay.MeetingTimeMinutes));
            }

            return availableMeetings;
        }

        public async Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(int doctorId, int month, int year)
        {
            var busyDays = await GetBusyDaysAsync(doctorId, month, year);
            var daysOff = await GetDaysOffAsync(doctorId, month, year);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var monthSchedule = new List<MonthScheduleResponse>();

            if (daysOff.Count() > 0)
            {
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateTime(year, month, day);

                    var isAvailable =
                        daysOff.Contains(date) ||
                        busyDays.Contains(date)
                        ? false : true;

                    monthSchedule.Add(new MonthScheduleResponse(date, isAvailable));
                }
            }

            return monthSchedule;
        }

        private async Task<IEnumerable<DateTime>> GetDaysOffAsync(int doctorId, int month, int year)
        {
            var monthDaysOff = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new
                {
                    DaysOff = d.DaysOff
                        .Where(doff => doff.Month == month)
                        .Select(doff => new DateTime(DateTime.Now.Year, doff.Month, doff.Day))
                        .ToList(),
                    WorkWeekDaysOff = d.WorkWeek
                        .Where(wd => !wd.IsWorkDay)
                        .Select(wd => wd.WeekDay)
                        .ToList()
                })
                .FirstAsync();

            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            var weeklyDaysOffDates = Enumerable.Range(0, (lastDateOfMonth - firstDateOfMonth).Days + 1)
                             .Select(day => firstDateOfMonth.AddDays(day))
                             .Where(date => monthDaysOff.WorkWeekDaysOff.Contains(date.DayOfWeek))
                             .ToList();

            var datesOffResult = new List<DateTime>();
            datesOffResult.AddRange(monthDaysOff.DaysOff);
            datesOffResult.AddRange(weeklyDaysOffDates);

            return datesOffResult;
        }

        private async Task<IEnumerable<DateTime>> GetBusyDaysAsync(int doctorId, int month, int year)
        {
            var shedule = await _context.DoctorWeekDays
                    .AsNoTracking()
                    .Where(d => d.Id == doctorId)
                    .Select(d => new
                    {
                        MeetingTimeMinutes = d.MeetingTimeMinutes,
                        AllMeetings = d.Doctor.Meetings
                            .Where(m => m.DateAndTime.Month == month && m.DateAndTime.Year == year)
                            .Select(m => m.DateAndTime),
                        WeekDays = d.Doctor.WorkWeek
                            .Where(wk => wk.IsWorkDay)
                            .Select(wk => new
                            {
                                WeekDay = wk.WeekDay,
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
                var dayOfWeek = shedule.WeekDays.FirstOrDefault(ww => ww.WeekDay == meetingDay.DayOfWeek);
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
