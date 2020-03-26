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
    public class KickBackRestClient : IKickBackRestClient
    {
        private readonly ICacheManager _cacheManager;

        public KickBackRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;

        }

        public async Task<HttpResponseMessage> CheckKickBackbalance(string cardNumber)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.CheckKickBackBalance, _cacheManager.TillNumber,
                _cacheManager.SaleNumber, cardNumber);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CheckKickBackResponse(bool response)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.CheckKickBackResponse, response,
                _cacheManager.TillNumber, _cacheManager.RegisterNumber, _cacheManager.SaleNumber);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ValidateGasKing()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.ValidateGasKing, _cacheManager.TillNumber,
                _cacheManager.SaleNumber, _cacheManager.RegisterNumber);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> VerifyKickBack(string pointCardNumber, string phoneNumber)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.VerifyKickBack, pointCardNumber, phoneNumber, _cacheManager.RegisterNumber,
                _cacheManager.TillNumber, _cacheManager.SaleNumber);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
