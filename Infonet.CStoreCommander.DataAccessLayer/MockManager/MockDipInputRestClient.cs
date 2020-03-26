using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockDipInputRestClient : IDipInputRestClient
    {
        public Task<HttpResponseMessage> GetDipInput()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetDipInputPrint()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveDipInput(HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
