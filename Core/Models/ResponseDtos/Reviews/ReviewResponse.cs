namespace Core.Models.ResponseDtos.Reviews
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string Reviewer { get; set; } = null!;
    }
}
