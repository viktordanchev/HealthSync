using Microsoft.Extensions.Caching.Memory;
using RestAPI.Services.Contracts;

namespace RestAPI.Services
{
    /// <summary>
    /// This class is to manage the verification of registered user.
    /// </summary>
    public class MemoryCacheService : IMemoryCacheService
    {
        private IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, string value, TimeSpan time)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = time
            });
        }

        public string Get(string key) 
        {
            return _memoryCache.Get(key) != null ? _memoryCache.Get(key).ToString() : string.Empty;
        }
    }
}
