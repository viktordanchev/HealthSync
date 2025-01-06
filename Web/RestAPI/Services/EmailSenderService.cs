using RestAPI.Services.Contracts;
using MimeKit;
using MailKit.Net.Smtp;

namespace RestAPI.Services
{
    /// <summary>
    /// This class is used for sending emails.
    /// </summary>
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;
        
        public EmailSenderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendVrfCode(string toEmail, string vrfCode)
        {
            var subject = "Confirm your registration!";
            var message = $"<h2>Your verification code: <strong>{vrfCode}</strong>.</h2>";
            await SendEmailAsync(toEmail, subject, message);
        }

        public async Task SendPasswordRecoverLink(string toEmail, string token)
        {
            var subject = "Password recover!";
            var message = $"<a href='https://localhost:5173/account/recoverPassword?token={token}' style='display: inline-block; padding: 10px 20px; background-color: #01bfa5; color: white; text-decoration: none; border-radius: 0.75rem; font-size: 16px;'>Recover Password</a>";
            await SendEmailAsync(toEmail, subject, message);
        }

        /// <summary>
        /// This method send emails.
        /// </summary>
        private async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = htmlMessage };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
