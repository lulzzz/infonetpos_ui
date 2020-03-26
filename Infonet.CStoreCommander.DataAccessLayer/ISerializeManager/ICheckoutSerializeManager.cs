using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    /// <summary>
    /// Serialize manager for Checkout operations
    /// </summary>
    public interface ICheckoutSerializeManager
    {
        /// <summary>
        /// Verifies the tax exemption scenarios for the current sale
        /// </summary>
        /// <returns></returns>
        Task<TaxExemptVerification> VerifyTaxExempt();

        /// <summary>
        /// Validates the card number for AITE
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barcode">Bar code</param>
        /// <param name="isCardNumber">Validate through Card number or Bar code</param>
        /// <returns>AiteValidate</returns>
        Task<AiteValidate> ValidateAITE(string cardNumber,
            string barcode, bool isCardNumber);

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
        /// Completes the overlimit for the current sale 
        /// </summary>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns>Sale summary</returns>
        Task<CheckoutSummary> CompleteOverLimit(string reason,
            string explanation, string location, DateTime date);

        /// <summary>
        /// Removes the Site Tax
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        Task<SiteValidate> RemoveSiteTax(string treatyNumber,
            int captureMethod, string treatyName, string documentNumber);

        /// <summary>
        /// Gets the Override limit details for the current sale
        /// </summary>
        /// <returns></returns>
        Task<OverrideLimitDetails> OverrideLimitDetails();

        /// <summary>
        /// Validates the Site
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        Task<SiteValidate> ValidateSite(string treatyNumber, int captureMethod, string treatyName, string documentNumber);

        /// <summary>
        /// Validates the QIte Band member
        /// </summary>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        Task<QiteValidate> ValidateQite(string bandMember);

        /// <summary>
        /// Gets the sale summary for the current sale
        /// </summary>
        /// <returns></returns>
        Task<CheckoutSummary> SaleSummary(bool isAiteValidated, bool isSiteValidated, double? kickBackAmount);

        /// <summary>
        /// Updates the tenders used in the Sale
        /// </summary>
        /// <param name="code">Code of the Tender</param>
        /// <param name="amount">Amount to be updated</param>
        /// <returns></returns>
        Task<TenderSummary> UpdateTender(string code, decimal? amount, string transactionType, bool isAmountEnteredManually);

        Task<Sale> CancelTenders(string TransactionType);

        /// <summary>
        /// Runs away the current sale
        /// </summary>
        /// <returns></returns>
        Task<CommonPaymentComplete> RunAway();

        /// <summary>
        /// Completes the payment of the ongoing sale
        /// </summary>
        /// <returns></returns>
        Task<CompletePayment> CompletePayment(string transactionType, bool issueStoreCredit);

        Task<TenderSummary> PaymentByCard(string tenderCode,
            string amountUsed, string transactionType,
            string cardNumber, string poNumber);

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
        /// Complete a transaction by givex
        /// </summary>
        /// <param name="cardNumber">Givex card number</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender summaty</returns>
        Task<TenderSummary> PaymentByGivex(string cardNumber, decimal? amount,
            string transactionType, string tenderCode);

        Task<CardSwipeInformation> GetCardInformation(string cardNumber, string transactionType);

        /// <summary>
        /// Completes the payment via Coupon
        /// </summary>
        /// <param name="number">Coupon Number</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender Summary</returns>
        Task<TenderSummary> PaymentByCoupon(string number, string tenderCode);

        Task<string> GetTreayName(string treatyNumber, string captureMethod);

        Task<VerifyByAccount> VerifyByAccount(string transactionType, bool tillClose,
            string tenderCode, decimal? amountEntered);

        Task<TenderSummary> PaymentByAccount(string purchaseOrder, bool overrideARLimit, string transactionType,
            bool tillClose, string tenderCode, decimal? amountEntered);

        Task<TenderSummary> AddVendorCoupon(string tenderCode, string couponNumber,
            string serialNumber);

        Task<VendorCoupon> GetVendorCoupon(string tenderCode);

        Task<TenderSummary> PaymentByVendorCoupon(string tenderCode);

        Task<TenderSummary> RemoveVendorCoupon(string tenderCode, string couponNumber);

        Task<string> SaveProfilePrompt(string cardNumber, string profileId,
            Dictionary<string, string> prompts);

        Task<CommonPaymentComplete> PaymentbyPumpTest();

        Task<SiteValidate> FNGTR(string phoneNumber);

        Task<Success> VerifyMultiPO(string purchaseOrder);

        Task<bool> SaveSignature(Uri image);
    }
}
