using System;
using System.Text.RegularExpressions;
using LinkConverter.Helpers.Interfaces;
using LinkConverter.Model.Enums;
using LinkConverter.Model.Common;


namespace LinkConverter.Helpers
{
    public class WebUrlValidation : IWebUrlValidation
    {
        public static UrlType urlType;
        public bool IsIncludeProductTag(string url)
        {
            return url.Contains("-p-");
        }
        public bool IsIncludeContentAfterProductTag(string url)
        {
            Uri uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            if (absolutePath.Contains("-p-"))
            {
                var regex = new Regex(@"([0-9])\d+");
                var splitUrl = absolutePath.Split("-p-");
                
                if(splitUrl[0].Length > 0 && regex.Match(splitUrl[0]).Success)
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
            var isContainMerchant = splitUrl.Length > 1 ? splitUrl[1].Contains("merchantId") : false;
            
            if (isContainMerchant)
            {
                var regex = new Regex(@"(=[0-9])\d+");
                if(splitUrl[1].Length > 0 && !regex.Match(splitUrl[1]).Success)
                    return false;

                return true;
            }

            return false;
        }
        public bool IsContainCampaignId(string url)
        {
            Uri uri = new Uri(url);
            var urlQuery = uri.Query;
            if (urlQuery == "")
                return false;
            
            var containCampaign = urlQuery.Contains("campaignId");
            return containCampaign;
        }
        public UrlType GetUrlType(string url)
        {
            Uri uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            if (absolutePath.Contains("/sr"))
            {
                urlType = UrlType.Search;
                return UrlType.Search;
            }
               
            if (absolutePath.Contains("-p-"))
            {
                urlType = UrlType.Product;
                return UrlType.Product;
            }

            urlType = UrlType.NotSet;
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
            if (absolutePath.Contains("/sr"))
                return false;
            if (absolutePath.Contains("-p-"))
                return false;
                    
            return true;
        }
        public ValidationModel ProductValidation(string url)
        {
            GetUrlType(url);

            ValidationModel valid = new ValidationModel();

            if(urlType == UrlType.Product)
            {
                bool productTagValidation = IsIncludeProductTag(url);

                if (!productTagValidation)
                {
                    valid.Message = "Web Url must contain product tag";
                    valid.Valid = false;
                    return valid;
                        
                }
                 

                bool contentIdValidation = IsIncludeContentAfterProductTag(url);

                if (!contentIdValidation)
                {
                   valid.Message = "Web Url must contain content id";
                   valid.Valid = false;
                   return valid;
                }
                   

                bool containCampaignId = IsContainCampaignId(url);

                if (containCampaignId)
                {
                   
                   valid.Message =  "Web Url shouldn't contain campaign id";
                   valid.Valid = false;
                   return valid;
                }
                   
            }
            valid.Valid = true;
            return valid;
        }
    }
}