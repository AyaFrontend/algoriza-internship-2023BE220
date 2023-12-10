using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Service.IService
{
    public interface IResponseCacheServices
    {
        public Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        public Task<string> GetCachedResponse(string cacheKey);
    }
}
