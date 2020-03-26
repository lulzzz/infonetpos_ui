using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    /// <summary>
    /// Rest client for the Checkout Operations
    /// </summary>
    public class CheckoutRestClient : ICheckoutRestClient
    {
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        public CheckoutRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Affixes the bar code
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> AffixBarcode(string cardNumber, string barCode)
        {
            var data = new
            {
                cardNumber,
                barCode
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AffixBarcode, content, _cacheManager.AuthKey);
        }


        /// <summary>
        /// Completes the Over limit for the current sale 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CompleteOverLimit(int saleNumber, int tillNumber,
            string reason, string explanation, string location, DateTime date)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                reason,
                explanation,
                location,
                date
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.CompleteOverLimit, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Calls the API for the Gst Pst tax exempt
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GstPstTaxExempt(int saleNumber,
            int tillNumber, int shiftNumber, byte registerNumber,
            string treatyNumber)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                shiftNumber,
                registerNumber,
                treatyNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GstPstExempt, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the Over limit details for the current sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number for the Current Sale</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> OverLimitDetails(int saleNumber, int tillNumber)
        {
            var url = string.Format(Urls.OverLimitDetails, saleNumber, tillNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the override limit details
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> OverrideLimitDetails(int saleNumber,
            int tillNumber , string treatyNumber, string treatyName)
        {
            var url = string.Format(Urls.OverrideLimitDetails, saleNumber, tillNumber ,treatyNumber , treatyName);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Removes the SITE Tax 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number for the sale</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture Method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="permitNumber">Document number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RemoveSiteTax(int saleNumber,
            int tillNumber, byte registerNumber, string treatyNumber,
            int captureMethod, string treatyName, string permitNumber)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                registerNumber,
                treatyNumber,
                captureMethod,
                treatyName,
                permitNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.RemoveSiteTax, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the Sale summary
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="registerNumber">Register number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SaleSummary(HttpContent content, double? kickBackAmount)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.SaleSummary, kickBackAmount);
            return await client.PostAsync(url, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Updates the tender amount for the sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="code">Tender code</param>
        /// <param name="amount">Tender amount</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UpdateTender(int saleNumber,
            int tillNumber, string code, decimal? amount, string transactionType, bool isAmountEnteredManually)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                transactionType = transactionType,
                tillClose = false,
                tender = new
                {
                    tenderCode = code,
                    amountEntered = amount
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(string.Format(Urls.UpdateTender, isAmountEnteredManually), 
                content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Calls the AITE Validation API 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till for the sale</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <param name="checkMode">Check mode</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ValidateAITE(int saleNumber,
            int tillNumber, int shiftNumber, byte registerNumber,
            string cardNumber, string barCode, int checkMode)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                shiftNumber,
                cardNumber,
                barCode,
                checkMode
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ValidateAITE, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Validates the QITE tax exemption
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ValidateQite(int saleNumber,
            int tillNumber, int shiftNumber, byte registerNumber,
            string bandMember)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                shiftNumber,
                registerNumber,
                bandMember
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ValidateQite, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Validates the Site
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number for the sale</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture Method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="permitNumber">Document number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ValidateSite(int saleNumber,
            int tillNumber, byte registerNumber,
            string treatyNumber, int captureMethod, string treatyName,
            string permitNumber)
        {
            var data = new
            {
                saleNumber,
                tillNumber,
                registerNumber,
                treatyNumber,
                captureMethod,
                treatyName,
                permitNumber
            };
            var content = new StringContent(JsonConvert.SerializeObject(data)
                , Encoding.UTF8, "application/json");
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ValidateSite, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Calls the Tax exempt API for the current sale and brings the response
        /// </summary>
        /// <returns>HTTP Response from the API</returns>
        public async Task<HttpResponseMessage> VerifyTaxExempt(int saleNumber,
            int tillNumber, int registerNumber)
        {
            var url = string.Format(Urls.VerifyTaxExempt, saleNumber, tillNumber, registerNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CancelTenders(string transactionType)
        {
            var url = string.Format(Urls.CancelTender,
                  _cacheManager.SaleNumber, _cacheManager.TillNumber, transactionType);
            var client = new HttpRestClient();
            return await client.DeleteAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> PaymentByCard(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PaymentByCard, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> RunAway(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.RunAway, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CompletePayment(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.CompletePayment, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Overrides the over limit details
        /// </summary>
        /// <param name="content">HTTP Content</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> OverrideLimitOverride(StringContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.OverrideLimitOverride, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Completes the override limit 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="registerNumber">Register number</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CompleteOverrideLimit(int saleNumber,
            int tillNumber, byte registerNumber)
        {
            var url = string.Format(Urls.CompleteOverrideLimit, saleNumber, tillNumber, registerNumber);
            var client = new HttpRestClient();
            return await client.PostAsync(url, null, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Complete a transaction by coupon
        /// </summary>
        /// <param name="content">Content</param>
        public async Task<HttpResponseMessage> PaymentByCoupon(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PaymentByCoupon, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Complete a transaction by GiveX
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PaymentByGivex(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PaymentByGivex, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Get card information and tender type for a swiped card
        /// </summary>
        /// <param name="content">content</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetCardInformation(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GetCardInformation, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to get treaty Name
        /// </summary>
        /// <param name="treatyNumber"></param>
        /// <param name="captureMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetTreatyName(string treatyNumber, string captureMethod)
        {
            var url = string.Format(Urls.GetTreatyName, treatyNumber, captureMethod);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> VerifyByAccount(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.VerifyByAccount, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> VerifyMultiPO(string purchaseOrder)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.VerfifyMultiPO, _cacheManager.SaleNumber, _cacheManager.TillNumber, purchaseOrder);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> PaymentByAccount(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PaymentByAccount, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetVendorCoupon(string vendorCode)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.GetVendorCoupon, _cacheManager.SaleNumber, _cacheManager.TillNumber, vendorCode);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> AddVendorCoupon(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddVendorCoupon, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> RemoveVendorCoupon(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.RemoveVendorCoupon, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> PaymentByVendorCoupon(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PaymentVendorCoupon, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveProfilePrompt(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SaveProfilePrompt, content, _cacheManager.AuthKey);

        }

        public async Task<HttpResponseMessage> PumpTest(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PumpTest, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> FNGTR(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.FNGTR, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveSignature(HttpContent content)
        {
            var url = string.Format(Urls.SaveSignature, _cacheManager.SaleNumber, _cacheManager.TillNumber);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("authToken", _cacheManager.AuthKey);
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(content, "fileToUpload");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "fileToUpload",
                FileName = "Signature.jpg"
            };
            form.Add(content);
            var response = await client.PostAsync(url, form);
            return response;
        }
    }
}
