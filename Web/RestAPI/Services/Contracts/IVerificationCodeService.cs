namespace RestAPI.Services.Contracts
{
    public interface IVerificationCodeService
    {
        string GenerateCode(string key);
        bool ValidateCode(string key, string vrfCode);
    }
}
