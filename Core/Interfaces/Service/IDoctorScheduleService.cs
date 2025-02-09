using Core.DTOs.ResponseDtos.DoctorSchedule;

namespace Core.Interfaces.Service
{
    public interface IDoctorScheduleService
    {
        //Task<bool> IsDayOffAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date);
        //Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(int doctorId, int month, int year);
    }
}
