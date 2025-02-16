using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Models.DoctorSchedule;

namespace Core.Interfaces.Service
{
    public interface IDoctorScheduleService
    {
        Task<bool> IsDayValidAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(GetAvailableMeetingHours requestData);
        Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(GetMonthScheduleRequest requestData);
        Task<IEnumerable<DoctorDayOffModel>> GetAllDaysOffAsync(string userId);
    }
}
