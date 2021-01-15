using RXN.AspNetCore.QuickBookWCSoap2Rest.Utils;
using System.Xml.Linq;

namespace RXN.AspNetCore.QuickBookWCSoap2Rest.Bridges
{
    public class WCResponseBridge
    {
        private readonly XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
        private readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private readonly XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
        private readonly XNamespace qb = "http://developer.intuit.com/";

        private XElement Envelope;
        private XElement Body;
        private XElement Response;
        private XElement Result;

        public WCResponseBridge(WCSkeletonWebMethod skeletonMethod, dynamic result)
        {
            Envelope = new XElement(soap + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soap", soap.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName)
            );
            Body = new XElement(soap + "Body");

            switch (skeletonMethod)
            {
                case WCSkeletonWebMethod.GetInteractiveURL:
                    Response = new XElement(qb + WC_RESPONSE.GET_INTERACTIVE_URL);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.GET_INTERACTIVE_URL, result);
                    break;

                case WCSkeletonWebMethod.InteractiveRejected:
                    Response = new XElement(qb + WC_RESPONSE.INTERACTIVE_REJECTED);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.INTERACTIVE_REJECTED, result);
                    break;

                case WCSkeletonWebMethod.InteractiveDone:
                    Response = new XElement(qb + WC_RESPONSE.INTERACTIVE_DONE);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.INTERACTIVE_DONE, result);
                    break;

                case WCSkeletonWebMethod.ServerVersion:
                    Response = new XElement(qb + WC_RESPONSE.SERVER_VERSION);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.SERVER_VERSION, result);
                    break;

                case WCSkeletonWebMethod.ClientVersion:
                    Response = new XElement(qb + WC_RESPONSE.CLIENT_VERSION);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.CLIENT_VERSION, result);
                    break;

                case WCSkeletonWebMethod.Authenticate:
                    Response = new XElement(qb + WC_RESPONSE.AUTHENTICATE);
                    Result = new XElement(
                        qb + WC_RESPONSE_RESULT.AUTHENTICATE,
                        new XElement(qb + "string", result[0]),
                        new XElement(qb + "string", result[1])
                    );
                    break;

                case WCSkeletonWebMethod.SendRequestXML:
                    Response = new XElement(qb + WC_RESPONSE.SEND_REQUEST_XML);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.SEND_REQUEST_XML, result);
                    break;

                case WCSkeletonWebMethod.ReceiveResponseXML:
                    Response = new XElement(qb + WC_RESPONSE.RECEIVE_RESPONSE_XML);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.RECEIVE_RESPONSE_XML, result);
                    break;

                case WCSkeletonWebMethod.ConnectionError:
                    Response = new XElement(qb + WC_RESPONSE.CONNECTION_ERROR);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.CONNECTION_ERROR, result);
                    break;

                case WCSkeletonWebMethod.GetLastError:
                    Response = new XElement(qb + WC_RESPONSE.GET_LAST_ERROR);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.GET_LAST_ERROR, result);
                    break;

                case WCSkeletonWebMethod.CloseConnection:
                    Response = new XElement(qb + WC_RESPONSE.CLOSE_CONNECTION);
                    Result = new XElement(qb + WC_RESPONSE_RESULT.CLOSE_CONNECTION, result);
                    break;

                default:
                    break;
            }

            Response.Add(Result);
            Body.Add(Response);
            Envelope.Add(Body);
        }

        public XElement ResponseXml()
        {
            return Envelope;
        }
    }
}
