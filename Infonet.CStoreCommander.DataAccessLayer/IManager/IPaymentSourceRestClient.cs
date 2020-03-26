using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IPaymentSourceRestClient
    {
        Task<HttpResponseMessage> GetDownloadedFilesAsync();
        Task<HttpResponseMessage> GetPSLogosAsync();
        Task<HttpResponseMessage> GetPSProductsAsync();
        Task<HttpResponseMessage> GetPSProfileAsync();
        Task<HttpResponseMessage> GetPSTransactionIDAsync();
        Task<HttpResponseMessage> GetPSVoucherInfoAsync(string ProdName);
        Task<HttpResponseMessage> SavePSTransactionIDAsync(int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID);
        Task<HttpResponseMessage> GetPSRefundInfoAsync(string TransactionID, int SALE_NO, int TILL_NUM);
        Task<HttpResponseMessage> GetPSTransactionsAsync(int TILL_NUM, int SALE_NO, int PastDays);
    }
}
