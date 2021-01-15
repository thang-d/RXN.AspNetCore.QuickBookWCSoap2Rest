
namespace RXN.AspNetCore.QuickBookWCSoap2Rest.Utils
{
    public enum WCSkeletonWebMethod
    {
        GetInteractiveURL = 0,
        InteractiveRejected = 1,
        InteractiveDone = 2,
        ServerVersion = 3,
        ClientVersion = 4,
        Authenticate = 5,
        SendRequestXML = 6,
        ReceiveResponseXML = 7,
        ConnectionError = 8,
        GetLastError = 9,
        CloseConnection = 10,
        None = 11
    }
}
