﻿using RestAPI.RequestDtos.DoctorEnums;

namespace RestAPI.RequestDtos.Doctor
{
    public class AllDoctorsRequest
    {
        public int Index { get; set; }

        public SortingOption Sorting { get; set; }

        public string Filter { get; set; } = string.Empty;

        public string Search { get; set; } = string.Empty;
    }
}
