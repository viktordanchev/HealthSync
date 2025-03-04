using Core.DTOs.RequestDtos.ChatHub;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.SignalR;

namespace RestAPI.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(AddMessageRequest request)
        {
            await _chatService.AddMessage(request);

            await Clients.User(request.ReceiverId).SendAsync("ReceiveMessage", request.SenderId, request.Message);
        }
    }
}
