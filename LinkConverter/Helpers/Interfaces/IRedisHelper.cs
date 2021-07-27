using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkConverter.CacheService.Redis;
using LinkConverter.Model.Cache;
using LinkConverter.Model.Common;

namespace LinkConverter.Helpers.Interfaces
{
    public interface IRedisHelper
    {
        public GeneralLinkModel Get(ICacheService cacheService,string url);
        public void Add(ICacheService cacheService, string url, RedisCacheModel cacheModel);
    }
}
