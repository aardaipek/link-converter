using LinkConverter.Model.Enums;

namespace LinkConverter.Model.Common
{
    public class ValidDeepLinkModel
    {
        public string Url { get; set; }
        public bool Valid { get; set; }
        public UrlType Type { get; set; }
        public int ContentId { get; set; }
        public bool ShowCampaign{ get; set; }
        public bool ShowMerchant { get; set; }
        public bool Redirect { get; set; }    
        public bool Query { get; set; }
    }
}