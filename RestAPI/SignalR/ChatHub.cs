﻿using Core.DTOs.RequestDtos.ChatHub;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace RestAPI.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private static readonly ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(AddMessageRequest request)
        {
            var messageId = await _chatService.AddMessage(request);
            await _chatService.UploadImagesAsync(messageId, request.ImageUrls);

            if (_connections.TryGetValue(request.ReceiverId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", request.SenderId, request.Message, request.DateAndTime, request.ImageUrls);
            }
        }

        public async Task SenderTyping(string receiverId, bool isTyping)
        {
            if (_connections.TryGetValue(receiverId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("UserTyping", isTyping);
            }
        }

        public override Task OnConnectedAsync()
        {
            _connections[Context.UserIdentifier] = Context.ConnectionId;

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.UserIdentifier ?? Context.ConnectionId;

            _connections.TryRemove(userId, out _);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
