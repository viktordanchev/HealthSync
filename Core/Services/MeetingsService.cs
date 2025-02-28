using Core.DTOs.RequestDtos.Meetings;
using Core.DTOs.ResponseDtos.Meetings;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly IMeetingsRepository _meetingsRepo;

        public MeetingsService(IMeetingsRepository meetingsRepo)
        {
            _meetingsRepo = meetingsRepo;
        }

        public async Task AddDoctorMeetingAsync(AddMeetingRequest requestData)
        {
            await _meetingsRepo.AddDoctorMeetingAsync(requestData);
        }

        public async Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId)
        {
            var meetings = await _meetingsRepo.GetUserMeetingsAsync(userId);

            return meetings;
        }

        public async Task DeleteMeetingAsync(int meetingId)
        {
            await _meetingsRepo.DeleteMeetingAsync(meetingId);
        }

        public async Task<bool> isMeetingScheduled(string patientId, int doctorId)
        {
            var isMeetingScheduled = await _meetingsRepo.IsMeetingScheduled(patientId, doctorId);

            return isMeetingScheduled;
        }

        public async Task<IEnumerable<DoctorMeetingResponse>> GetDoctorMeetingsAsync(string userId)
        {
            var allMeetings = await _meetingsRepo.GetDoctorMeetingsAsync(userId);

            var dates = allMeetings.Select(d => d.DateAndTime.Date).ToHashSet();
            var result = new List<DoctorMeetingResponse>(); 

            foreach (var date in dates)
            {
                result.Add(new DoctorMeetingResponse() 
                { 
                    Date = date.ToString("dd.MM.yyyy"),
                    DailyMeetings = allMeetings.Where(m => m.DateAndTime.Date == date)
                });
            }

            return result;
        }
    }
}
