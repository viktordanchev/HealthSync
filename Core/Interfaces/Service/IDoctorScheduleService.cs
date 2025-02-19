using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;

namespace Core.Interfaces.Service
{
    public interface IDoctorScheduleService
    {
        Task<bool> IsDateUnavailableAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date);
        Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(GetMonthScheduleRequest requestData);
        Task<IEnumerable<DayOffResponse>> GetAllDaysOffAsync(string userId);
        Task UpdateDaysOffAsync(string userId, IEnumerable<DayOffResponse> updatedDaysOff);
        Task UpdateWeeklySchedule(string userId, IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule);
    }
}
