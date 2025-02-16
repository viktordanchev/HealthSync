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

        public async Task<bool> IsDayValidAsync(int doctorId, DateTime date)
        {
            var daysOff = await GetMonthlyDaysOffAsync(doctorId, date.Month);
            var busyDays = await GetBusyDaysAsync(doctorId, date.Month, date.Year);
            var isDayOff = daysOff.Contains(date) || busyDays.Contains(date) ? true : false;
        
            return isDayOff;
        }

        public async Task<IEnumerable<string>> GetAvailableMeetingsAsync(GetAvailableMeetingHours requestData)
        {
            var dailySchedule = await _doctorScheduleRepo.GetDailyScheduleAsync(requestData.DoctorId, requestData.Date);

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
            var daysOff = await GetMonthlyDaysOffAsync(requestData.DoctorId, requestData.Month);
            var busyDays = await GetBusyDaysAsync(requestData.DoctorId, requestData.Month, requestData.Year);
            var daysInMonth = DateTime.DaysInMonth(requestData.Year, requestData.Month);
            var monthSchedule = new List<MonthScheduleResponse>();
        
            if (daysOff.Count() > 0)
            {
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateTime(requestData.Year, requestData.Month, day);
        
                    var isAvailable =
                        daysOff.Contains(date) ||
                        busyDays.Contains(date)
                        ? false : true;
        
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

            await _doctorScheduleRepo.UpdateDaysOffAsync(doctorId, updatedDaysOff);
        }

        private async Task<IEnumerable<DateTime>> GetMonthlyDaysOffAsync(int doctorId, int month)
        {
            var monthlyDaysOff = await _doctorScheduleRepo.GetMonthlyDaysOffAsync(doctorId, month);
        
            var firstDateOfMonth = new DateTime(DateTime.Now.Year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
        
            var weeklyDaysOffDates = Enumerable.Range(0, (lastDateOfMonth - firstDateOfMonth).Days + 1)
                             .Select(day => firstDateOfMonth.AddDays(day))
                             .Where(date => monthlyDaysOff.WorkWeekDaysOff.Contains(date.DayOfWeek))
                             .ToList();
        
            var datesOffResult = new List<DateTime>();
            datesOffResult.AddRange(monthlyDaysOff.DaysOff);
            datesOffResult.AddRange(weeklyDaysOffDates);
        
            return datesOffResult;
        }
        
        private async Task<IEnumerable<DateTime>> GetBusyDaysAsync(int doctorId, int month, int year)
        {
            var monthlyBusyDays = await _doctorScheduleRepo.GetMonthlyBusyDaysAsync(doctorId, month, year);
        
            var meetingDays = monthlyBusyDays.AllMeetings
                .Select(am => am.Date)
                .ToHashSet();
        
            foreach (var meetingDay in meetingDays)
            {
                var allMeetingsByDay = monthlyBusyDays.AllMeetings.Where(am => am.Date == meetingDay).Count();
                var dayOfWeek = monthlyBusyDays.WeekDays.FirstOrDefault(ww => ww.WeekDay == meetingDay.DayOfWeek);
                var count = (dayOfWeek.WorkDayEnd - dayOfWeek.WorkDayStart).TotalMinutes / dayOfWeek.MeetingTimeMinutes + 1;
        
                if (allMeetingsByDay != count)
                {
                    meetingDays.Remove(meetingDay);
                }
            }
        
            return meetingDays;
        }
    }
}
