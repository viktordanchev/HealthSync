namespace Core.DTOs.ResponseDtos.Reviews
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public DateTime DateAndTime { get; set; }

        public string? Comment { get; set; }

        public string Reviewer { get; set; } = null!;
    }
}
