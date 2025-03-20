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

        public async Task<int> AddMessage(AddMessageRequest requestData)
        {
            var newMessage = new ChatMessage()
            {
                SenderId = requestData.SenderId,
                ReceiverId = requestData.ReceiverId,
                Message = requestData.Message,
                DateAndTime = DateTime.Parse(requestData.DateAndTime)
            };

            await _context.ChatMessages.AddAsync(newMessage);
            await _context.SaveChangesAsync();

            return newMessage.Id;
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
                    Images = cm.Images
                        .Select(i => i.ImageUrl)
                })
                .ToListAsync();

            return history;
        }

        public async Task AddImagesAsync(int messageId, IEnumerable<string> imgUrls)
        {
            var data = new List<MessageImage>();

            foreach (var img in imgUrls)
            {
                data.Add(new MessageImage()
                {
                    ImageUrl = img,
                    MessageId = messageId
                });
            }

            await _context.MessageImages.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}
