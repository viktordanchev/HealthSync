using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; } = null!;

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public string? Message { get; set; }

        public IEnumerable<MessageImage> Images { get; set; }
    }
}
