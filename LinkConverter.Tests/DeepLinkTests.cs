using Xunit;
using FluentAssertions;
using LinkConverter.Helpers;

namespace LinkConverter.Tests
{
    public class DeepLinkTests
    {
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064", true)]
        [InlineData("ty://?Page=&ContentId=1925865&CampaignId=439892&MerchantId=105064", false)]
        [InlineData("ty://?ContentId=1925865&CampaignId=439892&MerchantId=105064", false)]
        public void Include_DeepLinkProduct_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new DeepLinkValidation(); 
            
            // Act
            var result = sut.IsIncludeProductTag(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064", true)]
        [InlineData("ty://?Page=Product&1925865&CampaignId=439892&MerchantId=105064", false)]
        [InlineData("ty://?Page=Product&ContentId=&CampaignId=439892&MerchantId=105064", false)]
        [InlineData("ty://?Page=Product&&CampaignId=439892&MerchantId=105064", false)]
        public void Include_DeepLinkContentId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new DeepLinkValidation();
            
            // Act
            var result = sut.IsIncludeContentAfterProductTag(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064", true)]
        [InlineData("ty://?Page=Product&ContentId=1925865&439892&MerchantId=105064", false)]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=&MerchantId=105064", false)]
        public void Include_DeepLinkCampaignId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new DeepLinkValidation();
            
            // Act
            var result = sut.IsContainCampaignId(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064", true)]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&105064", false)]
        [InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=", false)]
        public void Include_DeepLinkMerchantId_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new DeepLinkValidation();
            
            // Act
            var result = sut.IsContainMerchantId(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("ty://?Page=Favorites", true)]
        [InlineData("y://?", true)]
        public void Include_DeepLinkRedirectControl_WhenIsValid(string webUrl, bool expected)
        {
            // Arrange
            var sut = new DeepLinkValidation();
            
            // Act
            var result = sut.IsRedirectUrl(webUrl);
        
            // Assert
            result.Should().Be(expected);
        }
    }
}