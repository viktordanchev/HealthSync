using Core.Interfaces.Repository;
using Core.Models.DoctorSchedule;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly HealthSyncDbContext _context;

        public DoctorScheduleRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorDailyScheduleModel> GetDoctorDailyScheduleAsync(int doctorId, DateTime date)
        {
            var dailySchedule = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorDailyScheduleModel()
                {
                    WorkDay = d.WorkWeek
                        .Where(w => w.WeekDay == date.DayOfWeek)
                        .Select(wd => new WorkDayModel()
                        {
                            WorkDayStart = wd.WorkDayStart,
                            WorkDayEnd = wd.WorkDayEnd,
                            MeetingTimeMinutes = wd.MeetingTimeMinutes
                        })
                        .First(),
                    Meetings = d.Meetings
                        .Where(m => m.DateAndTime.Date == date.Date)
                        .Select(m => new TimeOnly(m.DateAndTime.Hour, m.DateAndTime.Minute))
                        .ToList()
                })
                .FirstAsync();

            return dailySchedule;
        }

        public async Task<MonthlyDaysOffModel> GetMonthlyDaysOffAsync(int doctorId, int month)
        {
            var monthlyDaysOff = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new MonthlyDaysOffModel()
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

            return monthlyDaysOff;
        }

        public async Task<DoctorMonthlyBusyDaysModel> GetMonthlyBusyDaysAsync(int doctorId, int month, int year)
        {
            var monthlyBusyDays = await _context.DoctorsWeekDays
            .AsNoTracking()
            .Where(d => d.Id == doctorId)
            .Select(d => new DoctorMonthlyBusyDaysModel()
            {
                AllMeetings = d.Doctor.Meetings
                    .Where(m => m.DateAndTime.Month == month && m.DateAndTime.Year == year)
                    .Select(m => m.DateAndTime),
                WeekDays = d.Doctor.WorkWeek
                    .Where(wk => wk.IsWorkDay)
                    .Select(wk => new WorkDayModel()
                    {
                        WorkDayStart = wk.WorkDayStart,
                        WorkDayEnd = wk.WorkDayEnd,
                        WeekDay = wk.WeekDay,
                        MeetingTimeMinutes = wk.MeetingTimeMinutes
                    })
            })
            .FirstAsync();

            return monthlyBusyDays;
        }
    }
}
