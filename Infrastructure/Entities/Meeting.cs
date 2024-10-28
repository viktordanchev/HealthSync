﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkScheduleId { get; set; }

        [ForeignKey(nameof(WorkScheduleId))]
        public WorkSchedule WorkSchedule { get; set; } = null!;

        [Required]
        public string PatientId { get; set; } = null!;

        [ForeignKey(nameof(PatientId))]
        public ApplicationUser Patient { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}
