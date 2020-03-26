using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockPaymentRestClient : IPaymentRestClient
    {
        public Task<HttpResponseMessage> FleetPayment(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetAllARCustomers(int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PayByExactChange(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveARPayment(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SearchARCustomer(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SearchARCustomers(string searchTearm, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ValidateFleet()
        {
            throw new NotImplementedException();
        }
    }
}
