using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    /// <summary>
    /// Interface for Cache Manager
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Clears the Cache
        /// </summary>
        /// <returns></returns>
        Task ClearCacheAsync();

        /// <summary>
        /// Gets or sets the Login Policies
        /// </summary>
        LoginPolicy LoginPolicies { get; set; }

        /// <summary>
        /// Gets or sets the IP Address
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the User name
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the Till number
        /// </summary>
        int TillNumber { get; set; }

        /// <summary>
        /// Gets or sets the Shift number
        /// </summary>
        int ShiftNumber { get; set; }

        string Culture { get; }

        /// <summary>
        /// Gets or sets the Sale number
        /// </summary>
        int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the Cash float
        /// </summary>
        string CashFloat { get; set; }

        /// <summary>
        /// Gets or sets the Authentication token
        /// </summary>
        string AuthKey { get; set; }
        string TrainerCaption { get; set; }

        /// <summary>
        /// Gets or sets the Use Shifts for the day
        /// </summary>
        bool UseShiftForTheDay { get; set; }
        string BaseCurrency { get; }

        /// <summary>
        /// Method to set all policies
        /// </summary>
        /// <param name="policies"></param>
        void SetAllPolicies(Policy policies);

        bool OperatorCanUseCustomer { get; }
        bool OperatorCanUseARCustomer { get; }
        bool ShowCustomerNoteOnOverlimit { get; }
        bool DisplayCustomerDetails { get; }
        string DefaultCustomerCode { get; }
        string TenderNameforARAccount { get; }
        string OperatorCanScanCustomerCard { get; }
        bool OperatorCanSwipeCustomerCard { get; }
        string DefaultMemberCodeForNonMembers { get; }
        bool SupportKitsInPurchase { get; }
        bool AddStockItemNotFoundInList { get; }
        bool ConfirmDeleteLineItem { get; }
        bool UseProductDiscount { get; }
        bool PrintReceiptForVoidAndReturn { get; }
        bool SuspendEmptySales { get; }
        bool ShareSuspendSale { get; }
        bool AllowPayout { get; }
        bool ReasonForPayout { get; }
        bool UseCustomerDiscountCode { get; }
        bool ForceAuthorizationOnVoid { get; }
        bool OperatorCanGiveDiscount { get; }
        bool OperatorCanChangePrice { get; }
        bool OperatorCanChangeQuantity { get; }
        bool OperatorCanVoidSale { get; }
        bool UseReasonForVoid { get; }
        bool OperatorCanSuspendOrUnsuspendSales { get; }
        bool OperatorCanReturnBottle { get; }
        decimal OperatorBottleReturnLimit { get; }
        bool SupportDipInput { get; }
        string DipInputTime { get; }
        bool OperatorCanSwipeMemberCodeAtPump { get; }
        bool PrintWriteOff { get; }
        int PaymentReceiptCopies { get; }
        bool OperatorCanDrawCash { get; }
        int CashDrawReceiptCopies { get; }
        int CashDropReceiptCopies { get; }
        bool OperatorCanDropCash { get; }
        bool UseReasonForCashDrop { get; }
        bool AskForCashDropReason { get; }
        bool RequireEnvelopNumber { get; }
        bool UseReasonForCashDraw { get; }
        bool AllowPOSMinimize { get; }
        int ArpayReceiptCopies { get; }
        bool SupportCashCreditpricing { get; }
        bool SwitchUserOnEachSale { get; }
        bool SupportKickback { get; }
        string CustomKickbackmsg { get; }
        double KickbackRedeemMsg { get; }
        int BottleReturnReceiptCopies { get; }

        bool ForceGiftCertificate { get; }

        bool ForcePrintReceipt { get; }

        bool GiftCertificateNumbered { get; }

        string GiftTender { get; }
        string CouponTender { get; }

        bool OperatorCanOpenCashDrawer { get; }

        bool OperatorIsTrainer { get; }

        int PayoutReceiptCopies { get; }

        int RefundReceiptCopies { get; }

        bool SupportsTaxExampt { get; }

        bool UseReasonForCashDrawer { get; }
        bool CheckSC { get; }
        bool EnableMsgInput { get; }
        bool TaxExemption { get; }

        /// <summary>
        /// gets or sets operatorCanAddStock
        /// </summary>
        bool OperatorCanAddStock { get; }

        bool OperatorCanUseLoyalty { get; }

        /// <summary>
        /// gets or sets customer name
        /// </summary>
        string CustomerName { get; set; }

        /// <summary>
        /// Return mode is set or not
        /// </summary>
        bool IsReturn { get; set; }

        /// <summary>
        /// Register number
        /// </summary>
        byte RegisterNumber { get; set; }

        bool UserCanChangePassword { get; }

        bool RequirePasswordOnChangeUser { get; }

        string ShiftDate { get; set; }

        bool AllowAdjustmentForGiveX { get; }

        string CertificateType { get; }

        int GiftNumber { get; set; }

        string PreviousAuthKey { get; set; }

        bool OperatorCanReturnSale { get; }

        bool IsGiveXCalledFromAddStock { get; set; }

        int TillNumberForSale { get; set; }

        long IdleIntervalAfterAppFreezes { get; }

        bool FreezeTillAutomatically { get; }

        string FramePriorSwitchUserNavigation { get; set; }
        bool EnableWriteOffButton { get; set; }
        int DelayInNewSale { get; }
        double? KickbackAmount { get; set; }
        bool EnableExactChange { get; }

        bool IsCurrentSaleEmpty { get; set; }

        string LastPrintReport { get; set; }

        bool IsPostPayOn { get; set; }

        bool IsPrePayOn { get; set; }

        bool IsPayAtPumpOn { get; set; }
        string KickBackCardNumber { get; set; }
        bool IsFuelOnlySystem { get; }
        bool IsPosOnlySystem { get; }
        bool AllowSwipeScan { get; }
        int FuelPriceNotificationTimeInterval { get; }
        bool SupportFuelPriceFromHO { get; }
        int FuelPriceNotificationCount { get; }

        bool UseReceiptPrinter { get; set; }
        bool UseOposReceiptPrinter { get; set; }
        string ReceiptPrinterName { get; set; }
        string ReceiptPrinterDriver { get; set; }

        int CustomerDisplayPort { get; set; }
        string CustomerDisplayName { get; set; }
        bool UseCustomerDisplay { get; set; }
        bool UseOposCustomerDisplay { get; set; }

        string CashDrawerName { get; set; }
        int CashDrawerOpenCode { get; set; }
        bool UseCashDrawer { get; set; }
        bool UseOposCashDrawer { get; set; }

        bool IsFuelPricingGrouped { get; }
        int PumpSpace { get; }
        bool IsFuelPriceDisplayUsed { get; }
        bool IsPriceIncrementEnabled { get; }
        bool IsTaxExemptionPricesEnabled { get; }
        bool StayOnFuelPricePage { get; }
        bool AreFuelPricesSaved { get; set; }
        bool IsKickBack { get; set; }
        bool RequireToGetCustomerName { get; }
        bool UserCanPerformManualSales { get; }
        bool RequireSignature { get; set; }
        bool IsFleetCardRequired { get; }
        int ClickDelayForPumps { get; }
        bool IsCarwashSupported { get; set; }
        bool IsCarwashIntegrated { get; set; }
        bool HasCarwashProductsInSale { get; set; }
        string TreatyNumber { get; set; }
        string TreatyName { get; set; }
        bool isFuelDiscountSupported { get; set; }  //Done by Tony 07/17/2018

        bool isTDRS_FUELDISCSupported { get; set; }  //Done by Tony 07/17/2018
        string displayCustGrpID { get; set; }   //Done by Tony 07/17/2018
        //For Payment Source. Done by Tony 10/11/2018
        bool SupportPSInet { get; set; }
        string PSINet_Type { get; set; }
        //For Payment Source. ----End
        //For receipt printing style type.  Done by Tony 07/26/2019
        string RECEIPT_TYPE { get; set; }
        //For receipt printing style type. ----End
        string VERSION { get; set; }  //Tony 09/05/2019
        //Done by Tony 05/22/2019
        string FuelDept { get; set; }
        //For Akroo gift cards. Done by Tony 12/189/2018
        string REWARDS_Message { get; set; }
        bool REWARDS_Enabled { get; set; }
        string REWARDS_Gift { get; set; }
        string REWARDS_TpsIp { get; set; }
        int REWARDS_TpsPort { get; set; }
        short REWARDS_Timeout { get; set; }
        string REWARDS_Carwash { get; set; }
        string REWARDS_CWGIFT { get; set; }
        string REWARDS_CWPKG { get; set; }
        bool REWARDS_DefaultLoyal { get; set; }
        //For Akroo gift cards. ----End


    }
}
