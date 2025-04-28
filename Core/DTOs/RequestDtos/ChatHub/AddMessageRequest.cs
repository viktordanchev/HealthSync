using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.ChatHub
{
    public class AddMessageRequest
    {
        public AddMessageRequest()
        {
            ImageUrls = new List<string>();
        }

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        [Required]
        public string DateAndTime { get; set; } = null!;

        public IEnumerable<string> ImageUrls { get; set; }
    }
}
