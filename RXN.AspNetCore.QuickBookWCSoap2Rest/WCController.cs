// Copyright (C) 2013-2020 Retail Exchange Network, Inc. All rights reserved.
using Microsoft.AspNetCore.Http;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Bridges;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces;
using RXN.AspNetCore.QuickBookWCSoap2Rest.Utils;
using System;
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

        /// <summary>
        /// 
        /// Recieved your custom handler implemented from IWCWebMethodAsync
        /// 
        /// </summary>
        /// <param name="hanlder"></param>
        public WCController(IWCWebMethodAsync hanlder)
        {
            _hanlderAsync = hanlder;
        }

        /// <summary>
        /// Listen all the request from Web Connector of QuickBooks and control them to your handler
        /// </summary>
        /// <returns></returns>
        public XElement Handle(HttpRequest request)
        {
            if (_hanlder is null)
            {
                throw new NullReferenceException("Only handlers implement IWCWebMethod that could call this method!");
            }

            var s2r = new WCRequestBridge(request);
            var soapAction = s2r.GetSkeletonActionMethod();

            object responseValue = null;

            switch (soapAction)
            {
                //case WCSkeletonWebMethod.GetInteractiveURL:
                //    responseValue = _hanlder.GetInteractiveURL(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.SESSION_ID)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveRejected:
                //    responseValue = _hanlder.InteractiveRejected(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.REASON)
                //    );
                //    break;

                //case WCSkeletonWebMethod.InteractiveDone:
                //    responseValue = _hanlder.InteractiveDone(s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET));
                //    break;

                case WCSkeletonWebMethod.ServerVersion:
                    responseValue = _hanlder.ServerVersion(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.ClientVersion:
                    responseValue = _hanlder.ClientVersion(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    break;

                case WCSkeletonWebMethod.Authenticate:
                    responseValue = _hanlder.Authenticate(
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_USERNAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_PASSWORD)
                    );
                    break;

                case WCSkeletonWebMethod.SendRequestXML:
                    responseValue = _hanlder.SendRequestXML(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.HCP_RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.COMPANY_FILENAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.XML_COUNTRY),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MAJOR_VERS)),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MINOR_VERS))
                    );
                    break;

                case WCSkeletonWebMethod.ReceiveResponseXML:
                    responseValue = _hanlder.ReceiveResponseXML(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.ConnectionError:
                    responseValue = _hanlder.ConnectionError(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    break;

                case WCSkeletonWebMethod.GetLastError:
                    responseValue = _hanlder.GetLastError(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                case WCSkeletonWebMethod.CloseConnection:
                    responseValue = _hanlder.CloseConnection(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    break;

                default:
                    break;
            }

            if (responseValue == null)
            {
                return null;
            }

            var resBridge = new WCResponseBridge(soapAction, responseValue);

            return resBridge.ResponseXml();
        }

        public async Task<XElement> HandleAsync(HttpRequest request)
        {
            if (_hanlderAsync is null)
            {
                throw new NullReferenceException("Only handlers implement IWCWebMethodAsync that could call this method!");
            }

            var s2r = new WCRequestBridge(request);
            var soapAction = s2r.GetSkeletonActionMethod();

            object responseValue = null;

            switch (soapAction)
            {
                //case WCSkeletonWebMethod.GetInteractiveURL:
                //    var taskGetInteractiveURL = _hanlderAsync.GetInteractiveURLAsync(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.SESSION_ID)
                //    );
                //    if (taskGetInteractiveURL != null)
                //    {
                //        responseValue = await taskGetInteractiveURL;
                //    }
                //    break;

                //case WCSkeletonWebMethod.InteractiveRejected:
                //    var taskInteractiveRejected = _hanlderAsync.InteractiveRejectedAsync(
                //        s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET),
                //        s2r.GetParam(WC_REQUEST_PARAMS.REASON)
                //    );
                //    if (taskInteractiveRejected != null)
                //    {
                //        responseValue = await taskInteractiveRejected;
                //    }
                //    break;

                //case WCSkeletonWebMethod.InteractiveDone:
                //    var taskInteractiveDone = _hanlderAsync.InteractiveDoneAsync(s2r.GetParam(WC_REQUEST_PARAMS.WC_TICKET));
                //    if (taskInteractiveDone != null)
                //    {
                //        responseValue = await taskInteractiveDone;
                //    }
                //    break;

                case WCSkeletonWebMethod.ServerVersion:
                    var taskServerVersion = _hanlderAsync.ServerVersionAsync(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    if (taskServerVersion != null)
                    {
                        responseValue = await taskServerVersion;
                    }
                    break;

                case WCSkeletonWebMethod.ClientVersion:
                    var taskClientVersion = _hanlderAsync.ClientVersionAsync(s2r.GetParam(WC_REQUEST_PARAMS.VERSION));
                    if (taskClientVersion != null)
                    {
                        responseValue = await taskClientVersion;
                    }
                    break;

                case WCSkeletonWebMethod.Authenticate:
                    var taskAuthenticate = _hanlderAsync.AuthenticateAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_USERNAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.AUTH_PASSWORD)
                    );
                    if (taskAuthenticate != null)
                    {
                        responseValue = await taskAuthenticate;
                    }
                    break;

                case WCSkeletonWebMethod.SendRequestXML:
                    var task = _hanlderAsync.SendRequestXMLAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.HCP_RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.COMPANY_FILENAME),
                        s2r.GetParam(WC_REQUEST_PARAMS.XML_COUNTRY),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MAJOR_VERS)),
                        int.Parse(s2r.GetParam(WC_REQUEST_PARAMS.XML_MINOR_VERS))
                    );
                    if (task != null)
                    {
                        responseValue = await task;
                    }
                    break;

                case WCSkeletonWebMethod.ReceiveResponseXML:
                    var taskReceiveResponseXML = _hanlderAsync.ReceiveResponseXMLAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.RESPONSE),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    if (taskReceiveResponseXML != null)
                    {
                        responseValue = await taskReceiveResponseXML;
                    }
                    break;

                case WCSkeletonWebMethod.ConnectionError:
                    var taskConnectionError = _hanlderAsync.ConnectionErrorAsync(
                        s2r.GetParam(WC_REQUEST_PARAMS.TICKET),
                        s2r.GetParam(WC_REQUEST_PARAMS.H_RESULT),
                        s2r.GetParam(WC_REQUEST_PARAMS.MESSAGE)
                    );
                    if (taskConnectionError != null)
                    {
                        responseValue = await taskConnectionError;
                    }
                    break;

                case WCSkeletonWebMethod.GetLastError:
                    var taskGetLastError = _hanlderAsync.GetLastErrorAsync(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    if (taskGetLastError != null)
                    {
                        responseValue = await taskGetLastError;
                    }
                    break;

                case WCSkeletonWebMethod.CloseConnection:
                    var taskCloseConnection = _hanlderAsync.CloseConnectionAsync(s2r.GetParam(WC_REQUEST_PARAMS.TICKET));
                    if (taskCloseConnection != null)
                    {
                        responseValue = await taskCloseConnection;
                    }
                    break;

                default:
                    break;
            }

            if (responseValue == null)
            {
                return null;
            }

            var resBridge = new WCResponseBridge(soapAction, responseValue);

            return resBridge.ResponseXml();
        }
    }
}
