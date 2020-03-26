using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    /// <summary>
    /// Business methods for Checkout
    /// </summary>
    public interface ICheckoutBusinessLogic
    {
        /// <summary>
        /// Verifies whether the current sale supports tax exemption
        /// </summary>
        /// <returns>Which tax exemption feature does current sale supports</returns>
        Task<TaxExemptVerification> VerifyTaxExempt();

        /// <summary>
        /// Validates the card number for AITE
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barcode">Bar code</param>
        /// <param name="isCardNumber">Validate through Card number or Bar code</param>
        /// <returns>AiteValidate</returns>
        Task<AiteValidate> ValidateAITE(string cardNumber, string barcode,
            bool isCardNumber);

        /// <summary>
        /// Removes the Site Tax
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        Task<SiteValidate> RemoveSiteTax(string treatyNumber, int captureMethod,
            string treatyName, string documentNumber);

        /// <summary>
        /// Completes the payment via Coupon
        /// </summary>
        /// <param name="number">Coupon Number</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender Summary</returns>
        Task<TenderSummary> PaymentByCoupon(string number, string tenderCode);

        /// <summary>
        /// Validates the Site 
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        Task<SiteValidate> ValidateSite(string treatyNumber, int captureMethod,
            string treatyName, string documentNumber);

        Task<CardSwipeInformation> GetCardInformation(string cardNumber, string transactionType);

        /// <summary>
        /// Validates the QIte Band member
        /// </summary>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        Task<QiteValidate> ValidateQite(string bandMember);

        /// <summary>
        /// Affixes the Bar code
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <returns>True or False</returns>
        Task<bool> AffixBarcode(string cardNumber, string barCode);

        /// <summary>
        /// Performs the GST PST Tax exemption for the treaty number
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <returns></returns>
        Task<AiteValidate> GstPstTaxExempt(string treatyNumber);

        /// <summary>
        /// Gets the Over limit details for the current sale 
        /// </summary>
        /// <returns>Over limit details</returns>
        Task<OverLimitDetails> OverLimitDetails();

        /// <summary>
        /// Completes the over limit for the current sale 
        /// </summary>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns>Sale summary</returns>
        Task<CheckoutSummary> CompleteOverLimit(string reason, string explanation,
            string location, DateTime date);

        /// <summary>
        /// Gets the Override limit details for the current sale
        /// </summary>
        /// <returns></returns>
        Task<OverrideLimitDetails> OverrideLimitDetails();

        /// <summary>
        /// Overrides the override limit details 
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="overrideCode">Override Code</param>
        /// <param name="documentNumber">Document number</param>
        /// <param name="documentDetail">Document detail</param>
        /// <returns></returns>
        Task<bool> OverrideLimitOverride(string item, string overrideCode,
            string documentNumber, string documentDetail);

        /// <summary>
        /// Completes the override limit
        /// </summary>
        /// <returns></returns>
        Task<CheckoutSummary> CompleteOverrideLimit();

        /// <summary>
        /// Gets the sale summary for the current sale
        /// </summary>
        /// <returns></returns>
        Task<CheckoutSummary> SaleSummary(bool isAiteValidated = false, bool isSiteValidated = false, double? kickBackAmount = null);

        /// <summary>
        /// Updates the tenders used in the Sale
        /// </summary>
        /// <param name="code">Code of the Tender</param>
        /// <param name="amount">Amount to be updated</param>
        /// <returns></returns>
        Task<TenderSummary> UpdateTender(string code, decimal? amount, string transactionType, bool isAmountEnteredManually = false);

        /// <summary>
        /// Method to cancel tenders 
        /// </summary>
        /// <param name="TransactionType"></param>
        /// <returns>Tender</returns>
        Task<Sale> CancelTenders(string TransactionType);

        /// <summary>
        /// Runs away the current sale
        /// </summary>
        /// <returns></returns>
        Task<CommonPaymentComplete> RunAway();

        /// <summary>
        /// Completes the Payment of the ongoing sale
        /// </summary>
        /// <returns></returns>
        Task<CompletePayment> CompletePayment(string transactionType, bool issueStoreCredit);

        Task<TenderSummary> PaymentByCard(string tenderCode,
            string amountUsed, string transactionType,
            string cardNumber, string poNumber = "");

        /// <summary>
        /// Complete a transaction by givex
        /// </summary>
        /// <param name="cardNumber">Givex card number</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender summary</returns>
        Task<TenderSummary> PaymentByGivex(string cardNumber, decimal? amount,
            string transactionType, string tenderCode);

        /// <summary>
        /// method to get treaty name
        /// </summary>
        /// <param name="treatyName"></param>
        /// <param name="captureMethod"></param>
        /// <returns></returns>
        Task<string> GetTreatyName(string treatyName, string captureMethod);

        Task<TenderSummary> PaymentByAccount(string purchaseOrder,
            bool overrideARLimit, string transactionType, bool tillClose,
            string tenderCode, decimal? amountEntered);

        Task<VerifyByAccount> VerifyByAccount(string transactionType,
           bool tillClose, string tenderCode, decimal? amountEntered);

        Task<TenderSummary> AddVendorCoupon(string tenderCode, string couponNumber,
           string serialNumber);

        Task<VendorCoupon> GetVendorCoupon(string tenderCode);

        Task<TenderSummary> PaymentByVendorCoupon(string tenderCode);

        Task<TenderSummary> RemoveVendorCoupon(string tenderCode, string couponNumber);

        Task<string> SaveProfilePrompt(string cardNumber, string profileId,
             Dictionary<string, string> prompts);

        Task<CommonPaymentComplete> PumpTest();

        Task<bool> VerifyMultiPO(string purchaseOrder);

        Task<SiteValidate> FNGTR(string phoneNumber);

        Task<bool> SaveSignature(Uri image);
    }
}