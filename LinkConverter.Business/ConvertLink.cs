using System;
using LinkConverter.Business.Helpers;
using LinkConverter.Business.Interfaces;
using LinkConverter.Model.Common;

namespace LinkConverter.Business
{
    public class ConvertLink : IConvert
    {
        private ILinkPieces _linkPieces = new LinkPieces();
        
        // public ConvertLink(ILinkPieces linkPieces)
        // {
        //     _linkPieces = linkPieces;
        // }
        public DeepLinkModel ConvertToDeepLink(ValidWebUrlModel validUrl)
        {
            DeepLinkModel deepLink = new DeepLinkModel();
            try
            {
                DeepLinkPiecesModel pieces = _linkPieces.SplitLink(validUrl);
                deepLink.Url =  _linkPieces.CombineLink(pieces);
                deepLink.Valid = true;
            }
            catch(Exception ex)
            {
                deepLink.Valid = false;
            }

            return deepLink;
        }
        public WebUrlModel ConvertToWebUrl(ValidDeepLinkModel validUrl)
        {
            WebUrlModel webUrl = new WebUrlModel();
            try
            {
                WebUrlPiecesModel pieces = _linkPieces.SplitLink(validUrl);
                webUrl.Url =  _linkPieces.CombineLink(pieces);
                webUrl.Valid = true;
            }
            catch(Exception ex)
            {
                webUrl.Valid = false;
            }

            return webUrl;
        }
    }
}