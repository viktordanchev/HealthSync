using Core.DTOs.RequestDtos.ChatHub;

namespace Core.Interfaces.Service
{
    public interface IChatService
    {
        Task AddMessage(AddMessageRequest requestData);
    }
}
