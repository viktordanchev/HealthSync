using Core.DTOs.RequestDtos.Meetings;
using Core.DTOs.ResponseDtos.Meetings;

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
