using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.ResponseDtos.Meetings;
using RestAPI.Dtos.RequestDtos.Meetings;

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

        public async Task<bool> IsMeetingExistAsync(int meetingId)
        {
            var ssMeetingExist = await _meetingsRepo.IsMeetingExistAsync(meetingId);

            return ssMeetingExist;
        }

        public async Task<bool> isMeetingScheduled(string patientId, int doctorId)
        {
            var isMeetingScheduled = await _meetingsRepo.IsMeetingScheduled(patientId, doctorId);

            return isMeetingScheduled;
        }
    }
}
