using System.ComponentModel.DataAnnotations;
using LinkConverter.Model.Enums;

namespace LinkConverter.Model.Common
{
    public class DeepLinkPiecesModel
    {
        public UrlType Type { get; set; }
        public int MerchantId { get; set; }
        public int CampaignId { get; set; }
        [Required]
        public int ContentId { get; set; }
        public string Host { get; set; }
        public string Query { get; set; }
        public string Page { get; set; }
    }
}