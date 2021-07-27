using System;
using System.Text.RegularExpressions;
using LinkConverter.Helpers.Interfaces;
using LinkConverter.Model.Enums;

namespace LinkConverter.Helpers
{
    public class DeepLinkValidation : IDeepLinkValidation
    {
        public bool IsIncludeProductTag(string url)
        {
            return url.Contains("Product");
        }
        public bool IsIncludeContentAfterProductTag(string url)
        {
            Uri uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            if (absolutePath.Contains("ContentId"))
            {
                var regex = new Regex(@"([0-9])\d+");
                var splitUrl = absolutePath.Split("=");
                
                if(splitUrl[0].Length != 0 && regex.Match(splitUrl[0]).Success)
                    return false;
                
                return regex.Match(absolutePath).Success;
            }
            else
            {
                return false;
            }

            return false;
        }
        public bool IsContainBoutiqueId(string url)
        {
            Uri uri = new Uri(url);
            var urlQuery = uri.Query;
            var splitUrl = urlQuery.Split('&');
            var isContainBoutique = splitUrl[0].Contains("boutiqueId");
            
            if (isContainBoutique)
            {
                var regex = new Regex(@"(=[0-9])\d+");
                if(splitUrl[0].Length > 0 && !regex.Match(splitUrl[0]).Success)
                    return false;

                return true;
            }

            return false;
        }
        public bool IsContainMerchantId(string url)
        {
            Uri uri = new Uri(url);
            var urlQuery = uri.Query;
            var splitUrl = urlQuery.Split('&');
            var isContainMerchant = splitUrl.Length == 3 ? splitUrl[2].Contains("MerchantId") : splitUrl.Length == 4 ? splitUrl[3].Contains("MerchantId") : false;
            
            if (isContainMerchant)
            {
                var regex = new Regex(@"(=[0-9])\d+");
                if(regex.Match(splitUrl.Length == 3 ? splitUrl[2] : splitUrl.Length == 4 ? splitUrl[3] : splitUrl[2]).Success)
                    return true;

                return false;
            }

            return false;
        }
        public bool IsContainCampaignId(string url)
        {
            Uri uri = new Uri(url);
            var urlQuery = uri.Query;
            if (urlQuery == "")
                return false;
            
            var containCampaign = urlQuery.Contains("CampaignId=");
            var splitQuery = urlQuery.Split("&");
            var regex = new Regex(@"(=[0-9])\d+");
            if(splitQuery[2].Length > 0 && !regex.Match(splitQuery[2]).Success)
                return false;
            
            return containCampaign;
        }
        public UrlType GetUrlType(string url)
        {
            Uri uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            if (absolutePath.Contains("/sr"))
                return UrlType.Search;
            if (absolutePath.Contains("-p-"))
                return UrlType.Product;
            return UrlType.NotSet;
        }
        public bool IsContainQuery(string url)
        {
            UrlType urlType = GetUrlType(url);

            if (urlType == UrlType.Search)
            {
                Uri uri = new Uri(url);
                var query = uri.Query;
                var splitQuery = query.Split("=");
                if (splitQuery.Length > 0)
                    return true;
            }

            return false;
        }
        public bool IsRedirectUrl(string url)
        {
            Uri uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            if (absolutePath.Contains("Page=Product"))
                return false;
            if (absolutePath.Contains("Page=Search"))
                return false;
                    
            return true;
        }
    }
}