﻿namespace Core.DTOs.ResponseDtos.Account
{
    public class ChatMessageResponse
    {
        public ChatMessageResponse()
        {
            Images = new List<string>();
        }

        public string SenderId { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public DateTime DateAndTime { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
