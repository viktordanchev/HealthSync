using Core.DTOs.RequestDtos.ChatHub;

namespace Core.Interfaces.Repository
{
    public interface IChatRepository
    {
        Task AddMessage(AddMessageRequest requestData);
    }
}
