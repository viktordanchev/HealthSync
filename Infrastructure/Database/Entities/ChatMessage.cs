using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entities
{
    public class ChatMessage
    {
        public ChatMessage()
        {
            Images = new List<MessageImage>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        public string? Message { get; set; }

        public IEnumerable<MessageImage> Images { get; set; }
    }
}
