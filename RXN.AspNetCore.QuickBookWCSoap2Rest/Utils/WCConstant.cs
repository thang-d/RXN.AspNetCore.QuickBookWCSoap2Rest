
namespace RXN.AspNetCore.QuickBookWCSoap2Rest.Utils
{
    public static class WC_REQUEST_METHOD
    {
        public const string GET_INTERACTIVE_URL = "getInteractiveURL";
        public const string INTERACTIVE_REJECTED = "interactiveRejected";
        public const string INTERACTIVE_DONE = "interactiveDone";
        public const string SERVER_VERSION = "serverVersion";
        public const string CLIENT_VERSION = "clientVersion";
        public const string AUTHENTICATE = "authenticate";
        public const string SEND_REQUEST_XML = "sendRequestXML";
        public const string RECEIVE_RESPONSE_XML = "receiveResponseXML";
        public const string CONNECTION_ERROR = "connectionError";
        public const string GET_LAST_ERROR = "getLastError";
        public const string CLOSE_CONNECTION = "closeConnection";
    }

    public static class WC_REQUEST_PARAMS
    {
        public const string WC_TICKET = "wcTicket";
        public const string SESSION_ID = "sessionID";
        public const string REASON = "reason";
        public const string VERSION = "strVersion";
        public const string AUTH_USERNAME = "strUserName";
        public const string AUTH_PASSWORD = "strPassword";
        public const string TICKET = "ticket";
        public const string HCP_RESPONSE = "strHCPResponse";
        public const string COMPANY_FILENAME = "strCompanyFileName";
        public const string XML_COUNTRY = "qbXMLCountry";
        public const string XML_MAJOR_VERS = "qbXMLMajorVers";
        public const string XML_MINOR_VERS = "qbXMLMinorVers";
        public const string RESPONSE = "response";
        public const string H_RESULT = "hresult";
        public const string MESSAGE = "message";
    }

    public static class WC_RESPONSE
    {
        public const string GET_INTERACTIVE_URL = "";
        public const string INTERACTIVE_REJECTED = "";
        public const string INTERACTIVE_DONE = "";
        public const string SERVER_VERSION = "serverVersionResponse";
        public const string CLIENT_VERSION = "clientVersionResponse";
        public const string AUTHENTICATE = "authenticateResponse";
        public const string SEND_REQUEST_XML = "sendRequestXMLResponse";
        public const string RECEIVE_RESPONSE_XML = "receiveResponseXMLResponse";
        public const string CONNECTION_ERROR = "connectionErrorResponse";
        public const string GET_LAST_ERROR = "getLastErrorResponse";
        public const string CLOSE_CONNECTION = "closeConnectionResponse";
    }

    public static class WC_RESPONSE_RESULT
    {
        public const string GET_INTERACTIVE_URL = "";
        public const string INTERACTIVE_REJECTED = "";
        public const string INTERACTIVE_DONE = "";
        public const string SERVER_VERSION = "serverVersionResult";
        public const string CLIENT_VERSION = "clientVersionResult";
        public const string AUTHENTICATE = "authenticateResult";
        public const string SEND_REQUEST_XML = "sendRequestXMLResult";
        public const string RECEIVE_RESPONSE_XML = "receiveResponseXMLResult";
        public const string CONNECTION_ERROR = "connectionErrorResult";
        public const string GET_LAST_ERROR = "getLastErrorResult";
        public const string CLOSE_CONNECTION = "closeConnectionResult";
    }

    public static class WC_REQUEST_STATUS
    {
        public const int REQUESTING = 1;
        public const int DONE = 100;
        public const int ERROR = -1;
    }

    public static class WC_AUTH_STATUS
    {
        public const string NVU = "nvu";
        public const string NONE = "none";
        public const string SUCCESS = "";
    }

    public static class WC_COMMON_ERROR_CODE
    {
        // 0x80040400 - QuickBooks found an error when parsing the provided XML text stream.
        public const string ERROR_WHEN_PARSING = "0x80040400";
        // 0x80040401 - Could not access QuickBooks. 
        public const string COULDNT_ACCESS_QB = "0x80040401";
        // 0x80040402 - Unexpected error. Check the qbsdklog.txt file for possible, additional information. 
        public const string UNEXPECTED_ERROR = "0x80040402";
    }
}
