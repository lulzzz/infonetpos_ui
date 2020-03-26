using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockCheckoutRestClient : ICheckoutRestClient
    {
        public Task<HttpResponseMessage> AddVendorCoupon(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AffixBarcode(string cardNumber, string barCode)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CancelTenders(string transactionType)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CompleteOverLimit(int saleNumber, int tillNumber, string reason, string explanation, string location, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CompleteOverrideLimit(int saleNumber, int tillNumber, byte registerNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CompletePayment(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> FNGTR(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetCardInformation(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetTreatyName(string treatyNumber, string captureMethod)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetVendorCoupon(string vendorCode)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GstPstTaxExempt(int saleNumber, int tillNumberForSale, int shiftNumber, byte registerNumber, string treatyNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> OverLimitDetails(int saleNumber, int tillNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> OverrideLimitDetails(int saleNumber, int tillNumberForSale , string treatyNumber, string treatyName)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> OverrideLimitOverride(StringContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PaymentByAccount(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PaymentByCard(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PaymentByCoupon(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PaymentByGivex(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PaymentByVendorCoupon(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PumpTest(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> RemoveSiteTax(int saleNumber, int tillNumberForSale, byte registerNumber, string treatyNumber, int captureMethod, string treatyName, string documentNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> RemoveVendorCoupon(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> RunAway(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaleSummary(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaleSummary(HttpContent content, double? kickBackAmount)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveProfilePrompt(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveSignature(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateTender(int saleNumber, int tillNumber, string code, decimal? amount, string transactionType, bool isAmountEnteredManually)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ValidateAITE(int saleNumber, int tillNumberForSale, int shiftNumber, byte registerNumber, string cardNumber, string barCode, int checkMode)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ValidateQite(int saleNumber, int tillNumberForSale, int shiftNumber, byte registerNumber, string bandMember)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ValidateSite(int saleNumber, int tillNumberForSale, byte registerNumber, string treatyNumber, int captureMethod, string treatyName, string documentNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> VerifyByAccount(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> VerifyMultiPO(string purchaseOrder)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> VerifyTaxExempt(int saleNumber, int tillNumber, int registerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
