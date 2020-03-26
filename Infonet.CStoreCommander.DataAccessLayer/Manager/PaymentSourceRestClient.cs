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
    public class PaymentSourceRestClient : IPaymentSourceRestClient
    {
        private readonly ICacheManager _cacheManager;
        public PaymentSourceRestClient(ICacheManager CacheManager)
        {
            _cacheManager = CacheManager;
        }
        public async Task<HttpResponseMessage> GetDownloadedFilesAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetDownloadedFiles;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPSLogosAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetPSLogos;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPSProductsAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetPSProducts;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPSProfileAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetPSProfile;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPSRefundInfoAsync(string TransactionID, int SALE_NO, int TILL_NUM)
        {
            var client = new HttpRestClient();
            
            var url = string.Format(Urls.GetPSRefundInfo, TransactionID, SALE_NO, TILL_NUM);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPSTransactionIDAsync()
        {
            var client = new HttpRestClient();
            var url = Urls.GetPSTransactionID;
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        

        public async Task<HttpResponseMessage> GetPSVoucherInfoAsync(string ProdName)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.GetPSVoucherInfo, ProdName);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
        public async Task<HttpResponseMessage> GetPSTransactionsAsync(int TILL_NUM, int SALE_NO, int PastDays)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.GetPSTransactions, TILL_NUM, SALE_NO, PastDays);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
        public async Task<HttpResponseMessage> SavePSTransactionIDAsync(int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.SavePSTransactionID, TILL_NUM, SALE_NO, LINE_NUM, TransID);
            return await client.GetAsync(url, _cacheManager.AuthKey);

        }
    }
}
