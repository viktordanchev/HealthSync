namespace RestAPI.Services.Contracts
{
    public interface IMemoryCacheService
    {
        void Add(string key, string value, TimeSpan time);
        bool isExist(string key);
        string GetValue(string key);
    }
}
