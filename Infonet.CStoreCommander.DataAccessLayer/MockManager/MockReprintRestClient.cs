using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockReprintRestClient : IReprintRestClient
    {

        public Task<HttpResponseMessage> GetReprintReport(string saleNumber, string saleDate, string reportType)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetReprintReportName()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetReprintSales(string reportType, string date)
        {
            throw new NotImplementedException();
        }
    }
}
