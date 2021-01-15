using Microsoft.AspNetCore.Http;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Bridges;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Utils;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RXN.AspNetCore.QuickBookWCSoap2Rest
{
    public class WCController
    {
        private readonly IWCWebMethod _hanlder;
        private readonly IWCWebMethodAsync _hanlderAsync;

        /// <summary>
        /// 
        /// Recieved your custom handler implemented from IWCWebMethod
        /// 
        /// </summary>
        /// <param name="hanlder"></param>
        public WCController(IWCWebMethod hanlder)
        {
            _hanlder = hanlder;
        }

        public WCController(IWCWebMethodAsync hanlderAsync)
        {
            _hanlderAsync = hanlderAsync;
        }

        /// <summary>
        /// Listen all the request from Web Connector of QuickBooks and control them to your handler
        /// </summary>
        /// <returns></returns>
        public XElement Handle(HttpRequest request)
        {
            var s2r = new WCRequestBridge(request);
            var soapAction = s2r.GetSkeletonActionMethod();

            object responeValue;

            switch (soapAction)
            {
                //case WCSkeletonWebMethod.GetInteractiveURL:
                //    responeValue = _hanlder.GetInteractiveURL(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.SESSION_ID)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveRejected:
                //    responeValue = _hanlder.InteractiveRejected(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.REASON)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveDone:
                //    responeValue = _hanlder.InteractiveDone(s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET));
                //    break;

                case WCSkeletonWebMethod.ServerVersion:
                    responeValue = _hanlder.ServerVersion(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.ClientVersion:
                    responeValue = _hanlder.ClientVersion(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.Authenticate:
                    responeValue = _hanlder.Authenticate(
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_USERNAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_PASSWORD)
                    );
                    break;

                case WCSkeletonWebMethod.SendRequestXML:
                    responeValue = _hanlder.SendRequestXML(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.HCP_RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.COMPANY_FILENAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.XML_COUNTRY),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MAJOR_VERS)),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MINOR_VERS))
                    );
                    break;

                case WCSkeletonWebMethod.ReceiveResponseXML:
                    responeValue = _hanlder.ReceiveResponseXML(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.ConnectionError:
                    responeValue = _hanlder.ConnectionError(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.GetLastError:
                    responeValue = _hanlder.GetLastError(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                case WCSkeletonWebMethod.CloseConnection:
                    responeValue = _hanlder.CloseConnection(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                default:
                    return null;
            }

            var resBridge = new WCResponseBridge(soapAction, responeValue);

            return resBridge.ResponseXml();
        }

        public async Task<XElement> HandleAsync(HttpRequest request)
        {
            var s2r = new WCRequestBridge(request);
            var soapAction = s2r.GetSkeletonActionMethod();

            object responeValue;

            switch (soapAction)
            {
                //case WCSkeletonWebMethod.GetInteractiveURL:
                //    responeValue = await _hanlderAsync.GetInteractiveURLAsync(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.SESSION_ID)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveRejected:
                //    responeValue = await _hanlderAsync.InteractiveRejectedAsync(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.REASON)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveDone:
                //    responeValue = await _hanlderAsync.InteractiveDoneAsync(s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET));
                //    break;

                case WCSkeletonWebMethod.ServerVersion:
                    responeValue = await _hanlderAsync.ServerVersionAsync(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.ClientVersion:
                    responeValue = await _hanlderAsync.ClientVersionAsync(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.Authenticate:
                    responeValue = await _hanlderAsync.AuthenticateAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_USERNAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_PASSWORD)
                    );
                    break;

                case WCSkeletonWebMethod.SendRequestXML:
                    responeValue = await _hanlderAsync.SendRequestXMLAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.HCP_RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.COMPANY_FILENAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.XML_COUNTRY),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MAJOR_VERS)),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MINOR_VERS))
                    );
                    break;

                case WCSkeletonWebMethod.ReceiveResponseXML:
                    responeValue = await _hanlderAsync.ReceiveResponseXMLAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.ConnectionError:
                    responeValue = await _hanlderAsync.ConnectionErrorAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.GetLastError:
                    responeValue = await _hanlderAsync.GetLastErrorAsync(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                case WCSkeletonWebMethod.CloseConnection:
                    responeValue = await _hanlderAsync.CloseConnectionAsync(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                default:
                    return null;
            }

            var resBridge = new WCResponseBridge(soapAction, responeValue);

            return resBridge.ResponseXml();
        }
    }
}
