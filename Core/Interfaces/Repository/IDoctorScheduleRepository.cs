using Core.Models.DoctorSchedule;

namespace Core.Interfaces.Repository
{
    public interface IDoctorScheduleRepository
    {
        Task<DoctorDailyScheduleModel> GetDoctorDailySchedule(int doctorId, DateTime dateAndTime);
    }
}
