using LinkConverter.Model.Common;

namespace LinkConverter.Business.Interfaces
{
    public interface ILinkPieces
    {
        DeepLinkPiecesModel SplitLink(ValidWebUrlModel webUrl);
        WebUrlPiecesModel SplitLink(ValidDeepLinkModel deepLink);
        string CombineLink(DeepLinkPiecesModel pieces);
        string CombineLink(WebUrlPiecesModel pices);
    }
}