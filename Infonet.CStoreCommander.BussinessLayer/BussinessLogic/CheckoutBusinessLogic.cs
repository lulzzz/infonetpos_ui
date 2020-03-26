using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business methods for Checkout
    /// </summary>
    public class CheckoutBusinessLogic : ICheckoutBusinessLogic
    {
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly ICheckoutSerializeManager _serializeManager;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager"></param>
        public CheckoutBusinessLogic(ICheckoutSerializeManager serializeManager,
            IReportsBussinessLogic reportsBusinessLogic,
            ICacheBusinessLogic cacheBusinessLogic)
        {
            _reportsBusinessLogic = reportsBusinessLogic;
            _serializeManager = serializeManager;
            _cacheBusinessLogic = cacheBusinessLogic;
        }

        /// <summary>
        /// Affixes the Bar code
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <param name="barCode">Bar code</param>
        /// <returns>True or False</returns>
        public async Task<bool> AffixBarcode(string cardNumber, string barCode)
        {
            return await _serializeManager.AffixBarcode(cardNumber,
                barCode);
        }

        /// <summary>
        /// Completes the over limit for the current sale 
        /// </summary>
        /// <param name="reason">Reason</param>
        /// <param name="explanation">Explanation</param>
        /// <param name="location">Location</param>
        /// <param name="date">Date</param>
        /// <returns>Sale summary</returns>
        public async Task<CheckoutSummary> CompleteOverLimit(string reason,
            string explanation, string location, DateTime date)
        {
            return await _serializeManager.CompleteOverLimit(reason,
                explanation, location, date);
        }

        /// <summary>
        /// Performs the GST PST Tax exemption for the treaty number
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <returns></returns>
        public async Task<AiteValidate> GstPstTaxExempt(string treatyNumber)
        {
            return await _serializeManager.GstPstTaxExempt(treatyNumber);
        }

        /// <summary>
        /// Gets the Over limit details for the current sale 
        /// </summary>
        /// <returns>Over limit details</returns>
        public async Task<OverLimitDetails> OverLimitDetails()
        {
            return await _serializeManager.OverLimitDetails();
        }

        /// <summary>
        /// Gets the Override limit details for the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<OverrideLimitDetails> OverrideLimitDetails()
        {
            return await _serializeManager.OverrideLimitDetails();
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
            return await _serializeManager.RemoveSiteTax(treatyNumber,
                captureMethod, treatyName, documentNumber);
        }

        /// <summary>
        /// Gets the sale summary for the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<CheckoutSummary> SaleSummary(bool isAiteValidated = false,
            bool isSiteValidated = false, double? kickBackAmount = null)
        {    
            var response = await _serializeManager.SaleSummary(isAiteValidated, 
                isSiteValidated, _cacheBusinessLogic.KickbackAmount);
            _cacheBusinessLogic.KickbackAmount = null;
            return response;
        }

        /// <summary>
        /// Updates the tenders used in the Sale
        /// </summary>
        /// <param name="code">Code of the Tender</param>
        /// <param name="amount">Amount to be updated</param>
        /// <returns></returns>
        public async Task<TenderSummary> UpdateTender(string code, decimal? amount,
            string transactionType, bool isAmountEnteredManually = false)
        {
            // null is to be send if amount value if 0
            //if (amount.HasValue)
            //{
            //    amount = null;
            //}
            var response = await _serializeManager.UpdateTender(code, amount, transactionType,isAmountEnteredManually);

            foreach (var report in response.Report)
            {
                await _reportsBusinessLogic.SaveReport(report);
            }

            return response;
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
            return await _serializeManager.ValidateAITE(cardNumber,
                barcode, isCardNumber);
        }

        /// <summary>
        /// Validates the QIte Band member
        /// </summary>
        /// <param name="bandMember">Band member</param>
        /// <returns></returns>
        public async Task<QiteValidate> ValidateQite(string bandMember)
        {
            return await _serializeManager.ValidateQite(bandMember);
        }

        /// <summary>
        /// Validates the Site 
        /// </summary>
        /// <param name="treatyNumber">Treaty number</param>
        /// <param name="captureMethod">Capture method</param>
        /// <param name="treatyName">Treaty name</param>
        /// <param name="documentNumber">Document Number</param>
        /// <returns></returns>
        public async Task<SiteValidate> ValidateSite(string treatyNumber,
            int captureMethod, string treatyName, string documentNumber)
        {
            return await _serializeManager.ValidateSite(treatyNumber,
                captureMethod, treatyName, documentNumber);
        }

        /// <summary>
        /// Verifies whether the current sale supports tax exemption
        /// </summary>
        /// <returns>Which tax exemption feature does current sale supports</returns>
        public async Task<TaxExemptVerification> VerifyTaxExempt()
        {
            return await _serializeManager.VerifyTaxExempt();
        }

        /// <summary>
        /// Runs away the current sale
        /// </summary>
        /// <returns></returns>
        public async Task<CommonPaymentComplete> RunAway()
        {
            var paymentComplete = await _serializeManager.RunAway();

            await _reportsBusinessLogic.SaveReport(paymentComplete.Receipt);
            return paymentComplete;
        }

        public async Task<CompletePayment> CompletePayment(string transactionType,
            bool issueStoreCredit)
        {
            var paymentComplete = await _serializeManager.CompletePayment(transactionType,
                issueStoreCredit);

            _cacheBusinessLogic.RequireSignature = false;
            foreach (var report in paymentComplete.Receipts)
            {
                await _reportsBusinessLogic.SaveReport(report);
            }

            return paymentComplete;
        }

        public async Task<TenderSummary> PaymentByCard(string tenderCode,
            string amountUsed, string transactionType,
            string cardNumber, string poNumber = "")
        {
            return await _serializeManager.PaymentByCard(tenderCode,
             amountUsed, transactionType, cardNumber, poNumber);
        }

        /// <summary>
        /// Complete a transaction by GiveX
        /// </summary>
        /// <param name="cardNumber">GiveX card number</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender summary</returns>
        public async Task<TenderSummary> PaymentByGivex(string cardNumber, decimal? amount,
            string transactionType, string tenderCode)
        {
            // null is to be send if amount value if 0
            if (amount.HasValue && amount.Value == 0M)
            {
                amount = null;
            }

            var response = await _serializeManager.PaymentByGivex(cardNumber, amount,
                transactionType, tenderCode);

            await _reportsBusinessLogic.SaveReport(response.Report.FirstOrDefault());

            return response;
        }

        /// <summary>
        /// Cancel tenders
        /// </summary>
        /// <param name="TransactionType">Transaction type</param>
        /// <returns></returns>
        public async Task<Sale> CancelTenders(string TransactionType)
        {
            var result = await _serializeManager.CancelTenders(TransactionType);
            _cacheBusinessLogic.RequireSignature = false;
            return result;
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
            return await _serializeManager.OverrideLimitOverride(item,
                overrideCode, documentNumber, documentDetail);
        }

        /// <summary>
        /// Completes the override limit
        /// </summary>
        /// <returns></returns>
        public async Task<CheckoutSummary> CompleteOverrideLimit()
        {
            return await _serializeManager.CompleteOverrideLimit();
        }

        public async Task<CardSwipeInformation> GetCardInformation(string cardNumber, string transactionType)
        {
            return await _serializeManager.GetCardInformation(cardNumber, transactionType);
        }

        /// <summary>
        /// Completes the payment via Coupon
        /// </summary>
        /// <param name="number">Coupon Number</param>
        /// <param name="tenderCode">Tender code</param>
        /// <returns>Tender Summary</returns>
        public async Task<TenderSummary> PaymentByCoupon(string number, string tenderCode)
        {
            return await _serializeManager.PaymentByCoupon(number, tenderCode);
        }

        /// <summary>
        /// method to get treaty name 
        /// </summary>
        /// <param name="treatyName"></param>
        /// <param name="captureMethod"></param>
        /// <returns></returns>
        public async Task<string> GetTreatyName(string treatyName, string captureMethod)
        {
            return await _serializeManager.GetTreayName(treatyName, captureMethod);
        }

        public async Task<TenderSummary> PaymentByAccount(string purchaseOrder,
            bool overrideARLimit, string transactionType, bool tillClose,
            string tenderCode, decimal? amountEntered)
        {
            return await _serializeManager.PaymentByAccount(purchaseOrder,
                overrideARLimit, transactionType, tillClose, tenderCode,
                amountEntered);
        }

        public async Task<VerifyByAccount> VerifyByAccount(string transactionType,
            bool tillClose, string tenderCode, decimal? amountEntered)
        {
            return await _serializeManager.VerifyByAccount(transactionType,
                tillClose, tenderCode, amountEntered);
        }

        public async Task<TenderSummary> AddVendorCoupon(string tenderCode, string couponNumber,
            string serialNumber)
        {
            return await _serializeManager.AddVendorCoupon(tenderCode, couponNumber,
                serialNumber);
        }

        public async Task<VendorCoupon> GetVendorCoupon(string tenderCode)
        {
            return await _serializeManager.GetVendorCoupon(tenderCode);
        }

        public async Task<TenderSummary> PaymentByVendorCoupon(string tenderCode)
        {
            return await _serializeManager.PaymentByVendorCoupon(tenderCode);
        }

        public async Task<TenderSummary> RemoveVendorCoupon(string tenderCode, string couponNumber)
        {
            return await _serializeManager.RemoveVendorCoupon(tenderCode, couponNumber);
        }

        public async Task<string> SaveProfilePrompt(string cardNumber, string profileId,
            Dictionary<string, string> prompts)
        {
            return await _serializeManager.SaveProfilePrompt(cardNumber, profileId,
                prompts);
        }

        public async Task<CommonPaymentComplete> PumpTest()
        {
            var response = await _serializeManager.PaymentbyPumpTest();

            await _reportsBusinessLogic.SaveReport(response.Receipt);

            return response;

        }

        public async Task<SiteValidate> FNGTR(string phoneNumber)
        {
            return await _serializeManager.FNGTR(phoneNumber);
        }

        public async Task<bool> VerifyMultiPO(string purchaseOrder)
        {
            var response = await _serializeManager.VerifyMultiPO(purchaseOrder);
            return response.IsSuccess;
        }

        public async Task<bool> SaveSignature(Uri image)
        {
            var success = await _serializeManager.SaveSignature(image);
            return success;
        }
    }
}
