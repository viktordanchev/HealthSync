namespace RestAPI.DTOs.Account
{
    public class ConfirmRegistrationRequest
    {
        public string Email { get; set; }
        public string VrfCode { get; set; }
    }
}
