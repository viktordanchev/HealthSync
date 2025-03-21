﻿using Core.Constants;
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
        private readonly IGoogleCloudStorageService _GCSService;

        public ChatService(IChatRepository chatRepo, IGoogleCloudStorageService gcsService)
        {
            _chatRepo = chatRepo;
            _GCSService = gcsService;
        }

        public async Task AddMessage(AddMessageRequest requestData)
        {
            var messageId = await _chatRepo.AddMessage(requestData);

            await _chatRepo.AddImagesAsync(messageId, requestData.Images);
        }

        public async Task<IEnumerable<ChatMessageResponse>> GetChatHistory(GetChatHistoryRequest requestData)
        {
            return await _chatRepo.GetChatHistory(requestData);
        }

        public async Task<IEnumerable<string>> UploadImageAsync(IEnumerable<IFormFile> images)
        {
            var imgUrls = new List<string>();

            foreach (var img in images)
            {
                imgUrls.Add(await _GCSService.UploadImageAsync(img, GoogleStorageConstants.ChatImages));
            }

            return imgUrls;
        }
    }
}
