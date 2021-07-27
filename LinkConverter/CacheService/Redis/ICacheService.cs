namespace LinkConverter.CacheService.Redis
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Add(string key, object data);
        void Remove(string key);
        void Clear();
        bool Any(string key);
    }
}