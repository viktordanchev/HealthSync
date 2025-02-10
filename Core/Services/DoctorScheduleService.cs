using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _repository;

        public DoctorScheduleService(IDoctorScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsDayOffAsync(int doctorId, DateTime date)
        {
            var daysOff = await GetDaysOffAsync(doctorId, date.Month, date.Year);
            var busyDays = await GetBusyDaysAsync(doctorId, date.Month, date.Year);
            var isDayOff = daysOff.Contains(date) || busyDays.Contains(date) ? true : false;
        
            return isDayOff;
        }

        public async Task<IEnumerable<string>> GetAvailableMeetingsAsync(int doctorId, DateTime date)
        {
            var dailySchedule = await _repository.GetDoctorDailyScheduleAsync(doctorId, date);

            var availableMeetings = new List<string>();

            while (dailySchedule.WorkDay.Start <= dailySchedule.WorkDay.End)
            {
                if (!dailySchedule.Meetings.Contains(dailySchedule.WorkDay.Start))
                {
                    availableMeetings.Add($"{dailySchedule.WorkDay.Start.Hours:D2} : {dailySchedule.WorkDay.Start.Minutes:D2}");
                }

                dailySchedule.WorkDay.Start = dailySchedule.WorkDay.Start.Add(TimeSpan.FromMinutes(dailySchedule.WorkDay.MeetingTimeMinutes));
            }

            return availableMeetings;
        }

        public async Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(GetMonthScheduleRequest requestData)
        {
            var busyDays = await GetBusyDaysAsync(requestData.DoctorId, requestData.Month, requestData.Year);
            var daysOff = await GetDaysOffAsync(requestData.DoctorId, requestData.Month, requestData.Year);
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
        
        private async Task<IEnumerable<DateTime>> GetDaysOffAsync(int doctorId, int month, int year)
        {
            var monthlyDaysOff = await _repository.GetMonthlyDaysOffAsync(doctorId, month);
        
            var firstDateOfMonth = new DateTime(year, month, 1);
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
            var monthlyBusyDays = await _repository.GetMonthlyBusyDaysAsync(doctorId, month, year);
        
            var meetingDays = monthlyBusyDays.AllMeetings
                .Select(am => am.Date)
                .ToHashSet();
        
            foreach (var meetingDay in meetingDays)
            {
                var allMeetingsByDay = monthlyBusyDays.AllMeetings.Where(am => am.Date == meetingDay).Count();
                var dayOfWeek = monthlyBusyDays.WeekDays.FirstOrDefault(ww => ww.WeekDay == meetingDay.DayOfWeek);
                var count = (dayOfWeek.End - dayOfWeek.Start).TotalMinutes / dayOfWeek.MeetingTimeMinutes + 1;
        
                if (allMeetingsByDay != count)
                {
                    meetingDays.Remove(meetingDay);
                }
            }
        
            return meetingDays;
        }
    }
}
