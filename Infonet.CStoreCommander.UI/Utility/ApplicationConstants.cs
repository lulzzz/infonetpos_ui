using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class ApplicationConstants
    {
        #region Private variables
        private static string _customer;
        private static string _pumpEmergency;
        private static string _shiftUsedForTheDay;
        private static string _maxLoginAttemptsReached;
        private static string _somethingBadHappned;
        private static string _customerSearch;
        private static string _loyaltySearch;
        private static string _stopAll;
        private static string _resumeAll;
        private static string _loyaltyGift;
        private static string _giveX;
        private static string _voidSale;
        private static string _cancel;
        private static string _confirm;
        private static string _languageNotSupported;
        private static string _apiNotConnected;
        private static string _discountType;
        private static string _cannotUnsuspend;
        private static string _shiftConfirmation;
        private static string _confirmed;
        private static string _notConfirmed;
        private static string _giftCardZeroPriceError;
        private static string _apiTimeoutMessage;
        private static string _loyalityNumberConfirmation;
        private static string _cash;
        private static string _cashDrop;
        private static string _cashDraw;
        private static string _writeOffConfirmation;
        private static string _noPrinterFound;
        private static string _cashDropType;
        private static string _atmDrop;
        private static string _safeDrop;
        private static string _maximumTenderValue;
        private static string _price;
        private static string _percent;
        private static string _enterAValue;
        private static string _tenderMaxAmountConfirmation;
        private static string _giftCertificateConfirmationMessage;
        private static string _runAwayConfirmation;
        private static string _deleteSaleLine;
        private static string _reprint;
        private static string _lastPrint;
        private static string _reprintText;
        private static string _maximumPriceError;
        private static string _invalidPassword;
        private static string _giftCertificateExpiredMessage;
        private static string _giftCertificateNotFound;
        private static string _overrideLimitMessage;
        private static string _storeCreditConfiramtionMessage;
        private static string _dipReadingWarning;
        private static string _messageConfirmation;
        private static string _noReportFound;
        private static string _pumpOptions;
        private static string _fuelPrice;
        private static string _tierLevel;
        private static string _propaneGrade;
        private static string _enterPinNumber;
        private static string _invalidPin;
        private static string _missingRequired;
        private static string _promptRequired;
        private static string _poRequired;
        private static string _reprintDisabled;
        private static string _taxExemptionNumber;
        private static string _pumpTestConfirmation;
        private static string _clearErrorFile;
        private static string _negativeDiscountsNotAllowed;
        private static string _initializeMessage;
        private static string _yes;
        private static string _no;
        private static string _firstUnitPrice;
        private static string _regularPrice;
        private static string _incrementalPrice;
        private static string _salePrice;
        private static string _xForPrice;
        private static string _ok;
        private static string _settingInProgress;
        private static string _setPrepayTitle;
        private static string _switchPrepayTitle;
        private static string _userCannotPerformManual;
        private static string _noCustomerDisplayFound;
        private static string _noCashDrawerFound;
        private static string _signaturePadNotConnected;
        private static string _noSignatureConfirmation;
        private static string _finish;
        private static string _prepay;
        private static string _manual;
        private static string _deletePrepay;
        private static string _switchPrepay;
        private static string _resume;
        private static string _stop;
        private static string _bigPump;
        private static string _authorize;
        private static string _deauthorize;
        private static string _pumping;
        private static string _inactive;
        private static string _stopped;
        private static string _calling;
        private static string _idle;
        private static string _current;
        private static string _next;
        private static string _forPumpOneOption;
        private static string _forPumpTwoOption;
        private static string _regularPriceText;
        private static string _acceptTheSignature;
        private static string _verifyKickbackMessage;

        public static string VerifyKickbackMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_verifyKickbackMessage))
                {
                    _verifyKickbackMessage = Helper.GetResourceString(nameof(VerifyKickbackMessage),
                        Language);
                }
                return _verifyKickbackMessage;
            }
            set { _verifyKickbackMessage = value; }
        }

        #endregion

        private static string _communticationErrorMessage;
        private static string _invalidLoyaltyCard;
        private static string _carwashServerErrorMessage;
        private static string _carwashCodeValidMessage;
        private static string _carwashCodeInValidMessage;
        private static string _carwashServerErrorMessageOnValidation;
        private static string _ackrooOutStandingMessage;

        public static string AckrooOutStandingMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_ackrooOutStandingMessage))
                {
                    _ackrooOutStandingMessage = Helper.GetResourceString(nameof(AckrooOutStandingMessage), Language);
                }
                return _ackrooOutStandingMessage;
            }
            set { _ackrooOutStandingMessage = value; }
        }
        public static string CarwashCodeValidMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_carwashCodeValidMessage))
                {
                    _carwashCodeValidMessage = Helper.GetResourceString(
                        nameof(CarwashCodeValidMessage),
                        Language);
                }
                return _carwashCodeValidMessage;
            }
            set
            {
                _carwashCodeValidMessage = value;
            }
        }

        public static string CarwashCodeInValidMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_carwashCodeInValidMessage))
                {
                    _carwashCodeInValidMessage = Helper.GetResourceString(
                        nameof(CarwashCodeInValidMessage),
                        Language);
                }
                return _carwashCodeInValidMessage;
            }
            set
            {
                _carwashCodeInValidMessage = value;
            }
        }

        public static string CarwashServerErrorMessageOnValidation
        {
            get
            {
                if (string.IsNullOrEmpty(_carwashServerErrorMessageOnValidation))
                {
                    _carwashServerErrorMessageOnValidation = Helper.GetResourceString(
                        nameof(CarwashServerErrorMessageOnValidation),
                        Language);
                }
                return _carwashServerErrorMessageOnValidation;
            }
            set
            {
                _carwashServerErrorMessageOnValidation = value;
            }
        }
        

        public static string CarwashServerErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_carwashServerErrorMessage))
                {
                    _carwashServerErrorMessage = Helper.GetResourceString(
                        nameof(CarwashServerErrorMessage),
                        Language);
                }
                return _carwashServerErrorMessage;
            }
            set
            {
                _carwashServerErrorMessage = value;
            }
        }


        public static string InvalidLoyaltyCard
        {
            get
            {
                if (string.IsNullOrEmpty(_invalidLoyaltyCard))
                {
                    _invalidLoyaltyCard = Helper.GetResourceString(
                        nameof(InvalidLoyaltyCard),
                        Language);
                }
                return _invalidLoyaltyCard;
            }
            set { _invalidLoyaltyCard = value; }
        }

        public static string CommunicationErrormessage
        {
            get
            {
                if (string.IsNullOrEmpty(_communticationErrorMessage))
                {
                    _communticationErrorMessage = Helper.GetResourceString(
                        nameof(CommunicationErrormessage),
                        Language);
                }
                return _communticationErrorMessage;
            }
            set
            {

                _communticationErrorMessage = value;
            }
        }

        public static string ForPumpTwoOption
        {
            get
            {
                if (string.IsNullOrEmpty(_forPumpTwoOption))
                {
                    _forPumpTwoOption = Helper.GetResourceString(nameof(ForPumpTwoOption),
                        Language);
                }
                return _forPumpTwoOption;
            }
            set { _forPumpTwoOption = value; }
        }

        public static string ForPumpOneOption
        {
            get
            {
                if (string.IsNullOrEmpty(_forPumpOneOption))
                {
                    _forPumpOneOption = Helper.GetResourceString(nameof(ForPumpOneOption),
                        Language);
                }
                return _forPumpOneOption;
            }
            set { _forPumpOneOption = value; }
        }

        public static string Next
        {
            get
            {
                if (string.IsNullOrEmpty(_next))
                {
                    _next = Helper.GetResourceString(nameof(Next),
                        Language);
                }
                return _next;
            }
            set { _next = value; }
        }

        public static string Current
        {
            get
            {
                if (string.IsNullOrEmpty(_current))
                {
                    _current = Helper.GetResourceString(nameof(Current),
                        Language);
                }
                return _current;
            }
            set { _current = value; }
        }

        public static string Idle
        {
            get
            {
                if (string.IsNullOrEmpty(_idle))
                {
                    _idle = Helper.GetResourceString(nameof(Idle),
                        Language);
                }
                return _idle;
            }
            set { _idle = value; }
        }

        public static string Calling
        {
            get
            {
                if (string.IsNullOrEmpty(_calling))
                {
                    _calling = Helper.GetResourceString(nameof(Calling),
                        Language);
                }
                return _calling;
            }
            set { _calling = value; }
        }

        public static string Stopped
        {
            get
            {
                if (string.IsNullOrEmpty(_stopped))
                {
                    _stopped = Helper.GetResourceString(nameof(Stopped),
                        Language);
                }
                return _stopped;
            }
            set { _stopped = value; }
        }

        public static string Inactive
        {
            get
            {
                if (string.IsNullOrEmpty(_inactive))
                {
                    _inactive = Helper.GetResourceString(nameof(Inactive),
                        Language);
                }
                return _inactive;
            }
            set { _inactive = value; }
        }

        public static string Pumping
        {
            get
            {
                if (string.IsNullOrEmpty(_pumping))
                {
                    _pumping = Helper.GetResourceString(nameof(Pumping),
                        Language);
                }
                return _pumping;
            }
            set { _pumping = value; }
        }

        public static string Deauthorize
        {
            get
            {
                if (string.IsNullOrEmpty(_deauthorize))
                {
                    _deauthorize = Helper.GetResourceString(nameof(Deauthorize),
                        Language);
                }
                return _deauthorize;
            }
            set { _deauthorize = value; }
        }

        public static string Authorize
        {
            get
            {
                if (string.IsNullOrEmpty(_authorize))
                {
                    _authorize = Helper.GetResourceString(nameof(Authorize),
                        Language);
                }
                return _authorize;
            }
            set { _authorize = value; }
        }

        public static string BigPump
        {
            get
            {
                if (string.IsNullOrEmpty(_bigPump))
                {
                    _bigPump = Helper.GetResourceString(nameof(BigPump),
                        Language);
                }
                return _bigPump;
            }
            set { _bigPump = value; }
        }

        public static string Stop
        {
            get
            {
                if (string.IsNullOrEmpty(_stop))
                {
                    _stop = Helper.GetResourceString(nameof(Stop),
                        Language);
                }
                return _stop;
            }
            set { _stop = value; }
        }

        public static string Resume
        {
            get
            {
                if (string.IsNullOrEmpty(_resume))
                {
                    _resume = Helper.GetResourceString(nameof(Resume),
                        Language);
                }
                return _resume;
            }
            set { _resume = value; }
        }

        public static string SwitchPrepay
        {
            get
            {
                if (string.IsNullOrEmpty(_switchPrepay))
                {
                    _switchPrepay = Helper.GetResourceString(nameof(SwitchPrepay),
                        Language);
                }
                return _switchPrepay;
            }
            set { _switchPrepay = value; }
        }

        public static string DeletePrepay
        {
            get
            {
                if (string.IsNullOrEmpty(_deletePrepay))
                {
                    _deletePrepay = Helper.GetResourceString(nameof(DeletePrepay),
                        Language);
                }
                return _deletePrepay;
            }
            set { _deletePrepay = value; }
        }

        public static string Manual
        {
            get
            {
                if (string.IsNullOrEmpty(_manual))
                {
                    _manual = Helper.GetResourceString(nameof(Manual),
                        Language);
                }
                return _manual;
            }
            set { _manual = value; }
        }

        public static string Prepay
        {
            get
            {
                if (string.IsNullOrEmpty(_prepay))
                {
                    _prepay = Helper.GetResourceString(nameof(Prepay),
                        Language);
                }
                return _prepay;
            }
            set { _prepay = value; }
        }

        public static string Finish
        {
            get
            {
                if (string.IsNullOrEmpty(_finish))
                {
                    _finish = Helper.GetResourceString(nameof(Finish),
                        Language);
                }
                return _finish;
            }
            set
            {
                _finish = value;
            }
        }

        public static string InitializeMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_initializeMessage))
                {
                    _initializeMessage = Helper.GetResourceString(nameof(InitializeMessage),
                        Language);
                }
                return _initializeMessage;
            }
            set { _initializeMessage = value; }
        }

        public static string NegativeDiscountsNotAllowed
        {
            get
            {
                if (string.IsNullOrEmpty(_negativeDiscountsNotAllowed))
                {
                    _negativeDiscountsNotAllowed = Helper.GetResourceString(nameof(NegativeDiscountsNotAllowed),
                        Language);
                }
                return _negativeDiscountsNotAllowed;
            }
            set
            {
                string _clearErrorFile = value;
            }
        }

        public static string ClearErrorFile
        {
            get
            {
                if (string.IsNullOrEmpty(_clearErrorFile))
                {
                    _clearErrorFile = Helper.GetResourceString(nameof(ClearErrorFile),
                        Language);
                }
                return _clearErrorFile;
            }
            set
            {
                string _clearErrorFile = value;
            }
        }

        public static string PumpTestConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_pumpTestConfirmation))
                {
                    _pumpTestConfirmation = Helper.GetResourceString(nameof(PumpTestConfirmation),
                        Language);
                }
                return _pumpTestConfirmation;
            }
            set { _pumpTestConfirmation = value; }
        }

        public static string TaxExemptionNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_taxExemptionNumber))
                {
                    _taxExemptionNumber = Helper.GetResourceString(nameof(TaxExemptionNumber),
                        Language);
                }
                return _taxExemptionNumber;
            }
            set { _taxExemptionNumber = value; }
        }

        public static string PORequired
        {
            get
            {
                if (string.IsNullOrEmpty(_poRequired))
                {
                    _poRequired = Helper.GetResourceString(nameof(PORequired),
                        Language);
                }
                return _poRequired;
            }
            set { _poRequired = value; }
        }

        public static string PromptRequired
        {
            get
            {
                if (string.IsNullOrEmpty(_promptRequired))
                {
                    _promptRequired = Helper.GetResourceString(nameof(PromptRequired),
                        Language);
                }
                return _promptRequired;
            }
            set { _promptRequired = value; }
        }

        public static string MissingRequired
        {
            get
            {
                if (string.IsNullOrEmpty(_missingRequired))
                {
                    _missingRequired = Helper.GetResourceString(nameof(MissingRequired),
                        Language);
                }
                return _missingRequired;
            }
            set { _missingRequired = value; }
        }

        public static string InvalidPin
        {
            get
            {
                if (string.IsNullOrEmpty(_invalidPin))
                {
                    _invalidPin = Helper.GetResourceString(nameof(InvalidPin),
                        Language);
                }
                return _invalidPin;
            }
            set { _invalidPin = value; }
        }

        public static string EnterPinNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_enterPinNumber))
                {
                    _enterPinNumber = Helper.GetResourceString(nameof(EnterPinNumber),
                        Language);
                }

                return _enterPinNumber;
            }
            set { _enterPinNumber = value; }
        }

        public static string PropaneGrade
        {
            get
            {
                if (string.IsNullOrEmpty(_propaneGrade))
                {
                    _propaneGrade = Helper.GetResourceString(nameof(PropaneGrade),
                        Language);
                }
                return _propaneGrade;
            }
            set { _propaneGrade = value; }
        }

        public static string TierLevel
        {
            get
            {
                if (string.IsNullOrEmpty(_tierLevel))
                {
                    _tierLevel = Helper.GetResourceString(nameof(TierLevel),
                        Language);
                }
                return _tierLevel;
            }
            set { _tierLevel = value; }
        }

        public static string FuelPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_fuelPrice))
                {
                    _fuelPrice = Helper.GetResourceString(nameof(FuelPrice),
                        Language);
                }
                return _fuelPrice;
            }
            set { _fuelPrice = value; }
        }

        public static string PumpOptions
        {
            get
            {
                if (string.IsNullOrEmpty(_pumpOptions))
                {
                    _pumpOptions = Helper.GetResourceString(nameof(PumpOptions),
                        Language);
                }
                return _pumpOptions;
            }
            set { _pumpOptions = value; }
        }

        public static string NoReportFound
        {
            get
            {
                if (string.IsNullOrEmpty(_noReportFound))
                {
                    _noReportFound = Helper.GetResourceString(nameof(NoReportFound),
                        Language);
                }
                return _noReportFound;
            }
            set { _noReportFound = value; }
        }

        public static string MessageConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_messageConfirmation))
                {
                    _messageConfirmation = Helper.GetResourceString(nameof(MessageConfirmation),
                        Language);
                }
                return _messageConfirmation;
            }
            set
            {
                _messageConfirmation = value;
            }
        }

        public static string DipReadingWarning
        {
            get
            {
                if (string.IsNullOrEmpty(_dipReadingWarning))
                {
                    _dipReadingWarning = Helper.GetResourceString(nameof(DipReadingWarning), Language);
                }
                return _dipReadingWarning;
            }
            set { _dipReadingWarning = value; }
        }

        public static string StoreCreditConfirmationMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_storeCreditConfiramtionMessage))
                {
                    _storeCreditConfiramtionMessage = Helper.GetResourceString(nameof(StoreCreditConfirmationMessage), Language);
                }
                return _storeCreditConfiramtionMessage;
            }
            set
            {
                _storeCreditConfiramtionMessage = value;
            }
        }

        public static string InvalidPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_invalidPassword))
                {
                    _invalidPassword = Helper.GetResourceString(nameof(InvalidPassword), Language);
                }

                return _invalidPassword;
            }
            set
            {
                _invalidPassword = value;
            }
        }

        public static string MaximumPriceError
        {
            get
            {
                if (string.IsNullOrEmpty(_maximumPriceError))
                {
                    _maximumPriceError = Helper.GetResourceString(nameof(MaximumPriceError), Language);
                }
                return _maximumPriceError;
            }
            set
            {
                _maximumPriceError = value;
            }
        }

        public static string ReprintText
        {
            get
            {
                if (string.IsNullOrEmpty(_reprintText))
                {
                    _reprintText = Helper.GetResourceString(nameof(ReprintText), Language);
                }
                return _reprintText;
            }
            set { _reprintText = value; }
        }

        public static string LastPrint
        {
            get
            {
                if (string.IsNullOrEmpty(_lastPrint))
                {
                    _lastPrint = Helper.GetResourceString(nameof(LastPrint), Language);
                }
                return _lastPrint;
            }
            set
            {
                _lastPrint = value;
            }
        }

        public static string Reprint
        {
            get
            {
                if (string.IsNullOrEmpty(_reprint))
                {
                    _reprint = Helper.GetResourceString(nameof(Reprint), Language);
                }
                return _reprint;
            }
            set
            {
                _reprint = value;
            }
        }

        public static string RunAwayConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_enterAValue))
                {
                    _runAwayConfirmation = Helper.GetResourceString(nameof(RunAwayConfirmation), Language);
                }
                return _runAwayConfirmation;
            }
            set
            {
                _runAwayConfirmation = value;
            }
        }

        internal const string TillsFileName = "TillNumbers.txt";
        internal const string CacheFolderName = "OfflineData";

        internal static string Language = "English";

        public static SolidColorBrush ButtonConfirmationColor;
        public static SolidColorBrush ButtonWarningColor;
        public static SolidColorBrush ButtonFooterColor;

        public static string DeleteSaleLine
        {
            get
            {
                if (string.IsNullOrEmpty(_deleteSaleLine))
                {
                    _deleteSaleLine = Helper.GetResourceString(nameof(DeleteSaleLine), Language);
                }
                return _deleteSaleLine;
            }
            set
            {
                _deleteSaleLine = value;
            }
        }

        public static string EnterAValue
        {
            get
            {
                if (string.IsNullOrEmpty(_enterAValue))
                {
                    _enterAValue = Helper.GetResourceString(nameof(EnterAValue), Language);
                }
                return _enterAValue;
            }
            set { _enterAValue = value; }
        }

        public static string Percent
        {
            get
            {
                if (string.IsNullOrEmpty(_percent))
                {
                    _percent = Helper.GetResourceString(nameof(Percent), Language);
                }
                return _percent;
            }
            set { _percent = value; }
        }

        public static string Prices
        {
            get
            {
                if (string.IsNullOrEmpty(_price))
                {
                    _price = Helper.GetResourceString(nameof(Prices), Language);
                }
                return _price;
            }
            set { _price = value; }
        }

        public static string MaximumTenderValue
        {
            get
            {
                if (string.IsNullOrEmpty(_maximumTenderValue))
                {
                    _maximumTenderValue = Helper.GetResourceString(nameof(MaximumTenderValue), Language);
                }
                return _maximumTenderValue;
            }
            set
            {
                _maximumTenderValue = value;
            }
        }

        public static string SafeDrop
        {
            get
            {
                if (string.IsNullOrEmpty(_safeDrop))
                {
                    _safeDrop = Helper.GetResourceString(nameof(SafeDrop), Language);
                }
                return _safeDrop;
            }
            set { _safeDrop = value; }
        }

        public static string ATMDrop
        {
            get
            {
                if (string.IsNullOrEmpty(_atmDrop))
                {
                    _atmDrop = Helper.GetResourceString(nameof(ATMDrop), Language);
                }
                return _atmDrop;
            }
            set { _atmDrop = value; }
        }

        public static string CashDropType
        {
            get
            {
                if (string.IsNullOrEmpty(_cashDropType))
                {
                    _cashDropType = Helper.GetResourceString(nameof(CashDropType), Language);
                }
                return _cashDropType;
            }
            set { _cashDropType = value; }
        }

        public static string CashDraw
        {
            get
            {
                if (string.IsNullOrEmpty(_cashDraw))
                {
                    _cashDraw = Helper.GetResourceString(nameof(CashDraw), Language);
                }
                return _cashDraw;
            }
            set { _cashDraw = value; }
        }

        public static string CashDrop
        {
            get
            {
                if (string.IsNullOrEmpty(_cashDrop))
                {
                    _cashDrop = Helper.GetResourceString(nameof(CashDrop), Language);
                }
                return _cashDrop;
            }
            set { _cashDrop = value; }
        }

        public static string Cash
        {
            get
            {
                if (string.IsNullOrEmpty(_cash))
                {
                    _cash = Helper.GetResourceString(nameof(Cash), Language);
                }
                return _cash;
            }
            set { _cash = value; }
        }

        public static string LoyalityNumberConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_loyalityNumberConfirmation))
                {
                    _loyalityNumberConfirmation = Helper.GetResourceString(nameof(LoyalityNumberConfirmation), Language);
                }
                return _loyalityNumberConfirmation;
            }
            set { _loyalityNumberConfirmation = value; }
        }

        public static string ShiftConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_shiftConfirmation))
                {
                    _shiftConfirmation = Helper.GetResourceString(nameof(ShiftConfirmation), Language);
                }
                return _shiftConfirmation;
            }
            set { _shiftConfirmation = value; }
        }

        public static string CannotUnsuspend
        {
            get
            {
                if (string.IsNullOrEmpty(_cannotUnsuspend))
                {
                    _cannotUnsuspend = Helper.GetResourceString(nameof(CannotUnsuspend), Language);
                }
                return _cannotUnsuspend;
            }
            set { _cannotUnsuspend = value; }
        }

        public static string DiscountType
        {
            get
            {
                if (string.IsNullOrEmpty(_discountType))
                {
                    _discountType = Helper.GetResourceString(nameof(DiscountType), Language);
                }
                return _discountType;
            }
            set { _discountType = value; }
        }

        public static string ApiNotConnected
        {
            get
            {
                if (string.IsNullOrEmpty(_apiNotConnected))
                {
                    _apiNotConnected = Helper.GetResourceString(nameof(ApiNotConnected), Language);
                }
                return _apiNotConnected;
            }
            set { _apiNotConnected = value; }
        }

        public static string LanguageNotSupported
        {
            get
            {
                if (string.IsNullOrEmpty(_languageNotSupported))
                {
                    _languageNotSupported = Helper.GetResourceString(nameof(LanguageNotSupported), Language);
                }
                return _languageNotSupported;
            }
            set { _languageNotSupported = value; }
        }

        public static string Confirm
        {
            get
            {
                if (string.IsNullOrEmpty(_confirm))
                {
                    _confirm = Helper.GetResourceString(nameof(Confirm), Language);
                }
                return _confirm;
            }
            set { _confirm = value; }
        }

        public static string Cancel
        {
            get
            {
                if (string.IsNullOrEmpty(_cancel))
                {
                    _cancel = Helper.GetResourceString(nameof(Cancel), Language);
                }
                return _cancel;
            }
            set { _cancel = value; }
        }

        public static string VoidSale
        {
            get
            {
                if (string.IsNullOrEmpty(_voidSale))
                {
                    _voidSale = Helper.GetResourceString(nameof(VoidSale), Language);
                }
                return _voidSale;
            }
            set { _voidSale = value; }
        }

        public static string GiveX
        {
            get
            {
                if (string.IsNullOrEmpty(_giveX))
                {
                    _giveX = Helper.GetResourceString(nameof(GiveX), Language);
                }
                return _giveX;
            }
            set { _giveX = value; }
        }

        public static string LoyaltyGift
        {
            get
            {
                if (string.IsNullOrEmpty(_loyaltyGift))
                {
                    _loyaltyGift = Helper.GetResourceString(nameof(LoyaltyGift), Language);
                }
                return _loyaltyGift;
            }
            set { _loyaltyGift = value; }
        }

        public static string ResumeAll
        {
            get
            {
                if (string.IsNullOrEmpty(_resumeAll))
                {
                    ResumeAll = Helper.GetResourceString(nameof(ResumeAll), Language);
                }
                return _resumeAll;
            }
            set { _resumeAll = value; }
        }

        public static string StopAll
        {
            get
            {
                if (string.IsNullOrEmpty(_stopAll))
                {
                    _stopAll = Helper.GetResourceString(nameof(StopAll), Language);
                }
                return _stopAll;
            }
            set { _stopAll = value; }
        }

        public static string LoyaltySearch
        {
            get
            {
                if (string.IsNullOrEmpty(_loyaltySearch))
                {
                    _loyaltySearch = Helper.GetResourceString(nameof(LoyaltySearch), Language);
                }
                return _loyaltySearch;
            }
            set { _loyaltySearch = value; }
        }

        public static string CustomerSearch
        {
            get
            {
                if (string.IsNullOrEmpty(_customerSearch))
                {
                    _customerSearch = Helper.GetResourceString(nameof(CustomerSearch), Language);
                }

                return _customerSearch;
            }
            set { _customerSearch = value; }
        }

        public static string SomethingBadHappned
        {
            get
            {
                if (string.IsNullOrEmpty(_somethingBadHappned))
                {
                    _somethingBadHappned = Helper.GetResourceString(nameof(SomethingBadHappned), Language);
                }
                return _somethingBadHappned;
            }
            set { _somethingBadHappned = value; }
        }

        public static string MaxLoginAttemptsReached
        {
            get
            {
                if (string.IsNullOrEmpty(_maxLoginAttemptsReached))
                {
                    _maxLoginAttemptsReached = Helper.GetResourceString(nameof(MaxLoginAttemptsReached), Language);
                }

                return _maxLoginAttemptsReached;
            }
            set { _maxLoginAttemptsReached = value; }
        }

        public static string ShiftUsedForTheDay
        {
            get
            {
                if (string.IsNullOrEmpty(_shiftUsedForTheDay))
                {
                    _shiftUsedForTheDay = Helper.GetResourceString(nameof(ShiftUsedForTheDay), Language);
                }
                return _shiftUsedForTheDay;
            }
            set { _shiftUsedForTheDay = value; }
        }

        public static string PumpEmergency
        {
            get
            {
                if (string.IsNullOrEmpty(_pumpEmergency))
                {
                    _pumpEmergency = Helper.GetResourceString(nameof(PumpEmergency), Language);
                }


                return _pumpEmergency;
            }
            set { _pumpEmergency = value; }
        }

        public static string Customer
        {
            get
            {
                if (string.IsNullOrEmpty(_customer))
                {
                    _customer = Helper.GetResourceString(nameof(Customer), Language);
                }
                return _customer;
            }
            set { _customer = value; }
        }

        public static string Confirmed
        {
            get
            {
                if (string.IsNullOrEmpty(_confirmed))
                {
                    _confirmed = Helper.GetResourceString(nameof(Confirmed), Language);
                }
                return _confirmed;
            }
            set { _confirmed = value; }
        }

        public static string NotConfirmed
        {
            get
            {
                if (string.IsNullOrEmpty(_notConfirmed))
                {
                    _notConfirmed = Helper.GetResourceString(nameof(NotConfirmed), Language);
                }
                return _notConfirmed;
            }
            set { _notConfirmed = value; }
        }

        public static string GiftCardZeroPriceError
        {
            get
            {
                if (string.IsNullOrEmpty(_giftCardZeroPriceError))
                {
                    _giftCardZeroPriceError = Helper.GetResourceString(nameof(GiftCardZeroPriceError), Language);
                }
                return _giftCardZeroPriceError;
            }
        }

        public static string ApiTimeoutMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_apiTimeoutMessage))
                {
                    _apiTimeoutMessage = Helper.GetResourceString(nameof(ApiTimeoutMessage), Language);
                }
                return _apiTimeoutMessage;
            }
        }

        public static string WriteOffConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_writeOffConfirmation))
                {
                    _writeOffConfirmation = Helper.GetResourceString(nameof(WriteOffConfirmation), Language);
                }
                return _writeOffConfirmation;
            }
        }

        public static string NoPrinterFound
        {
            get
            {
                if (string.IsNullOrEmpty(_noPrinterFound))
                {
                    _noPrinterFound = Helper.GetResourceString(nameof(NoPrinterFound), Language);
                }
                return _noPrinterFound;
            }
        }

        public static string TenderMaxAmountConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_tenderMaxAmountConfirmation))
                {
                    _tenderMaxAmountConfirmation = Helper.GetResourceString(nameof(TenderMaxAmountConfirmation), Language);
                }
                return _tenderMaxAmountConfirmation;
            }
        }

        public static string GiftCertificateConfirmationMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_giftCertificateConfirmationMessage))
                {
                    _giftCertificateConfirmationMessage = Helper.GetResourceString(nameof(GiftCertificateConfirmationMessage), Language);
                }
                return _giftCertificateConfirmationMessage;
            }
        }

        public static string GiftCertificateExpiredMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_giftCertificateExpiredMessage))
                {
                    _giftCertificateExpiredMessage = Helper.GetResourceString(nameof(GiftCertificateExpiredMessage), Language);
                }
                return _giftCertificateExpiredMessage;
            }
        }

        public static string GiftCertificateNotFound
        {
            get
            {
                if (string.IsNullOrEmpty(_giftCertificateNotFound))
                {
                    _giftCertificateNotFound = Helper.GetResourceString(nameof(GiftCertificateNotFound), Language);
                }
                return _giftCertificateNotFound;
            }
        }

        public static string OverrideLimitMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_overrideLimitMessage))
                {
                    _overrideLimitMessage = Helper.GetResourceString(nameof(OverrideLimitMessage), Language);
                }
                return _overrideLimitMessage;
            }
        }

        public static string ReprintDisabled
        {
            get
            {
                if (string.IsNullOrEmpty(_reprintDisabled))
                {
                    _reprintDisabled = Helper.GetResourceString(nameof(ReprintDisabled), Language);
                }
                return _reprintDisabled;
            }
        }

        public static string Yes
        {
            get
            {
                if (string.IsNullOrEmpty(_yes))
                {
                    _yes = Helper.GetResourceString(nameof(Yes), Language);
                }
                return _yes;
            }
        }

        public static string No
        {
            get
            {
                if (string.IsNullOrEmpty(_no))
                {
                    _no = Helper.GetResourceString(nameof(No), Language);
                }
                return _no;
            }
        }

        public static string Ok
        {
            get
            {
                if (string.IsNullOrEmpty(_ok))
                {
                    _ok = Helper.GetResourceString(nameof(Ok), Language);
                }
                return _ok;
            }
        }

        public static string IncrementalPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_incrementalPrice))
                {
                    _incrementalPrice = Helper.GetResourceString(nameof(IncrementalPrice), Language);
                }
                return _incrementalPrice;
            }
        }

        public static string XPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_xForPrice))
                {
                    _xForPrice = Helper.GetResourceString(nameof(XPrice), Language);
                }
                return _xForPrice;
            }
        }

        public static string FirstUnitPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_firstUnitPrice))
                {
                    _firstUnitPrice = Helper.GetResourceString(nameof(FirstUnitPrice), Language);
                }
                return _firstUnitPrice;
            }
        }

        public static string SalePrice
        {
            get
            {
                if (string.IsNullOrEmpty(_salePrice))
                {
                    _salePrice = Helper.GetResourceString(nameof(SalePrice), Language);
                }
                return _salePrice;
            }
        }

        public static string RegularPrice
        {
            get
            {
                if (string.IsNullOrEmpty(_regularPrice))
                {
                    _regularPrice = Helper.GetResourceString(nameof(RegularPrice), Language);
                }
                return _regularPrice;
            }
        }

        public static string SettingInProgress
        {
            get
            {
                if (string.IsNullOrEmpty(_settingInProgress))
                {
                    _settingInProgress = Helper.GetResourceString(nameof(SettingInProgress), Language);
                }
                return _settingInProgress;
            }
        }

        public static string SetPrepayTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_setPrepayTitle))
                {
                    _setPrepayTitle = Helper.GetResourceString(nameof(SetPrepayTitle), Language);
                }
                return _setPrepayTitle;
            }
        }

        public static string SwitchPrepayTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_switchPrepayTitle))
                {
                    _switchPrepayTitle = Helper.GetResourceString(nameof(SwitchPrepayTitle), Language);
                }
                return _switchPrepayTitle;
            }
        }

        public static string UserCannotPerformManual
        {
            get
            {
                if (string.IsNullOrEmpty(_userCannotPerformManual))
                {
                    _userCannotPerformManual = Helper.GetResourceString(nameof(UserCannotPerformManual), Language);
                }
                return _userCannotPerformManual;
            }
        }

        public static string NoCustomerDisplayFound
        {
            get
            {
                if (string.IsNullOrEmpty(_noCustomerDisplayFound))
                {
                    _noCustomerDisplayFound = Helper.GetResourceString(nameof(NoCustomerDisplayFound), Language);
                }
                return _noCustomerDisplayFound;
            }
        }

        public static string NoCashDrawerFound
        {
            get
            {
                if (string.IsNullOrEmpty(_noCashDrawerFound))
                {
                    _noCashDrawerFound = Helper.GetResourceString(nameof(NoCashDrawerFound), Language);
                }
                return _noCashDrawerFound;
            }
        }

        public static string SignaturePadNotConnected
        {
            get
            {
                if (string.IsNullOrEmpty(_signaturePadNotConnected))
                {
                    _signaturePadNotConnected = Helper.GetResourceString(nameof(SignaturePadNotConnected), Language);
                }
                return _signaturePadNotConnected;
            }
        }

        public static string RegularPriceText
        {
            get
            {
                if (string.IsNullOrEmpty(_regularPriceText))
                {
                    _regularPriceText = Helper.GetResourceString(nameof(RegularPriceText), Language);
                }
                return _regularPriceText;
            }
        }

        public static string NoSignatureConfirmation
        {
            get
            {
                if (string.IsNullOrEmpty(_noSignatureConfirmation))
                {
                    _noSignatureConfirmation = Helper.GetResourceString(nameof(NoSignatureConfirmation), Language);
                }
                return _noSignatureConfirmation;
            }
        }

        public static string AcceptTheSignature
        {
            get
            {
                if (string.IsNullOrEmpty(_acceptTheSignature))
                {
                    _acceptTheSignature = Helper.GetResourceString(nameof(AcceptTheSignature), Language);
                }
                return _acceptTheSignature;
            }
        }
    }
}
