using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    /// <summary>
    /// Rest client for Login Screen
    /// </summary>
    public interface ILoginRestClient
    {
        /// <summary>
        /// Gets the Active tills 
        /// </summary>
        /// <param name="content">Http Payload for API</param>
        /// <returns>Http Response for Tills</returns>
        Task<HttpResponseMessage> GetActiveTillsAsync(HttpContent content);

        /// <summary>
        /// Gets the login policies
        /// </summary>
        /// <param name="ipAddress">IpAddress of the POS</param>
        /// <returns>Resonse for Login policies</returns>
        Task<HttpResponseMessage> GetLoginPolicyAsync(string ipAddress);

        /// <summary>
        /// Gets the active shifts
        /// </summary>
        /// <param name="content">Http payload for API</param>
        /// <returns>Response for Active shifts</returns>
        Task<HttpResponseMessage> GetActiveShiftsAsync(HttpContent content);

        /// <summary>
        /// Logins into the POS using API
        /// </summary>
        /// <param name="content">Payload for Login API</param>
        /// <returns>Http response for the Login API</returns>
        Task<HttpResponseMessage> LoginAsync(HttpContent content);

        Task<HttpResponseMessage> GetPasswordAsync(string userName);
    }
}
