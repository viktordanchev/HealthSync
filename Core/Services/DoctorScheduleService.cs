using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepo;
        private readonly IDoctorsRepository _doctorsRepo;

        public DoctorScheduleService(IDoctorScheduleRepository repository, IDoctorsRepository doctorsRepo)
        {
            _doctorScheduleRepo = repository;
            _doctorsRepo = doctorsRepo;
        }

        public async Task<bool> IsDateUnavailableAsync(int doctorId, DateTime date)
        {
            return await _doctorScheduleRepo.IsDateUnavailableAsync(doctorId, date);
        }

        public async Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date)
        {
            var dailySchedule = await _doctorScheduleRepo.GetDailyScheduleAsync(doctorId, date);

            var availableMeetings = new List<string>();

            while (dailySchedule.WorkDay.WorkDayStart <= dailySchedule.WorkDay.WorkDayEnd)
            {
                if (!dailySchedule.Meetings.Contains(dailySchedule.WorkDay.WorkDayStart))
                {
                    availableMeetings.Add($"{dailySchedule.WorkDay.WorkDayStart.Hour:D2} : {dailySchedule.WorkDay.WorkDayStart.Minute:D2}");
                }

                dailySchedule.WorkDay.WorkDayStart = dailySchedule.WorkDay.WorkDayStart.Add(TimeSpan.FromMinutes(dailySchedule.WorkDay.MeetingTimeMinutes));
            }

            return availableMeetings;
        }

        public async Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(GetMonthScheduleRequest requestData)
        {
            var unavailableDays = await _doctorScheduleRepo.GetMonthlyUnavailableDaysAsync(requestData);
            var daysInMonth = DateTime.DaysInMonth(requestData.Year, requestData.Month);
            var monthSchedule = new List<MonthScheduleResponse>();

            if(unavailableDays.WeeklyDaysOff.Any())
            {
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateTime(requestData.Year, requestData.Month, day);
                    var isAvailable = !(unavailableDays.BusyDays.Contains(date) ||
                        unavailableDays.DaysOff.Contains(date) ||
                        unavailableDays.WeeklyDaysOff.Contains(date.DayOfWeek));

                    monthSchedule.Add(new MonthScheduleResponse(date, isAvailable));
                }
            }

            return monthSchedule;
        }

        public async Task<IEnumerable<DayOffResponse>> GetAllDaysOffAsync(string userId)
        {
            return await _doctorScheduleRepo.GetAllDaysOffAsync(userId);
        }

        public async Task UpdateDaysOffAsync(string userId, IEnumerable<DayOffResponse> updatedDaysOff)
        {
            var doctorId = await _doctorsRepo.GetDoctorIdAsync(userId);
            var daysOff = await _doctorScheduleRepo.GetAllDaysOffAsync(userId);

            var daysOffToAdd = updatedDaysOff
                .Where(udoff => !daysOff.Any(doff => udoff.Day == doff.Day && udoff.Month == doff.Month))
                .ToList();

            if (daysOffToAdd.Any())
            {
                await _doctorScheduleRepo.AddDaysOffAsync(doctorId, daysOffToAdd);
            }

            var daysOffToRemove = daysOff.Where(doff => !updatedDaysOff.Any(udoff => udoff.Day == doff.Day && udoff.Month == doff.Month)).ToList();

            if (daysOffToRemove.Any())
            {
                await _doctorScheduleRepo.RemoveDaysOffAsync(doctorId, daysOffToRemove);
            }
        }

        public async Task UpdateWeeklySchedule(string userId, IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule)
        {
            await _doctorScheduleRepo.UpdateWeeklySchedule(userId, weeklySchedule);
        }
    }
}
