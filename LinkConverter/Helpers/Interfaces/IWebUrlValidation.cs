using LinkConverter.Model.Enums;
using LinkConverter.Model.Common;

namespace LinkConverter.Helpers.Interfaces
{
    public interface IWebUrlValidation
    {
        public static UrlType urlType;
        bool IsIncludeProductTag(string url);
        bool IsIncludeContentAfterProductTag(string url);
        public bool IsContainBoutiqueId(string url);
        public bool IsContainMerchantId(string url);
        public bool IsContainCampaignId(string url);
        public UrlType GetUrlType(string url);
        public bool IsContainQuery(string url);
        public bool IsRedirectUrl(string url);
        public ValidationModel ProductValidation(string url);
    }
}