using Core.DTOs.RequestDtos.Doctors;
using Core.Models.DoctorSchedule;

namespace Core.Interfaces.Repository
{
    public interface IDoctorScheduleRepository
    {
        Task<DoctorDailyScheduleModel> GetDoctorDailyScheduleAsync(int doctorId, DateTime date);
        Task<MonthlyDaysOffModel> GetMonthlyDaysOffAsync(int doctorId, int month);
        Task<DoctorMonthlyBusyDaysModel> GetMonthlyBusyDaysAsync(int doctorId, int month, int year);
    }
}
