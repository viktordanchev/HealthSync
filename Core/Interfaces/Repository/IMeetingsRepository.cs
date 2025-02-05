using Core.Models.ResponseDtos.Meetings;
using RestAPI.Dtos.RequestDtos.Meetings;

namespace Core.Interfaces.Repository
{
    public interface IMeetingsRepository
    {
        Task AddDoctorMeetingAsync(AddMeetingRequest requestData);
        Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId);
        Task DeleteMeetingAsync(int meetingId);
        Task<bool> IsMeetingExistAsync(int meetingId);
        Task<bool> IsMeetingScheduled(string patientId, int doctorId);
    }
}
