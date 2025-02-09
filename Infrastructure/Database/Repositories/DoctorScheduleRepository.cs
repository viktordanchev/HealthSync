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

        public async Task<DoctorDailyScheduleModel> GetDoctorDailySchedule(int doctorId, DateTime dateAndTime)
        {
            var dailySchedule = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorDailyScheduleModel()
                {
                    WorkDay = d.WorkWeek
                        .Where(w => w.WeekDay == dateAndTime.DayOfWeek)
                        .Select(wd => new WorkDayModel() 
                        { 
                            Start = wd.Start,
                            End = wd.End,
                            MeetingTimeMinutes = wd.MeetingTimeMinutes
                        })
                        .First(),
                    Meetings = d.Meetings
                        .Where(m => m.DateAndTime.Date == dateAndTime.Date)
                        .Select(m => m.DateAndTime.TimeOfDay)
                        .ToList()
                })
                .FirstAsync();

            return dailySchedule;
        }
    }
}
