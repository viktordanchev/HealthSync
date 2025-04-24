using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs.RequestDtos.ChatHub
{
    public class AddMessageRequest
    {
        public AddMessageRequest()
        {
            Images = new List<IFormFile>();
        }

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        public string ReceiverId { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        [Required]
        public string DateAndTime { get; set; } = null!;

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
