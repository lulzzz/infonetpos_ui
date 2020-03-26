using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.PaymentSource;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.PaymentSource;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.PaymentSource;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class PaymentSourceSerializeManager : SerializeManager, IPaymentSourceSerializeManager
    {
        private IPaymentSourceRestClient _ipaymentSourceRestClient;
        public PaymentSourceSerializeManager(IPaymentSourceRestClient paymentSourceRestClient)
        {
            _ipaymentSourceRestClient = paymentSourceRestClient;
        }
        public async Task<bool> DownloadFilesAsync()
        {
            var action = new GetDownloadedFilesSerializeAction(_ipaymentSourceRestClient);
            await PerformTask(action);
           
            return (bool)action.ResponseValue;
        }

        public async Task<List<PSLogo>> GetPSLogosAsync()
        {
            var action = new GetPSLogosSerializeAction(_ipaymentSourceRestClient);
            await PerformTask(action);
            List<PSLogoContract> olist = (List<PSLogoContract>)action.ResponseValue;
            return new Mapper().MapPSLogo(olist);
        }

        public async Task<List<PSProduct>> GetPSProductsAsync()
        {
            var action = new GetPSProductsSerializeAction(_ipaymentSourceRestClient);
            await PerformTask(action);
            List<PSProductContract> olist = (List<PSProductContract>)action.ResponseValue;
            return new Mapper().MapPSProd(olist);
        }

        public async Task<PSProfile> GetPSProfileAsync()
        {
            var action = new GetPSProfileSerializeAction(_ipaymentSourceRestClient);
            await PerformTask(action);
            PSProfileContract pspf = (PSProfileContract)action.ResponseValue;
            return Mapper.MapPSProfile(pspf);
        }

        public async Task<PSRefund> GetPSRefundInfoAsync(string TransactionID, int SALE_NO, int TILL_NUM)
        {
            var action = new GetPSRefundInfoSerialAction(_ipaymentSourceRestClient,TransactionID, SALE_NO, TILL_NUM);
            await PerformTask(action);
            PSRefundContract psrt = (PSRefundContract)action.ResponseValue;
            if (psrt != null)
                return Mapper.MapPSRefund(psrt);
            else
                return null;

        }

        public async Task<string> GetPSTransactionIDAsync()
        {
            var action = new GetPSTransactionIDSerializeAction(_ipaymentSourceRestClient);
            await PerformTask(action);
            string sVal = (string)action.ResponseValue;
            return sVal;
        }

        public async Task<PSVoucherInfo> GetPSVoucherInfoAsync(string ProdName)
        {
            var action = new GetPSVoucherInfoSerializeAction(_ipaymentSourceRestClient,ProdName);
            await PerformTask(action);
            PSVoucherInfoContract psvcinfo =(PSVoucherInfoContract)action.ResponseValue;
            return Mapper.MapPSVoucherInfo(psvcinfo);
        }

        public async Task<bool> SavePSTransactionIDAsync(int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID)
        {
            var action = new SavePSTransactionIDSerializeAction(_ipaymentSourceRestClient, TILL_NUM, SALE_NO, LINE_NUM, TransID);
            await PerformTask(action);
            return (bool)action.ResponseValue;
        }
        public async Task<List<PSTransaction>> GetPSTransactionsAsync(int TILL_NUM, int SALE_NO, int PastDays)
        {
            var action = new GetPSTransactionsSerialAction(_ipaymentSourceRestClient, TILL_NUM, SALE_NO, PastDays);
            await PerformTask(action);
            List<PSTransactionContract> pstcontract = (List<PSTransactionContract>)action.ResponseValue;
            return new Mapper().MapPSTransactions(pstcontract);
        }
    }
}
