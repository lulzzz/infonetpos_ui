using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    /// <summary>
    /// Cache Manager
    /// </summary>
    public class CacheManager : ICacheManager
    {
        private readonly ApplicationDataContainer _localSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public CacheManager()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Clears the Cache
        /// </summary>
        /// <returns></returns>
        public async Task ClearCacheAsync()
        {
            await ApplicationData.Current.ClearAsync(ApplicationDataLocality.LocalCache);
        }

        /// <summary>
        /// Gets or sets the PosId
        /// </summary>
        public int PosId
        {
            get
            {
                return (int)_localSettings.Values[nameof(LoginPolicy.PosID)];
            }
            set
            {
                _localSettings.Values[nameof(LoginPolicy.PosID)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the IP Address
        /// </summary>
        public string IpAddress
        {
            get
            {
                return _localSettings.Values["IPAddress"].ToString();
            }
            set
            {
                _localSettings.Values["IPAddress"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the User name
        /// </summary>
        public string UserName
        {
            get
            {
                return _localSettings.Values["UserName"].ToString();
            }
            set
            {
                if (value == null)
                {
                    _localSettings.Values["UserName"] = string.Empty;
                }

                _localSettings.Values["UserName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password
        {
            get
            {
                return _localSettings.Values["Password"].ToString();
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                _localSettings.Values["Password"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Till number
        /// </summary>
        public int TillNumber
        {
            get
            {
                var obj = _localSettings.Values["TillNumber"];
                if (obj != null)
                {
                    return (int)obj;
                }
                return 0;
            }
            set
            {
                _localSettings.Values["TillNumber"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Shift number
        /// </summary>
        public int ShiftNumber
        {
            get
            {
                return (int)_localSettings.Values["ShiftNumber"];
            }
            set
            {
                _localSettings.Values["ShiftNumber"] = value;
            }
        }

        public string Culture => (string)_localSettings.Values[nameof(LoginPolicy.PosLanguage)] == "English" ? "en-US" : "fr-FR";

        /// <summary>
        /// Gets or sets the Sale number
        /// </summary>
        public int SaleNumber
        {
            get
            {
                return (int)_localSettings.Values["SaleNumber"];
            }
            set
            {
                _localSettings.Values["SaleNumber"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Authentication token
        /// </summary>
        public string AuthKey
        {
            get
            {
                return _localSettings.Values["AuthKey"].ToString();
            }
            set
            {
                _localSettings.Values["AuthKey"] = value;

                var previousAuthKey = (string)_localSettings.Values["PreviousAuthKey"];

                if (string.IsNullOrEmpty(previousAuthKey))
                {
                    _localSettings.Values["PreviousAuthKey"] = _localSettings.Values["AuthKey"];
                }

            }
        }

        /// <summary>
        /// Gets or sets the Use Shifts for the day
        /// </summary>
        public bool UseShiftForTheDay
        {
            get
            {
                return (bool)_localSettings.Values["UseShiftForTheDay"];
            }
            set
            {
                _localSettings.Values["UseShiftForTheDay"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Login Policies
        /// </summary>
        public LoginPolicy LoginPolicies
        {
            get
            {
                return new LoginPolicy
                {
                    PosLanguage = (string)_localSettings.Values[nameof(LoginPolicy.PosLanguage)],
                    ProvideTillFloat = (bool)_localSettings.Values[nameof(LoginPolicy.ProvideTillFloat)],
                    PosID = (int)_localSettings.Values[nameof(LoginPolicy.PosID)],
                    UsePredefinedTillNumber = (bool)_localSettings.Values[nameof(LoginPolicy.UsePredefinedTillNumber)],
                    WindowsLogin = (bool)_localSettings.Values[nameof(LoginPolicy.WindowsLogin)],
                    UseShifts = (bool)_localSettings.Values[nameof(LoginPolicy.UseShifts)],
                    AutoShiftPick = (bool)_localSettings.Values[nameof(LoginPolicy.AutoShiftPick)],
                    KeypadFormat = (string)_localSettings.Values[nameof(LoginPolicy.KeypadFormat)]
                };
            }
            set
            {
                _localSettings.Values[nameof(LoginPolicy.PosLanguage)] = value.PosLanguage;
                _localSettings.Values[nameof(LoginPolicy.PosID)] = value.PosID;
                _localSettings.Values[nameof(LoginPolicy.UsePredefinedTillNumber)] = value.UsePredefinedTillNumber;
                _localSettings.Values[nameof(LoginPolicy.ProvideTillFloat)] = value.ProvideTillFloat;
                _localSettings.Values[nameof(LoginPolicy.WindowsLogin)] = value.WindowsLogin;
                _localSettings.Values[nameof(LoginPolicy.UseShifts)] = value.UseShifts;
                _localSettings.Values[nameof(LoginPolicy.AutoShiftPick)] = value.AutoShiftPick;
                _localSettings.Values[nameof(LoginPolicy.KeypadFormat)] = value.KeypadFormat;
            }
        }


        /// <summary>
        /// Gets or sets the Cash float
        /// </summary>
        public string CashFloat
        {
            get
            {
                return _localSettings.Values["CashFloat"].ToString();
            }

            set
            {
                _localSettings.Values["CashFloat"] = value;
            }
        }

        /// <summary>
        /// Method to set all policies
        /// </summary>
        /// <param name="policies"></param>
        public void SetAllPolicies(Policy policies)
        {
            _localSettings.Values[nameof(policies.AddStockItemNotFoundInList)] = policies.AddStockItemNotFoundInList;
            _localSettings.Values[nameof(policies.AllowPayout)] = policies.AllowPayout;
            _localSettings.Values[nameof(policies.ConfirmDeleteLineItem)] = policies.ConfirmDeleteLineItem;
            _localSettings.Values[nameof(policies.DefaultCustomerCode)] = policies.DefaultCustomerCode;
            _localSettings.Values[nameof(policies.DefaultMemberCodeForNonMembers)] = policies.DefaultMemberCodeForNonMembers;
            _localSettings.Values[nameof(policies.DipInputTime)] = policies.DipInputTime;
            _localSettings.Values[nameof(policies.DisplayCustomerDetails)] = policies.DisplayCustomerDetails;
            _localSettings.Values[nameof(policies.ForceAuthorizationOnVoid)] = policies.ForceAuthorizationOnVoid;
            _localSettings.Values[nameof(policies.OperatorBottleReturnLimit)] = policies.OperatorBottleReturnLimit;
            _localSettings.Values[nameof(policies.OperatorCanChangePrice)] = policies.OperatorCanChangePrice;
            _localSettings.Values[nameof(policies.OperatorCanChangeQuantity)] = policies.OperatorCanChangeQuantity;
            _localSettings.Values[nameof(policies.OperatorCanGiveDiscount)] = policies.OperatorCanGiveDiscount;
            _localSettings.Values[nameof(policies.OperatorCanReturnBottle)] = policies.OperatorCanReturnBottle;
            _localSettings.Values[nameof(policies.OperatorCanReturnSale)] = policies.OperatorCanReturnSale;
            _localSettings.Values[nameof(policies.OperatorCanScanCustomerCard)] = policies.OperatorCanScanCustomerCard;
            _localSettings.Values[nameof(policies.OperatorCanSuspendOrUnsuspendSales)] = policies.OperatorCanSuspendOrUnsuspendSales;
            _localSettings.Values[nameof(policies.OperatorCanSwipeCustomerCard)] = policies.OperatorCanSwipeCustomerCard;
            _localSettings.Values[nameof(policies.OperatorCanSwipeMemberCodeAtPump)] = policies.OperatorCanSwipeMemberCodeAtPump;
            _localSettings.Values[nameof(policies.OperatorCanUseARCustomer)] = policies.OperatorCanUseARCustomer;
            _localSettings.Values[nameof(policies.OperatorCanUseCustomer)] = policies.OperatorCanUseCustomer;
            _localSettings.Values[nameof(policies.OperatorCanVoidSale)] = policies.OperatorCanVoidSale;
            _localSettings.Values[nameof(policies.PrintReceiptForVoidAndReturn)] = policies.PrintReceiptForVoidAndReturn;
            _localSettings.Values[nameof(policies.ReasonForPayout)] = policies.ReasonForPayout;
            _localSettings.Values[nameof(policies.ShareSuspendSale)] = policies.ShareSuspendSale;
            _localSettings.Values[nameof(policies.ShowCustomerNoteOnOverlimit)] = policies.ShowCustomerNoteOnOverlimit;
            _localSettings.Values[nameof(policies.SupportDipInput)] = policies.SupportDipInput;
            _localSettings.Values[nameof(policies.SupportKitsInPurchase)] = policies.SupportKitsInPurchase;
            _localSettings.Values[nameof(policies.SuspendEmptySales)] = policies.SuspendEmptySales;
            _localSettings.Values[nameof(policies.TenderNameforARAccount)] = policies.TenderNameforARAccount;
            _localSettings.Values[nameof(policies.UseCustomerDiscountCode)] = policies.UseCustomerDiscountCode;
            _localSettings.Values[nameof(policies.UseProductDiscount)] = policies.UseProductDiscount;
            _localSettings.Values[nameof(policies.UseReasonForVoid)] = policies.UseReasonForVoid;
            _localSettings.Values[nameof(policies.OperatorCanAddStock)] = policies.OperatorCanAddStock;
            _localSettings.Values[nameof(policies.OperatorCanUseLoyalty)] = policies.OperatorCanUseLoyalty;
            _localSettings.Values[nameof(policies.UserCanChangePassword)] = policies.UserCanChangePassword;
            _localSettings.Values[nameof(policies.RequirePasswordOnChangeUser)] = policies.RequirePasswordOnChangeUser;
            _localSettings.Values[nameof(policies.AllowAdjustmentForGiveX)] = policies.AllowAdjustmentForGiveX;
            _localSettings.Values[nameof(policies.CertificateType)] = policies.CertificateType;
            _localSettings.Values[nameof(policies.IdleIntervalAfterAppFreezes)] = policies.IdleIntervalAfterAppFreezes;
            _localSettings.Values[nameof(policies.FreezeTillAutomatically)] = policies.FreezeTillAutomatically;
            _localSettings.Values[nameof(policies.PaymentReceiptCopies)] = policies.PaymentReceiptCopies;
            _localSettings.Values[nameof(policies.OperatorCanDrawCash)] = policies.OperatorCanDrawCash;
            _localSettings.Values[nameof(policies.CashDrawReceiptCopies)] = policies.CashDrawReceiptCopies;
            _localSettings.Values[nameof(policies.CashDropReceiptCopies)] = policies.CashDropReceiptCopies;
            _localSettings.Values[nameof(policies.OperatorCanDropCash)] = policies.OperatorCanDropCash;
            _localSettings.Values[nameof(policies.AskForCashDropReason)] = policies.AskForCashDropReason;
            _localSettings.Values[nameof(policies.RequireEnvelopNumber)] = policies.RequireEnvelopNumber;
            _localSettings.Values[nameof(policies.UseReasonForCashDraw)] = policies.UseReasonForCashDraw;
            _localSettings.Values[nameof(policies.AllowPOSMinimize)] = policies.AllowPOSMinimize;
            _localSettings.Values[nameof(policies.ArpayReceiptCopies)] = policies.ArpayReceiptCopies;
            _localSettings.Values[nameof(policies.BottleReturnReceiptCopies)] = policies.BottleReturnReceiptCopies;
            _localSettings.Values[nameof(policies.ForceGiftCertificate)] = policies.ForceGiftCertificate;
            _localSettings.Values[nameof(policies.ForcePrintReceipt)] = policies.ForcePrintReceipt;
            _localSettings.Values[nameof(policies.GiftCertificateNumbered)] = policies.GiftCertificateNumbered;
            _localSettings.Values[nameof(policies.GiftTender)] = policies.GiftTender;
            _localSettings.Values[nameof(policies.OperatorCanOpenCashDrawer)] = policies.OperatorCanOpenCashDrawer;
            _localSettings.Values[nameof(policies.OperatorIsTrainer)] = policies.OperatorIsTrainer;
            _localSettings.Values[nameof(policies.PaymentReceiptCopies)] = policies.PaymentReceiptCopies;
            _localSettings.Values[nameof(policies.PayoutReceiptCopies)] = policies.PayoutReceiptCopies;
            _localSettings.Values[nameof(policies.RefundReceiptCopies)] = policies.RefundReceiptCopies;
            _localSettings.Values[nameof(policies.SupportsTaxExampt)] = policies.SupportsTaxExampt;
            _localSettings.Values[nameof(policies.UseReasonForCashDrawer)] = policies.UseReasonForCashDrawer;
            _localSettings.Values[nameof(policies.DelayInNewSale)] = policies.DelayInNewSale;
            _localSettings.Values[nameof(policies.EnableExactChange)] = policies.EnableExactChange;
            _localSettings.Values[nameof(policies.BaseCurrency)] = policies.BaseCurrency;
            _localSettings.Values[nameof(policies.CheckSC)] = policies.CheckSC;
            _localSettings.Values[nameof(policies.EnableMsgInput)] = policies.EnableMsgInput;
            _localSettings.Values[nameof(policies.CouponTender)] = policies.CouponTender;
            _localSettings.Values[nameof(policies.IsFuelOnlySystem)] = policies.IsFuelOnlySystem;
            _localSettings.Values[nameof(policies.IsPosOnlySystem)] = policies.IsPosOnlySystem;
            _localSettings.Values[nameof(policies.IsPrePayOn)] = policies.IsPrePayOn;
            _localSettings.Values[nameof(policies.IsPostPayOn)] = policies.IsPostPayOn;
            _localSettings.Values[nameof(policies.IsPayAtPumpOn)] = policies.IsPayAtPumpOn;
            _localSettings.Values[nameof(policies.AllowSwipeScan)] = policies.AllowSwipeScan;
            _localSettings.Values[nameof(policies.FuelPriceNotificationTimeInterval)] = policies.FuelPriceNotificationTimeInterval;
            _localSettings.Values[nameof(policies.SupportFuelPriceFromHO)] = policies.SupportFuelPriceFromHO;
            _localSettings.Values[nameof(policies.FuelPriceNotificationCount)] = policies.FuelPriceNotificationCount;
            _localSettings.Values[nameof(policies.SupportCashCreditpricing)] = policies.SupportCashCreditpricing;
            _localSettings.Values[nameof(policies.TaxExemption)] = policies.TaxExemption;
            _localSettings.Values[nameof(policies.IsFuelPricingGrouped)] = policies.IsFuelPricingGrouped;
            _localSettings.Values[nameof(policies.PumpSpace)] = policies.PumpSpace;
            _localSettings.Values[nameof(policies.IsFuelPriceDisplayUsed)] = policies.IsFuelPriceDisplayUsed;
            _localSettings.Values[nameof(policies.IsPriceIncrementEnabled)] = policies.IsPriceIncrementEnabled;
            _localSettings.Values[nameof(policies.IsTaxExemptionPricesEnabled)] = policies.IsTaxExemptionPricesEnabled;
            _localSettings.Values[nameof(policies.SwitchUserOnEachSale)] = policies.SwitchUserOnEachSale;
            _localSettings.Values[nameof(policies.StayOnFuelPricePage)] = policies.StayOnFuelPricePage;
            _localSettings.Values[nameof(policies.RequireToGetCustomerName)] = policies.RequireToGetCustomerName;
            _localSettings.Values[nameof(policies.UserCanPerformManualSales)] = policies.UserCanPerformManualSales;
            _localSettings.Values[nameof(policies.RequireSignature)] = policies.RequireSignature;
            _localSettings.Values[nameof(policies.IsFleetCardRequired)] = policies.IsFleetCardRequired;
            _localSettings.Values[nameof(policies.ClickDelayForPumps)] = policies.ClickDelayForPumps;
            _localSettings.Values[nameof(policies.KickbackRedeemMsg)] = policies.KickbackRedeemMsg;
            _localSettings.Values[nameof(policies.SupportKickback)] = policies.SupportKickback;
            _localSettings.Values[nameof(policies.CustomKickbackmsg)] = policies.CustomKickbackmsg;
            _localSettings.Values[nameof(policies.IsCarwashSupported)] = policies.IsCarwashSupported;
            _localSettings.Values[nameof(policies.IsCarwashIntegrated)] = policies.IsCarwashIntegrated;
            //Done by Tony 07/17/2018
            _localSettings.Values[nameof(policies.isFuelDiscountSupported)] = policies.isFuelDiscountSupported;
            _localSettings.Values[nameof(policies.isTDRS_FUELDISCSupported)] = policies.isTDRS_FUELDISCSupported;
            _localSettings.Values[nameof(policies.displayCustGrpID)] = policies.displayCustGrpID;
            //Done by Tony 10/17/2018
            _localSettings.Values[nameof(policies.SupportPSInet)] = policies.SupportPSInet;
            //Done by Tony 05/22/2019
            _localSettings.Values[nameof(policies.PSINet_Type)] = policies.PSINet_Type;
            
            //Tony 09/05/2019
            _localSettings.Values[nameof(policies.VERSION)] = policies.VERSION;
            //Done by Tony 07/26/2019
            _localSettings.Values[nameof(policies.RECEIPT_TYPE)] = policies.RECEIPT_TYPE;
            _localSettings.Values[nameof(policies.FuelDept)] = policies.FuelDept;
            //Done by Tony 12/19/2018
            _localSettings.Values[nameof(policies.REWARDS_Enabled)] = policies.REWARDS_Enabled;
            _localSettings.Values[nameof(policies.REWARDS_Gift)] = policies.REWARDS_Gift;
            _localSettings.Values[nameof(policies.REWARDS_TpsIp)] = policies.REWARDS_TpsIp;
            _localSettings.Values[nameof(policies.REWARDS_TpsPort)] = policies.REWARDS_TpsPort;
            _localSettings.Values[nameof(policies.REWARDS_Timeout)] = policies.REWARDS_Timeout;
            _localSettings.Values[nameof(policies.REWARDS_Carwash)] = policies.REWARDS_Carwash;
            _localSettings.Values[nameof(policies.REWARDS_CWGIFT)] = policies.REWARDS_CWGIFT;
            _localSettings.Values[nameof(policies.REWARDS_CWPKG)] = policies.REWARDS_CWPKG;
            _localSettings.Values[nameof(policies.REWARDS_DefaultLoyal)] = policies.REWARDS_DefaultLoyal;
            _localSettings.Values[nameof(policies.REWARDS_Message)] = policies.REWARDS_Message;

        }

        public bool SupportKickback => (bool)_localSettings.Values["SupportKickback"];

        public double KickbackRedeemMsg => (double)_localSettings.Values["KickbackRedeemMsg"];

        public bool CheckSC => (bool)_localSettings.Values["CheckSC"];

        public bool OperatorCanUseCustomer => (bool)_localSettings.Values["OperatorCanUseCustomer"];

        public bool OperatorCanUseARCustomer => (bool)_localSettings.Values["OperatorCanUseARCustomer"];

        public bool ShowCustomerNoteOnOverlimit => (bool)_localSettings.Values["ShowCustomerNoteOnOverlimit"];

        public bool DisplayCustomerDetails => (bool)_localSettings.Values["DisplayCustomerDetails"];

        public string DefaultCustomerCode => (string)_localSettings.Values["DefaultCustomerCode"];

        public string TenderNameforARAccount => (string)_localSettings.Values["TenderNameforARAccount"];

        public string OperatorCanScanCustomerCard => (string)_localSettings.Values["OperatorCanScanCustomerCard"];

        public bool OperatorCanSwipeCustomerCard => (bool)_localSettings.Values["OperatorCanSwipeCustomerCard"];

        public string DefaultMemberCodeForNonMembers => (string)_localSettings.Values["DefaultMemberCodeForNonMembers"];

        public bool SupportKitsInPurchase => (bool)_localSettings.Values["SupportKitsInPurchase"];

        public bool AddStockItemNotFoundInList => (bool)_localSettings.Values["AddStockItemNotFoundInList"];

        public bool ConfirmDeleteLineItem => (bool)_localSettings.Values["ConfirmDeleteLineItem"];

        public bool UseProductDiscount => (bool)_localSettings.Values["UseProductDiscount"];

        public bool PrintReceiptForVoidAndReturn => (bool)_localSettings.Values["PrintReceiptForVoidAndReturn"];

        public bool SuspendEmptySales => (bool)_localSettings.Values["SuspendEmptySales"];

        public bool ShareSuspendSale => (bool)_localSettings.Values["ShareSuspendSale"];

        public bool AllowPayout => (bool)_localSettings.Values["AllowPayout"];

        public bool ReasonForPayout => (bool)_localSettings.Values["ReasonForPayout"];

        public bool UseCustomerDiscountCode => (bool)_localSettings.Values["UseCustomerDiscountCode"];

        public bool ForceAuthorizationOnVoid => (bool)_localSettings.Values["ForceAuthorizationOnVoid"];

        public bool OperatorCanGiveDiscount => (bool)_localSettings.Values["OperatorCanGiveDiscount"];

        public bool OperatorCanChangePrice => (bool)_localSettings.Values["OperatorCanChangePrice"];

        public bool OperatorCanChangeQuantity => (bool)_localSettings.Values["OperatorCanChangeQuantity"];
        public string CouponTender => (string)_localSettings.Values["CouponTender"];
        public bool SwitchUserOnEachSale => (bool)_localSettings.Values["SwitchUserOnEachSale"];
        public bool OperatorCanVoidSale => (bool)_localSettings.Values["OperatorCanVoidSale"];
        public string CustomKickbackmsg => (string)_localSettings.Values["CustomKickbackmsg"];
        public bool UseReasonForVoid => (bool)_localSettings.Values["UseReasonForVoid"];

        public bool OperatorCanSuspendOrUnsuspendSales => (bool)_localSettings.Values["OperatorCanSuspendOrUnsuspendSales"];

        public bool OperatorCanReturnBottle => (bool)_localSettings.Values["OperatorCanReturnBottle"];

        public bool OperatorCanReturnSale => (bool)_localSettings.Values["OperatorCanReturnSale"];

        public decimal OperatorBottleReturnLimit => (decimal)_localSettings.Values["OperatorBottleReturnLimit"];

        public bool SupportDipInput => (bool)_localSettings.Values["SupportDipInput"];

        public string DipInputTime => (string)_localSettings.Values["DipInputTime"];

        public bool OperatorCanSwipeMemberCodeAtPump => (bool)_localSettings.Values["OperatorCanSwipeMemberCodeAtPump"];

        public bool PrintWriteOff => (bool)_localSettings.Values["PrintWriteOff"];

        public bool OperatorCanDrawCash => (bool)_localSettings.Values["OperatorCanDrawCash"];

        public int CashDrawReceiptCopies => (int)_localSettings.Values["CashDrawReceiptCopies"];

        public int CashDropReceiptCopies => (int)_localSettings.Values["CashDropReceiptCopies"];

        public bool OperatorCanDropCash => (bool)_localSettings.Values["OperatorCanDropCash"];

        public bool UseReasonForCashDrop => (bool)_localSettings.Values["UseReasonForCashDrop"];

        public bool AskForCashDropReason => (bool)_localSettings.Values["AskForCashDropReason"];

        public bool RequireEnvelopNumber => (bool)_localSettings.Values["RequireEnvelopNumber"];

        public bool AllowPOSMinimize => (bool)_localSettings.Values["AllowPOSMinimize"];

        public int ArpayReceiptCopies => (int)_localSettings.Values["ArpayReceiptCopies"];

        public int BottleReturnReceiptCopies => (int)_localSettings.Values["BottleReturnReceiptCopies"];

        public bool ForceGiftCertificate => (bool)_localSettings.Values["ForceGiftCertificate"];

        public bool ForcePrintReceipt => (bool)_localSettings.Values["ForcePrintReceipt"];

        public bool GiftCertificateNumbered => (bool)_localSettings.Values["GiftCertificateNumbered"];

        public string GiftTender => (string)_localSettings.Values["GiftTender"];

        public bool OperatorCanOpenCashDrawer => (bool)_localSettings.Values["OperatorCanOpenCashDrawer"];

        public bool OperatorIsTrainer => (bool)_localSettings.Values["OperatorIsTrainer"];

        public int PaymentReceiptCopies => (int)_localSettings.Values["PaymentReceiptCopies"];

        public int PayoutReceiptCopies => (int)_localSettings.Values["PayoutReceiptCopies"];

        public int RefundReceiptCopies => (int)_localSettings.Values["RefundReceiptCopies"];

        public bool SupportsTaxExampt => (bool)_localSettings.Values["SupportsTaxExampt"];

        public bool UseReasonForCashDrawer => (bool)_localSettings.Values["UseReasonForCashDrawer"];

        public int DelayInNewSale => (int)_localSettings.Values["DelayInNewSale"];

        public bool EnableExactChange => (bool)_localSettings.Values["EnableExactChange"];
        public string BaseCurrency => (string)_localSettings.Values["BaseCurrency"];

        public bool EnableMsgInput => (bool)_localSettings.Values["EnableMsgInput"];

        /// <summary>
        /// gets or sets customer name
        /// </summary>
        public string CustomerName
        {
            get
            {
                return (string)_localSettings.Values["CustomerName"];
            }

            set
            {
                _localSettings.Values["CustomerName"] = value;
            }
        }

        /// <summary>
        /// Return mode is set or not
        /// </summary>
        public bool IsReturn
        {
            get
            {
                if (_localSettings.Values["IsReturn"] != null)
                {
                    return (bool)_localSettings.Values["IsReturn"];
                }
                return false;
            }
            set
            {
                _localSettings.Values["IsReturn"] = value;
            }
        }

        /// <summary>
        /// Register number
        /// </summary>
        public byte RegisterNumber
        {
            get { return (byte)_localSettings.Values["RegisterNumber"]; }
            set { _localSettings.Values["RegisterNumber"] = value; }
        }

        public bool OperatorCanAddStock => (bool)_localSettings.Values["OperatorCanAddStock"];

        public bool OperatorCanUseLoyalty => (bool)_localSettings.Values["OperatorCanUseLoyalty"];

        public bool UserCanChangePassword => (bool)_localSettings.Values["UserCanChangePassword"];

        public bool RequirePasswordOnChangeUser => (bool)_localSettings.Values["RequirePasswordOnChangeUser"];

        public bool UseReasonForCashDraw => (bool)_localSettings.Values["UseReasonForCashDraw"];

        public bool SupportCashCreditpricing => (bool)_localSettings.Values["SupportCashCreditpricing"];
        public bool TaxExemption => (bool)_localSettings.Values["TaxExemption"];

        public string ShiftDate
        {
            get { return (string)_localSettings.Values["ShiftDate"]; }
            set { _localSettings.Values["ShiftDate"] = value; }
        }

        public bool AllowAdjustmentForGiveX => (bool)_localSettings.Values["AllowAdjustmentForGiveX"];

        public string CertificateType => (string)_localSettings.Values["CertificateType"];

        public int GiftNumber
        {
            get
            {
                try
                {
                    return (int)_localSettings.Values["GiftNumber"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { _localSettings.Values["GiftNumber"] = value; }
        }

        public string PreviousAuthKey
        {
            get
            {
                return _localSettings.Values["PreviousAuthKey"] != null ?
            _localSettings.Values["PreviousAuthKey"].ToString() : string.Empty;
            }

            set
            {
                _localSettings.Values["PreviousAuthKey"] = value;
            }
        }

        public bool IsGiveXCalledFromAddStock
        {
            get
            {
                return (bool)_localSettings.Values["IsGiveXCalledFromAddStock"];
            }

            set
            {
                _localSettings.Values["IsGiveXCalledFromAddStock"] = value;
            }
        }

        public int TillNumberForSale
        {
            get
            {
                return (int)_localSettings.Values["TillNumberForSale"];
            }

            set
            {
                _localSettings.Values["TillNumberForSale"] = value;
            }
        }

        public long IdleIntervalAfterAppFreezes => Convert.ToInt64(_localSettings.Values["IdleIntervalAfterAppFreezes"].ToString());

        public bool FreezeTillAutomatically
        {
            get
            {
                try
                {
                    return (bool)_localSettings.Values["FreezeTillAutomatically"];
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }

        public string FramePriorSwitchUserNavigation
        {
            get
            {
                var data = (string)_localSettings.Values["FramePriorSwitchUserNavigation"];
                return data == null ? string.Empty : data;
            }
            set { _localSettings.Values["FramePriorSwitchUserNavigation"] = value; }
        }

        public bool EnableWriteOffButton
        {
            get
            {
                return (bool)_localSettings.Values["EnableWriteOffButton"];
            }

            set
            {
                _localSettings.Values["EnableWriteOffButton"] = value;
            }
        }

        public bool IsCurrentSaleEmpty
        {
            get
            {
                return (bool)_localSettings.Values["IsCurrentSaleEmpty"];
            }

            set
            {
                _localSettings.Values["IsCurrentSaleEmpty"] = value;
            }
        }

        public string LastPrintReport
        {
            get
            {
                return (string)_localSettings.Values["LastPrintReport"];
            }

            set
            {
                _localSettings.Values["LastPrintReport"] = value;
            }
        }

        public bool IsPostPayOn
        {
            get
            {
                return (bool)_localSettings.Values["IsPostPayOn"];
            }

            set
            {
                _localSettings.Values["IsPostPayOn"] = value;
            }
        }

        public bool IsPrePayOn
        {
            get
            {
                return (bool)_localSettings.Values["IsPrePayOn"];
            }

            set
            {
                _localSettings.Values["IsPrePayOn"] = value;
            }
        }

        public bool IsPayAtPumpOn
        {
            get
            {
                return (bool)_localSettings.Values["IsPayAtPumpOn"];
            }

            set
            {
                _localSettings.Values["IsPayAtPumpOn"] = value;
            }
        }

        public bool IsFuelOnlySystem => (bool)_localSettings.Values["IsFuelOnlySystem"];

        public bool IsPosOnlySystem => (bool)_localSettings.Values["IsPosOnlySystem"];

        public bool AllowSwipeScan => (bool)_localSettings.Values["AllowSwipeScan"];

        public int FuelPriceNotificationTimeInterval => (int)_localSettings.Values["FuelPriceNotificationTimeInterval"];

        public bool SupportFuelPriceFromHO => (bool)_localSettings.Values["SupportFuelPriceFromHO"];

        public int FuelPriceNotificationCount => (int)_localSettings.Values["FuelPriceNotificationCount"];

        public bool StayOnFuelPricePage => (bool)_localSettings.Values["StayOnFuelPricePage"];

        public string TrainerCaption
        {
            get
            {
                return (string)_localSettings.Values["TrainerCaption"];
            }

            set
            {
                _localSettings.Values["TrainerCaption"] = value;
            }
        }
        public bool UseReceiptPrinter
        {
            get
            {
                return (bool)_localSettings.Values["UseReceiptPrinter"];
            }
            set
            {
                _localSettings.Values["UseReceiptPrinter"] = value;
            }
        }

        public bool UseOposReceiptPrinter
        {
            get
            {
                return (bool)_localSettings.Values["UseOposReceiptPrinter"];
            }
            set
            {
                _localSettings.Values["UseOposReceiptPrinter"] = value;
            }
        }

        public string ReceiptPrinterName
        {
            get
            {
                return (string)_localSettings.Values["ReceiptPrinterName"];
            }
            set
            {
                _localSettings.Values["ReceiptPrinterName"] = value;
            }
        }

        public string ReceiptPrinterDriver
        {
            get
            {
                return (string)_localSettings.Values["ReceiptPrinterDriver"];
            }
            set
            {
                _localSettings.Values["ReceiptPrinterDriver"] = value;
            }
        }

        public int CustomerDisplayPort
        {
            get
            {
                return (int)_localSettings.Values["CustomerDisplayPort"];
            }
            set
            {
                _localSettings.Values["CustomerDisplayPort"] = value;
            }
        }

        public string CustomerDisplayName
        {
            get
            {
                return (string)_localSettings.Values["CustomerDisplayName"];
            }
            set
            {
                _localSettings.Values["CustomerDisplayName"] = value;
            }
        }

        public bool UseCustomerDisplay
        {
            get
            {
                return (bool)_localSettings.Values["UseCustomerDisplay"];
            }
            set
            {
                _localSettings.Values["UseCustomerDisplay"] = value;
            }
        }

        public bool UseOposCustomerDisplay
        {
            get
            {
                return (bool)_localSettings.Values["UseOposCustomerDisplay"];
            }
            set
            {
                _localSettings.Values["UseOposCustomerDisplay"] = value;
            }
        }

        public string CashDrawerName
        {
            get
            {
                return (string)_localSettings.Values["CashDrawerName"];
            }
            set
            {
                _localSettings.Values["CashDrawerName"] = value;
            }
        }

        public int CashDrawerOpenCode
        {
            get
            {
                return (int)_localSettings.Values["CashDrawerOpenCode"];
            }
            set
            {
                _localSettings.Values["CashDrawerOpenCode"] = value;
            }
        }

        public bool UseCashDrawer
        {
            get
            {
                return (bool)_localSettings.Values["UseCashDrawer"];
            }
            set
            {
                _localSettings.Values["UseCashDrawer"] = value;
            }
        }

        public bool UseOposCashDrawer
        {
            get
            {
                return (bool)_localSettings.Values["UseOposCashDrawer"];
            }
            set
            {
                _localSettings.Values["UseOposCashDrawer"] = value;
            }
        }

        public bool IsFuelPricingGrouped => (bool)_localSettings.Values["IsFuelPricingGrouped"];

        public int PumpSpace => (int)_localSettings.Values["PumpSpace"];

        public bool IsFuelPriceDisplayUsed => (bool)_localSettings.Values["IsFuelPriceDisplayUsed"];

        public bool IsPriceIncrementEnabled => (bool)_localSettings.Values["IsPriceIncrementEnabled"];

        public bool IsTaxExemptionPricesEnabled => (bool)_localSettings.Values["IsTaxExemptionPricesEnabled"];

        public bool AreFuelPricesSaved
        {
            get
            {
                return (bool)_localSettings.Values["AreFuelPricesSaved"];
            }

            set
            {
                _localSettings.Values["AreFuelPricesSaved"] = value;
            }
        }

        public bool RequireToGetCustomerName => (bool)_localSettings.Values["RequireToGetCustomerName"];

        public bool UserCanPerformManualSales => (bool)_localSettings.Values["UserCanPerformManualSales"];

        public bool RequireSignature
        {
            get
            {
                return (bool)_localSettings.Values["RequireSignature"];
            }
            set
            {
                _localSettings.Values["RequireSignature"] = value;
            }
        }

        public bool IsFleetCardRequired => (bool)_localSettings.Values["IsFleetCardRequired"];

        public int ClickDelayForPumps => (int)_localSettings.Values["ClickDelayForPumps"];

        public double? KickbackAmount
        {
            get { return (double?)_localSettings.Values["KickbackAmount"]; }
            set { _localSettings.Values["KickbackAmount"] = value; }
        }

        public string KickBackCardNumber
        {
            get { return (string)_localSettings.Values["KickBackCardNumber"]; }
            set { _localSettings.Values["KickBackCardNumber"] = value; }
        }

        public bool IsKickBack
        {
            get { return (bool)_localSettings.Values["IsKickBack"]; }
            set { _localSettings.Values["IsKickBack"] = value; }
        }

        public bool IsCarwashSupported
        {
            get { return (bool)_localSettings.Values["IsCarwashSupported"]; }
            set { _localSettings.Values["IsCarwashSupported"] = value; }
        }

        public bool IsCarwashIntegrated
        {
            get { return (bool)_localSettings.Values["IsCarwashIntegrated"]; }
            set { _localSettings.Values["IsCarwashIntegrated"] = value; }
        }

        public bool HasCarwashProductsInSale
        {
            get { return (bool)_localSettings.Values["HasCarwashProductInSale"]; }
            set { _localSettings.Values["HasCarwashProductInSale"] = value; }
        }
        public string TreatyNumber
        {
            get { return (string)_localSettings.Values["TreatyNumber"]; }
            set { _localSettings.Values["TreatyNumber"] = value; }
        }

        public string TreatyName 
        {
            get { return (string)_localSettings.Values["TreatyName"]; }
            set { _localSettings.Values["TreatyName"] = value; }
        }

        public string displayCustGrpID  //Done by Tony 07/17/2018
        {
            get { return _localSettings.Values["displayCustGrpID"].ToString(); }
            set { _localSettings.Values["displayCustGrpID"] = value; }
        }


        public bool isFuelDiscountSupported   //Done by Tony 07/17/2018
        {
            get { return (bool)_localSettings.Values["isFuelDiscountSupported"]; }
            set { _localSettings.Values["isFuelDiscountSupported"] = value; }
        }
        public bool isTDRS_FUELDISCSupported  //Done by Tony 07/17/2018
        {
            get { return (bool)_localSettings.Values["isTDRS_FUELDISCSupported"]; }
            set { _localSettings.Values["isTDRS_FUELDISCSupported"] = value; }
        }
        //Done by Tony 10/11/2018
        public bool SupportPSInet
        {
            get { return (bool)_localSettings.Values["SupportPSInet"]; }
            set { _localSettings.Values["SupportPSInet"] = value; }
        }
        public string PSINet_Type
        {
            get { return _localSettings.Values["PSINet_Type"].ToString(); }
            set { _localSettings.Values["PSINet_Type"] = value; }
        }
        //Done by Tony 05/22/2019
        public string FuelDept
        {
            get { return _localSettings.Values["FuelDept"].ToString(); }
            set { _localSettings.Values["FuelDept"] = value; }
        }
        //Tony 09/05/2019
        public string VERSION
        {
            get { return _localSettings.Values["VERSION"]==null?null:_localSettings.Values["VERSION"].ToString(); }
            set { _localSettings.Values["VERSION"] = value; }
        }
        //Tony 09/05/2019 ----End
        //Done by Tony 07/26/2019
        public string RECEIPT_TYPE
        {
            get { return _localSettings.Values["RECEIPT_TYPE"].ToString(); }
            set { _localSettings.Values["RECEIPT_TYPE"] = value; }
        }
        //Done by Tony 12/19/2018
        public string REWARDS_Message
        {
            get { return _localSettings.Values["REWARDS_Message"].ToString(); }
            set { _localSettings.Values["REWARDS_Message"] = value; }
        }
        public bool REWARDS_Enabled
        {
            get { return (bool)_localSettings.Values["REWARDS_Enabled"]; }
            set { _localSettings.Values["REWARDS_Enabled"] = value; }
        }
        public string REWARDS_Gift
        {
            get { return _localSettings.Values["REWARDS_Gift"].ToString(); }
            set { _localSettings.Values["REWARDS_Gift"] = value; }
        }
        public string REWARDS_TpsIp
        {
            get { return _localSettings.Values["REWARDS_TpsIp"].ToString(); }
            set { _localSettings.Values["REWARDS_TpsIp"] = value; }
        }
        public int REWARDS_TpsPort
        {
            get { return (int)_localSettings.Values["REWARDS_TpsPort"]; }
            set { _localSettings.Values["REWARDS_TpsPort"] = value; }
        }
        public short REWARDS_Timeout
        {
            get { return (short)_localSettings.Values["REWARDS_Timeout"]; }
            set { _localSettings.Values["REWARDS_Timeout"] = value; }
        }
        public string REWARDS_Carwash
        {
            get { return _localSettings.Values["REWARDS_Carwash"].ToString(); }
            set { _localSettings.Values["REWARDS_Carwash"] = value; }
        }
        public string REWARDS_CWGIFT
        {
            get { return _localSettings.Values["REWARDS_CWGIFT"].ToString(); }
            set { _localSettings.Values["REWARDS_CWGIFT"] = value; }
        }
        public string REWARDS_CWPKG
        {
            get { return _localSettings.Values["REWARDS_CWPKG"].ToString(); }
            set { _localSettings.Values["REWARDS_CWPKG"] = value; }
        }
        public bool REWARDS_DefaultLoyal
        {
            get { return (bool)_localSettings.Values["REWARDS_DefaultLoyal"]; }
            set { _localSettings.Values["REWARDS_DefaultLoyal"] = value; }
        }


    }
}
