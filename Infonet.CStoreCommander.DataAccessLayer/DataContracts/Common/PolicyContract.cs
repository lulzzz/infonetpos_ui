namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common
{
    public class PolicyContract
    {
        public bool operatorCanUseCustomer { get; set; }
        public bool operatorCanUseARCustomer { get; set; }
        public bool showCustomerNoteOnOverlimit { get; set; }
        public bool displayCustomerDetails { get; set; }
        public string defaultCustomerCode { get; set; }
        public string tenderNameforARAccount { get; set; }
        public bool operatorCanScanCustomerCard { get; set; }
        public bool operatorCanSwipeCustomerCard { get; set; }
        public bool operatorCanSwipeMemberCodeAtPump { get; set; }
        public string defaultMemberCodeForNonMembers { get; set; }
        public bool supportKitsInPurchase { get; set; }
        public bool addStockItemNotFoundInList { get; set; }
        public bool confirmDeleteLineItem { get; set; }
        public bool useProductDiscount { get; set; }
        public bool printReceiptForVoidAndReturn { get; set; }
        public bool suspendEmptySales { get; set; }
        public bool shareSuspendSale { get; set; }
        public bool allowPayout { get; set; }
        public bool reasonForPayout { get; set; }
        public bool useCustomerDiscountCode { get; set; }
        public bool forceAuthorizationOnVoid { get; set; }
        public bool operatorCanGiveDiscount { get; set; }
        public bool operatorCanChangePrice { get; set; }
        public bool operatorCanChangeQantity { get; set; }
        public bool operatorCanVoidSale { get; set; }
        public bool useReasonForVoid { get; set; }
        public bool operatorCanSuspendOrUnsuspendSales { get; set; }
        public bool operatorCanReturnBottle { get; set; }
        public double operatorBottleReturnLimit { get; set; }
        public bool supportDipInput { get; set; }
        public string dipInputTime { get; set; }
        public bool allowPOSMinimize { get; set; }
        public bool userCanChangePassword { get; set; }
        public bool operatorCanAddStock { get; set; }
        public bool operatorCanUseLoyalty { get; set; }
        public string certificateType { get; set; }
        public bool allowAdjustmentForGiveX { get; set; }
        public bool requirePasswordOnChangeUser { get; set; }
        public bool operatorCanReturnSale { get; set; }
        public bool supportsTaxExampt { get; set; }
        public bool freezeTillAutomatically { get; set; }
        public int idleIntervalAfterAppFreezes { get; set; }
        public bool operatorCanDrawCash { get; set; }
        public bool operatorCanDropCash { get; set; }
        public bool useReasonForCashDraw { get; set; }
        public int refundReceiptCopies { get; set; }
        public int arpayReceiptCopies { get; set; }
        public int payoutReceiptCopies { get; set; }
        public int paymentReceiptCopies { get; set; }
        public int bottleReturnReceiptCopies { get; set; }
        public int cashDropReceiptCopies { get; set; }
        public int cashDrawReceiptCopies { get; set; }
        public bool operatorIsTrainer { get; set; }
        public bool askForCashDropReason { get; set; }
        public bool requireEnvelopNumber { get; set; }
        public bool operatorCanOpenCashDrawer { get; set; }
        public bool useReasonForCashDrawer { get; set; }
        public string giftTender { get; set; }
        public bool forceGiftCertificate { get; set; }
        public bool giftCertificateNumbered { get; set; }
        public bool forcePrintReceipt { get; set; }
        public int delayInNewSale { get; set; }
        public bool enableExactChange { get; set; }
        public string baseCurrency { get; set; }
        public bool checkSC { get; set; }
        public bool enableMsgInput { get; set; }
        public string couponTender { get; set; }
        public bool isFuelOnlySystem { get; set; }
        public bool isPosOnlySystem { get; set; }
        public bool postPayEnabled { get; set; }
        public bool prepayEnabled { get; set; }
        public bool payAtPumpEnabled { get; set; }
        public bool allowSwipeScan { get; set; }
        public int fuelPriceNotificationTimeInterval { get; set; }
        public bool supportFuelPriceFromHO { get; set; }
        public int fuelPriceNotificationCount { get; set; }
        public bool supportCashCreditpricing { get; set; }
        public bool taxExemption { get; set; }
        public bool isFuelPricingGrouped { get; set; }
        public bool isFuelPriceDisplayUsed { get; set; }
        public bool isPriceIncrementEnabled { get; set; }
        public bool isTaxExemptionPricesEnabled { get; set; }
        public int pumpSpace { get; set; }
        public bool switchUserOnEachSale { get; set; }
        public bool stayOnFuelPricePage { get; set; }
        public bool requireToGetCustomerName { get; set; }
        public bool userCanPerformManualSales { get; set; }
        public bool requireSignature { get; set; }
        public bool isFleetCardRequired { get; set; }
        public int clickDelayForPumps { get; set; }
        public string customKickbackmsg { get; set; }
        public bool supportKickback { get; set; }
        public double kickbackRedeemMsg { get; set; }
        public bool isCarwashSupported { get; set; }
        public bool isCarwashIntegrated { get; set; }

        public bool isFuelDiscountSupported { get; set; } //done by Tony 07/17/2018
        public bool isTDRS_FUELDISCSupported { get; set; } //done by Tony 07/17/2018

        public string displayCustGrpID { get; set; }  //done by Tony 07/17/2018

        public bool supportPSInet { get; set; } //done by Tony 10/11/2018
        public string psiNet_Type { get; set; } //done by Tony 10/11/2018
        public string receipT_TYPE { get; set; } //done by Tony 07/29/2019
        public string version { get; set; }  //Tony 09/05/2019
        public string fuelDept { get; set; }
        //For Akroo gift cards. Done by Tony 12/19/2018
        public bool rewardS_Enabled { get; set; }
        public string rewardS_Gift { get; set; }
        public string rewardS_TpsIp { get; set; }
        public int rewardS_TpsPort { get; set; }
        public short rewardS_Timeout { get; set; }
        public string rewardS_Carwash { get; set; }
        public string rewardS_CWGIFT { get; set; }
        public string rewardS_CWPKG { get; set; }
        public bool rewardS_DefaultLoyal { get; set; }
        public string rewardS_Message { get; set; }
        //For Akroo gift cards. ----End
    }
}