using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IMaintenanceRestClient
    {
        /// <summary>
        /// Method to change password 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> ChangePassword(HttpContent content);

        Task<HttpResponseMessage> Initialize(HttpContent content);

        Task<HttpResponseMessage> CloseBatch(HttpContent content);

        Task<HttpResponseMessage> SetPrepay(bool isPrepay);

        Task<HttpResponseMessage> SetPostPay(bool isPostPay);
    }
}
