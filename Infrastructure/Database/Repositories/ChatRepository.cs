using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;
using Core.Interfaces.Repository;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly HealthSyncDbContext _context;

        public ChatRepository(HealthSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddMessage(AddMessageRequest requestData)
        {
            await _context.ChatMessages.AddAsync(new ChatMessage()
            {
                SenderId = requestData.SenderId,
                ReceiverId = requestData.ReceiverId,
                Message = requestData.Message,
                DateAndTime = DateTime.Parse(requestData.DateAndTime)
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData)
        {
            var history = await _context.ChatMessages
                .AsNoTracking()
                .Where(cm => cm.SenderId == requestData.SenderId && cm.ReceiverId == requestData.ReceiverId ||
                    cm.SenderId == requestData.ReceiverId && cm.ReceiverId == requestData.SenderId)
                .Select(cm => new ChatMessageResponse()
                {
                    SenderId = cm.SenderId,
                    Sender = $"{cm.Sender.FirstName} {cm.Sender.LastName}",
                    Receiver = $"{cm.Receiver.FirstName} {cm.Receiver.LastName}",
                    DateAndTime = cm.DateAndTime,
                    Message = cm.Message,
                    ImgUrls = cm.Images
                        .Select(i => i.ImageUrl)
                })
                .ToListAsync();

            return history;
        }
    }
}
