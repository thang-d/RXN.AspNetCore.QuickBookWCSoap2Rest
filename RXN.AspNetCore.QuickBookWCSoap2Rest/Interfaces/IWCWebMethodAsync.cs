using System.Threading.Tasks;

namespace RXN.AspNetCore.QuickBookWCSoap2Rest.Interfaces
{
    public interface IWCWebMethodAsync
    {
        Task<string> GetInteractiveURLAsync(string wcTicket, string sessionID);
        Task<string> InteractiveRejectedAsync(string wcTicket, string reason);
        Task<string> InteractiveDoneAsync(string wcTicket);
        Task<string> ServerVersionAsync(string strVersion);
        Task<string> ClientVersionAsync(string strVersion);
        Task<string[]> AuthenticateAsync(string strUserName, string strPassword);
        Task<string> SendRequestXMLAsync(string ticket, string strHCPResponse, string strCompanyFileName, string qbXMLCountry, int qbXMLMajorVers, int qbXMLMinorVers);
        Task<int> ReceiveResponseXMLAsync(string ticket, string response, string hresult, string message);
        Task<string> ConnectionErrorAsync(string ticket, string hresult, string message);
        Task<string> GetLastErrorAsync(string ticket);
        Task<string> CloseConnectionAsync(string ticket);
    }
}
