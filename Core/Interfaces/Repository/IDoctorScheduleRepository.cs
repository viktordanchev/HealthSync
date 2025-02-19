using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Models.DoctorSchedule;

namespace Core.Interfaces.Repository
{
    public interface IDoctorScheduleRepository
    {
        Task<DailyScheduleModel> GetDailyScheduleAsync(int doctorId, DateTime date);
        Task<IEnumerable<DayOffResponse>> GetAllDaysOffAsync(string userId);
        Task RemoveDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff);
        Task AddDaysOffAsync(int doctorId, IEnumerable<DayOffResponse> daysOff);
        Task<bool> IsDateUnavailableAsync(int doctorId, DateTime date);
        Task<MonthlyUnavailableDaysModel> GetMonthlyUnavailableDaysAsync(GetMonthScheduleRequest requestData);
        Task UpdateWeeklySchedule(string userId, IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule);
    }
}
