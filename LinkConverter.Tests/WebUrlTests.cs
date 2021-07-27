using Xunit;
using FluentAssertions;
using LinkConverter.Helpers;

namespace LinkConverter.Tests
{
    public class WebUrlTests
    {
        [Theory]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", true)]
        [InlineData("https://www.trendyol.com/casio/saat?boutiqueId=439892&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat1925865?boutiqueId=439892&merchantId=105064", false)]
        public void Include_WebUrlProduct_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation(); 
            
            // Act
            var result = sut.IsIncludeProductTag(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", true)]
        [InlineData("https://www.trendyol.com/casio/saat?boutiqueId=439892&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-?boutiqueId=439892&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat1925865-p-?boutiqueId=439892&merchantId=105064", false)]
        public void Include_WebUrlContentId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation();
            
            // Act
            var result = sut.IsIncludeContentAfterProductTag(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", true)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?439892&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?439892boutiqueId=&merchantId=105064", false)]
        public void Include_WebUrlBoutiqueId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation();
            
            // Act
            var result = sut.IsContainBoutiqueId(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", true)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&105064merchantId=", false)]
        public void Include_WebUrlMerchantId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation();
            
            // Act
            var result = sut.IsContainMerchantId(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?merchantId=105064", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064&campaignId=123456", true)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?merchantId=105064&campaignId=123456", true)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&campaignId=123456", true)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865?campaignId=123456", true)]
        public void Include_WebUrlCampaign_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation();
            
            // Act
            var result = sut.IsContainCampaignId(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        // URLTYPE
        // [Theory]
        // [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064", true)]
        // [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892", false)]
        // [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&105064", false)]
        // [InlineData("https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&105064merchantId=", false)]
        // public void Include_WebUrlType_WhenIsValid(string webUrl, bool expected)
        // {
        //     // Arrange
        //     var sut = new WebUrlValidation();
        //     
        //     // Act
        //     var result = sut.GetUrlType(webUrl);
        //
        //     // Assert
        //     result.Should().Be(expected);
        // }
        
        [Theory]
        [InlineData("https://www.trendyol.com/Hesabim/#/Siparislerim", true)]
        [InlineData("https://www.trendyol.com/Hesabim/Favoriler", true)]
        [InlineData("https://www.trendyol.com/sr?q=%C3%BCt%C3%BC", false)]
        [InlineData("https://www.trendyol.com/casio/saat-p-1925865", false)]
        public void Include_WebUrlRedirectControl_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new WebUrlValidation();
            
            // Act
            var result = sut.IsRedirectUrl(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
    }
}