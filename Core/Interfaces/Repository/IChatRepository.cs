using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;

namespace Core.Interfaces.Repository
{
    public interface IChatRepository
    {
        Task AddMessage(AddMessageRequest requestData);
        Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData);
    }
}
