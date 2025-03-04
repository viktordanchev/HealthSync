using Core.DTOs.RequestDtos.ChatHub;
using Core.Interfaces.Repository;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Repositories
{
    class ChatRepository : IChatRepository
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
                Message = requestData.Message
            });
            await _context.SaveChangesAsync();
        }
    }
}
