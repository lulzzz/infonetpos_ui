using Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IPaymentSourceSerializeManager
    {
       Task<bool> DownloadFilesAsync();
        Task<List<PSLogo>> GetPSLogosAsync();
        Task<List<PSProduct>> GetPSProductsAsync();
        Task<PSProfile> GetPSProfileAsync();
        Task<string> GetPSTransactionIDAsync();
        Task<PSVoucherInfo> GetPSVoucherInfoAsync(string ProdName);
        Task<bool> SavePSTransactionIDAsync(int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID);
        Task<PSRefund> GetPSRefundInfoAsync(string TransactionID, int SALE_NO, int TILL_NUM);
        Task<List<PSTransaction>> GetPSTransactionsAsync(int TILL_NUM, int SALE_NO, int PastDays);
    }
}
