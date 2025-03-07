using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.ChatHub
{
    public class AddMessageRequest
    {
        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        [Required]
        public string DateAndTime { get; set; } = null!;
    }
}
