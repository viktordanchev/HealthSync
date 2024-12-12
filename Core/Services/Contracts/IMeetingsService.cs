using Core.Models.ResponseDtos.Meetings;

namespace Core.Services.Contracts
{
    public interface IMeetingsService
    {
        Task AddDoctorMeetingAsync(int doctorId, DateTime date, string patientId);
        Task<IEnumerable<DoctorMeetingInfoResponse>> GetUserMeetingsAsync(string userId);
        Task DeleteMeetingAsync(int meetingId);
        Task<bool> IsMeetingExistAsync(int meetingId);
        Task<bool> IsUserHasMeetingAsync(string userId, int doctorId);
    }
}
