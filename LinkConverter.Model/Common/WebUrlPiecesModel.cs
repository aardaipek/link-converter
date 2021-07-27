using System.ComponentModel.DataAnnotations;
using LinkConverter.Model.Enums;

namespace LinkConverter.Model.Common
{
    public class WebUrlPiecesModel
    {
        public UrlType Type { get; set; }
        public int MerchantId { get; set; } = 0;
        public int BoutiqueId { get; set; } = 0;
        [Required]
        public int ContentId { get; set; }
        public string Host { get; set; }
        public string Query { get; set; }
        public string Page { get; set; }

    }
}