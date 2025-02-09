using System.ComponentModel.DataAnnotations;
using static Common.Constants;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class GetMonthScheduleRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Range(MonthRangeMin, MonthRangeMax)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
