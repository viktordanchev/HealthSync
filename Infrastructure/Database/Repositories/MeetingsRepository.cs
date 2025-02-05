using Core.Interfaces.Repository;
using Core.Models.ResponseDtos.Meetings;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using RestAPI.Dtos.RequestDtos.Meetings;

namespace Infrastructure.Database.Repositories
{
    public class MeetingsRepository : IMeetingsRepository
    {
        private readonly HealthSyncDbContext _context;

        public MeetingsRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorMeetingAsync(AddMeetingRequest requestData)
        {
            var meeting = new Meeting()
            {
                DoctorId = requestData.DoctorId,
                DateAndTime = requestData.DateAndTime,
                PatientId = requestData.PatientId
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

        public async Task<bool> IsMeetingScheduled(string patientId, int doctorId)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.PatientId == patientId && m.DoctorId == doctorId);

            return meeting != null;
        }
    }
}
