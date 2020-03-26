using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class PaymentSourceBusinessLogic : IPaymentSourceBusinessLogic
    {
        IPaymentSourceSerializeManager _PaymentSourceSerializeManager;
        public PaymentSourceBusinessLogic(IPaymentSourceSerializeManager PaymentSourceSerializeManager)
        {
            _PaymentSourceSerializeManager = PaymentSourceSerializeManager;
        }
        public async Task<bool> DownloadFilesAsync()
        {
            return await _PaymentSourceSerializeManager.DownloadFilesAsync();
        }

        public async Task<List<PSLogo>> GetPSLogosAsync()
        {
            return await _PaymentSourceSerializeManager.GetPSLogosAsync();
        }

        public async Task<List<PSProduct>> GetPSProductsAsync()
        {
            return await _PaymentSourceSerializeManager.GetPSProductsAsync();
        }

        public async Task<PSProfile> GetPSProfileAsync()
        {
            return await _PaymentSourceSerializeManager.GetPSProfileAsync();
        }

        public async Task<PSRefund> GetPSRefundInfoAsync(string TransactionID, int SALE_NO, int TILL_NUM)
        {
            return await _PaymentSourceSerializeManager.GetPSRefundInfoAsync(TransactionID, SALE_NO, TILL_NUM);
        }

        public async Task<string> GetPSTransactionIDAsync()
        {
            return await _PaymentSourceSerializeManager.GetPSTransactionIDAsync();
        }

        public async Task<PSVoucherInfo> GetPSVoucherInfoAsync(string ProdName)
        {
            return await _PaymentSourceSerializeManager.GetPSVoucherInfoAsync(ProdName);
        }

        public async Task<bool> SavePSTransactionIDAsync(int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID)
        {
            return await _PaymentSourceSerializeManager.SavePSTransactionIDAsync(TILL_NUM, SALE_NO, LINE_NUM, TransID);
        }
        public async Task<List<PSTransaction>> GetPSTransactionsAsync(int TILL_NUM, int SALE_NO, int PastDays)
        {
            return await _PaymentSourceSerializeManager.GetPSTransactionsAsync(TILL_NUM, SALE_NO, PastDays);
        }
    }
}
