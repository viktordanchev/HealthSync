using System.ComponentModel.DataAnnotations;
using static Common.Constants;

namespace RestAPI.Dtos.RequestDtos.Doctors
{
    public class GetMonthDaysOffRequest
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
