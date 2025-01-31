﻿namespace RestAPI.Services.Contracts
{
    public interface IEmailSenderService
    {
        Task SendVrfCode(string toEmail, string vrfCode);
        Task SendPasswordRecoverLink(string toEmail, string token);
    }
}
