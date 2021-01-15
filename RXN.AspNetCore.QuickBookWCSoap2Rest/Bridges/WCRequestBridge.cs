using Microsoft.AspNetCore.Http;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Utils;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RXN.AspNetCore.QuickBookWCSoap2Rest.Bridges
{
    public class WCRequestBridge
    {
        private WCSkeletonWebMethod _skeletonAction;
        private Dictionary<string, string> _requestParams;

        public WCRequestBridge(HttpRequest request)
        {
            _requestParams = new Dictionary<string, string>();
            _skeletonAction = WCSkeletonWebMethod.None;

            //ParseHeaders(request.Headers);
            ParseBody(request.Body);
        }

        public WCSkeletonWebMethod GetSkeletonActionMethod()
        {
            return _skeletonAction;
        }

        public string GetParam(string key)
        {
            _requestParams.TryGetValue(key, out string value);

            return value;
        }

        //private void ParseHeaders(IHeaderDictionary headers)
        //{
        //    StringValues soapActions;
        //    string soapAction = string.Empty;

        //    // parse soap action in header of this request
        //    if (headers.TryGetValue("SOAPAction", out soapActions))
        //    {
        //        soapAction = soapActions.First().ToString();
        //        _skeletonSoapAction = MatchingSkeleton(ExploreSoapActionFrom(soapAction));
        //    }
        //}

        private void ParseBody(Stream body)
        {
            string rawBody = null;

            using (var reader = new StreamReader(body))
            {
                rawBody = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(rawBody))
            {
                var xmlBody = new XmlDocument();
                xmlBody.LoadXml(rawBody);

                var soapBodyContent = xmlBody.GetElementsByTagName("soap:Body").Item(0).FirstChild;
                _skeletonAction = MatchingSkeletonActionMethod(soapBodyContent.Name);

                if (soapBodyContent.HasChildNodes)
                {
                    foreach (XmlNode node in soapBodyContent.ChildNodes)
                    {
                        _requestParams.Add(node.Name.Trim(), node.InnerText.Trim());
                    }
                }
            }
        }

        private WCSkeletonWebMethod MatchingSkeletonActionMethod(string soapAction)
        {
            switch (soapAction)
            {
                case WC_REQUEST_METHOD.GET_INTERACTIVE_URL:
                    return WCSkeletonWebMethod.GetInteractiveURL;

                case WC_REQUEST_METHOD.INTERACTIVE_REJECTED:
                    return WCSkeletonWebMethod.InteractiveRejected;

                case WC_REQUEST_METHOD.INTERACTIVE_DONE:
                    return WCSkeletonWebMethod.InteractiveDone;

                case WC_REQUEST_METHOD.SERVER_VERSION:
                    return WCSkeletonWebMethod.ServerVersion;

                case WC_REQUEST_METHOD.CLIENT_VERSION:
                    return WCSkeletonWebMethod.ClientVersion;

                case WC_REQUEST_METHOD.AUTHENTICATE:
                    return WCSkeletonWebMethod.Authenticate;

                case WC_REQUEST_METHOD.SEND_REQUEST_XML:
                    return WCSkeletonWebMethod.SendRequestXML;

                case WC_REQUEST_METHOD.RECEIVE_RESPONSE_XML:
                    return WCSkeletonWebMethod.ReceiveResponseXML;

                case WC_REQUEST_METHOD.CONNECTION_ERROR:
                    return WCSkeletonWebMethod.ConnectionError;

                case WC_REQUEST_METHOD.GET_LAST_ERROR:
                    return WCSkeletonWebMethod.GetLastError;

                case WC_REQUEST_METHOD.CLOSE_CONNECTION:
                    return WCSkeletonWebMethod.CloseConnection;

                default:
                    return WCSkeletonWebMethod.None;
            }
        }

        //private string ExploreSoapActionFrom(string soapActionNamespace)
        //{
        //    string pattern = @"\/(\w+)";
        //    var regex = new Regex(pattern);
        //    var matched = regex.Match(soapActionNamespace);

        //    return matched.Value;
        //}
    }
}
