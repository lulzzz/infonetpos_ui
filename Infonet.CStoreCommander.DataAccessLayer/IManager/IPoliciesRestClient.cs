using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IPolicyRestClient
    {
        /// <summary>
        /// Method to get all policies
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAllPolicies();

        Task<HttpResponseMessage> RefreshPolicies();
    }
}
