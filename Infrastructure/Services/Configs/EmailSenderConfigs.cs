namespace Infrastructure.Services.Configs
{
    public class EmailSenderConfigs
    {
        public string SMTPServer { get; set; } = null!;
        public int Port { get; set; }
        public string From { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
