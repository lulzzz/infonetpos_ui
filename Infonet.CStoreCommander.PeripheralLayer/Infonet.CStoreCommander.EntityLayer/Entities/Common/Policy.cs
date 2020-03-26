namespace Infonet.CStoreCommander.EntityLayer.Entities.Common
{
    public class Policy
    {
        public bool OperatorCanUseCustomer { get; set; }
        public bool OperatorCanUseARCustomer { get; set; }
        public bool ShowCustomerNoteOnOverlimit { get; set; }
        public bool DisplayCustomerDetails { get; set; }
        public string DefaultCustomerCode { get; set; }
        public string TenderNameforARAccount { get; set; }
        public bool OperatorCanScanCustomerCard { get; set; }
        public bool OperatorCanSwipeCustomerCard { get; set; }
        public bool OperatorCanSwipeMemberCodeAtPump { get; set; }
        public string DefaultMemberCodeForNonMembers { get; set; }
        public bool SupportKitsInPurchase { get; set; }
        public bool AddStockItemNotFoundInList { get; set; }
        public bool ConfirmDeleteLineItem { get; set; }
        public bool UseProductDiscount { get; set; }
        public bool PrintReceiptForVoidAndReturn { get; set; }
        public bool SuspendEmptySales { get; set; }
        public bool ShareSuspendSale { get; set; }
        public bool AllowPayout { get; set; }
        public bool ReasonForPayout { get; set; }
        public bool UseCustomerDiscountCode { get; set; }
        public bool ForceAuthorizationOnVoid { get; set; }
        public bool OperatorCanGiveDiscount { get; set; }
        public bool OperatorCanChangePrice { get; set; }
        public bool OperatorCanChangeQuantity { get; set; }
        public bool OperatorCanVoidSale { get; set; }
        public bool UseReasonForVoid { get; set; }
        public bool OperatorCanSuspendOrUnsuspendSales { get; set; }
        public bool OperatorCanReturnBottle { get; set; }
        public double OperatorBottleReturnLimit { get; set; }
        public bool SupportDipInput { get; set; }
        public string DipInputTime { get; set; }
        public bool AllowPOSMinimize { get; set; }
        public bool UserCanChangePassword { get; set; }
        public bool OperatorCanAddStock { get; set; }
        public bool OperatorCanUseLoyalty { get; set; }
        public string CertificateType { get; set; }
        public bool AllowAdjustmentForGiveX { get; set; }
        public bool RequirePasswordOnChangeUser { get; set; }
        public bool OperatorCanReturnSale { get; set; }
        public bool SupportsTaxExampt { get; set; }
        public bool FreezeTillAutomatically { get; set; }
        public int IdleIntervalAfterAppFreezes { get; set; }
        public bool OperatorCanDrawCash { get; set; }
        public bool OperatorCanDropCash { get; set; }
        public bool UseReasonForCashDraw { get; set; }
        public int RefundReceiptCopies { get; set; }
        public int ArpayReceiptCopies { get; set; }
        public int PayoutReceiptCopies { get; set; }
        public int PaymentReceiptCopies { get; set; }
        public int BottleReturnReceiptCopies { get; set; }
        public int CashDropReceiptCopies { get; set; }
        public int CashDrawReceiptCopies { get; set; }
        public bool OperatorIsTrainer { get; set; }
        public bool AskForCashDropReason { get; set; }
        public bool RequireEnvelopNumber { get; set; }
        public bool OperatorCanOpenCashDrawer { get; set; }
        public bool UseReasonForCashDrawer { get; set; }
        public string GiftTender { get; set; }
        public bool ForceGiftCertificate { get; set; }
        public bool GiftCertificateNumbered { get; set; }
        public bool ForcePrintReceipt { get; set; }
        public int DelayInNewSale { get; set; }
        public bool EnableExactChange { get; set; }
        public string BaseCurrency { get; set; }
        public bool CheckSC { get; set; }
        public bool EnableMsgInput { get; set; }
        public string CouponTender { get; set; }
        public bool IsFuelOnlySystem { get; set; }
        public bool IsPosOnlySystem { get; set; }
        public bool IsPostPayOn { get; set; }
        public bool IsPrePayOn { get; set; }
        public bool IsPayAtPumpOn { get; set; }
        public bool AllowSwipeScan { get; set; }
        public int FuelPriceNotificationTimeInterval { get; set; }
        public bool SupportFuelPriceFromHO { get; set; }
        public int FuelPriceNotificationCount { get; set; }
        public bool SupportCashCreditpricing { get; set; }
        public bool TaxExemption { get; set; }
        public bool IsFuelPricingGrouped { get; set; }
        public int PumpSpace { get; set; }
        public bool IsFuelPriceDisplayUsed { get; set; }
        public bool IsPriceIncrementEnabled { get; set; }
        public bool IsTaxExemptionPricesEnabled { get; set; }
        public bool SwitchUserOnEachSale { get; set; }
        public bool StayOnFuelPricePage { get; set; }
        public bool RequireToGetCustomerName { get; set; }
        public bool UserCanPerformManualSales { get; set; }
        public bool RequireSignature { get; set; }
        public bool IsFleetCardRequired { get; set; }
        public int ClickDelayForPumps { get; set; }
        public bool SupportKickback { get; set; }
        public double KickbackRedeemMsg { get; set; }
        public string CustomKickbackmsg { get; set; }
        public bool IsCarwashSupported { get; set; }
        public bool IsCarwashIntegrated { get; set; }
        public string FuelDept { get; set; }  // done by Tony 05/22/2019
        public bool isFuelDiscountSupported { get; set; } //done by Tony 07/17/2018
        public bool isTDRS_FUELDISCSupported { get; set; } //done by Tony 07/17/2018
        public string displayCustGrpID { get; set; } //done by Tony 07/17/2018
        //For payment source. Done by Tony 08/10/2018
        public bool SupportPSInet { get; set; }

        public string PSINet_Type { get; set; }
        //For payment source. ----End
        //For Akroo gift cards. Done by Tony 12/19/2018
        public string REWARDS_Message { get; set; }
        public bool REWARDS_Enabled { get; set; }
        public string REWARDS_Gift { get; set; }
        public string REWARDS_TpsIp { get; set; }
        public int REWARDS_TpsPort { get; set; }
        public short REWARDS_Timeout { get; set; }
        public string REWARDS_Carwash { get; set; }
        public string REWARDS_CWGIFT { get; set; }
        public string REWARDS_CWPKG { get; set; }
        public bool REWARDS_DefaultLoyal { get; set; }
        //For Akroo gift cards. ----End
    }
}
