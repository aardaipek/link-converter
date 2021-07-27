using LinkConverter.Model.Common;

namespace LinkConverter.Business.Interfaces
{
    public interface IConvert
    {
        DeepLinkModel ConvertToDeepLink(ValidWebUrlModel webUrl);
        WebUrlModel ConvertToWebUrl(ValidDeepLinkModel webUrl);
    }
}