using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IPaymentRestClient
    {
        Task<HttpResponseMessage> PayByExactChange(HttpContent content);
        Task<HttpResponseMessage> GetAllARCustomers(int pageIndex);
        Task<HttpResponseMessage> SearchARCustomers(string searchTearm, int pageIndex);
        Task<HttpResponseMessage> SaveARPayment(HttpContent content);
        Task<HttpResponseMessage> SearchARCustomer(HttpContent content);
        Task<HttpResponseMessage> ValidateFleet();
        Task<HttpResponseMessage> FleetPayment(HttpContent content);
    }
}
