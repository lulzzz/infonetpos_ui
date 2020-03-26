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
    public class FuelDiscountRestClient : IFuelDiscountRestClient
    {
        private readonly ICacheManager _cacheManager;
        public FuelDiscountRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetFuelCodesAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetFuelCodes;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetFuelDiscountItemsAsyc()
        {
            var client = new HttpRestClient();
            var url = Urls.GetAllDiscounts;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
