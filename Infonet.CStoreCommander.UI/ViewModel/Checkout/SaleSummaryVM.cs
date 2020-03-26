using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.PeripheralLayer;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Checkout;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class SaleSummaryVM : VMBase
    {
        #region Private variables
        private SaleSummaryModel _saleSummary;
        private TenderModel _selectedTender;
        private Reasons _reasonForOpenCashDrawer = new Reasons();
        private CompletePaymentBase _paymentCompleteBase;
        private CompletePayment _completePayment;
        private CommonPaymentComplete _commonPaymentComplete;
        private ObservableCollection<TenderModel> _tenders;
        private ObservableCollection<SaleSummaryLineModel> _lines;
        private ObservableCollection<Model.Cash.CashButtonModel> _cashButtons;
        private DispatcherTimer _changeDueAppTimer;
        private bool _isPaymentCompleteByOtherMode;
        private int _timeRemaining;
        private decimal? _previousAmount;
        private string _previousPurchaseOrderNumber;
        private string _issueStoreCreditMessage;
        private bool _isAccountPaymentPriorAuthorization;
        private bool _wasPrintEnabled;
        private bool _wasPrintOn;
        private bool _isPumpTestOn;
        private bool _isPumpTestEnabled;
        private string _outStandingAmount;

        private bool _completePaymentEnabled;
        private bool _displayNoReceiptButton;
        private bool _isRunAwayEnabled;
        private bool _runAway;
        private bool _isPrintEnabled;
        private bool _print;
        private string _transactionType;
        private bool _isOverrideARLimit;
        private VerifyByAccount _verifyByAccount;

        private decimal? _amountEntered;
        private string _outstandingAmount;
        private string _purchaseOrderNumber;
        private string _testCard;
        private CardSwipeInformation _fleetCardResponse;
        #endregion

        #region Commands
        public RelayCommand CompleteSaleCommand { get; set; }
        public RelayCommand<object> OpenNumberPadCommand { get; set; }
        public RelayCommand<object> SetTenderAmountCommand { get; set; }
        public RelayCommand CancelTenderCommand { get; set; }
        public RelayCommand<object> OpenCashDrawerReasonSelectedCommand { get; private set; }
        public RelayCommand PurchaseOrderNumberEnteredCommand { get; set; }
        public RelayCommand ClosePurchaseOrderNumberEnteredCommand { get; set; }
        public RelayCommand TestCardEnteredCommand { get; set; }
        #endregion

        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly ICashBusinessLogic _cashBusinessLogic;
        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        #region Properties
        private string _cardNumber;
        private bool _isDeletePrepay;
        private bool _isSignatureDone;

        public string PurchaseOrderNumber
        {
            get { return _purchaseOrderNumber; }
            set
            {
                _purchaseOrderNumber = value;
                RaisePropertyChanged(nameof(PurchaseOrderNumber));
            }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                if (NavigateService.Instance.SecondFrame.Content.GetType().Name == "Tender")
                {
                    _cardNumber = value;
                    Log.Info(string.Format("Card number swiped is: {0}", value));
                    Log.Info(string.Format("Card number used for processing is: {0}", new Helper().GetTrack2Data(value)));
                    if (!string.IsNullOrEmpty(_cardNumber))
                    {
                        PerformTransaction();
                    }
                }
            }
        }

        public bool IsPumpTestEnabled
        {
            get { return _isPumpTestEnabled; }
            set
            {
                if (_isPumpTestEnabled != value)
                {
                    _isPumpTestEnabled = value;
                    RaisePropertyChanged(nameof(IsPumpTestEnabled));
                }
            }
        }

        public bool IsPumpTestOn
        {
            get { return _isPumpTestOn; }
            set
            {
                if (value != _isPumpTestOn)
                {
                    _isPumpTestOn = value;

                    if (IsRunAwayEnabled && RunAway && _isPumpTestOn)
                    {
                        RunAway = false;
                    }
                    CompletePaymentEnabled = value;
                    EnableOrDisableTenders();
                    SetPrintToggleValues(!_isPumpTestOn);
                    RaisePropertyChanged(nameof(IsPumpTestOn));
                }
            }
        }

        public bool RunAway
        {
            get { return _runAway; }
            set
            {
                if (value != _runAway)
                {
                    _runAway = value;

                    if (IsPumpTestEnabled && IsPumpTestOn && _runAway)
                    {
                        IsPumpTestOn = false;
                    }
                    CompletePaymentEnabled = value;
                    EnableOrDisableTenders();
                    SetPrintToggleValues(!_runAway);
                    RaisePropertyChanged(nameof(RunAway));
                }
            }
        }

        public ObservableCollection<Model.Cash.CashButtonModel> CashButtons
        {
            get { return _cashButtons; }
            set
            {
                _cashButtons = value;
                RaisePropertyChanged(nameof(CashButtons));
            }

        }

        public SaleSummaryModel SaleSummary
        {
            get { return _saleSummary; }
            set
            {
                _saleSummary = value;
                RaisePropertyChanged(nameof(SaleSummary));
            }
        }

        public ObservableCollection<TenderModel> Tenders
        {
            get { return _tenders; }
            set
            {
                _tenders = value;
                RaisePropertyChanged(nameof(Tenders));
            }
        }

        public bool CompletePaymentEnabled
        {
            get { return _completePaymentEnabled; }
            set
            {
                _completePaymentEnabled = value;
                RaisePropertyChanged(nameof(CompletePaymentEnabled));
            }
        }

        public bool DisplayNoReceiptButton
        {
            get { return _displayNoReceiptButton; }
            set
            {
                _displayNoReceiptButton = value;
                RaisePropertyChanged(nameof(DisplayNoReceiptButton));
            }
        }

        public TenderModel SelectedTender
        {
            get { return _selectedTender; }
            set
            {
                _selectedTender = value;
                RaisePropertyChanged(nameof(SelectedTender));
            }
        }

        public ObservableCollection<SaleSummaryLineModel> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                RaisePropertyChanged(nameof(Lines));
            }
        }

        public bool IsRunAwayEnabled
        {
            get { return _isRunAwayEnabled; }
            set
            {
                _isRunAwayEnabled = value;
                if (_isRunAwayEnabled == false)
                {
                    RunAway = false;
                }
                RaisePropertyChanged(nameof(IsRunAwayEnabled));
            }
        }

        public bool IsPrintEnabled
        {
            get { return _isPrintEnabled; }
            set
            {
                _isPrintEnabled = value;
                RaisePropertyChanged(nameof(IsPrintEnabled));
            }
        }

        public bool Print
        {
            get { return _print; }
            set
            {
                _print = value;
                RaisePropertyChanged(nameof(Print));
            }
        }

        public string TestCard
        {
            get
            {
                return _testCard;
            }
            set
            {
                if (_testCard != value)
                {
                    _testCard = value;
                    RaisePropertyChanged(nameof(TestCard));
                }
            }
        }
        #endregion

        public SaleSummaryVM(IReasonListBussinessLogic reasonListBussinessLogic,
            ICheckoutBusinessLogic checkoutBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic,
             ICashBusinessLogic cashBussinessLogic,
             ICacheBusinessLogic cacheBusinessLogic
            )
        {
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _checkoutBusinessLogic = checkoutBusinessLogic;
            _cashBusinessLogic = cashBussinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            _cacheBusinessLogic = cacheBusinessLogic;
            InitializeCommands();
            RegisterMessages();
            CashButtons = new ObservableCollection<Model.Cash.CashButtonModel>();
            SaleSummary = new SaleSummaryModel();
            PerformAction(GetCashButtons);
        }


        private void InitializeCommands()
        {
            OpenCashDrawerReasonSelectedCommand = new RelayCommand<object>(OpenCashDrawerReasonSelected);
            CancelTenderCommand = new RelayCommand(() => PerformAction(CancelTenders));
            CompleteSaleCommand = new RelayCommand(() => CompletePayment(null));
            SetTenderAmountCommand = new RelayCommand<object>((s) => SetTenderAmount(s));
            OpenNumberPadCommand = new RelayCommand<object>((s) => OpenNumberPadForTenderItem(s));
            PurchaseOrderNumberEnteredCommand = new RelayCommand(PurchaseOrderNumberEntered);
            ClosePurchaseOrderNumberEnteredCommand = new RelayCommand(ClosePurchaseOrderPopup);
            TestCardEnteredCommand = new RelayCommand(TestCardEntered);
        }

        private void TestCardEntered()
        {
            CardNumber = TestCard;
            TestCard = string.Empty;
        }

        private async Task CancelTenders()
        {
            if (_isDeletePrepay)
            {
                _transactionType = TransactionType.DeletePrepay.ToString();
            }
            var response = await _checkoutBusinessLogic.CancelTenders(_transactionType);

            var sale = response.ToModel();
            MessengerInstance.Send(sale, "UpdateSale");

            NavigateService.Instance.NavigateToHome();
            MessengerInstance.Send<bool>(true, "UserNavigatedToSaleSummaryPage");
            MessengerInstance.Unregister<string>(this, "CurrencyTapped", CurrencyTapped);
            MessengerInstance.Send<bool>(true, "EnableHamburgerIcon");
        }

        private async Task GetCashButtons()
        {
            var response = await _cashBusinessLogic.GetCashButtons();
            CashButtons.Clear();
            foreach (var button in response)
            {
                CashButtons.Add(new Model.Cash.CashButtonModel
                {
                    Button = button.Button,
                    Value = button.Value
                });
            }
        }

        private void CompletePayment(Uri signature)
        {
            // See if we have to collect signature from user
            if (!_isSignatureDone && !RunAway && !IsPumpTestOn && CacheBusinessLogic.RequireSignature)
            {
                NavigateService.Instance.NavigateToSignatureCapture();
            }
            else
            {
                PerformAction(async () =>
                {
                    if (RunAway)
                    {
                        ShowConfirmationMessage(ApplicationConstants.RunAwayConfirmation, CompletePaymentByRunAway);
                    }
                    else if (IsPumpTestOn)
                    {
                        ShowConfirmationMessage(ApplicationConstants.PumpTestConfirmation, CompleteByPumpTest);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_issueStoreCreditMessage))
                        {
                            ShowConfirmationMessage(_issueStoreCreditMessage,
                                async () =>
                                {
                                    await CompletePayment(true, signature);
                                },
                                async () =>
                                {
                                    await CompletePayment(false, signature);
                                },
                                async () =>
                                {
                                    await CompletePayment(false, signature);
                                });
                        }
                        else
                        {
                            await CompletePayment(false, signature);
                        }

                    }
                });
            }
        }

        private void CompleteByPumpTest()
        {
            PerformAction(async () =>
            {
                _paymentCompleteBase = _commonPaymentComplete = await _checkoutBusinessLogic.PumpTest();
                await ProcessCompletePayment(false, null);
            });
        }

        private async Task CompletePayment(bool issueStoreCreditMessage, Uri signature)
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();
                try
                {
                    if (_isDeletePrepay)
                    {
                        _transactionType = "Delete Prepay";
                    }

                    _paymentCompleteBase = _completePayment = await
                     _checkoutBusinessLogic.CompletePayment(_transactionType, issueStoreCreditMessage);
                    _isSignatureDone = false;
                    _isPaymentCompleteByOtherMode = false;
                    //Added by Tony 07/30/2019
                    if (_cacheBusinessLogic.RECEIPT_TYPE.ToUpper() != "DEFAULT")
                    {
                        _completePayment.Receipts = Helper.ReceiptLabelMapping(_completePayment.Receipts, _cacheBusinessLogic.RECEIPT_TYPE);
                    }
                    //Displaying the first message now 
                    WriteToLineDisplay(_paymentCompleteBase.LineDisplay?[0]);

                    if (!string.IsNullOrEmpty(_completePayment.KickabckServerError))
                    {
                        ShowNotification(_completePayment.KickabckServerError,
                          async () =>
                          {
                              await ProcessLimitExcededMessage(signature);
                          },
                           async () =>
                           {
                               await ProcessLimitExcededMessage(signature);
                           },
                           ApplicationConstants.ButtonWarningColor);
                    }
                    else
                    {
                        await ProcessLimitExcededMessage(signature);
                    }
                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in complete payment is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private async Task ProcessLimitExcededMessage(Uri signature)
        {
            if (!string.IsNullOrEmpty(_paymentCompleteBase.LimitExceedMessage))
            {
                ShowNotification(_paymentCompleteBase.LimitExceedMessage,
                   async () =>
                   {
                       await ProcessCompletePayment(false, signature);
                   },
                    async () =>
                    {
                        await ProcessCompletePayment(false, signature);
                    },
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                await ProcessCompletePayment(false, signature);
            }
        }

        internal void CompletePaymentByRunAway()
        {
            PerformAction(async () =>
            {
                _paymentCompleteBase = _commonPaymentComplete = await _checkoutBusinessLogic.RunAway();
                await ProcessCompletePayment(true, null);
            });
        }

        private async Task ProcessCompletePayment(bool isRunAway, Uri signature)
        {
            Action processPayment = () =>
            {
                if (_paymentCompleteBase.OpenCashDrawer)
                {
                    base.OpenCashDrawer();
                }
                PostPaymentProcess(isRunAway);
                if (_paymentCompleteBase?.LineDisplay?.Count > 1)
                {
                    WriteToLineDisplay(_paymentCompleteBase.LineDisplay?[1]);
                }
            };

            var isPrinterFound = true;

            try
            {
                if (Print)
                {
                    isPrinterFound = await CheckIfPrinterExists(() =>
                    {
                        ProceedWithPostPayment(processPayment);
                    });

                    if (isPrinterFound)
                    {
                        if (_isPaymentCompleteByOtherMode)
                        {
                            var reportContent = await GetReportContent(isRunAway);
                            PerformPrint(reportContent, _commonPaymentComplete.Receipt.Copies, true, signature);
                        }
                        else
                        {
                            PerformPrint(_completePayment.Receipts, 1, signature);
                        }
                    }
                }
            }
            finally
            {
                if (isPrinterFound)
                {
                    ProceedWithPostPayment(processPayment);
                }
            }
        }

        private void ProceedWithPostPayment(Action processPayment)
        {
            if (!string.IsNullOrEmpty(_paymentCompleteBase.ChangeDue))
            {
                ShowNotification(_paymentCompleteBase.ChangeDue,
                   () =>
                   {
                       processPayment();
                       ResetDispatcher();
                   },
                    () =>
                    {
                        processPayment();
                        ResetDispatcher();
                    },
                    ApplicationConstants.ButtonConfirmationColor);
                StartTimer();
            }
            else
            {
                processPayment();
            }
        }

        private void PostPaymentProcess(bool isRunAway)
        {
            MessengerInstance.Send(_paymentCompleteBase.Sale.ToModel(), "UpdateSale");

            // Deleting all the previous signatures once new sale is initlaized
            new Helper().DeleteAllSignatures();
            MessengerInstance.Unregister<string>(this, "CurrencyTapped", CurrencyTapped);
            MessengerInstance.Send<bool>(true, "EnableHamburgerIcon");

            if (CacheBusinessLogic.SwitchUserOnEachSale && !RunAway
                && !IsPumpTestOn)
            {
                NavigateService.Instance.NavigateToLogout();
                MessengerInstance.Send<ForceUserIDMessage>(new ForceUserIDMessage());
            }
            else
            {
                NavigateService.Instance.NavigateToHome();
            }
        }

        private async Task<List<string>> GetReportContent(bool isRunAway)
        {
            string reportType = string.Empty;

            switch (_transactionType)
            {
                case nameof(TransactionType.ARPay):
                    {
                        reportType = ReportType.ArPayFile;
                        break;
                    }
                case nameof(TransactionType.Sale):
                    if (isRunAway)
                    {
                        reportType = ReportType.RunAwayFile;
                    }
                    else
                    {
                        reportType = ReportType.ReceiptFile;
                    }
                    break;
                case nameof(TransactionType.Payment):
                    {
                        reportType = ReportType.PaymentFile;
                        break;
                    }
                default:
                    return new List<string>();
            }

            if (IsPumpTestOn)
            {
                reportType = ReportType.PumpTestFile;
            }

            return await _reportsBusinessLogic.GetReceipt(reportType);
        }

        private void OpenNumberPadForTenderItem(object s)
        {
            SelectedTender = Tenders.FirstOrDefault(x => x.Code == s.ToString());
            if (SelectedTender.IsEnabled)
            {
                NavigateService.Instance.NavigateToTendersQuantityPad();
                MessengerInstance.Send(true,
                    "ResetNumberPadVM");
                MessengerInstance.Send(SelectedTender.AmountEntered,
                    "SetQuantiyUsingNumberPad");
            }
        }

        private void EnableOrDisableTenders()
        {
            var isTenderEnable = true;

            if (_runAway || _isPumpTestOn)
            {
                isTenderEnable = false;
            }

            if (Tenders != null)
            {
                foreach (var tender in Tenders)
                {
                    tender.IsEnabled = isTenderEnable;
                }
            }
        }

        private void SetPrintToggleValues(bool enableTender)
        {

            // Setting value of print toggle depending on run away
            if (enableTender)
            {
                IsPrintEnabled = _wasPrintEnabled;
                Print = _wasPrintOn;
            }
            else
            {
                _wasPrintEnabled = IsPrintEnabled;
                _wasPrintOn = Print;

                IsPrintEnabled = false;
                Print = true;
            }
        }

        private void SetTenderAmount(object s)
        {
            var outStandingAmount = 0M;
            decimal.TryParse(_outstandingAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out outStandingAmount);
            if (string.IsNullOrEmpty(s.ToString()))
            {
                _amountEntered = outStandingAmount; 
            }
            else
            {
                var amount = 0M;
                decimal.TryParse(s.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                _amountEntered = amount;
            }
            if (SelectedTender.MaximumValue != 0M &&
                (SelectedTender.MaximumValue < _amountEntered ||
                SelectedTender.MaximumValue < outStandingAmount))
            {
                var message = string.Format(ApplicationConstants.TenderMaxAmountConfirmation,
                    SelectedTender.Code,
                    SelectedTender.MaximumValue);
                ShowConfirmationMessage(message,
                    () =>
                    {
                        _amountEntered = SelectedTender.MaximumValue;
                        CheckForTenders();
                    },
                    ResetTender,
                    ResetTender);
            }
            else
            {
                CheckForTenders();
            }
        }

        private void ProcessStoreCreditCards()
        {
            decimal? amount = null;
            amount = _amountEntered;

            var outStandingAmount = 0M;
            decimal.TryParse(_outstandingAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out outStandingAmount);

            var storeCreditCheck = CacheBusinessLogic.CheckSC
              && outStandingAmount >= 0;

            if (storeCreditCheck)
            {
                if (amount != null && amount != _previousAmount && amount > 0)
                {
                    ShowConfirmationMessage(ApplicationConstants.StoreCreditConfirmationMessage, UpdateTender,
                         () =>
                         {
                             _amountEntered = 0M;
                             UpdateTender();
                         },
                         () =>
                         {
                             _amountEntered = 0M;
                             UpdateTender();
                         });
                }
                else if (amount == null)
                {
                    NavigateService.Instance.NavigateToStoreCredit();
                    MessengerInstance.Send(new StoreCreditSelectedMessage
                    {
                        Amount = _amountEntered,
                        TenderCode = SelectedTender.Code,
                        TransactionType = _transactionType
                    });
                }
                else
                {
                    UpdateTender();
                }
                _previousAmount = amount;
            }
            else
            {
                UpdateTender();
            }
        }

        private void ProcessGiftCertificate()
        {
            decimal? amount = null;
            if (_amountEntered != 0)
            {
                amount = _amountEntered;
            }

            // TODO: Create ENUM for Gift Tender if required
            var giftCertificateCheck = CacheBusinessLogic.ForceGiftCertificate
                && CacheBusinessLogic.GiftCertificateNumbered;
            if (amount != null)
            {
                if (giftCertificateCheck)
                {
                    ShowConfirmationMessage(ApplicationConstants.GiftCertificateConfirmationMessage,
                        UpdateTender,
                        NavigateService.Instance.NavigateToTenderScreen,
                        NavigateService.Instance.NavigateToTenderScreen);
                }
                else
                {
                    UpdateTender();
                }
            }
            else
            {
                if (giftCertificateCheck)
                {
                    NavigateService.Instance.NavigateToGiftCertificate();
                    MessengerInstance.Send(new GiftCertificateSelectedMessage
                    {
                        Amount = _amountEntered,
                        TenderCode = SelectedTender.Code,
                        TransactionType = _transactionType
                    });
                }
                else
                {
                    UpdateTender();
                }
            }
        }

        private void ResetTender()
        {
            MessengerInstance.Send(true, "ResetNumberPadVM");
            MessengerInstance.Send(SelectedTender.AmountEntered, "SetQuantiyUsingNumberPad");
        }

        private void CheckForTenders()
        {
            if (CacheBusinessLogic.CouponTender.ToUpper().Equals(SelectedTender.Code))
            {
                ProcessCoupon();
            }
            else
            {
                switch (SelectedTender.Class)
                {
                    case "GIFTCERT":
                        ProcessGiftCertificate();
                        break;
                    case "GIFTCARD":
                        ProcessGiftCard();
                        break;
                    case "CREDIT":
                        ProcessStoreCreditCards();
                        break;
                    case "ACCOUNT":
                        ProcessAccount(true, false);
                        break;
                    case "COUPON":
                        ProcessVendorCoupon();
                        break;
                    case "FLEET":
                        ProcessFleet();
                        break;
                    default:
                        UpdateTender();
                        break;
                }
            }
        }

        private void ProcessFleet()
        {
            NavigateService.Instance.NavigateToPaymentByFleet();

            var message = new SetFleetMessage
            {
                Amount = _amountEntered.ToString(),
                OutStandingAmount = _outstandingAmount,
                TenderCode = SelectedTender.Code,
                TransactionType = _transactionType

            };

            MessengerInstance.Send(message, "SetFleetMessage");
        }

        private void ProcessVendorCoupon()
        {
            NavigateService.Instance.NavigateToSaleVendorCoupon();
            MessengerInstance.Send(SelectedTender.Code, "SetSelectedTenderCode");
            MessengerInstance.Send(_outstandingAmount, "SetOutstandingAmount");
        }

        #region Payment By Account

        private void ProcessAccount(bool isNew, bool arePromptsSaved, string poNumber = null,
             bool isKickBack = false, string outstandingAmount = null)
        {
            PerformAction(async () =>
            {
                if (isNew)
                {
                    if (outstandingAmount != null)
                    {
                        decimal tempOutstandingAmount = 0M;

                        decimal.TryParse(outstandingAmount, out tempOutstandingAmount);

                        _amountEntered = tempOutstandingAmount;
                    }

                    _verifyByAccount = await _checkoutBusinessLogic.VerifyByAccount(_transactionType,
                        false, SelectedTender.Code, _amountEntered);
                }

                if (poNumber != null)
                {
                    _previousPurchaseOrderNumber = poNumber;
                }

                if (_verifyByAccount.CardSummary != null && !arePromptsSaved)
                {
                    MessengerInstance.Send(_verifyByAccount.CardSummary, "ShowPrivateRestrictionsForAccount");
                }
                else
                {
                    // Complete payment by account directly if new user is also not authorized
                    if (_isAccountPaymentPriorAuthorization && _verifyByAccount.UnauthorizedMessage != null &&
                    !string.IsNullOrEmpty(_verifyByAccount.UnauthorizedMessage.Message))
                    {
                        PaymentByAccount();
                        return;
                    }

                    if (_verifyByAccount.IsPurchaseOrderRequired && !_isAccountPaymentPriorAuthorization
                        && (CacheBusinessLogic.IsKickBack || !arePromptsSaved))
                    {
                        OpenPurchaseOrderPopup();
                    }
                    else if (_verifyByAccount.CreditMessage?.Message != null && !_isAccountPaymentPriorAuthorization
                        && !arePromptsSaved)
                    {
                        CheckForCreditMessage();
                    }
                    else if (_verifyByAccount.OverrideArLimitMessage?.Message != null)
                    {
                        CheckForOverrideARMessage();
                    }
                    else if (_verifyByAccount.UnauthorizedMessage?.Message != null)
                    {
                        CheckForUnauthorizedMessage();
                    }
                    else if (_verifyByAccount.IsPurchaseOrderRequired && !_isAccountPaymentPriorAuthorization)
                    {
                        OpenPurchaseOrderPopup();
                    }
                    else
                    {
                        PaymentByAccount();
                    }
                }
            });
        }

        private void CheckForUnauthorizedMessage()
        {
            if (!string.IsNullOrEmpty(_verifyByAccount.UnauthorizedMessage?.Message))
            {
                ShowConfirmationMessage(_verifyByAccount.UnauthorizedMessage.Message,
                   () =>
                   {
                       CacheBusinessLogic.FramePriorSwitchUserNavigation = "PaymentByAccount";
                       NavigateService.Instance.NavigateToLogout();
                   },
                       PaymentByAccount,
                       PaymentByAccount);
            }
            else
            {
                PaymentByAccount();
            }
        }

        private void PaymentByAccount()
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();
                try
                {
                    var response = await _checkoutBusinessLogic.PaymentByAccount(_previousPurchaseOrderNumber,
                        _isOverrideARLimit, _transactionType, false, SelectedTender.Code, _amountEntered);

                    UpdateTenderSummary(response);

                    NavigateService.Instance.NavigateToTenderScreen();

                    if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "PaymentByAccount")
                    {
                        CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;

                        CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                    }
                }
                catch (PurchaseOrderRequiredException ex)
                {
                    Log.Info(ex.Message, ex);
                    ShowNotification(ex.Message,
                         OpenPurchaseOrderPopup,
                       OpenPurchaseOrderPopup,
                        ApplicationConstants.ButtonWarningColor);
                }
                finally
                {
                    _isAccountPaymentPriorAuthorization = false;
                    timer.Stop();
                    Log.Info(string.Format("Time taken in payment by account is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private void OpenPurchaseOrderPopup()
        {
            PopupService.PopupInstance.IsPurchaseOrderPopupOpen = true;
            PopupService.PopupInstance.IsPopupOpen = true;
            PopupService.PopupInstance.IsYesbuttonEnabled = true;
            PopupService.PopupInstance.IsNoButtonEnabled = true;
        }

        private void PurchaseOrderNumberEntered()
        {
            if (!string.IsNullOrEmpty(PurchaseOrderNumber))
            {
                var purchaseOrderNumber = PurchaseOrderNumber;
                MessengerInstance.Send(new CloseKeyboardMessage());
                ClosePurchaseOrderPopup();
                if (!_verifyByAccount.IsMutiliPO)
                {
                    PerformAction(async () =>
                    {
                        try
                        {
                            var reponse = await _checkoutBusinessLogic.VerifyMultiPO(purchaseOrderNumber);
                            CheckForCreditMessage();
                        }
                        catch (ApiDataException ex)
                        {
                            ShowNotification(ex.Message, OpenPurchaseOrderPopup,
                                OpenPurchaseOrderPopup, ApplicationConstants.ButtonWarningColor);
                        }
                    });
                }
                else
                {
                    CheckForCreditMessage();
                }
            }
        }

        private void CheckForOverrideARMessage()
        {
            if (!string.IsNullOrEmpty(_verifyByAccount.OverrideArLimitMessage?.Message))
            {
                ShowConfirmationMessage(_verifyByAccount.OverrideArLimitMessage.Message,
                   () =>
                   {
                       _isOverrideARLimit = true;
                       CheckForUnauthorizedMessage();
                   },
                   () =>
                   {
                       _isOverrideARLimit = false;
                       CheckForUnauthorizedMessage();
                   }, () =>
                   {
                       _isOverrideARLimit = false;
                       CheckForUnauthorizedMessage();
                   });
            }
            else
            {
                _isOverrideARLimit = false;
                CheckForUnauthorizedMessage();
            }
        }

        private void CheckForCreditMessage()
        {
            if (!string.IsNullOrEmpty(_verifyByAccount.CreditMessage?.Message))
            {
                ShowNotification(_verifyByAccount.CreditMessage.Message,
                    CheckForOverrideARMessage,
                    CheckForOverrideARMessage,
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                CheckForOverrideARMessage();
            }
        }

        private void ClosePurchaseOrderPopup()
        {
            _previousPurchaseOrderNumber = PurchaseOrderNumber;
            PurchaseOrderNumber = string.Empty;
            PopupService.PopupInstance.IsPurchaseOrderPopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        #endregion

        private void ProcessGiftCard()
        {
            var message = new GiveXSelectedMessage
            {
                Amount = _amountEntered,
                TenderCode = SelectedTender.Code,
                TransactionType = _transactionType,
                OutStandingAmount = _outstandingAmount,
                CardNumber = new Helper().GetTrack2Data(CardNumber)
            };
            CardNumber = string.Empty;

            NavigateService.Instance.NavigateToGiveXTender();

            MessengerInstance.Send(message, "SetGiveXMessage");
        }

        private void ProcessCoupon()
        {
            NavigateService.Instance.NavigateToCoupon();
            MessengerInstance.Send(SelectedTender.Code, "SetSelectedTenderCode");
            MessengerInstance.Send(_outstandingAmount, "SetOutstandingAmount");
        }

        private void UpdateTender()
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();

                try
                {
                    decimal? amount = null;
                    amount = _amountEntered;
                    if (amount.HasValue && SelectedTender.MaximumValue != 0M &&
                        amount.Value > SelectedTender.MaximumValue)
                    {
                        amount = SelectedTender.MaximumValue;
                    }
                    var tenderSummary = await _checkoutBusinessLogic.UpdateTender(SelectedTender.Code,
                        amount, _transactionType, true);

                    await PerformPrint(tenderSummary.Report);

                    UpdateTenderSummary(tenderSummary);
                    NavigateService.Instance.NavigateToTenderScreen();

                }
                finally
                {
                    timer.Stop();
                    Log.Info(string.Format("Time taken in update tender is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private void UpdateSaleSummaryLines(ObservableCollection<TenderModel> tenders)
        {
            Lines = new ObservableCollection<SaleSummaryLineModel>(SaleSummary.Lines.Concat(
                (from t in tenders
                 where !string.IsNullOrEmpty(t.AmountEntered?.Trim())
                 select new SaleSummaryLineModel
                 {
                     Amount = t.AmountEntered,
                     Name = t.Name,
                     Value = t.AmountValue
                 }).ToList()
            ));
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<CheckoutSummary>(this, UpdateCheckoutSummary);
            MessengerInstance.Register<bool>(this, "ResetSaleSummary",
                ResetSaleSummary);
            MessengerInstance.Register<CheckoutSummary>(this, "PaymentByAR",
                PaymentByAR);
            MessengerInstance.Register<CheckoutSummary>(this, "PaymentByFleet",
               PaymentByFleet);
            MessengerInstance.Register<TenderSummary>(this, "UpdateTenderSummary",
                UpdateTenderSummary);
            MessengerInstance.Register<Uri>(this, "CompletePaymentAfterSignature",
                CompletePaymentAfterSignature);
            MessengerInstance.Register<KickBackForTenderScreenMessage>(this, "CompleteAptTenderTransaction",
                CompleteAptTenderTransaction);
            MessengerInstance.Register<string>(this, "CompleteAccountPaymentAfterValidations",
                CompleteAccountPaymentAfterValidations);
        }


        private void CompleteAccountPaymentAfterValidations(string obj)
        {
            _previousPurchaseOrderNumber = obj;
            _verifyByAccount.CardSummary = null;
            ProcessAccount(false, true);
        }

        private void CompleteAptTenderTransaction(KickBackForTenderScreenMessage message)
        {
            _fleetCardResponse.PoNumber = message.PoNumber;
            PerformTransactionUsingAptTender(_fleetCardResponse, message.IsKickBack, message.OutstandingAmount);
        }

        private void CompletePaymentAfterSignature(Uri signature)
        {
            _isSignatureDone = true;
            CompletePayment(signature);
        }

        private void PaymentByFleet(CheckoutSummary summary)
        {
            _transactionType = TransactionType.Payment.ToString();
            UpdateCheckoutSummary(summary);
        }

        private void PaymentByAR(CheckoutSummary summary)
        {
            _transactionType = TransactionType.ARPay.ToString();
            UpdateCheckoutSummary(summary);
        }

        private void PerformTransaction()
        {
            PerformAction(async () =>
            {
                var card = new Helper().GetTrack2Data(CardNumber);

                var response = await _checkoutBusinessLogic.GetCardInformation(
                    Helper.EncodeToBase64(card),
                    _transactionType);
                switch (response.CardType)
                {
                    case CardType.Credit:
                    case CardType.Debit:
                        await PaymentByCreditOrDebitCard(response);
                        break;
                    case CardType.Fleet:
                        _fleetCardResponse = response;
                        PaymentByFleetCard(response, response.IsArCustomer);
                        break;
                    case CardType.Givex:
                        NavigateService.Instance.NavigateToGiveXTender();
                        MessengerInstance.Send(response, "PerformTransactionForGivex");
                        break;
                    case CardType.None:
                        break;
                }
            });
        }

        private void PaymentByFleetCard(CardSwipeInformation response, bool performAptTender = false)
        {
            NavigateService.Instance.NavigateToPaymentByFleet();

            var message = new SetFleetMessage
            {
                CardNumber = new Helper().GetTrack2Data(CardNumber),
                OutStandingAmount = _outstandingAmount,
                TenderCode = response.TenderCode,
                TransactionType = _transactionType,
                PerformAptTender = performAptTender,
                IsGasKing = response.IsGasKing,
                KickbackPoints = response.KickbackPoints,
                KickBackValue = response.KickBackValue,
                IsKickBackLinked = response.IsKickBackLinked,
                IsFleet = response.IsFleet,
                IsInvalidLoyaltyCard = response.IsInvalidLoyaltyCard
            };

            MessengerInstance.Send(message, "SetFleetMessage");
            NavigateService.Instance.SecondFrameBackNavigation();

            MessengerInstance.Send<CardSwipeInformation>(response, "FleetCardSwiped");
        }

        private async Task PaymentByCreditOrDebitCard(CardSwipeInformation response)
        {
            var card = new Helper().GetTrack2Data(CardNumber);

            var tenderSummary = await _checkoutBusinessLogic.PaymentByCard(
                response.TenderCode, response.Amount, _transactionType,
                Helper.EncodeToBase64(card));

            var isPrinterFound = true;

            isPrinterFound = await CheckIfPrinterExists(() =>
            {
                UpdateTenderSummary(tenderSummary);
            });

            if (isPrinterFound)
            {
                PerformPrint(tenderSummary?.Report);
                UpdateTenderSummary(tenderSummary);
            }
        }

        private void PerformTransactionUsingAptTender(CardSwipeInformation cardInformation, bool useKickBack, string outstandingAmount)
        {
            SelectedTender = new TenderModel
            {
                Code = cardInformation.TenderCode
            };

            var amountEntered = 0M;
            decimal.TryParse(string.IsNullOrEmpty(cardInformation.Amount) ? _outStandingAmount : cardInformation.Amount,
                NumberStyles.Any, CultureInfo.InvariantCulture, out amountEntered);
            var tenderClass = TenderClass.None;
            Enum.TryParse<TenderClass>(cardInformation.TenderClass, out tenderClass);

            if (amountEntered != 0)
            {
                _amountEntered = amountEntered;
            }
            switch (tenderClass)
            {
                case TenderClass.ACCOUNT:
                    ProcessAccount(true, true, cardInformation.PoNumber, useKickBack, outstandingAmount);
                    break;
                case TenderClass.COUPON:
                    if (CacheBusinessLogic.CouponTender.ToUpper().Equals(SelectedTender.Code))
                    {
                        ProcessCoupon();
                    }
                    else
                    {
                        ProcessVendorCoupon();
                    }
                    break;
                case TenderClass.CREDIT:
                    ProcessStoreCreditCards();
                    break;
                case TenderClass.CRCARD:
                case TenderClass.DBCARD:
                case TenderClass.VISA:
                case TenderClass.DEBIT:
                    PerformAction(async () => await PaymentByCreditOrDebitCard(cardInformation));
                    break;
                case TenderClass.FLEET:
                    PaymentByFleetCard(cardInformation);
                    break;
                case TenderClass.GIFTCARD:
                    ProcessGiftCard();
                    break;
                case TenderClass.GIFTCERT:
                    ProcessGiftCertificate();
                    break;
                case TenderClass.CASH:
                case TenderClass.CHEQUE:
                case TenderClass.POINTS:
                case TenderClass.THIRDPARTY:
                    UpdateTender();
                    break;
            }
        }

        private async Task<bool> CheckIfPrinterExists(Action action = null)
        {
            if (CacheBusinessLogic.UseReceiptPrinter && !CacheBusinessLogic.UseOposReceiptPrinter)
            {
                return true;
            }

            if (CacheBusinessLogic.UseReceiptPrinter && CacheBusinessLogic.UseOposReceiptPrinter &&
                VMBase.OposPrinter != null && VMBase.OposPrinter.IsAvailable())
            {
                return true;
            }
            else if (CacheBusinessLogic.UseReceiptPrinter && CacheBusinessLogic.UseOposReceiptPrinter)
            {
                ShowNotification(ApplicationConstants.NoPrinterFound,
                    action,
                    action,
                       ApplicationConstants.ButtonWarningColor);
                return false;
            }

            return true;
        }

        private void UpdateCheckoutSummary(CheckoutSummary summary)
        {
            _isDeletePrepay = summary.IsDeletePrepay;
            Lines = SaleSummary.Lines = new ObservableCollection<SaleSummaryLineModel>(
                from t in summary.SaleSummary.Summary
                select new SaleSummaryLineModel
                {
                    Name = t.Key,
                    Amount = t.Value
                });

            // Resetting the Tenders so that new tenders can be updated
            Tenders = null;
            UpdateTenderSummary(summary.TenderSummary);
        }

        private void UpdateTenderSummary(TenderSummary tenderSummary)
        {
            _issueStoreCreditMessage = tenderSummary.IssueStoreCreditMessage;

            WriteToLineDisplay(tenderSummary.LineDisplay);

            var tenders = new ObservableCollection<TenderModel>(
                 from t in tenderSummary.Tenders
                 select t.ToModel());

            if (Tenders == null)
            {
                Tenders = new ObservableCollection<TenderModel>(tenders);
            }

            foreach (var tender in Tenders)
            {
                tender.AmountEntered = null;
                tender.AmountValue = null;
            }


            // Update all the existing tenders
            foreach (var tender in tenderSummary.Tenders)
            {
                var tempTender = tender.ToModel();
                var selectedTender = Tenders
                    .FirstOrDefault(x => x.Code == tempTender.Code);
                if (selectedTender != null)
                {
                    selectedTender.AmountEntered = tempTender.AmountEntered;
                    selectedTender.AmountValue = tempTender.AmountValue;
                    selectedTender.Image = tempTender.Image;
                    selectedTender.Class = tempTender.Class;
                    selectedTender.MaximumValue = tempTender.MaximumValue;
                    selectedTender.MinimumValue = tender.MinimumValue;
                    selectedTender.Name = tempTender.Name;
                    selectedTender.IsEnabled = tempTender.IsEnabled;
                }
                else
                {
                    Tenders.Add(selectedTender);
                }
            }

            SaleSummary.Summary1 = tenderSummary.Summary1;
            SaleSummary.Summary2 = tenderSummary.Summary2;
            IsRunAwayEnabled = tenderSummary.EnableRunAway;
            IsPumpTestEnabled = tenderSummary.EnablePumpTest;
            CompletePaymentEnabled = tenderSummary.EnableCompletePayment;
            _outstandingAmount = tenderSummary.OutstandingAmount;
            _wasPrintEnabled = IsPrintEnabled = tenderSummary.DisplayNoReceiptButton;
            // if amount paid using any tender then disable runaway
            foreach (var tender in Tenders)
            {
                var amountEntered = 0M;
                decimal.TryParse(tender.AmountEntered, NumberStyles.Any, CultureInfo.InvariantCulture, out amountEntered);

                if (amountEntered != 0M)
                {
                    IsRunAwayEnabled = false;
                    RunAway = false;
                }
            }

            UpdateSaleSummaryLines(tenders);
            _outStandingAmount = tenderSummary.OutstandingAmount;
            ShowTenderMessages(tenderSummary.Messages);
        }

        private void ShowTenderMessages(List<Error> messages)
        {
            if (messages == null || messages.Count == 0)
            {
                return;
            }

            var message = messages.FirstOrDefault().Message;
            messages.RemoveAt(0);
            ShowNotification(message,
                () => { ShowTenderMessages(messages); },
                () => { ShowTenderMessages(messages); },
                ApplicationConstants.ButtonWarningColor);
        }

        private void ResetSaleSummary(bool check)
        {
            SaleSummary = new SaleSummaryModel();
            Lines = new ObservableCollection<SaleSummaryLineModel>();
            IsRunAwayEnabled = false;
            IsPumpTestEnabled = false;
            //  IsPrintEnabled = !CacheBusinessLogic.ForcePrintReceipt;
            CompletePaymentEnabled = false;
            _transactionType = TransactionType.Sale.ToString();
        }

        #region Open Cash Drawer

        private void OpenCashDrawerReasonSelected(object reason)
        {
            _reasonForOpenCashDrawer = reason as Reasons;
            CloseReasonPopup();
            PerformAction(async () =>
            {
                await _cashBusinessLogic.OpenCashDrawer(_reasonForOpenCashDrawer.Code);

                PostPaymentProcess(false);

            });
        }

        private async Task OpenCashDrawer()
        {
            if (CacheBusinessLogic.UseReasonForCashDrawer)
            {
                await GetReasonListAsync(EntityLayer.ReasonType.openCashDrawer,
               OpenCashDrawerReasonSelectedCommand);
            }
            else
            {
                await _cashBusinessLogic.OpenCashDrawer(_reasonForOpenCashDrawer.Code);
            }
        }

        #endregion

        /// <summary>
        /// Method to get reason list
        /// </summary>
        /// <returns></returns>
        private async Task GetReasonListAsync(ReasonType reasonEnum,
            RelayCommand<object> reasonSelectCommand)
        {
            if (!PopupService.IsPopupOpen)
            {
                PopupService.ReasonList?.Clear();
                var response = await _reasonListBussinessLogic.GetReasonListAsync(reasonEnum.ToString());

                foreach (var reason in response.Reasons)
                {
                    PopupService.ReasonList.Add(new Reasons
                    {
                        Code = reason.Code,
                        Description = reason.Description
                    });
                }

                PopupService.Title = response.ReasonTitle;
                PopupService.MessageItemClicked = reasonSelectCommand;
                PopupService.IsPopupOpen = true;
                PopupService.IsReasonPopupOpen = true;

                PopupService.CloseCommand = new RelayCommand(() =>
                {
                    PostPaymentProcess(false);
                });
            }
        }

        public void StartTimer()
        {
            if (_changeDueAppTimer == null)
            {
                _timeRemaining = CacheBusinessLogic.DelayInNewSale;
                _changeDueAppTimer = new DispatcherTimer();
                _changeDueAppTimer.Interval = new TimeSpan(0, 0, 1);
                _changeDueAppTimer.Tick -= AppTimerTick;
                _changeDueAppTimer.Tick += AppTimerTick;
            }
            _changeDueAppTimer.Start();
        }

        private void AppTimerTick(object sender, object e)
        {
            if (--_timeRemaining <= 0)
            {
                if (PopupService.CloseCommand != null)
                {
                    PopupService.CloseCommand.Execute(null);
                    ResetDispatcher();
                }
            }
        }

        public void ResetDispatcher()
        {
            if (_changeDueAppTimer != null)
            {
                _changeDueAppTimer.Stop();
                _changeDueAppTimer.Tick -= AppTimerTick;
                _changeDueAppTimer = null;
                _timeRemaining = 0;
            }
        }

        private void CurrencyTapped(string s)
        {
            if (SelectedTender.Class.Equals(CacheBusinessLogic.BaseCurrency))
            {
                SetTenderAmount(s);
            }
        }

        public void ReInitializeVM()
        {
            MessengerInstance.Send<bool>(false, "UserNavigatedToSaleSummaryPage");
            MessengerInstance.Register<string>(this, "CurrencyTapped", CurrencyTapped);
            _wasPrintOn = Print = true;
            _wasPrintEnabled = IsPrintEnabled = !CacheBusinessLogic.ForcePrintReceipt;
            _transactionType = TransactionType.Sale.ToString();
            _isAccountPaymentPriorAuthorization = false;
            IsPumpTestOn = RunAway = false;
            TestCard = string.Empty;
            _previousAmount = null;
            _isDeletePrepay = false;
            _isPaymentCompleteByOtherMode = true;
            if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "PaymentByAccount")
            {
                _isAccountPaymentPriorAuthorization = true;
                ProcessAccount(true, false);
            }
        }
    }
}
