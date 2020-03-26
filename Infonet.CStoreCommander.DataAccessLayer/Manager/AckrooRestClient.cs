using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class AckrooRestClient : IAckrooRestClient
    {
        private readonly ICacheManager _cacheManager;
        public AckrooRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        public async Task<HttpResponseMessage> GetAckrooStockCode()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetAckrooStockCode, _cacheManager.AuthKey);
        }
        public async Task<HttpResponseMessage> GetCarwashCategories()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetCarwashCategories, _cacheManager.AuthKey);
        }
        public async Task<HttpResponseMessage> GetAckrooCarwashStockCode(string sDesc)
        {
            var client = new HttpRestClient();
            string url = string.Format(Urls.GetAckrooCarwashStockCode, sDesc);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
