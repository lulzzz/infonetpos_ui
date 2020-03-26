using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Class containing properties for Caching operations
    /// </summary>
    public class CacheBusinessLogic : ICacheBusinessLogic
    {
        private readonly ICacheManager _cacheManager;
        private bool _hasCarwashProductsInSale;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        public CacheBusinessLogic(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public LoginPolicy LoginPolicies
        {
            get { return _cacheManager.LoginPolicies; }
            set { _cacheManager.LoginPolicies = value; }
        }

        public string IpAddress
        {
            get { return _cacheManager.IpAddress; }
            set { _cacheManager.IpAddress = value; }
        }

        public string UserName
        {
            get { return _cacheManager.UserName; }
            set { _cacheManager.UserName = value; }
        }

        public string Password
        {
            get { return _cacheManager.Password; }
            set { _cacheManager.Password = value; }
        }

        public string ShiftDate
        {
            get { return _cacheManager.ShiftDate; }
            set { _cacheManager.ShiftDate = value; }
        }

        public int TillNumber
        {
            get { return _cacheManager.TillNumber; }
            set { _cacheManager.TillNumber = value; }
        }

        public int ShiftNumber
        {
            get { return _cacheManager.ShiftNumber; }
            set { _cacheManager.ShiftNumber = value; }
        }

        public string Culture => _cacheManager.LoginPolicies.PosLanguage == "English" ? "en-US" : "fr-FR";

        public int SaleNumber
        {
            get
            {
                try
                {
                    return _cacheManager.SaleNumber;
                }
                catch (NullReferenceException)
                {
                    return 0;
                }
            }
            set { _cacheManager.SaleNumber = value; }
        }

        public string CashFloat
        {
            get { return _cacheManager.CashFloat; }
            set { _cacheManager.CashFloat = value; }
        }

        public string AuthKey
        {
            get { return _cacheManager.AuthKey; }
            set
            {
                _cacheManager.AuthKey = value;
            }
        }

        public bool UseShiftForTheDay
        {
            get { return _cacheManager.UseShiftForTheDay; }
            set { _cacheManager.UseShiftForTheDay = value; }
        }

        public bool OperatorCanUseCustomer => _cacheManager.OperatorCanUseCustomer;

        public bool OperatorCanUseARCustomer => _cacheManager.OperatorCanUseARCustomer;

        public bool ShowCustomerNoteOnOverlimit => _cacheManager.ShowCustomerNoteOnOverlimit;

        public bool DisplayCustomerDetails => _cacheManager.DisplayCustomerDetails;

        public string DefaultCustomerCode => _cacheManager.DefaultCustomerCode;

        public string TenderNameforARAccount => _cacheManager.TenderNameforARAccount;

        public string OperatorCanScanCustomerCard => _cacheManager.OperatorCanScanCustomerCard;

        public bool OperatorCanSwipeCustomerCard => _cacheManager.OperatorCanSwipeCustomerCard;

        public string DefaultMemberCodeForNonMembers => _cacheManager.DefaultMemberCodeForNonMembers;

        public bool SupportKitsInPurchase => _cacheManager.SupportKitsInPurchase;

        public bool AddStockItemNotFoundInList => _cacheManager.AddStockItemNotFoundInList;

        public bool ConfirmDeleteLineItem => _cacheManager.ConfirmDeleteLineItem;

        public bool UseProductDiscount => _cacheManager.UseProductDiscount;

        public bool PrintReceiptForVoidAndReturn => _cacheManager.PrintReceiptForVoidAndReturn;

        public bool SuspendEmptySales => _cacheManager.SuspendEmptySales;

        public bool ShareSuspendSale => _cacheManager.ShareSuspendSale;

        public bool AllowPayout => _cacheManager.AllowPayout;

        public bool ReasonForPayout => _cacheManager.ReasonForPayout;

        public bool UseCustomerDiscountCode => _cacheManager.UseCustomerDiscountCode;

        public bool ForceAuthorizationOnVoid => _cacheManager.ForceAuthorizationOnVoid;

        public bool OperatorCanGiveDiscount => _cacheManager.OperatorCanGiveDiscount;

        public bool OperatorCanChangePrice => _cacheManager.OperatorCanChangePrice;

        public bool OperatorCanChangeQuantity => _cacheManager.OperatorCanChangeQuantity;

        public bool OperatorCanVoidSale => _cacheManager.OperatorCanVoidSale;

        public bool UseReasonForVoid => _cacheManager.UseReasonForVoid;

        public bool OperatorCanSuspendOrUnsuspendSales => _cacheManager.OperatorCanSuspendOrUnsuspendSales;

        public bool OperatorCanReturnBottle => _cacheManager.OperatorCanReturnBottle;

        public decimal OperatorBottleReturnLimit => _cacheManager.OperatorBottleReturnLimit;

        public bool SupportDipInput => _cacheManager.SupportDipInput;

        public DateTime DipInputTime => Convert.ToDateTime(_cacheManager.DipInputTime);

        public bool OperatorCanSwipeMemberCodeAtPump => _cacheManager.OperatorCanSwipeMemberCodeAtPump;

        public int DelayInNewSale => _cacheManager.DelayInNewSale;

        public bool EnableExactChange => _cacheManager.EnableExactChange;
        /// <summary>
        /// gets or sets customer name 
        /// </summary>
        public string CustomerName
        {
            get { return _cacheManager.CustomerName; }
            set { _cacheManager.CustomerName = value; }
        }

        public bool IsReturn
        {
            get { return _cacheManager.IsReturn; }
            set { _cacheManager.IsReturn = value; }
        }

        public byte RegisterNumber
        {
            get { return _cacheManager.RegisterNumber; }
            set { _cacheManager.RegisterNumber = value; }
        }

        public bool OperatorCanAddStock => _cacheManager.OperatorCanAddStock;

        public bool OperatorCanUseLoyalty => _cacheManager.OperatorCanUseLoyalty;

        public bool UserCanChangePassword => _cacheManager.UserCanChangePassword;

        public bool RequirePasswordOnChangeUser => _cacheManager.RequirePasswordOnChangeUser;

        public bool AllowAdjustmentForGiveX => _cacheManager.AllowAdjustmentForGiveX;
        public bool CheckSC => _cacheManager.CheckSC;

        public string CertificateType => _cacheManager.CertificateType;

        public int GiftNumber
        {
            get { return _cacheManager.GiftNumber; }
            set { _cacheManager.GiftNumber = value; }
        }

        public string PreviousAuthKey
        {
            get
            {
                return _cacheManager.PreviousAuthKey;
            }
            set { _cacheManager.PreviousAuthKey = value; }
        }


        public bool OperatorCanReturnSale => _cacheManager.OperatorCanReturnSale;

        public bool IsGiveXCalledFromAddStock
        {
            get
            {
                try
                {
                    return _cacheManager.IsGiveXCalledFromAddStock;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            set { _cacheManager.IsGiveXCalledFromAddStock = value; }
        }

        public int TillNumberForSale
        {
            get { return _cacheManager.TillNumberForSale; }
            set { _cacheManager.TillNumberForSale = value; }
        }

        public string BaseCurrency
        {
            get
            {
                return _cacheManager.BaseCurrency;
            }
        }

        public long IdleIntervalAfterAppFreezes => _cacheManager.IdleIntervalAfterAppFreezes;

        public bool PrintWriteOff => _cacheManager.PrintWriteOff;

        public bool FreezeTillAutomatically => _cacheManager.FreezeTillAutomatically;

        public string FramePriorSwitchUserNavigation
        {
            get { return _cacheManager.FramePriorSwitchUserNavigation; }
            set { _cacheManager.FramePriorSwitchUserNavigation = value; }
        }

        public int PaymentReceiptCopies => _cacheManager.PaymentReceiptCopies;
        public bool SupportKickback => _cacheManager.SupportKickback;
        public double KickbackRedeemMsg => _cacheManager.KickbackRedeemMsg;

        public bool OperatorCanDrawCash => _cacheManager.OperatorCanDrawCash;

        public int CashDrawReceiptCopies => _cacheManager.CashDrawReceiptCopies;

        public int CashDropReceiptCopies => _cacheManager.CashDropReceiptCopies;

        public bool OperatorCanDropCash => _cacheManager.OperatorCanDropCash;

        public bool UseReasonForCashDrop => _cacheManager.UseReasonForCashDrop;
        public bool AskForCashDropReason => _cacheManager.AskForCashDropReason;
        public bool RequireEnvelopNumber => _cacheManager.RequireEnvelopNumber;

        public bool UseReasonForCashDraw => _cacheManager.UseReasonForCashDraw;

        public bool AllowPOSMinimize => _cacheManager.AllowPOSMinimize;

        public int ArpayReceiptCopies => _cacheManager.ArpayReceiptCopies;

        public int BottleReturnReceiptCopies => _cacheManager.BottleReturnReceiptCopies;

        public bool ForceGiftCertificate => _cacheManager.ForceGiftCertificate;

        public bool ForcePrintReceipt => _cacheManager.ForcePrintReceipt;

        public bool GiftCertificateNumbered => _cacheManager.GiftCertificateNumbered;

        public string GiftTender => _cacheManager.GiftTender;

        public bool OperatorCanOpenCashDrawer => _cacheManager.OperatorCanOpenCashDrawer;

        public bool OperatorIsTrainer => _cacheManager.OperatorIsTrainer;

        public int PayoutReceiptCopies => _cacheManager.PayoutReceiptCopies;

        public int RefundReceiptCopies => _cacheManager.RefundReceiptCopies;

        public bool SupportsTaxExampt => _cacheManager.SupportsTaxExampt;

        public bool UseReasonForCashDrawer => _cacheManager.UseReasonForCashDrawer;
        public bool IsFuelOnlySystem => _cacheManager.IsFuelOnlySystem;
        public bool IsPosOnlySystem => _cacheManager.IsPosOnlySystem;
        public bool EnableMsgInput => _cacheManager.EnableMsgInput;
        public string CouponTender => _cacheManager.CouponTender;

        public bool AllowSwipeScan => _cacheManager.AllowSwipeScan;

        public bool EnableWriteOffButton
        {
            get { return _cacheManager.EnableWriteOffButton; }
            set { _cacheManager.EnableWriteOffButton = value; }
        }

        public bool IsCurrentSaleEmpty
        {
            get
            {
                try
                {
                    return _cacheManager.IsCurrentSaleEmpty;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
            set { _cacheManager.IsCurrentSaleEmpty = value; }
        }

        public string LastPrintReport
        {
            get
            {
                return _cacheManager.LastPrintReport == null ? string.Empty :
                _cacheManager.LastPrintReport;
            }
            set { _cacheManager.LastPrintReport = value; }
        }

        public bool IsPostPayOn
        {
            get
            {
                return _cacheManager.IsPostPayOn;
            }

            set
            {
                _cacheManager.IsPostPayOn = value;
            }
        }

        public bool IsPrePayOn
        {
            get
            {
                return _cacheManager.IsPrePayOn;
            }

            set
            {
                _cacheManager.IsPrePayOn = value;
            }
        }

        public bool IsPayAtPumpOn
        {
            get
            {
                return _cacheManager.IsPayAtPumpOn;
            }

            set
            {
                _cacheManager.IsPayAtPumpOn = value;
            }
        }

        public int FuelPriceNotificationTimeInterval => _cacheManager.FuelPriceNotificationTimeInterval;

        public bool SupportFuelPriceFromHO => _cacheManager.SupportFuelPriceFromHO;

        public int FuelPriceNotificationCount => _cacheManager.FuelPriceNotificationCount;

        public bool SupportCashCreditpricing => _cacheManager.SupportCashCreditpricing;

        public bool TaxExemption => _cacheManager.TaxExemption;

        public string TrainerCaption
        {
            get
            {
                return _cacheManager.TrainerCaption;
            }
            set
            {
                _cacheManager.TrainerCaption = value;
            }
        }

        public bool UseReceiptPrinter
        {
            get
            {
                return _cacheManager.UseReceiptPrinter;
            }

            set
            {
                _cacheManager.UseReceiptPrinter = value;
            }
        }

        public bool UseOposReceiptPrinter
        {
            get
            {
                return _cacheManager.UseOposReceiptPrinter;
            }

            set
            {
                _cacheManager.UseOposReceiptPrinter = value;
            }
        }

        public string ReceiptPrinterName
        {
            get
            {
                return _cacheManager.ReceiptPrinterName;
            }

            set
            {
                _cacheManager.ReceiptPrinterName = value;
            }
        }

        public string ReceiptPrinterDriver
        {
            get
            {
                return _cacheManager.ReceiptPrinterDriver;
            }

            set
            {
                _cacheManager.ReceiptPrinterDriver = value;
            }
        }

        public int CustomerDisplayPort
        {
            get
            {
                return _cacheManager.CustomerDisplayPort;
            }

            set
            {
                _cacheManager.CustomerDisplayPort = value;
            }
        }

        public string CustomerDisplayName
        {
            get
            {
                return _cacheManager.CustomerDisplayName;
            }

            set
            {
                _cacheManager.CustomerDisplayName = value;
            }
        }

        public bool UseCustomerDisplay
        {
            get
            {
                return _cacheManager.UseCustomerDisplay;
            }

            set
            {
                _cacheManager.UseCustomerDisplay = value;
            }
        }

        public bool UseOposCustomerDisplay
        {
            get
            {
                return _cacheManager.UseOposCustomerDisplay;
            }

            set
            {
                _cacheManager.UseOposCustomerDisplay = value;
            }
        }

        public string CashDrawerName
        {
            get
            {
                return _cacheManager.CashDrawerName;
            }

            set
            {
                _cacheManager.CashDrawerName = value;
            }
        }

        public int CashDrawerOpenCode
        {
            get
            {
                return _cacheManager.CashDrawerOpenCode;
            }

            set
            {
                _cacheManager.CashDrawerOpenCode = value;
            }
        }

        public bool UseCashDrawer
        {
            get
            {
                return _cacheManager.UseCashDrawer;
            }

            set
            {
                _cacheManager.UseCashDrawer = value;
            }
        }

        public bool UseOposCashDrawer
        {
            get
            {
                return _cacheManager.UseOposCashDrawer;
            }

            set
            {
                _cacheManager.UseOposCashDrawer = value;
            }
        }

        public bool IsFuelPricingGrouped => _cacheManager.IsFuelPricingGrouped;

        public int PumpSpace => _cacheManager.PumpSpace;

        public bool IsFuelPriceDisplayUsed => _cacheManager.IsFuelPriceDisplayUsed;

        public bool IsPriceIncrementEnabled => _cacheManager.IsPriceIncrementEnabled;

        public bool IsTaxExemptionPricesEnabled => _cacheManager.IsTaxExemptionPricesEnabled;

        public bool SwitchUserOnEachSale => _cacheManager.SwitchUserOnEachSale;

        public bool StayOnFuelPricePage => _cacheManager.StayOnFuelPricePage;

        public bool AreFuelPricesSaved
        {
            get
            {
                return _cacheManager.AreFuelPricesSaved;
            }
            set
            {
                _cacheManager.AreFuelPricesSaved = value;
            }
        }

        public bool RequireToGetCustomerName => _cacheManager.RequireToGetCustomerName;

        public bool UserCanPerformManualSales => _cacheManager.UserCanPerformManualSales;

        public bool RequireSignature
        {
            get { return _cacheManager.RequireSignature; }
            set { _cacheManager.RequireSignature = value; }
        }

        public bool IsFleetCardRequired => _cacheManager.IsFleetCardRequired;
        public int ClickDelayForPumps => _cacheManager.ClickDelayForPumps;

        public double? KickbackAmount
        {
            get { return _cacheManager.KickbackAmount; }
            set { _cacheManager.KickbackAmount = value; }
        }

        public string CustomKickbackmsg => _cacheManager.CustomKickbackmsg;

        public string KickBackCardNumber
        {
            get { return _cacheManager.KickBackCardNumber; }
            set { _cacheManager.KickBackCardNumber = value; }
        }

        public bool IsKickBack
        {
            get { return _cacheManager.IsKickBack; }
            set { _cacheManager.IsKickBack = value; }
        }

        public bool IsCarwashIntegrated
        {
            get {return _cacheManager.IsCarwashIntegrated;}
            set{ _cacheManager.IsCarwashIntegrated = value;}
        }

        public bool IsCarwashSupported
        {
            get {return _cacheManager.IsCarwashSupported;}
            set {_cacheManager.IsCarwashSupported = value;}
        }

        public bool HasCarwashProductInSale
        {
            get{ return _cacheManager.HasCarwashProductsInSale ;}

            set { _cacheManager.HasCarwashProductsInSale = value;}
        }

        public string TreatyNumber
        {
            get { return _cacheManager.TreatyNumber; }
            set { _cacheManager.TreatyNumber = value; }
        }

        public string TreatyName
        {
            get { return _cacheManager.TreatyName; }
            set { _cacheManager.TreatyName = value; }
        }

        /// <summary>
        /// Clears the Cache 
        /// </summary>
        public void ClearCache()
        {
            _cacheManager.ClearCacheAsync();
        }

        /// <summary>
        /// Sets all the policies in the cache from the Model
        /// </summary>
        /// <param name="policies">Policy model</param>
        public void SetAllPolicies(Policy policies)
        {
            _cacheManager.SetAllPolicies(policies);
        }
        //For fue discount. Done by Tony 8/10/2018
        public bool isFuelDiscountSupported
        {
            get { return _cacheManager.isFuelDiscountSupported; }
            set { _cacheManager.isFuelDiscountSupported = value; }
        }
        public bool isTDRS_FUELDISCSupported
        {
            get { return _cacheManager.isTDRS_FUELDISCSupported; }
            set { _cacheManager.isTDRS_FUELDISCSupported = value; }
        }

        public string displayCustGrpID
        {
            get { return _cacheManager.displayCustGrpID; }
            set { _cacheManager.displayCustGrpID = value; }
        }
        //For fue discount. ----End
        //For Payment Source. Done by Tony 10/11/2018
        public bool SupportPSInet
        {
            get { return _cacheManager.SupportPSInet; }
            set { _cacheManager.SupportPSInet = value; }
        }
        public string PSINet_Type
        {
            get { return _cacheManager.PSINet_Type; }
            set { _cacheManager.PSINet_Type = value; }
        }
        //For Payment Source. ----End
        //Tony 09/05/2019
        public string VERSION
        {
            get { return _cacheManager.VERSION; }
            set { _cacheManager.VERSION = value; }
        }
        //Tony 09/05/2019 ----End
        //For receipt printing style type. Done by Tony 07/26/2019

        public string RECEIPT_TYPE
        {
            get { return _cacheManager.RECEIPT_TYPE; }
            set { _cacheManager.RECEIPT_TYPE = value; }
        }

        //For receipt printing style type. ----End
        public string FuelDept
        {
            get { return _cacheManager.FuelDept; }
            set { _cacheManager.FuelDept = value; }
        }
        //For Akroo gift cards. Done by Tony 12/19/2018
        public bool REWARDS_Enabled
        {
            get { return _cacheManager.REWARDS_Enabled; }
            set { _cacheManager.REWARDS_Enabled = value; }
        }
        public string REWARDS_Gift
        {
            get { return _cacheManager.REWARDS_Gift; }
            set { _cacheManager.REWARDS_Gift = value; }
        }
        public string REWARDS_TpsIp
        {
            get { return _cacheManager.REWARDS_TpsIp; }
            set { _cacheManager.REWARDS_TpsIp = value; }
        }
        public int REWARDS_TpsPort
        {
            get { return _cacheManager.REWARDS_TpsPort; }
            set { _cacheManager.REWARDS_TpsPort = value; }
        }
        public short REWARDS_Timeout
        {
            get { return _cacheManager.REWARDS_Timeout; }
            set { _cacheManager.REWARDS_Timeout = value; }
        }
        public string REWARDS_Carwash
        {
            get { return _cacheManager.REWARDS_Carwash; }
            set { _cacheManager.REWARDS_Carwash = value; }
        }
        public string REWARDS_CWGIFT
        {
            get { return _cacheManager.REWARDS_CWGIFT; }
            set { _cacheManager.REWARDS_CWGIFT = value; }
        }
        public string REWARDS_CWPKG
        {
            get { return _cacheManager.REWARDS_CWPKG; }
            set { _cacheManager.REWARDS_CWPKG = value; }
        }
        public bool REWARDS_DefaultLoyal
        {
            get { return _cacheManager.REWARDS_DefaultLoyal; }
            set { _cacheManager.REWARDS_DefaultLoyal = value; }
        }
        public string REWARDS_Message
        {
            get { return _cacheManager.REWARDS_Message; }
            set { _cacheManager.REWARDS_Message = value; }
        }
        //For Akroo gift cards. ----End
    }
}
