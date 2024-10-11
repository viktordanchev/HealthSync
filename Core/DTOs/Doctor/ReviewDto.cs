namespace Core.DTOs.Doctor
{
    public class ReviewDto
    {
        public string Text { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime Date { get; set; }

        public string Reviewer { get; set; } = null!;
    }
}
