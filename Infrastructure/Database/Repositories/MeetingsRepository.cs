using Core.DTOs.RequestDtos.Meetings;
using Core.DTOs.ResponseDtos.Meetings;
using Core.Interfaces.Repository;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

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
            var meeting = new DoctorMeeting()
            {
                DoctorId = requestData.DoctorId,
                DateAndTime = DateTime.Parse(requestData.DateAndTime),
                PatientId = requestData.PatientId
            };

            await _context.DoctorsMeetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId)
        {
            var meetings = await _context.DoctorsMeetings
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
            var meeting = await _context.DoctorsMeetings
                .FirstAsync(m => m.Id == meetingId);

            _context.DoctorsMeetings.Remove(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsMeetingScheduled(string patientId, int doctorId)
        {
            var meeting = await _context.DoctorsMeetings.FirstOrDefaultAsync(m => m.PatientId == patientId && m.DoctorId == doctorId);

            return meeting != null;
        }

        public async Task<IEnumerable<DailyMeetingResponse>> GetDoctorMeetingsAsync(string userId)
        {
            return await _context.DoctorsMeetings
                .AsNoTracking()
                .Where(dm => dm.Doctor.IdentityId == userId)
                .OrderBy(dm => dm.DateAndTime)
                .Select(dm => new DailyMeetingResponse() 
                {
                    Id = dm.Id,
                    DateAndTime = dm.DateAndTime,
                    PatientId = dm.PatientId,
                    PatientName = $"{dm.Patient.FirstName} {dm.Patient.LastName}",
                    PatientPhoneNumber = dm.Patient.PhoneNumber
                })
                .ToListAsync();
        }
    }
}
