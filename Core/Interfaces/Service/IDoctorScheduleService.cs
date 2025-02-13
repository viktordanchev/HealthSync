﻿using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;

namespace Core.Interfaces.Service
{
    public interface IDoctorScheduleService
    {
        Task<bool> IsDayOffAsync(int doctorId, DateTime date);
        Task<IEnumerable<string>> GetAvailableMeetingsAsync(GetAvailableMeetingHours requestData);
        Task<IEnumerable<MonthScheduleResponse>> GetMonthScheduleAsync(GetMonthScheduleRequest requestData);
    }
}
