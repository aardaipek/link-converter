using LinkConverter.Model.Enums;

namespace LinkConverter.Helpers.Interfaces
{
    public interface IDeepLinkValidation
    {
        bool IsIncludeProductTag(string url);
        bool IsIncludeContentAfterProductTag(string url);
        public bool IsContainBoutiqueId(string url);
        public bool IsContainMerchantId(string url);
        public bool IsContainCampaignId(string url);
        public UrlType GetUrlType(string url);
        public bool IsContainQuery(string url);
        public bool IsRedirectUrl(string url);
    }
}