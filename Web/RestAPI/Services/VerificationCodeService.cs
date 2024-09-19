using Microsoft.Extensions.Caching.Memory;
using RestAPI.Services.Contracts;

namespace RestAPI.Services
{
    /// <summary>
    /// This class is to manage the verification of registered user.
    /// </summary>
    public class VerificationCodeService : IVerificationCodeService
    {
        private IMemoryCache _memoryCache;

        public VerificationCodeService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string GenerateCode(string key)
        {
            var vrfCode = Guid.NewGuid().ToString().Substring(0, 6);
            _memoryCache.Set(key, vrfCode, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            return vrfCode;
        }

        public bool ValidateCode(string key, string vrfCode)
        {
            var code = _memoryCache.Get(key);

            return code != null && _memoryCache.Get(key)!.ToString() == vrfCode;
        }
    }
}
