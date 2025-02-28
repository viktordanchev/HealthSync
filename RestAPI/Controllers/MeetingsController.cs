using Core.DTOs.RequestDtos.Meetings;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Common.Errors;
using static Common.Errors.Meetings;
using static Common.Messages.Meetings;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingsService _meetingsService;
        private readonly IDoctorScheduleService _doctorScheduleService;

        public MeetingsController(
            IMeetingsService meetingsService,  
            IDoctorScheduleService doctorScheduleService)
        {
            _meetingsService = meetingsService;
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost("addDoctorMeeting")]
        [Authorize]
        public async Task<IActionResult> AddDoctorMeeting([FromBody] AddMeetingRequest request)
        {
            var dateAndTimeParsed = DateTime.Parse(request.DateAndTime);
            
            if (await _doctorScheduleService.IsDateUnavailableAsync(request.DoctorId, dateAndTimeParsed))
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

        [HttpGet("getUserMeetings")]
        [Authorize]
        public async Task<IActionResult> GetUserMeetings()
        {
            var meetings = await _meetingsService.GetUserMeetingsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(meetings);
        }

        [HttpDelete("deleteMeeting")]
        [Authorize]
        public async Task<IActionResult> DeleteMeeting([FromBody] int meetingId)
        {
            try
            {
                await _meetingsService.DeleteMeetingAsync(meetingId);

                return Ok(new { Message = DeletedMeeting });
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpGet("getDoctorMeetings")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorMeetings()
        {
            var meetings = await _meetingsService.GetDoctorMeetingsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(meetings);
        }
    }
}
