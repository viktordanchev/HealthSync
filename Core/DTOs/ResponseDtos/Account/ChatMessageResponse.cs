namespace Core.DTOs.ResponseDtos.Account
{
    public class ChatMessageResponse
    {
        public ChatMessageResponse()
        {
            ImgUrls = new List<string>();
        }

        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public DateTime DateAndTime { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string> ImgUrls { get; set; }
    }
}
