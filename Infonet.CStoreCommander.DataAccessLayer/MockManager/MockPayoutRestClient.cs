using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockPayoutRestClient : IPayoutRestClient
    {
        public Task<HttpResponseMessage> GetVendorPayout()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PayoutComplete(HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
