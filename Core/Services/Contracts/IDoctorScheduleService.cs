using Core.Models.ResponseDtos.DoctorSchedule;

namespace Core.Services.Contracts
{
    public interface IDoctorScheduleService
    {
        Task<bool> IsDateValidAsync(int doctorId, DateTime date);
        Task<bool> IsDayOffAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date);
        Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(int doctorId, int month, int year);
    }
}
