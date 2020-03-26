using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface ICustomerRestClient
    {
        Task<HttpResponseMessage> AddCustomer(HttpContent content);

        Task<HttpResponseMessage> GetAllCustomers(int pageIndex, bool loyalty);

        Task<HttpResponseMessage> SearchCustomers(string searchTerm, int pageIndex, bool loyalty);

        Task<HttpResponseMessage> SetCustomerForSale(string code, int saleNumber, int tillNumber, 
            byte registerNumber);

        Task<HttpResponseMessage> SetloyalityCustomer(HttpContent content);

        Task<HttpResponseMessage> GetCustomerByCard(HttpContent content);
    }
}
