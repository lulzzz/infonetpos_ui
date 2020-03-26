using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface ILogoutRestClient
    {
        /// <summary>
        /// Method to switch user
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SwitchUser(HttpContent content);

        Task<HttpResponseMessage> Logout(HttpContent content);

        Task<HttpResponseMessage> CloseTill();

        Task<HttpResponseMessage> ReadTankDip();

        Task<HttpResponseMessage> EndShift();

        Task<HttpResponseMessage> ValidateTillClose();

        Task<HttpResponseMessage> UpdateTillClose(HttpContent content);

        Task<HttpResponseMessage> FinishTillClose(bool? readTankDip, bool? readTotalizer);
    }
}
