using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ex04_SharedState_MemoryCache
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMemoryCache();

            Console.WriteLine("");

            TestLazyCache();
        }

        private static void TestMemoryCache()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());

            int counter = 0;

            Console.WriteLine("TestMemoryCache");

            Parallel.ForEach(Enumerable.Range(1, 10), i =>
            {
                var item = cache.GetOrCreate("test-key", cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);

                    return Interlocked.Increment(ref counter);
                });

                Console.Write($"{item} ");
            });
        }

        private static void TestLazyCache()
        {
            IAppCache cache = new CachingService();

            int counter = 0;

            Console.WriteLine("TestLazyCache");

            Parallel.ForEach(Enumerable.Range(1, 10), i =>
            {
                var item = cache.GetOrAdd("test-key", cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(10);
                    return Interlocked.Increment(ref counter);
                });

                Console.Write($"{item} ");
            });
        }
    }
}
