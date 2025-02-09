using Core.DTOs.Enums;

namespace Core.DTOs.RequestDtos.Doctors
{
    public class GetDoctorsRequest
    {
        public int Index { get; set; }

        public SortingOption Sorting { get; set; }

        public string Filter { get; set; } = string.Empty;

        public string Search { get; set; } = string.Empty;
    }
}
