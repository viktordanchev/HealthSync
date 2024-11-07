namespace Core.Models.ResponseDtos.Doctor
{
    public class ReviewResponse
    {
        public int Rating { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string Reviewer { get; set; } = null!;
    }
}
