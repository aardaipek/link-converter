using System;
using System.Threading.Tasks;
using LinkConverter.Business;
using LinkConverter.CacheService.Redis;
using LinkConverter.Helpers.Interfaces;
using LinkConverter.Model.Cache;
using LinkConverter.Model.Common;
using LinkConverter.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LinkConverter.Controllers
{
    [Route("api/convert")]
    public class ConvertController : BaseController
    {
        private IRedisHelper _redisHelper;
        private IDeepLinkValidation _deepLinkValidation;
        private IWebUrlValidation _webUrlValidation;
        private ICacheService _cacheService;

        public ConvertController(IRedisHelper redisHelper,ICacheService cacheService,IDeepLinkValidation deepLinkValidation, IWebUrlValidation webUrlValidation)
        {
            _redisHelper = redisHelper;
            _cacheService = cacheService;
            _deepLinkValidation = deepLinkValidation;
            _webUrlValidation = webUrlValidation;
        }

        [HttpGet, Route("deep_link")]
        public async Task<DeepLinkModel> Convert([FromBody] WebUrlModel webUrl)
        {
            DeepLinkModel validDeepLink = new DeepLinkModel();
            try
            {
                GeneralLinkModel redisModel = _redisHelper.Get(_cacheService, webUrl.Url);
                if (redisModel.Valid)
                {
                    validDeepLink.Url = redisModel.Url;
                    return validDeepLink;
                }
                    
                ValidationModel validation = _webUrlValidation.ProductValidation(webUrl.Url);

                if (!validation.Valid)
                    throw new Exception(validation.Message);
                

                bool containBoutique =
                    _webUrlValidation.IsContainBoutiqueId(webUrl.Url); // False ise Campaign olmayacak

                bool containMerchant =
                    _webUrlValidation.IsContainMerchantId(webUrl.Url); // False ise MerchantId olmayacak

                bool containQuery = _webUrlValidation.IsContainQuery(webUrl.Url);

                bool isRedirect = _webUrlValidation.IsRedirectUrl(webUrl.Url);

                // Valid ise
                ValidWebUrlModel validWebUrl = new ValidWebUrlModel();
                validWebUrl.Url = webUrl.Url;
                validWebUrl.Valid = true;
                validWebUrl.Type = IWebUrlValidation.urlType;
                validWebUrl.Query = containQuery;
                validWebUrl.ShowCampaign = containBoutique;
                validWebUrl.ShowMerchant = containMerchant;
                validWebUrl.Redirect = isRedirect;

                ConvertLink convertWebUrlToDeepLink = new ConvertLink();

                validDeepLink = convertWebUrlToDeepLink.ConvertToDeepLink(validWebUrl);
                
                RedisCacheModel cacheModel = new RedisCacheModel();
                cacheModel.requestUrl = webUrl.Url;
                cacheModel.responseUrl = validDeepLink.Url;

                _redisHelper.Add(_cacheService,webUrl.Url, cacheModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return validDeepLink;
        }

        [HttpGet, Route("web_url")]
        public async Task<WebUrlModel> Convert([FromBody] DeepLinkModel deepLinkUrl)
        {
            WebUrlModel webUrlModel = new WebUrlModel();

            try
            {
                if (_cacheService.Any(deepLinkUrl.Url))
                {
                    WebUrlModel cacheValidWebUrl = new WebUrlModel();
                    var url = _cacheService.Get<JObject>(deepLinkUrl.Url);
                    string responseUrl = url["responseUrl"].ToString();
                    cacheValidWebUrl.Valid = true;
                    cacheValidWebUrl.Url = responseUrl;
                    return cacheValidWebUrl;
                }
                
                // Validasyonlar
                UrlType urlType = _deepLinkValidation.GetUrlType(deepLinkUrl.Url);

                if (urlType == UrlType.Product)
                {
                    bool productTagValidation = _deepLinkValidation.IsIncludeProductTag(deepLinkUrl.Url);

                    if (!productTagValidation)
                        throw new Exception("Deep Link must contain product tag");

                    bool contentIdValidation = _deepLinkValidation.IsIncludeContentAfterProductTag(deepLinkUrl.Url);

                    if (!contentIdValidation)
                        throw new Exception("Deep Link must contain content id");

                    bool containBoutique = _deepLinkValidation.IsContainBoutiqueId(deepLinkUrl.Url);

                    if (containBoutique)
                        throw new Exception("Deep Link shouldn't contain boutique");
                }

                bool containCampaign =
                    _deepLinkValidation.IsContainCampaignId(deepLinkUrl.Url); // False ise Campaign olmayacak

                bool containMerchant =
                    _deepLinkValidation.IsContainMerchantId(deepLinkUrl.Url); // False ise MerchantId olmayacak

                bool containQuery = _deepLinkValidation.IsContainQuery(deepLinkUrl.Url);

                bool isRedirect = _deepLinkValidation.IsRedirectUrl(deepLinkUrl.Url);

                // Valid ise
                ValidDeepLinkModel validDeepLink = new ValidDeepLinkModel();
                validDeepLink.Url = deepLinkUrl.Url;
                validDeepLink.Valid = true;
                validDeepLink.Type = urlType;
                validDeepLink.ShowCampaign = containCampaign;
                validDeepLink.ShowMerchant = containMerchant;
                validDeepLink.Query = containQuery;
                validDeepLink.Redirect = isRedirect;

                ConvertLink convertDeepLinkToWebUrl = new ConvertLink();

                webUrlModel = convertDeepLinkToWebUrl.ConvertToWebUrl(validDeepLink);
                
                RedisCacheModel cacheModel = new RedisCacheModel();
                cacheModel.requestUrl = deepLinkUrl.Url;
                cacheModel.responseUrl = webUrlModel.Url;
                
                _cacheService.Add( deepLinkUrl.Url,cacheModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return webUrlModel;
        }
    }
}