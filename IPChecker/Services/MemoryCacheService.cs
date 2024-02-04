using IPChecker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IPChecker.Services
{
    public interface IMemoryCacheService
    {
        IpAddress? Get(string ipAddress);
        void Remove(IpAddress ipAddress);
        void Set(IpAddress ipAddress);
    }

    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;


        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IpAddress? Get(string ipAddress)
        {
            _memoryCache.TryGetValue(ipAddress, out IpAddress? data);

            return data;
        }

        public void Set(IpAddress ipAddress)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2));

            _memoryCache.Set(ipAddress, cacheOptions);

        }

        public void Remove(IpAddress ipAddress)
        {
            _memoryCache.Remove(ipAddress);
        }


    }
}
