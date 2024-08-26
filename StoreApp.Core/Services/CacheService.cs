using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Core.Contracts;

namespace StoreApp.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMongoCacheRepository _cacheRepository;

        public CacheService (IMongoCacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task DropCaches()
        {
            while (true)
            {
                Console.WriteLine("Cache clear in 3 minutes");
                await Task.Delay(180000);
                await _cacheRepository.DropCaches();
                Console.WriteLine("Cache cleared.");

            }
        }
    }
}
