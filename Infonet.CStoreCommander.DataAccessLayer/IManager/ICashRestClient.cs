using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface ICashRestClient
    {
        Task<HttpResponseMessage> GetCashDrawTypes();

        Task<HttpResponseMessage> CompleteCashDraw(HttpContent content);

        Task<HttpResponseMessage> GetAllTenders(string transactionType,
            bool billTillClose, string dropreason);

        Task<HttpResponseMessage> GetCashButtons();

        Task<HttpResponseMessage> UpdateTenderForCashdrop(HttpContent content);

        Task<HttpResponseMessage> CompleteCashCashdrop(HttpContent content);

        Task<HttpResponseMessage> OpenCashDrawer(HttpContent content);
    }
}
