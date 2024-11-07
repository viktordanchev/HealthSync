using System.ComponentModel.DataAnnotations;
using static Common.Constants;

namespace RestAPI.RequestDtos.Doctor
{
    public class GetDaysInMonthRequest
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
