using Core.Models.ResponseDtos.Meetings;
using RestAPI.Dtos.RequestDtos.Meetings;

namespace Core.Interfaces.Service
{
    public interface IMeetingsService
    {
        Task AddDoctorMeetingAsync(AddMeetingRequest requestData);
        Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId);
        Task DeleteMeetingAsync(int meetingId);
        Task<bool> IsMeetingExistAsync(int meetingId);
        Task<bool> isMeetingScheduled(string userId, int doctorId);
    }
}
