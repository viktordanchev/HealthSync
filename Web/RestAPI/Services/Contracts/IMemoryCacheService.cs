namespace RestAPI.Services.Contracts
{
    public interface IMemoryCacheService
    {
        void Add(string key, string value, TimeSpan time);
        string Get(string key);
    }
}
