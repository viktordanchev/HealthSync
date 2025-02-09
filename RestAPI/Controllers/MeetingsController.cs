using Core.DTOs.RequestDtos.Meetings;
using Core.Interfaces.Service;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Common.Errors;
using static Common.Errors.Meetings;
using static Common.Messages.Meetings;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingsController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMeetingsService _meetingsService;
        private readonly IDoctorsService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;

        public MeetingsController(
            UserManager<ApplicationUser> userManager, 
            IMeetingsService meetingsService, 
            IDoctorsService doctorService, 
            IDoctorScheduleService doctorScheduleService)
        {
            _userManager = userManager;
            _meetingsService = meetingsService;
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost("addDoctorMeeting")]
        [Authorize]
        public async Task<IActionResult> AddDoctorMeeting([FromBody] AddMeetingRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId) ||
                await _doctorScheduleService.IsDayOffAsync(request.DoctorId, request.DateAndTime))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            if(await _meetingsService.isMeetingScheduled(request.PatientId, request.DoctorId))
            {
                return BadRequest(new { Error = ExistingMeeting });
            }

            await _meetingsService.AddDoctorMeetingAsync(request);

            return Ok(new { Message = AddedMeeting });
        }

        [HttpPost("getUserMeetings")]
        [Authorize]
        public async Task<IActionResult> GetUserMeetings([FromBody] string userId)
        {
            var user = _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var meetings = await _meetingsService.GetUserMeetingsAsync(userId);

            return Ok(meetings);
        }

        [HttpDelete("deleteMeeting")]
        [Authorize]
        public async Task<IActionResult> DeleteMeeting([FromBody] int meetingId)
        {
            if (!await _meetingsService.IsMeetingExistAsync(meetingId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            await _meetingsService.DeleteMeetingAsync(meetingId);

            return Ok(new { Message = DeletedMeeting });
        }
    }
}
