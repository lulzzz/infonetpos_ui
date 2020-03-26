using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IReasonListRestClient
    {
        /// <summary>
        /// Method to get reasons List
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetReasonList(string reason);
    }
}
