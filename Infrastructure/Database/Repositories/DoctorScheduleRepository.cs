using Core.DTOs.RequestDtos.Doctors;
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

        public async Task RemoveDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff)
        {
            var daysOffInDb = await _context.DoctorsDaysOff
                .Where(doff => doff.DoctorId == doctorId)
                .ToListAsync();

            var daysOffToRemove = daysOffInDb
                .Where(doff => daysOff.Any(r => r.Day == doff.Day && r.Month == doff.Month))
                .ToList();

            _context.DoctorsDaysOff.RemoveRange(daysOffToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task AddDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff)
        {
            var daysOffToAdd = daysOff.Select(doff => new DoctorDayOff()
            {
                Day = doff.Day,
                Month = doff.Month,
                DoctorId = doctorId
            });

            await _context.DoctorsDaysOff.AddRangeAsync(daysOffToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDateUnavailableAsync(int doctorId, DateTime date)
        {
            return await _context.Doctors
                .Where(d => d.Id == doctorId)
                .AnyAsync(d => d.DaysOff.Any(ddoff => ddoff.Day == date.Day && ddoff.Month == date.Month)
                || d.Meetings.Any(m => m.DateAndTime == date)
                || d.WorkWeek
                    .Where(wd => !wd.IsWorkDay)
                    .Any(wd => wd.WeekDay == date.DayOfWeek));
        }

        public async Task<MonthlyUnavailableDaysModel> GetMonthlyUnavailableDaysAsync(GetMonthScheduleRequest requestData)
        {
            return await _context.Doctors
                .Where(d => d.Id == requestData.DoctorId)
                .Select(d => new MonthlyUnavailableDaysModel()
                {
                    DaysOff = d.DaysOff
                        .Where(ddoff => ddoff.Month == requestData.Month)
                        .Select(ddoff => new DateTime(requestData.Year, requestData.Month, ddoff.Day))
                        .ToList(),
                    BusyDays = d.Meetings
                        .Where(m => m.DateAndTime.Month == requestData.Month && m.DateAndTime.Year == requestData.Year)
                        .Select(m => m.DateAndTime)
                        .ToList(),
                    WeeklyDaysOff = d.WorkWeek
                        .Where(ww => !ww.IsWorkDay)
                        .Select(ww => ww.WeekDay)
                        .ToList()
                })
            .FirstAsync();
        }

        public async Task UpdateWeeklySchedule(string userId, IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule)
        {
            var weekDaysIds = weeklySchedule.Select(d => d.Id);

            var weekDays = await _context.DoctorsWeekDays
                .Where(dwd => weekDaysIds.Contains(dwd.Id))
                .ToListAsync();
            
            foreach (var weekDay in weekDays)
            {
                var currentDay = weeklySchedule.First(d => d.Id == weekDay.Id);
            
                weekDay.IsWorkDay = currentDay.IsWorkDay;
                weekDay.WorkDayStart = currentDay.WorkDayStart;
                weekDay.WorkDayEnd = currentDay.WorkDayEnd;
                weekDay.MeetingTimeMinutes = currentDay.MeetingTimeMinutes;
            }

            await _context.SaveChangesAsync();
        }
    }
}
