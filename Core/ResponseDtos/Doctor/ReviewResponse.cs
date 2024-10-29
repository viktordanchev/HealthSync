namespace Core.ResponseDtos.Doctor
{
    public class ReviewResponse
    {
        public int Rating { get; set; }

        public DateTime Date { get; set; }

        public string Reviewer { get; set; } = null!;
    }
}
