using Core.Constants;
using Core.DTOs.RequestDtos.Account;
using Core.DTOs.RequestDtos.ChatHub;
using Core.DTOs.ResponseDtos.Account;
using Core.Interfaces.ExternalServices;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepo;
        private readonly IBlobStorageServiceService _BlobStorageService;

        public ChatService(IChatRepository chatRepo, IBlobStorageServiceService blobStorageService)
        {
            _chatRepo = chatRepo;
            _BlobStorageService = blobStorageService;
        }

        public async Task<int> AddMessage(AddMessageRequest requestData)
        {
            return await _chatRepo.AddMessage(requestData);
        }

        public async Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData)
        {
            return await _chatRepo.GetChatHistory(requestData);
        }

        public async Task<IEnumerable<string>> UploadImagesAsync(IEnumerable<IFormFile> images)
        {
            return await _BlobStorageService.UploadChatImagesAsync(images, BlobStorageContainers.ChatImages);
        }
    }
}
