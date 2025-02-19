﻿using Core.DTOs.RequestDtos.Doctors;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Common.Errors;
using static Common.Messages.Doctors;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDoctorsService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IHospitalsService _hospitalsService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ISpecialtiesService _specialtiesService;

        public DoctorsController(
            IUserService userService,
            IDoctorsService doctorService,
            IDoctorScheduleService doctorScheduleService,
            IHospitalsService hospitalsService,
            IJwtTokenService jwtTokenService,
            ISpecialtiesService specialtiesService)
        {
            _userService = userService;
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
            _hospitalsService = hospitalsService;
            _jwtTokenService = jwtTokenService;
            _specialtiesService = specialtiesService;
        }

        [HttpPost("getDoctors")]
        public async Task<IActionResult> GetDoctors([FromBody] GetDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctorsAsync(request,
                User.FindFirstValue(ClaimTypes.Email));

            return Ok(doctors);
        }

        [HttpPost("getDetails")]
        public async Task<IActionResult> GetDetails([FromBody] int doctorId)
        {
            if (!await _doctorService.IsDoctorExistAsync(doctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var doctorDetails = await _doctorService.GetDoctorDetailsAsync(doctorId);

            return Ok(doctorDetails);
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _specialtiesService.GetSpecialtiesAsync();

            return Ok(specialties);
        }

        [HttpPost("getAvailableMeetingHours")]
        public async Task<IActionResult> GetAvailableMeetingHours([FromBody] GetAvailableMeetingHours request)
        {
            var dateParsed = DateTime.Parse(request.Date);

            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId) ||
                await _doctorScheduleService.IsDateUnavailableAsync(request.DoctorId, dateParsed))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
            
            var times = await _doctorScheduleService.GetAvailableMeetingsAsync(request.DoctorId, dateParsed);

            return Ok(times);
        }

        [HttpPost("getMonthSchedule")]
        public async Task<IActionResult> GetMonthSchedule([FromBody] GetMonthScheduleRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var monthSchedule = await _doctorScheduleService.GetMonthScheduleAsync(request);

            return Ok(monthSchedule);
        }

        [HttpPost("becomeDoctor")]
        [Authorize]
        public async Task<IActionResult> BecomeDoctor([FromForm] BecomeDoctorRequest request)
        {
            if (await _doctorService.IsUserDoctorAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)) ||
                !await _hospitalsService.IsHospitalExistAsync(request.HospitalId) ||
                !await _specialtiesService.IsSpecialtyExistAsync(request.SpecialtyId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            await _doctorService.AddDoctorAsync(request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var userClaims = await _userService.GetUserClaimsAsync(User.FindFirstValue(ClaimTypes.Email));
            var accessToken = _jwtTokenService.GenerateAccessToken(userClaims);

            return Ok(
                new
                {
                    Message = RegisteredDoctor,
                    Token = accessToken
                });
        }

        [HttpGet("getProfileInfo")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetProfileInfo()
        {
            var doctorInfo = await _doctorService.GetDoctorPersonalInfoAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(doctorInfo);
        }

        [HttpGet("getDaysOff")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDaysOff()
        {
            var daysOff = await _doctorScheduleService.GetAllDaysOffAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(daysOff);
        }

        [HttpPost("updateWeeklySchedule")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateWeeklySchedule([FromBody] IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule)
        {
            await _doctorScheduleService.UpdateWeeklySchedule(User.FindFirstValue(ClaimTypes.NameIdentifier)!, weeklySchedule);

            return Ok(new { Message = UpdatedWeeklySchedule });
        }
    }
}
