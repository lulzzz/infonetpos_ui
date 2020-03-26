using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    /// <summary>
    /// Interface for Cache Manager
    /// </summary>
    public interface ICacheBusinessLogic
    {
        /// <summary>
        /// Clears the System cache
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Gets and sets the Login policies
        /// </summary>
        LoginPolicy LoginPolicies { get; set; }

        /// <summary>
        /// Gets and sets the Ip Address
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// Gets and sets the Username
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets and sets the Password
        /// </summary>
        string Password { get; set; }

        string ShiftDate { get; set; }

        /// <summary>
        /// Gets and sets the Till number
        /// </summary>
        int TillNumber { get; set; }

        /// <summary>
        /// Gets and sets the Shift number
        /// </summary>
        int ShiftNumber { get; set; }

        /// <summary>
        /// Current culture
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// Gets and sets the Sale number
        /// </summary>
        int SaleNumber { get; set; }

        /// <summary>
        /// Gets and sets the Cash float
        /// </summary>
        string CashFloat { get; set; }

        /// <summary>
        /// Gets and sets the Authentication key
        /// </summary>
        string AuthKey { get; set; }
        string BaseCurrency { get; }
        string TrainerCaption { get; set; }
        /// <summary>
        /// Gets the Previous User Authentication key
        /// </summary>
        string PreviousAuthKey { get; set; }

        /// <summary>
        /// Gets and sets the Use Shifts for the day
        /// </summary>
        bool UseShiftForTheDay { get; set; }

        /// <summary>
        /// Sets all policies
        /// </summary>
        void SetAllPolicies(Policy policy);

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
        DateTime DipInputTime { get; }
        bool OperatorCanSwipeMemberCodeAtPump { get; }
        bool OperatorCanAddStock { get; }
        bool OperatorCanUseLoyalty { get; }
        bool UserCanChangePassword { get; }
        bool RequirePasswordOnChangeUser { get; }
        bool AllowAdjustmentForGiveX { get; }
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
        int BottleReturnReceiptCopies { get; }
        bool ForceGiftCertificate { get; }
        bool ForcePrintReceipt { get; }
        bool GiftCertificateNumbered { get; }
        string GiftTender { get; }
        bool OperatorCanOpenCashDrawer { get; }
        bool OperatorIsTrainer { get; }
        int PayoutReceiptCopies { get; }
        int RefundReceiptCopies { get; }
        bool SupportsTaxExampt { get; }
        bool UseReasonForCashDrawer { get; }
        bool EnableExactChange { get; }
        bool CheckSC { get; }
        bool EnableMsgInput { get; }
        int DelayInNewSale { get; }
        string CouponTender { get; }
        bool AllowSwipeScan { get; }
        bool SupportCashCreditpricing { get; }
        bool TaxExemption { get; }
        int PumpSpace { get; }
        bool SwitchUserOnEachSale { get; }
        bool StayOnFuelPricePage { get; }
        string CustomerName { get; set; }
        bool IsReturn { get; set; }
        byte RegisterNumber { get; set; }
        string CertificateType { get; }
        int GiftNumber { get; set; }
        bool SupportKickback { get; }
        double KickbackRedeemMsg { get; }
        bool OperatorCanReturnSale { get; }

        bool IsGiveXCalledFromAddStock { get; set; }

        int TillNumberForSale { get; set; }

        long IdleIntervalAfterAppFreezes { get; }

        bool FreezeTillAutomatically { get; }
        string CustomKickbackmsg { get; }
        string FramePriorSwitchUserNavigation { get; set; }
        bool EnableWriteOffButton { get; set; }

        bool IsCurrentSaleEmpty { get; set; }

        string LastPrintReport { get; set; }

        bool IsPostPayOn { get; set; }

        bool IsPrePayOn { get; set; }

        bool IsPayAtPumpOn { get; set; }
        bool IsFuelOnlySystem { get; }
        bool IsPosOnlySystem { get; }

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
        bool IsFuelPriceDisplayUsed { get; }
        bool IsPriceIncrementEnabled { get; }
        bool IsTaxExemptionPricesEnabled { get; }
        bool AreFuelPricesSaved { get; set; }

        bool RequireToGetCustomerName { get; }
        bool UserCanPerformManualSales { get; }
        bool RequireSignature { get; set; }
        bool IsKickBack { get; set; }
        bool IsFleetCardRequired { get; }
        int ClickDelayForPumps { get; }

        double? KickbackAmount { get; set; }

        string KickBackCardNumber { get; set; }

        bool IsCarwashIntegrated { get; set; }

        bool IsCarwashSupported { get; set; }

        bool HasCarwashProductInSale { get; set; }

        string TreatyNumber { get; set; }

        string TreatyName { get; set; }

        //For Fue discount. Done by Tony 08/20/2018
        bool isFuelDiscountSupported { get; set; }

        bool isTDRS_FUELDISCSupported { get; set; }

        string displayCustGrpID { get; set; }
        //For Fue discount. ----End
        //For Payment Source. Done by Tony 10/10/2018
        bool SupportPSInet { get; set; }
        string PSINet_Type { get; set; }
        //For Payment Source. ----End
        //Done by Tony 05/22/2019
        //For receipt printing style type.  Done by Tony 07/26/2019
        string RECEIPT_TYPE { get; set; }
        //For receipt printing style type. ----End
        string VERSION { get; set; }  //Tony 09/05/2019
        string FuelDept { get; set; }
        //For Akroo gift cards. Done by Tony 12/19/2018
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
