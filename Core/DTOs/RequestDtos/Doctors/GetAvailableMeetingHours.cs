﻿using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class GetAvailableMeetingHours
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
