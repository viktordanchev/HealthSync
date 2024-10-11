using RestAPI.DTOs.Enums;

namespace RestAPI.DTOs.Doctors
{
    public class AllDoctorsRequest
    {
        public SortingOption Sorting { get; set; }

        public string Filter { get; set; } = string.Empty;

        public string Search { get; set; } = string.Empty;
    }
}
