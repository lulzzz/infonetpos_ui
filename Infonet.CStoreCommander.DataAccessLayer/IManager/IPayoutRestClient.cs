using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IPayoutRestClient
    {
        Task<HttpResponseMessage> GetVendorPayout();

        Task<HttpResponseMessage> PayoutComplete(HttpContent content);
    }
}
