using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Models.DoctorSchedule;

namespace Core.Interfaces.Repository
{
    public interface IDoctorScheduleRepository
    {
        Task<DailyScheduleModel> GetDailyScheduleAsync(int doctorId, DateTime date);
        Task<MonthlyDaysOffModel> GetMonthlyDaysOffAsync(int doctorId, int month);
        Task<IEnumerable<DayOffResponse>> GetAllDaysOffAsync(string userId);
        Task<MonthlyBusyDaysModel> GetMonthlyBusyDaysAsync(int doctorId, int month, int year);
        Task RemoveDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff);
        Task AddDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff);
    }
}
