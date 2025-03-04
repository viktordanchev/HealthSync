using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Account
{
    public class GetChatHistoryRequest
    {
        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;
    }
}
