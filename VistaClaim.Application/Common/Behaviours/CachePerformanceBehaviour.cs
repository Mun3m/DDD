using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Common.Mappings;

namespace VistaClaim.Application.Common.Behaviours
{
    public class CachePerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MappingCache _mappingCache;
        private readonly ICurrentUserService _userService;

        public CachePerformanceBehaviour(IMemoryCache memoryCache, ICurrentUserService userService, MappingCache mappingCache)
        {
            _memoryCache = memoryCache;
            _mappingCache = mappingCache;
            _userService = userService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_mappingCache.ContainsKey(typeof(TRequest).Name))
            {
                var key = $"{_userService.UserId}_{typeof(TRequest).Name}";

                TResponse response;

                // Look for cache key.
                if (!_memoryCache.TryGetValue(key, out response))
                {
                    // Key not in cache, so get data.
                    response = await next();

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    // Save data in cache.
                    _memoryCache.Set(key, response, cacheEntryOptions);
                }

                return response;
            }
            else
            {
                return await next();
            }
        }
    }
}
