using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;
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

        public async Task<int> AddMessage(AddMessageRequest requestData)
        {
            return await _chatRepo.AddMessage(requestData);
        }

        public async Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData)
        {
            return await _chatRepo.GetChatHistory(requestData);
        }

        public async Task UploadImagesAsync(int messageId, IEnumerable<string> imageUrls)
        {
            await _chatRepo.AddImagesAsync(messageId, imageUrls);
        }
    }
}
