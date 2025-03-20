using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Service
{
    public interface IChatService
    {
        Task AddMessage(AddMessageRequest requestData);
        Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData);
        Task<IEnumerable<string>> UploadImageAsync(IEnumerable<IFormFile> images);
    }
}
