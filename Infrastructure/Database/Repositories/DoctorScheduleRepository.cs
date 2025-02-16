using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Interfaces.Repository;
using Core.Models.DoctorSchedule;
using Infrastructure.Database.Entities;
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

        public async Task<DailyScheduleModel> GetDailyScheduleAsync(int doctorId, DateTime date)
        {
            var dailySchedule = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DailyScheduleModel()
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

        public async Task<IEnumerable<DayOffResponse>> GetAllDaysOffAsync(string userId)
        {
            var daysOff = await _context.DoctorsDaysOff
                .AsNoTracking()
                .Where(ddo => ddo.Doctor.IdentityId == userId)
                .Select(ddo => new DayOffResponse()
                {
                    Month = ddo.Month,
                    Day = ddo.Day,
                })
                .ToListAsync();

            return daysOff;
        }

        public async Task<MonthlyBusyDaysModel> GetMonthlyBusyDaysAsync(int doctorId, int month, int year)
        {
            var monthlyBusyDays = await _context.DoctorsWeekDays
            .AsNoTracking()
            .Where(d => d.Id == doctorId)
            .Select(d => new MonthlyBusyDaysModel()
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

        public async Task UpdateDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> updatedDaysOff)
        {
            var daysOff = await _context.DoctorsDaysOff
                .AsNoTracking()
                .Where(doff => doff.DoctorId == doctorId)
                .ToListAsync();

            var newDaysOff = updatedDaysOff
                .Where(udoff => !daysOff.Any(doff => udoff.Day == doff.Day && udoff.Month == doff.Month))
                .Select(udoff => new DoctorDayOff() 
                { 
                    DoctorId = doctorId,
                    Day = udoff.Day,
                    Month = udoff.Month
                })
                .ToList();

            var removeDaysOff = daysOff.Where(doff => !updatedDaysOff.Any(udoff => udoff.Day == doff.Day && udoff.Month == doff.Month)).ToList();
            _context.DoctorsDaysOff.RemoveRange(removeDaysOff);

            await _context.DoctorsDaysOff.AddRangeAsync(newDaysOff);
            await _context.SaveChangesAsync();
        }
    }
}
