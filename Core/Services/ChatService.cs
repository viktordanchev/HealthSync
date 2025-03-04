using Core.DTOs.RequestDtos.ChatHub;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepo;

        public ChatService(IChatRepository chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public async Task AddMessage(AddMessageRequest requestData)
        {
            await _chatRepo.AddMessage(requestData);
        }
    }
}
