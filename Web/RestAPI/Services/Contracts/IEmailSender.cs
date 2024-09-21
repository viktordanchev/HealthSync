namespace RestAPI.Services.Contracts
{
    public interface IEmailSender
    {
        Task SendVrfCode(string toEmail, string vrfCode);
    }
}
