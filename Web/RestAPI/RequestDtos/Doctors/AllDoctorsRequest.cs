using RestAPI.RequestDtos.Enums;

namespace RestAPI.RequestDtos.Doctors
{
    public class AllDoctorsRequest
    {
        public int Index { get; set; }

        public SortingOption Sorting { get; set; }

        public string Filter { get; set; } = string.Empty;

        public string Search { get; set; } = string.Empty;
    }
}
