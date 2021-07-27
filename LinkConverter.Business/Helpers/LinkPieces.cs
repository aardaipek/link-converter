using System;
using System.Linq;
using LinkConverter.Business.Interfaces;
using LinkConverter.Model.Common;
using LinkConverter.Model.Enums;

namespace LinkConverter.Business.Helpers
{
    public class LinkPieces : ILinkPieces
    {
        public  DeepLinkPiecesModel SplitLink(ValidWebUrlModel webUrl)
        {
            DeepLinkPiecesModel pieces = new DeepLinkPiecesModel();
            try
            {
                Uri uri = new Uri(webUrl.Url);
                
                if (webUrl.Type == UrlType.Product)
                {
                    pieces.ContentId = GetProductId(uri);

                    if (webUrl.ShowCampaign)
                        pieces.CampaignId = GetBoutiqueId(uri);
                    
                    if (webUrl.ShowMerchant)
                        pieces.MerchantId = GetMerchantId(uri);

                    pieces.Page = UrlType.Product.ToString();
                    
                    if (webUrl.Redirect)
                        pieces.Page = "Home";

                    pieces.Host = "ty://";
                    pieces.Type = webUrl.Type;

                }

                if (webUrl.Type == UrlType.Search)
                {
                    pieces.Query = GetSearchQuery(uri);
                    pieces.Page = UrlType.Search.ToString();
                    pieces.Host = "ty://";
                    pieces.Type = webUrl.Type;
                }

                if (webUrl.Type == UrlType.NotSet)
                {
                    pieces.Page = "Home";
                    pieces.Host = "ty://";
                    pieces.Type = webUrl.Type; 
                }
            }
            catch (Exception ex)
            {
                
            }

            return pieces;
        }
        public WebUrlPiecesModel SplitLink(ValidDeepLinkModel deepLink)
        {
            WebUrlPiecesModel pieces = new WebUrlPiecesModel();

            try
            {
                Uri uri = new Uri(deepLink.Url);

                if (deepLink.Type == UrlType.Product)
                {
                    pieces.ContentId = GetProductId(uri);
                    
                    if (deepLink.ShowCampaign)
                        pieces.BoutiqueId = GetBoutiqueId(uri);
                    
                    if (deepLink.ShowMerchant)
                        pieces.MerchantId = GetMerchantId(uri);

                    string home = "www.trendyol.com";
                    
                    if (deepLink.Redirect)
                        pieces.Page = "-p-";
                    
                    pieces.Host = home;
                    pieces.Type = deepLink.Type;
                }

                if (deepLink.Type == UrlType.Search)
                {
                    pieces.Query = GetSearchQuery(uri);
                    pieces.Page = "/sr";
                    pieces.Host = "www.trendyol.com";
                    pieces.Type = deepLink.Type;
                }
                if (deepLink.Type == UrlType.NotSet)
                {
                    pieces.Page = "";
                    pieces.Host = "www.trendyol.com";
                    pieces.Type = deepLink.Type; 
                }
            }
            catch (Exception ex)
            {
                
            }

            return pieces;
        }
        public string CombineLink(DeepLinkPiecesModel pieces)
        {
            string url = "";

            if (pieces.Type == UrlType.Product)
            {
                string campaign = pieces.CampaignId != 0 ? "&CampaignId=" + pieces.CampaignId : "";
                
                string merchant = pieces.MerchantId != 0 ? "&MerchantId=" + pieces.MerchantId : "";
                
                url = pieces.Host + "?Page=" + pieces.Page + "&ContentId=" + pieces.ContentId + campaign + merchant;
                
            }

            if (pieces.Type == UrlType.Search)
            {
                url = pieces.Host + "?Page=" + pieces.Page + "&Query="+pieces.Query;
            }

            if (pieces.Type == UrlType.NotSet)
            {
                url = pieces.Host + "?Page=" + pieces.Page;
            }

            return url;
        }
        public string CombineLink(WebUrlPiecesModel pieces)
        {
            string url = "";

            if (pieces.Type == UrlType.Product)
            {
                string boutique = pieces.BoutiqueId != 0 ? "?boutiqueId=" + pieces.BoutiqueId : "";
                
                string merchant = pieces.MerchantId != 0 ? "&merchantId=" + pieces.MerchantId : "";
                
                url = pieces.Host + "/brand/name" + pieces.Page + pieces.ContentId + boutique + merchant;
                
            }

            if (pieces.Type == UrlType.Search)
            {
                url = pieces.Host + pieces.Page + "?q="+pieces.Query;
            }
            if (pieces.Type == UrlType.NotSet)
            {
                url = pieces.Host;
            }

            return url;
        }
        private int GetBoutiqueId(Uri uri)
        {
            var linkQuery = uri.Query;
            var splitQuery = linkQuery.Split("&");
            var boutiquePart = splitQuery.Where(x => x.Contains("boutiqueId")).ToString();
            int boutiqueId = int.Parse(boutiquePart.Split("=")[1]);

            return boutiqueId;
        }
        private int GetMerchantId(Uri uri)
        {
            var linkQuery = uri.Query;
            var splitQuery = linkQuery.Split("&");
            var merchantPart = splitQuery.Where(x => x.Contains("merchantId")).ToString();
            int merchantId = int.Parse(merchantPart.Split("=")[1]);

            return merchantId;
        }
        private int GetProductId(Uri uri)
        {
            var absolutePath = uri.AbsolutePath;
            var splitAbsolutePath = absolutePath.Split("-p-");
            int productId = int.Parse(splitAbsolutePath[1]);

            return productId;
        }
        private string GetSearchQuery(Uri uri)
        {
            var query = uri.Query;
            var splitQuery = query.Split("=");
            string stringQuery = splitQuery[1].ToString();

            return stringQuery;
        }
    }
}