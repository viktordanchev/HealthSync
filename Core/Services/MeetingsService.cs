using Core.Models.ResponseDtos.Meetings;
using Core.Services.Contracts;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly HealthSyncDbContext _context;

        public MeetingsService(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorMeetingAsync(int doctorId, DateTime date, string patientId)
        {
            var meeting = new Meeting()
            {
                DoctorId = doctorId,
                DateAndTime = date,
                PatientId = patientId
            };

            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId)
        {
            var meetings = await _context.Meetings
                .AsNoTracking()
                .Where(m => m.PatientId == userId)
                .OrderBy(m => m.DateAndTime)
                .Select(m => new DoctorMeetingInfoResponse()
                {
                    Id = m.Id,
                    Name = $"{m.Doctor.Identity.FirstName} {m.Doctor.Identity.LastName}",
                    ImgUrl = m.Doctor.ImgUrl,
                    Hospital = m.Doctor.Hospital.Name,
                    HospitalAddress = m.Doctor.Hospital.Address,
                    DateAndTime = m.DateAndTime
                })
                .ToListAsync();

            return meetings;
        }

        public async Task DeleteMeetingAsync(int meetingId)
        {
            var meeting = await _context.Meetings
                .FirstAsync(m => m.Id == meetingId);

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsMeetingExistAsync(int meetingId)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == meetingId);

            return meeting != null;
        }

        public async Task<bool> isMeetingScheduled(string userId, int doctorId)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.PatientId == userId && m.DoctorId == doctorId);

            return meeting != null;
        }
    }
}
