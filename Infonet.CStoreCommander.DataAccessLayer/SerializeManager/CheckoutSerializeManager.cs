using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    /// <summary>
    /// Serialize manager for Checkout operations
    /// </summary>
    public class CheckoutSerializeManager : SerializeManager,
        ICheckoutSerializeManager
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="restClient">Checkout Rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public CheckoutSerializeManager(ICheckoutRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Verifies the tax exemption scenarios for the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<TaxExemptVerification> VerifyTaxExempt()
        {
            var action = new TaxExemptVerificationSerializeAction(_restClient,
                _cacheManager);

            await PerformTask(action);

            return (TaxExemptVerification)action.ResponseValue;
        }

        /// <summary>
        /// Validates the card number for AITE
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barcode">Bar code</param>
        /// <param name="isCardNumber">Validate through Card number or Bar code</param>
        /// <returns>AiteValidate</returns>
        public async Task<AiteValidate> ValidateAITE(string cardNumber,
            string barcode, bool isCardNumber)
        {
            var action = new ValidateAITESerializeAction(_restClient,
                _cacheManager, cardNumber, barcode, isCardNumber);

            await PerformTask(action);

            return (AiteValidate)action.ResponseValue;
        }

        /// <summary>
        /// Affixes the Bar code
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <returns>True or False</returns>
        public async Task<bool> AffixBarcode(string cardNumber, string barCode)
        {
            var action = new AffixBarcodeSerializeAction(_restClient,
                _cacheManager, cardNumber, barCode);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        /// <summary>
        /// Removes the Site Tax
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        public async Task<SiteValidate> RemoveSiteTax(string treatyNumber,
            int captureMethod, string treatyName, string documentNumber)
        {
            var action = new RemoveSiteTaxSerializeAction(_restClient,
                _cacheManager, treatyNumber, captureMethod, treatyName,
                documentNumber);

            await PerformTask(action);

            return (SiteValidate)action.ResponseValue;
        }

        /// <summary>
        /// Validates the Site
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        public async Task<SiteValidate> ValidateSite(string treatyNumber, int captureMethod, string treatyName, string documentNumber)
        {
            var action = new ValidateSiteSerializeAction(_restClient,
                _cacheManager, treatyNumber, captureMethod, treatyName,
                documentNumber);

            await PerformTask(action);

            return (SiteValidate)action.ResponseValue;
        }

        /// <summary>
        /// Validates the QIte Band member
        /// </summary>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        public async Task<QiteValidate> ValidateQite(string bandMember)
        {
            var action = new ValidateQiteSerializeAction(_restClient,
               _cacheManager, bandMember);

            await PerformTask(action);

            return (QiteValidate)action.ResponseValue;
        }

        /// <summary>
        /// Performs the GST PST Tax exemption for the treaty number
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <returns></returns>
        public async Task<AiteValidate> GstPstTaxExempt(string treatyNumber)
        {
            var action = new GstPstTaxExemptSerializeAction(_restClient,
               _cacheManager, treatyNumber);

            await PerformTask(action);

            return (AiteValidate)action.ResponseValue;
        }

        /// <summary>
        /// Gets the Over limit details for the current sale 
        /// </summary>
        /// <returns>Over limit details</returns>
        public async Task<OverLimitDetails> OverLimitDetails()
        {
            var action = new OverLimitDetailsSerializeAction(_restClient,
               _cacheManager);

            await PerformTask(action);

            return (OverLimitDetails)action.ResponseValue;
        }

        /// <summary>
        /// Completes the overlimit for the current sale 
        /// </summary>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns>Sale summary</returns>
        public async Task<CheckoutSummary> CompleteOverLimit(string reason,
            string explanation, string location, DateTime date)
        {
            var action = new CompleteOverLimitsSerializeAction(_restClient,
               _cacheManager, reason, explanation, location, date);

            await PerformTask(action);

            return (CheckoutSummary)action.ResponseValue;
        }

        /// <summary>
        /// Gets the Override limit details for the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<OverrideLimitDetails> OverrideLimitDetails()
        {
            var action = new OverrideLimitDetailsSerializeAction(_restClient,
              _cacheManager);

            await PerformTask(action);

            return (OverrideLimitDetails)action.ResponseValue;
        }

        /// <summary>
        /// Gets the sale summary for the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<CheckoutSummary> SaleSummary(bool isAiteValidated,
            bool isSiteValidated, double? kickBackAmount)
        {
            var action = new SaleSummarySerializeAction(_restClient,
              _cacheManager, isAiteValidated, isSiteValidated, kickBackAmount);

            await PerformTask(action);

            return (CheckoutSummary)action.ResponseValue;
        }

        /// <summary>
        /// Updates the tenders used in the Sale
        /// </summary>
        /// <param name="code">Code of the Tender</param>
        /// <param name="amount">Amount to be updated</param>
        /// <returns></returns>
        public async Task<TenderSummary> UpdateTender(string code, decimal? amount,
            string transactionType, bool isAmountEnteredManually)
        {
            var action = new UpdateTenderSerializeAction(_restClient,
              _cacheManager, code, amount, transactionType, isAmountEnteredManually);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        /// <summary>
        /// Method to cancel tenders 
        /// </summary>
        /// <param name="TransactionType"></param>
        /// <returns>Tender</returns>
        public async Task<Sale> CancelTenders(string transactionType)
        {
            var action = new CancelTenderSerializeAction(_restClient,
             _cacheManager, transactionType);

            await PerformTask(action);

            return (Sale)action.ResponseValue;
        }

        /// <summary>
        /// Runs away the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<CommonPaymentComplete> RunAway()
        {
            var action = new RunAwaySerializeAction(_restClient,
               _cacheManager);

            await PerformTask(action);

            return (CommonPaymentComplete)action.ResponseValue;
        }

        /// <summary>
        /// Completes the payment of the ongoing sale
        /// </summary>
        /// <returns></returns>
        public async Task<CompletePayment> CompletePayment(string transactionType, bool issueStoreCredit)
        {
            var action = new CompletePaymentSerializeAction(_restClient,
             _cacheManager, transactionType, issueStoreCredit);

            await PerformTask(action);

            return (CompletePayment)action.ResponseValue;
        }

        public async Task<TenderSummary> PaymentByCard(string tenderCode,
            string amountUsed, string transactionType, string cardNumber, string poNumber = "")
        {
            var action = new PaymentByCardSerializeAction(_restClient,
              _cacheManager, tenderCode, amountUsed,
               transactionType, cardNumber, poNumber);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        /// <summary>
        /// Overrides the override limit details 
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="overrideCode">Override Code</param>
        /// <param name="documentNumber">Document number</param>
        /// <param name="documentDetail">Document detail</param>
        /// <returns></returns>
        public async Task<bool> OverrideLimitOverride(string item,
            string overrideCode, string documentNumber, string documentDetail)
        {
            var action = new OverrideLimitOverrideSerializeAction(_restClient,
              _cacheManager, item, overrideCode, documentNumber, documentDetail);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        /// <summary>
        /// Completes the override limit
        /// </summary>
        /// <returns></returns>
        public async Task<CheckoutSummary> CompleteOverrideLimit()
        {
            var action = new CompleteOverrideLimitSerializeAction(_restClient,
              _cacheManager);

            await PerformTask(action);

            return (CheckoutSummary)action.ResponseValue;
        }

        /// <summary>
        /// Complete a transaction by givex
        /// </summary>
        /// <param name="cardNumber">Givex card number</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender summary</returns>
        public async Task<TenderSummary> PaymentByGivex(string cardNumber, decimal? amount, string transactionType, string tenderCode)
        {
            var action = new PaymentByGivexSerializeAction(_restClient,
             _cacheManager, cardNumber, amount, transactionType, tenderCode);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<CardSwipeInformation> GetCardInformation(string cardNumber, string transactionType)
        {
            var action = new CardSwipeSerializeAction(_restClient,
             _cacheManager, cardNumber, transactionType);

            await PerformTask(action);

            return (CardSwipeInformation)action.ResponseValue;
        }

        /// <summary>
        /// Completes the payment via Coupon
        /// </summary>
        /// <param name="number">Coupon Number</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender Summary</returns>
        public async Task<TenderSummary> PaymentByCoupon(string number,
            string tenderCode)
        {
            var action = new PaymentByCouponSerializeAction(_restClient,
            _cacheManager, number, tenderCode);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<string> GetTreayName(string treatyNumber, string captureMethod)
        {
            var action = new GetTreatyNameSerializeAction(_restClient,
            treatyNumber, captureMethod);

            await PerformTask(action);

            return action.ResponseValue.ToString();
        }

        public async Task<VerifyByAccount> VerifyByAccount(string transactionType,
            bool tillClose, string tenderCode, decimal? amountEntered)
        {
            var action = new VerifyByAccountSerializeAction(_restClient,
                _cacheManager, transactionType, tillClose, tenderCode,
                amountEntered);

            await PerformTask(action);

            return (VerifyByAccount)action.ResponseValue;
        }

        public async Task<Success> VerifyMultiPO(string purchaseOrder)
        {
            var action = new VerifyMultiPOSerializeAction(_restClient, purchaseOrder);

            await PerformTask(action);

            return (Success)action.ResponseValue;
        }

        public async Task<TenderSummary> PaymentByAccount(string purchaseOrder,
            bool overrideARLimit, string transactionType, bool tillClose,
            string tenderCode, decimal? amountEntered)
        {
            var action = new PaymentByAccountSerializeAction(_restClient,
             _cacheManager, purchaseOrder, overrideARLimit, transactionType, tillClose,
             tenderCode, amountEntered);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<TenderSummary> AddVendorCoupon(string tenderCode, string couponNumber,
            string serialNumber)
        {
            var action = new AddVendorCouponSerializeAction(_restClient,
              _cacheManager, tenderCode, couponNumber, serialNumber);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<VendorCoupon> GetVendorCoupon(string tenderCode)
        {
            var action = new GetVendorCouponSerializeAction(_restClient, tenderCode);

            await PerformTask(action);

            return (VendorCoupon)action.ResponseValue;
        }

        public async Task<TenderSummary> PaymentByVendorCoupon(string tenderCode)
        {
            var action = new PaymentByVendorCouponSerializeAction(_restClient,
              _cacheManager, tenderCode);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<TenderSummary> RemoveVendorCoupon(string tenderCode, string couponNumber)
        {
            var action = new RemoveVendorCouponSerializeAction(_restClient,
             _cacheManager, tenderCode, couponNumber);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<string> SaveProfilePrompt(string cardNumber,
            string profileId, Dictionary<string, string> prompts)
        {
            var action = new SaveProfilePromptSerializeAction(_restClient,
              _cacheManager, cardNumber, profileId, prompts);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }

        public async Task<CommonPaymentComplete> PaymentbyPumpTest()
        {
            var action = new PaymentByPumpTestSerializeAction(_restClient,
               _cacheManager);

            await PerformTask(action);

            return (CommonPaymentComplete)action.ResponseValue;
        }

        public async Task<SiteValidate> FNGTR(string phoneNumber)
        {
            var action = new FNGTRSerializeAction(_restClient,
               _cacheManager, phoneNumber);

            await PerformTask(action);

            return (SiteValidate)action.ResponseValue;
        }

        public async Task<bool> SaveSignature(Uri image)
        {
            var action = new SaveSignatureSerializeAction(_restClient,
                  _cacheManager, image);

            await PerformTask(action);

            return (bool)action.ResponseValue;            
        }
    }
}

