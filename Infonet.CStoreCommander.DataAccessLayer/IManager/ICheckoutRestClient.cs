using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    /// <summary>
    /// Rest client for the Checkout Operations
    /// </summary>
    public interface ICheckoutRestClient
    {
        /// <summary>
        /// Calls the Tax exempt API for the current sale and brings the response
        /// </summary>
        /// <returns>HTTP Response from the API</returns>
        Task<HttpResponseMessage> VerifyTaxExempt(int saleNumber,
            int tillNumber, int registerNumber);

        /// <summary>
        /// Affixes the bar code
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <returns></returns>
        Task<HttpResponseMessage> AffixBarcode(string cardNumber,
            string barCode);

        /// <summary>
        /// Completes the override limit 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="registerNumber">Register number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> CompleteOverrideLimit(int saleNumber,
            int tillNumber, byte registerNumber);

        /// <summary>
        /// Updates the tender amount for the sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="code">Tender code</param>
        /// <param name="amount">Tender amount</param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateTender(int saleNumber, int tillNumber,
            string code, decimal? amount, string transactionType, bool isAmountEnteredManually);

        /// <summary>
        /// Gets the Sale summary
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="registerNumber">Register number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> SaleSummary(HttpContent content, double? kickBackAmount);

        /// <summary>
        /// Overrides the over limit details
        /// </summary>
        /// <param name="content">HTTP Content</param>
        /// <returns></returns>
        Task<HttpResponseMessage> OverrideLimitOverride(StringContent content);

        /// <summary>
        /// Gets the override limit details
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> OverrideLimitDetails(int saleNumber,
            int tillNumberForSale, string treatyNumber, string treatyName);

        /// <summary>
        /// Gets the Over limit details for the current sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number for the Current Sale</param>
        /// <returns></returns>
        Task<HttpResponseMessage> OverLimitDetails(int saleNumber, int tillNumber);

        /// <summary>
        /// Completes the overlimit for the current sale 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns></returns>
        Task<HttpResponseMessage> CompleteOverLimit(int saleNumber, int tillNumber,
            string reason, string explanation, string location, DateTime date);

        /// <summary>
        /// Calls the API for the Gst Pst tax exempt
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till number</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GstPstTaxExempt(int saleNumber, int tillNumberForSale,
            int shiftNumber, byte registerNumber, string treatyNumber);

        /// <summary>
        /// Validates the Qite tax exemption
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till number</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        Task<HttpResponseMessage> ValidateQite(int saleNumber, int tillNumberForSale,
            int shiftNumber, byte registerNumber, string bandMember);

        /// <summary>
        /// Validates the Site
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till number for the sale</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture Method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> ValidateSite(int saleNumber, int tillNumberForSale,
            byte registerNumber, string treatyNumber, int captureMethod,
            string treatyName, string documentNumber);

        /// <summary>
        /// Removes the SITE Tax 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till number for the sale</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture Method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document number</param>
        /// <returns></returns>
        Task<HttpResponseMessage> RemoveSiteTax(int saleNumber, int tillNumberForSale,
            byte registerNumber, string treatyNumber, int captureMethod,
            string treatyName, string documentNumber);

        /// <summary>
        /// Calls the AITE Validation API 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumberForSale">Till for the sale</param>
        /// <param name="shiftNumber">Shift number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <param name="checkMode">Check mode</param>
        /// <returns></returns>
        Task<HttpResponseMessage> ValidateAITE(int saleNumber,
            int tillNumberForSale, int shiftNumber, byte registerNumber,
            string cardNumber, string barCode, int checkMode);

        Task<HttpResponseMessage> RunAway(HttpContent content);

        Task<HttpResponseMessage> CompletePayment(HttpContent content);

        Task<HttpResponseMessage> PaymentByCard(HttpContent content);

        /// <summary>
        /// Cancel tenders and navigate to cancel tenders page
        /// </summary>
        /// <param name="transactionType">transaction type</param>
        /// <returns></returns>
        Task<HttpResponseMessage> CancelTenders(string transactionType);

        /// <summary>
        /// Paymnet by coupon
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PaymentByCoupon(HttpContent content);

        /// <summary>
        /// Payment by coupon
        /// </summary>
        /// <param name="content">content</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PaymentByGivex(HttpContent content);

        /// <summary>
        /// Get card information and tender type for a  swiped card
        /// </summary>
        /// <param name="content">content</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetCardInformation(HttpContent content);

        Task<HttpResponseMessage> GetTreatyName(string treatyNumber, string captureMethod);

        Task<HttpResponseMessage> VerifyByAccount(HttpContent content);

        Task<HttpResponseMessage> PaymentByAccount(HttpContent content);

        Task<HttpResponseMessage> GetVendorCoupon(string vendorCode);

        Task<HttpResponseMessage> AddVendorCoupon(HttpContent content);

        Task<HttpResponseMessage> RemoveVendorCoupon(HttpContent content);

        Task<HttpResponseMessage> PaymentByVendorCoupon(HttpContent content);

        Task<HttpResponseMessage> SaveProfilePrompt(HttpContent content);

        Task<HttpResponseMessage> PumpTest(HttpContent content);

        Task<HttpResponseMessage> FNGTR(HttpContent content);

        Task<HttpResponseMessage> VerifyMultiPO(string purchaseOrder);

        Task<HttpResponseMessage> SaveSignature(HttpContent content);
    }
}
