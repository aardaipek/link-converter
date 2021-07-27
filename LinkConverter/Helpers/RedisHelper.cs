using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkConverter.CacheService.Redis;
using LinkConverter.Helpers.Interfaces;
using LinkConverter.Model.Cache;
using LinkConverter.Model.Common;
using Newtonsoft.Json.Linq;

namespace LinkConverter.Helpers
{
    public class RedisHelper:IRedisHelper
    {
        public GeneralLinkModel Get(ICacheService cacheService, string url)
        {
            GeneralLinkModel linkModel = new GeneralLinkModel();
            linkModel.Valid = false;
            if (cacheService.Any(url))
            {
                var URL = cacheService.Get<JObject>(url);
                string responseUrl = URL["responseUrl"].ToString();
                linkModel.Valid = true;
                linkModel.Url = responseUrl;
                return linkModel;
            }
            return linkModel;
        }
        public void Add(ICacheService cacheService, string url, RedisCacheModel cacheModel)
        {
            cacheService.Add(url, cacheModel);
        }
    }
}
