﻿namespace Core.Models.ResponseDtos.Doctors
{
    public class DoctorPersonalInfoModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string Hospital { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public string? ContactEmail { get; set; }
            
        public string? ContactPhoneNumber { get; set; }
    }
}