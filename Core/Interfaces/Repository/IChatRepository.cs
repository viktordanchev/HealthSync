using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;

namespace Core.Interfaces.Repository
{
    public interface IChatRepository
    {
        Task<int> AddMessage(AddMessageRequest requestData);
        Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData);
        Task AddImagesAsync(int messageId, IEnumerable<string> imgUrls);
    }
}
