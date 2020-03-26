using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Sale
{
    public class SaleGridVM : VMBase
    {
        #region Private properties  
        private bool _isSuspendOrVoidDone;
        private bool _supportKickBack;
        private string _cashDropReason;
        private string _currentStockCodeToProcess = string.Empty;
        private SaleModel _saleModel;
        private SaleLineModel _selectedSaleLine;
        private Reasons _selectedMessage;
        private GiftCardModel _giftCard;
        private StockModel _stock;
        private Reasons _reasonForVoidingSale;
        private Reasons _reasonForSaleLineUpdate = new Reasons();
        private Reasons _reasonForWriteOff = new Reasons();
        private Reasons _reasonForOpenCashDrawer = new Reasons();
        private VerifyStock _verifyStockModel;
        private TaxExemptVerification _verifyTaxExempt;
        private TaxExemptionVM _taxExemptionVM;
        private string _customerName;
        private string _discountType;
        private int _saleNumber;
        private bool _operatorCanUnsuspendSale;
        private bool _operatorCanSuspendSale;
        private bool _operatorCanReturnSale;
        private bool _canOperatorVoidSale;
        private bool _isBottleReturnEnabled;
        private bool _isSaleReturnEnabled;
        private bool _isVoidSaleVisible;
        private bool _isSwitchUserPerformed;
        private bool _isWriteOffEnabled;
        private bool _isExactCashEnabled;
        private bool _focusOnNewRow;
        private bool _isCarwashIntegrated;
        private bool _isCarwashSupported;
        private bool _REWARDS_Enabled;
        private bool _isPSInetSupported;
        private string _psinet_Type;
        private string _envelopeNumber;
        private string _genericReson;
        private SaleLineModel _changedSaleLine;
        private List<Error> _saleErrors;
        private bool _isAcceptTenderEnabled;
        private DateTime _startTime;
        private DateTime _endTime;
        private Stopwatch _tracker;
        private bool _isSaveMessageButtonEnable;
        private bool _IsMessageInputEnable;
        private bool _isCashDrawerEnabled;
        private bool _carwashServerDown = false; 

        #endregion

        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly IPaymentBussinessLogic _paymentBusinessLogic;
        private readonly ICashBusinessLogic _cashBusinessLogic;
        private readonly IMessageBusinessLogic _messageBusinessLogic;
        private readonly IKickBackBusinessLogic _kickBackBusinessLogic;
        private readonly ICarwashBusinessLogic _carwashBusinessLogic;
        private readonly ICacheBusinessLogic _cacheBussinessLogic;
        private readonly IFuelDiscountBusinessLogic _fuelDiscountBusinessLogic;
        private readonly ICheckoutBusinessLogic _checkoutBusinessLogic;

        #region Public Properties
        public bool REWARDS_Enabled
        {
            get { return _REWARDS_Enabled; }
            set
            {
                _REWARDS_Enabled = value;
                RaisePropertyChanged(nameof(REWARDS_Enabled));
            }
        }
        public string PSINet_Type
        {
            get { return _psinet_Type; }
            set
            {
                _psinet_Type = value;
                RaisePropertyChanged(nameof(PSINet_Type));
            }
        }
        public bool ISPSInetSupported
        {
            get { return _isPSInetSupported; }
            set { Set(nameof(ISPSInetSupported), ref _isPSInetSupported, value); }
        }
        public bool IsCarwashSupported
        {
            get { return _isCarwashSupported; }
            set { Set(nameof(IsCarwashSupported), ref _isCarwashSupported, value); }
        }

        public bool IsCarwashIntegrated
        {
            get { return _isCarwashIntegrated; }
            set { Set(nameof(IsCarwashIntegrated), ref _isCarwashIntegrated, value); }
        }

        public bool SupportKickBack
        {
            get { return _supportKickBack; }
            set { Set(nameof(SupportKickBack), ref _supportKickBack, value); }
        }

        public bool AreScanEventsAttached { get; set; }

        public bool IsCashDrawerEnabled
        {
            get { return _isCashDrawerEnabled; }
            set
            {
                if (_isCashDrawerEnabled != value)
                {
                    _isCashDrawerEnabled = value;
                    RaisePropertyChanged(nameof(IsCashDrawerEnabled));
                }
            }
        }

        public bool IsSaveMessageButtonEnable
        {
            get { return _isSaveMessageButtonEnable; }
            set
            {
                _isSaveMessageButtonEnable = value;
                RaisePropertyChanged(nameof(IsSaveMessageButtonEnable));
            }
        }

        public bool IsAcceptTenderEnabled
        {
            get { return _isAcceptTenderEnabled; }
            set
            {
                if (_isAcceptTenderEnabled != value)
                {
                    _isAcceptTenderEnabled = value;
                    RaisePropertyChanged(nameof(IsAcceptTenderEnabled));
                }
            }
        }

        public string GenericReason
        {
            get { return _genericReson; }
            set
            {
                if (_genericReson != value)
                {
                    _genericReson = value;
                    IsSaveMessageButtonEnable = !string.IsNullOrEmpty(_genericReson);
                    RaisePropertyChanged(nameof(GenericReason));
                }
            }
        }

        public bool IsExactCashEnabled
        {
            get { return _isExactCashEnabled; }
            set
            {
                if (value != _isExactCashEnabled)
                {
                    _isExactCashEnabled = value;
                    RaisePropertyChanged(nameof(IsExactCashEnabled));
                }
            }
        }

        public bool IsVoidSaleVisible
        {
            get { return _isVoidSaleVisible; }
            set
            {
                if (_isVoidSaleVisible != value)
                {
                    _isVoidSaleVisible = value;
                    RaisePropertyChanged(nameof(IsVoidSaleVisible));
                }
            }
        }

        public bool OperatorCanSuspendSale
        {
            get { return _operatorCanSuspendSale; }
            set
            {
                if (_operatorCanSuspendSale != value)
                {
                    _operatorCanSuspendSale = value;
                    RaisePropertyChanged(nameof(OperatorCanSuspendSale));
                }
            }
        }

        public bool OperatorCanUnsuspendSales
        {
            get { return _operatorCanUnsuspendSale; }
            set
            {
                if (_operatorCanUnsuspendSale != value)
                {
                    _operatorCanUnsuspendSale = value;
                    RaisePropertyChanged(nameof(OperatorCanUnsuspendSales));
                }
            }
        }

        public bool FocusOnNewRow
        {
            get { return _focusOnNewRow; }
            set
            {
                _focusOnNewRow = value;
                RaisePropertyChanged(nameof(FocusOnNewRow));
            }
        }

        public SaleLineModel NewSaleLine { get; set; }
        = new SaleLineModel();

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    RaisePropertyChanged(nameof(CustomerName));
                }
            }
        }

        public int SaleNumber
        {
            get { return _saleNumber; }
            set
            {
                if (_saleNumber != value)
                {
                    _saleNumber = value;
                    CacheBusinessLogic.SaleNumber = _saleNumber;
                    RaisePropertyChanged(nameof(SaleNumber));
                }
            }
        }

        public SaleModel SaleModel
        {
            get { return _saleModel; }
            set
            {
                if (value == null)
                {
                    value = new SaleModel();
                }
                value.SaleLines.Remove(NewSaleLine);
                NewSaleLine = new SaleLineModel { AllowStockCodeChange = true };
                value.SaleLines.Add(NewSaleLine);

                _saleModel = value;
                RaisePropertyChanged(nameof(SaleModel));
            }
        }

        public GiftCardModel GiftCard
        {
            get { return _giftCard; }
            set
            {
                if (_giftCard != value)
                {
                    _giftCard = value;
                    RaisePropertyChanged(nameof(GiftCard));
                }
            }
        }

        public SaleLineModel SelectedSaleLine
        {
            get { return _selectedSaleLine; }
            set
            {
                if (_selectedSaleLine != value)
                {
                    _selectedSaleLine = value;
                    RaisePropertyChanged(nameof(SelectedSaleLine));
                }
            }
        }

        public bool CanOperatorVoidSale
        {
            get { return _canOperatorVoidSale; }
            set
            {
                if (_canOperatorVoidSale != value)
                {
                    _canOperatorVoidSale = value;
                    RaisePropertyChanged(nameof(CanOperatorVoidSale));
                }
            }
        }

        public bool IsReturn
        {
            get { return CacheBusinessLogic.IsReturn; }
            set
            {
                if (CacheBusinessLogic.IsReturn != value)
                {
                    CacheBusinessLogic.IsReturn = value;
                    RaisePropertyChanged(nameof(IsReturn));
                }
            }
        }

        public bool IsBottleReturnEnabled
        {
            get { return _isBottleReturnEnabled; }
            set
            {
                if (_isBottleReturnEnabled != value)
                {
                    _isBottleReturnEnabled = value;
                    RaisePropertyChanged(nameof(IsBottleReturnEnabled));
                }
            }
        }

        public bool IsSaleReturnEnabled
        {
            get { return _isSaleReturnEnabled; }
            set
            {
                if (_isSaleReturnEnabled != value)
                {
                    _isSaleReturnEnabled = value;
                    RaisePropertyChanged(nameof(IsSaleReturnEnabled));
                }
            }
        }

        public bool OperatorCanReturnSale
        {
            get { return _operatorCanReturnSale; }
            set
            {
                if (_operatorCanReturnSale != value)
                {
                    _operatorCanReturnSale = value;
                    if (!_operatorCanReturnSale)
                    {
                        IsReturn = false;
                    }
                    RaisePropertyChanged(nameof(OperatorCanReturnSale));
                }
            }
        }

        public bool IsWriteOffEnabled
        {
            get { return _isWriteOffEnabled; }
            set
            {
                if (_isWriteOffEnabled != value)
                {
                    _isWriteOffEnabled = value;
                    RaisePropertyChanged(nameof(IsWriteOffEnabled));
                }
            }
        }

        public string EnvelopeNumber
        {
            get { return _envelopeNumber; }
            set
            {
                if (_envelopeNumber != value)
                {
                    _envelopeNumber = value;
                    RaisePropertyChanged(nameof(EnvelopeNumber));
                }
            }
        }


        public bool IsMessageInputEnable
        {
            get { return _IsMessageInputEnable; }
            set
            {
                _IsMessageInputEnable = value;
                RaisePropertyChanged(nameof(IsMessageInputEnable));
            }
        }

        #endregion

        private string _stockStream;

        private object objLock = new object();
        private int startIndex = 0;
        private char[] _charsToRemove = new char[2];

        public string StockStream
        {
            get { return _stockStream; }
            set
            {
                lock (objLock)
                {
                    _stockStream = value?.TrimStart();
                    if (string.IsNullOrEmpty(_stockStream) || _stockStream.IndexOf('\n') == -1)
                    {
                        startIndex = 0;
                    }
                    var firstIndex = _stockStream.IndexOf('\n', startIndex);
                    var secondIndex = firstIndex != -1 ? _stockStream.IndexOf('\n', firstIndex) : -1;
                    if (firstIndex > 0 && secondIndex > 0)
                    {
                        _currentStockCodeToProcess = firstIndex == secondIndex ? _stockStream.Substring(startIndex, firstIndex - startIndex) :
                            _stockStream.Substring(firstIndex + 1, secondIndex - firstIndex);
                        AddNewStock();
                        startIndex = firstIndex + 1;
                    }
                }
            }
        }

        #region Commands
        public RelayCommand AckrooCommand { get; private set; }

        public RelayCommand RaiseMessagePopupCommand;
        public RelayCommand ExactChangeCommand;
        public RelayCommand RaiseVoidSalePopupCommand { get; private set; }
        public RelayCommand<Reasons> MessageItemClickedCommand { get; private set; }
        public RelayCommand OpenCheckoutOptionsPopupCommmand { get; private set; }
        public RelayCommand CloseCheckoutOptionsPopupCommand { get; private set; }
        public RelayCommand OpenReturnsPopupCommand { get; private set; }
        public RelayCommand OpenSuspendedSalesCommand { get; private set; }
        public RelayCommand CloseReturnsPopupCommand { get; private set; }
        public RelayCommand BottleReturnsCommand { get; private set; }
        public RelayCommand SuspendSaleCommand { get; private set; }
        public RelayCommand RaiseCashPopupCommand { get; private set; }
        public RelayCommand ReturnSaleCommand { get; private set; }
        public RelayCommand RaiseLoyaltyGiftPopupCommand { get; private set; }
        public RelayCommand PSInetCommand { get; private set; }
        public RelayCommand<object> QuantityChangedCommand { get; private set; }
        public RelayCommand<object> PriceChangedCommand { get; private set; }
        public RelayCommand<Reasons> PriceChangedWithReasonCommand { get; private set; }
        public RelayCommand<Reasons> QuantityChangedWithReasonCommand { get; private set; }
        public RelayCommand<object> DiscountChangedCommand { get; private set; }
        public RelayCommand<object> EnterPressedOnQuantityCommand { get; private set; }
        public RelayCommand<Reasons> DiscountChangedWithReasonCommand { get; private set; }
        public RelayCommand<object> MessageSelectedCommand { get; private set; }
        public RelayCommand<SaleLineModel> SaleLineDeletedCommand { get; private set; }
        public RelayCommand AddStockItemForSaleCommand { get; private set; }
        public RelayCommand RaiseCustomerPopupCommand { get; private set; }
        public RelayCommand AcceptTenderCommand { get; private set; }
        public RelayCommand WriteOffCommand { get; private set; }
        public RelayCommand OpenCashDrawerCommand { get; private set; }
        public RelayCommand<Reasons> WriteOffReasonSelectedCommand { get; private set; }
        public RelayCommand<Reasons> OpenCashDrawerReasonSelectedCommand { get; private set; }
        public RelayCommand OpenKickbackBalancePopupCommand { get; set; }
        private delegate void SaveMessageDelegate();
        private SaveMessageDelegate _saveMessageDelegate;
        public RelayCommand CloseKickBackNumberPopupCommand { get; set; }

        public RelayCommand SaveMessageCommand { get; private set; }

        public RelayCommand OpenCarwashPopupCommand { get; private set; }

        public RelayCommand CloseCarwashPopupCommand { get; private set; }

        public  RelayCommand CheckCarwashCodeCommand { get; private set; }

        #endregion



        public SaleGridVM(IReasonListBussinessLogic reasonListBussinessLogic,
            ISaleBussinessLogic saleBussinessLogic,
            IPaymentBussinessLogic paymentBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic,
            ICashBusinessLogic cashBusinessLogic,
             IMessageBusinessLogic messageBusinessLogic,
             IKickBackBusinessLogic kickBackBusinessLogic,
             ICarwashBusinessLogic carwashBussinessLogic,
             IFuelDiscountBusinessLogic fuelDiscountBusinessLogic,
             ICheckoutBusinessLogic checkoutBusinessLogic,
             ICacheBusinessLogic cacheBusinessLogic)
        {
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _saleBussinessLogic = saleBussinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            _paymentBusinessLogic = paymentBusinessLogic;
            _cashBusinessLogic = cashBusinessLogic;
            _messageBusinessLogic = messageBusinessLogic;
            _kickBackBusinessLogic = kickBackBusinessLogic;
            _carwashBusinessLogic = carwashBussinessLogic;
            _cacheBussinessLogic = cacheBusinessLogic;
            _fuelDiscountBusinessLogic = fuelDiscountBusinessLogic;
            _checkoutBusinessLogic = checkoutBusinessLogic;

            InitializeCommands();
            InitializeData();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<SaleModel>(this,
                "UpdateSale",
                UpdateSale);
            MessengerInstance.Register<RequestForSyncHotProductsMessage>(this, SyncHotProducts);
            MessengerInstance.Register<GiftCard>(this,
                "RegisterGiftCardModelFromGivexPage",
                RegisterGiftCardModelFromGivexPage);
            MessengerInstance.Register<AddStockToSaleMessage>(this,
                "VerifyAndAddStockForSale",
                VerifyAndAddStockForSale);
            MessengerInstance.Register<bool>(this,
                "CompleteVoidSale",
                CompleteVoidSale);
            MessengerInstance.Register<int>(this,
                "CompleteDeleteSaleLine",
                CompleteDeleteSaleLine);
            MessengerInstance.Register<SetFocusOnGridMessage>(this, SetFocusOnGrid);
            MessengerInstance.Register<FuelDiscount>(this, IssueDiscount);
        }

        private void SetFocusOnGrid(SetFocusOnGridMessage message)
        {
            SelectedSaleLine = SaleModel?.SaleLines?.Last();
            FocusOnNewRow = true;
            MessengerInstance.Send(new CloseKeyboardMessage());
        }

        private void SyncHotProducts(RequestForSyncHotProductsMessage message)
        {
            MessengerInstance.Send(SaleModel, "UpdateSale");
        }

        private void CompleteDeleteSaleLine(int lineNumber)
        {
            try
            {
                DeleteSaleLine(SaleModel.SaleLines.FirstOrDefault(x => x.LineNumber == lineNumber));
                MessengerInstance.Send(false, "LoadAllPolicies");
            }
            finally
            {
                CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
            }
        }

        private void RegisterGiftCardModelFromGivexPage(GiftCard giftCard)
        {
            CacheBusinessLogic.IsGiveXCalledFromAddStock = false;
            GiftCard.CardNumber = giftCard.CardNumber;
            GiftCard.Price = giftCard.Price;
            AddStockItemForSale();
        }

        /// <summary>
        /// Method to initialize data
        /// </summary>
        private void InitializeData()
        {
            _tracker = new Stopwatch();
            _saleErrors = new List<Error>();
            GiftCard = new GiftCardModel();
            CanOperatorVoidSale = CacheBusinessLogic.OperatorCanVoidSale;
            OperatorCanReturnSale = CacheBusinessLogic.OperatorCanReturnSale;
            IsCarwashSupported = CacheBusinessLogic.IsCarwashSupported;
            IsCarwashIntegrated = CacheBusinessLogic.IsCarwashIntegrated;
            ISPSInetSupported = CacheBusinessLogic.SupportPSInet;
            PSINet_Type = CacheBusinessLogic.PSINet_Type;
            REWARDS_Enabled = CacheBusinessLogic.REWARDS_Enabled;
            PerformAction(InitializeNewSale);
        }

        public void ReInitialize()
        {
            try
            {
                MessengerInstance.Send(new SetFocusOnGridMessage { });
                CanOperatorVoidSale = CacheBusinessLogic.OperatorCanVoidSale;
                OperatorCanReturnSale = CacheBusinessLogic.OperatorCanReturnSale;
                IsExactCashEnabled = CacheBusinessLogic.IsCurrentSaleEmpty ? false : CacheBusinessLogic.EnableExactChange;
                IsVoidSaleVisible = !CacheBusinessLogic.IsCurrentSaleEmpty;
                IsAcceptTenderEnabled = IsVoidSaleVisible = !CacheBusinessLogic.IsCurrentSaleEmpty;
                IsBottleReturnEnabled = CacheBusinessLogic.OperatorCanReturnBottle
                    && !IsVoidSaleVisible;
                IsSaleReturnEnabled = CacheBusinessLogic.OperatorCanReturnSale
                    && !IsVoidSaleVisible;
                IsWriteOffEnabled = !CacheBusinessLogic.IsCurrentSaleEmpty &&
                    CacheBusinessLogic.EnableWriteOffButton;
                IsCashDrawerEnabled = CacheBusinessLogic.OperatorCanOpenCashDrawer;
                CacheBusinessLogic.KickbackAmount = null;

                if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "SwitchUserToCashDraw")
                {
                    PerformCashDraw();
                }
            }
            catch (Exception)
            {

            }
        }

        private void UpdateSale(SaleModel sale)
        {
            if (sale == null)
            {
                return;
            }

            if (CacheBusinessLogic.SaleNumber != sale.SaleNumber)
            {
                IsReturn = false;
            }
            
            SaleModel = sale;
            _saleErrors.AddRange(sale.SaleLineError);

            WriteToLineDisplay(sale.LineDisplay);

            ShowSaleErrorPopups();
            CacheBusinessLogic.EnableWriteOffButton = sale.EnableWriteOffButton;
            SetSaleNumberAndCustomerName();
            MessengerInstance.Send(sale.SaleLines, "SyncHotProducts");
            CacheBusinessLogic.IsCurrentSaleEmpty = IsSaleEmpty();
            CacheBusinessLogic.IsKickBack = false;
            if (CacheBusinessLogic.IsCurrentSaleEmpty)
            {
                CacheBusinessLogic.KickBackCardNumber = null;
                _isSuspendOrVoidDone = false;
            }
            UpdateSaleSuspensionFlags();
            IsExactCashEnabled = !CacheBusinessLogic.IsCurrentSaleEmpty && SaleModel.EnableExactChange;
            IsVoidSaleVisible = !CacheBusinessLogic.IsCurrentSaleEmpty;
            IsAcceptTenderEnabled = IsVoidSaleVisible = !CacheBusinessLogic.IsCurrentSaleEmpty;
            IsBottleReturnEnabled = CacheBusinessLogic.OperatorCanReturnBottle
                && !IsVoidSaleVisible;
            IsSaleReturnEnabled = CacheBusinessLogic.OperatorCanReturnSale
                && !IsVoidSaleVisible;
            IsWriteOffEnabled = !CacheBusinessLogic.IsCurrentSaleEmpty &&
                CacheBusinessLogic.EnableWriteOffButton;
           // _cacheBussinessLogic.HasCarwashProductInSale = sale.HasCarwashProducts;
            SetFocusOnGrid(new SetFocusOnGridMessage());
            MessengerInstance.Send(new EnableDisablePaymentButtonMessage());
        }

        private void ShowSaleErrorPopups()
        {
            if (_saleErrors?.Count > 0)
            {
                ShowNotification(_saleErrors.FirstOrDefault().Message,
                  ShowSaleErrorPopups,
                  ShowSaleErrorPopups,
                  ApplicationConstants.ButtonWarningColor);
                _saleErrors.RemoveAt(0);
            }
        }

        /// <summary>
        /// Method to Initialize Commands
        /// </summary>
        private void InitializeCommands()
        {
            EnterPressedOnQuantityCommand = new RelayCommand<object>((s) => EnterPressedOnQuantity(s));
            RaiseVoidSalePopupCommand = new RelayCommand(() => { PerformAction(ValidateVoidSale); });
            MessageItemClickedCommand = new RelayCommand<Reasons>(VoidSaleWithReason);
            OpenCheckoutOptionsPopupCommmand = new RelayCommand(OpenCheckoutOptionsPopup);
            CloseCheckoutOptionsPopupCommand = new RelayCommand(CloseCheckoutOptionsPopup);
            OpenReturnsPopupCommand = new RelayCommand(OpenReturnsPopup);
            CloseReturnsPopupCommand = new RelayCommand(CloseReturnsPopup);
            BottleReturnsCommand = new RelayCommand(NavigateToBottleReturns);
            OpenSuspendedSalesCommand = new RelayCommand(OpenSuspendSalePage);
            SuspendSaleCommand = new RelayCommand(() => PerformAction(SuspendSale));
            RaiseLoyaltyGiftPopupCommand = new RelayCommand(OpenLoyaltyGiftPopup);
            ReturnSaleCommand = new RelayCommand(ReturnSale);
            QuantityChangedCommand = new RelayCommand<object>(UpdateQuantity);
            PriceChangedCommand = new RelayCommand<object>(UpdatePrice);
            PriceChangedWithReasonCommand = new RelayCommand<Reasons>(PriceChangedWithReason);
            QuantityChangedWithReasonCommand = new RelayCommand<Reasons>(QuantityChangedWithReason);
            DiscountChangedCommand = new RelayCommand<object>(DiscountConfirmation);
            DiscountChangedWithReasonCommand = new RelayCommand<Reasons>(DiscountChangedWithReason);
            MessageSelectedCommand = new RelayCommand<object>(MessageSelected);
            SaleLineDeletedCommand = new RelayCommand<SaleLineModel>(DeleteConfirmation);
            AddStockItemForSaleCommand = new RelayCommand(ConfirmGiftCardSale);
            RaiseCustomerPopupCommand = new RelayCommand(RaiseCustomerPopup);
            AcceptTenderCommand = new RelayCommand(AcceptTenders);
            RaiseCashPopupCommand = new RelayCommand(RaiseCashPopup);
            RaiseMessagePopupCommand = new RelayCommand(RaiseMessagePopup);
            WriteOffCommand = new RelayCommand(WriteOff);
            WriteOffReasonSelectedCommand = new RelayCommand<Reasons>(WriteOffReasonSelected);
            ExactChangeCommand = new RelayCommand(() => PerformAction(ExactChange));
            OpenCashDrawerReasonSelectedCommand = new RelayCommand<Reasons>(OpenCashDrawerReasonSelected);
            OpenCashDrawerCommand = new RelayCommand(() => PerformAction(OpenCashDrawer));
            SaveMessageCommand = new RelayCommand(SaveMessage);
            OpenKickbackBalancePopupCommand = new RelayCommand(OpenKickbackBalancePopup);
            CloseKickBackNumberPopupCommand = new RelayCommand(KickBackCanceled);
            OpenCarwashPopupCommand = new RelayCommand(OpenCarwashPopup);
            CloseCarwashPopupCommand = new RelayCommand(CloseCarwashPopup);
            CheckCarwashCodeCommand = new RelayCommand(CheckCarwashCode);
            PSInetCommand = new RelayCommand(OpenPSInetPage);
            AckrooCommand = new RelayCommand(OpenAkrooPage);
        }

        private void OpenAkrooPage()
        {
            CloseCheckoutOptionsPopup();
            NavigateService.Instance.NavigateToAkrooPage();
            MessengerInstance.Send(new AkrooMessage());
        }

        private void OpenPSInetPage()
        {
            CloseCheckoutOptionsPopup();
            NavigateService.Instance.NavigateToPSInetPage();
            Messenger.Default.Send<PSButtonMessage>(new PSButtonMessage { CurrentSale = SaleModel });
        }

        public void CheckCarwashCode()
        {
            PopupService.IsCarwashPopupOpen = false;
            var carwashCode = PopupService.CarwashCode;
            PerformAction(async () =>
            {
                try
                {
                    if (await _carwashBusinessLogic.GetCarwasServerStatus())
                    {
                        var isCodeValid = await _carwashBusinessLogic.ValidateCarwashCode(carwashCode);
                        if (isCodeValid)
                        {
                            PopupService.IsPopupOpen = false;
                            ShowNotification(ApplicationConstants.CarwashCodeValidMessage, navigateToAddStock, navigateToAddStock);
                            Log.Info("Message Shown:" + ApplicationConstants.CarwashCodeValidMessage);
                        }
                        else
                        {
                            PopupService.IsPopupOpen = false;
                            ShowNotification(ApplicationConstants.CarwashCodeInValidMessage, navigateToAddStock, navigateToAddStock);
                            Log.Info("Message Shown:" + ApplicationConstants.CarwashCodeInValidMessage);
                        }
                    }
                    else
                    {
                        PopupService.IsPopupOpen = false;
                        ShowNotification(ApplicationConstants.CarwashServerErrorMessageOnValidation, navigateToAddStock, navigateToAddStock);
                        Log.Info("Message Shown:" + ApplicationConstants.CarwashServerErrorMessageOnValidation);
                    }
                }
                catch(Exception ex)
                {
                }
            });

           
        }

        private void CloseCarwashPopup()
        {
            PopupService.IsCarwashPopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void OpenCarwashPopup()
        {
            PopupService.CarwashCode = "";
            CloseCheckoutOptionsPopup();
            OpenCarwashPopupService();
        }

        private void OpenCarwashPopupService()
        {
            PopupService.IsPopupOpen = true;
            PopupService.IsCarwashPopupOpen  = true;
        }

        private void KickBackCanceled()
        {
            CloseKickBackNumberPopup();
            PopupService.KickBackNumber = string.Empty;
            ProcessTaxExemption();
        }

        private void KickbackNumberEntered(object s)
        {
            if (Helper.IsEnterKey(s))
            {
                CloseKickBackNumberPopup();
                VerifyKickBack();
            }
        }

        private void VerifyKickBack()
        {
            PerformAction(async () =>
            {
                try
                {
                    var verifyKickbackResponse = new VerifyKickback();

                    if (!string.IsNullOrEmpty(CacheBusinessLogic.KickBackCardNumber))
                    {
                        if (Helper.IsKickBackCardNumber(CacheBusinessLogic.KickBackCardNumber))
                        {
                            verifyKickbackResponse = await _kickBackBusinessLogic.
                                        VerifyKickBack(CacheBusinessLogic.KickBackCardNumber, null);
                        }
                        else
                        {
                            verifyKickbackResponse = await _kickBackBusinessLogic.
                                        VerifyKickBack(null, CacheBusinessLogic.KickBackCardNumber);
                        }
                    }
                    if (Helper.GetDoubleValue(verifyKickbackResponse.BalancePoints) >= CacheBusinessLogic.KickbackRedeemMsg 
                            && verifyKickbackResponse.Verify)
                    {
                       // if (verifyKickbackResponse.Verify)
                        //{
                            SetKickBackBalanceInCache(verifyKickbackResponse.Value);
                            VerifyKickBack(verifyKickbackResponse.BalancePoints, verifyKickbackResponse.Value);
                       // }
                    }
                    else
                    {
                        ProcessTaxExemption();
                    }

                }
                catch (ApiDataException ex)
                {
                    if (!string.IsNullOrEmpty(ex.Error?.Message) &&
                             ApplicationConstants.CommunicationErrormessage.Equals(ex.Message))
                    {
                        Log.Warn(ex);
                        ShowNotification(ex.Error.Message,
                            () => { ProcessTaxExemption(); },
                            () => { ProcessTaxExemption(); },
                            ApplicationConstants.ButtonWarningColor);
                    }
                    else
                    {
                        CacheBusinessLogic.KickBackCardNumber = null;
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    CacheBusinessLogic.KickBackCardNumber = null;
                    throw;
                }
                finally
                {
                    PopupService.KickBackNumber = string.Empty;
                }

            });
        }

        private void VerifyKickBack(string balancePoints, string value)
        {
            var kickBackMessage = string.Format(ApplicationConstants.VerifyKickbackMessage,
                balancePoints, value);

            ShowConfirmationMessage(kickBackMessage,
             () =>
             {               
                 CloseKickBackNumberPopup();
                 CheckKickBackResponse(true);
             },
            () =>
            {
                CloseKickBackNumberPopup();
                CheckKickBackResponse(false);
            },
            () =>
            {
                CloseKickBackNumberPopup();
                CheckKickBackResponse(false);
            });
        }

        private async Task CheckKickBackResponse(bool response)
        {
            if (!response)
            {
                CacheBusinessLogic.KickbackAmount = null;
            }
            CacheBusinessLogic.IsKickBack = true;
            var checkKickBackresponse = await _kickBackBusinessLogic.CheckKickBackResponse(response);
            ProcessTaxExemption();
        }

        private void OpenKickbackBalancePopup()
        {
            CloseCheckoutOptionsPopup();

            PopupService.IsPopupOpen = true;
            PopupService.IsKickbackBalancePopupOpen = true;
        }

        private async Task ValidateVoidSale()
        {
            string message = "";
            try
            {
                var result = (from c in _saleModel.SaleLines
                              where c.Dept == "40"
                              select c).ToList();
                if (result.Count > 0)
                {
                    message = "The sale contains PSInet item(s)\r\n"
                                 + " and Can't be void here.~Void Sale";
                    ShowNotification(message,
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor);
                    return;
                }
                var result_ackroo = (from c in _saleModel.SaleLines
                                     where c.Code == "ACKG" || c.Code == "ACKGCWR" || c.Code == "ACKGCWU"
                                     select c).ToList();
                if (result_ackroo.Count > 0)
                {
                    message = "The sale contains Ackroo item(s)\r\n"
                                 + " and Can't be void here.~Void Sale";
                    ShowNotification(message,
                        null,
                        null,
                        ApplicationConstants.ButtonWarningColor);
                    return;
                }
                await _saleBussinessLogic.ValidateVoidSale();
                OpenVoidSalePopup();
            }
            catch (SwitchUserException ex)
            {
                ShowNotification(ex.Error.Message,
                    () =>
                    {
                        CacheBusinessLogic.FramePriorSwitchUserNavigation = "SwitchUserToVoidSale";
                        NavigateService.Instance.NavigateToLogout();
                        MessengerInstance.Send(_reasonForVoidingSale,
                            "SwitchUserAndVoidSale");
                    },
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private async Task ExactChange()
        {
            var response = await _paymentBusinessLogic.PayByExactChange();

            if (response.LineDisplays != null)
            {
                foreach (var lineDisplay in response.LineDisplays)
                {
                    WriteToLineDisplay(lineDisplay);
                }
            }

            if (response.OpenCashDrawer)
            {
                base.OpenCashDrawer();
            }

            MessengerInstance.Send(response.NewSale.ToModel(), "UpdateSale");

            if (!string.IsNullOrEmpty(response.LimitExceedMessage))
            {
                ShowNotification(response.LimitExceedMessage,
                    async () =>
                    {
                        await ExactChangePostOperations(response);
                    },
                     async () =>
                     {
                         await ExactChangePostOperations(response);
                     },
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                await ExactChangePostOperations(response);
            }
        }

        private async Task ExactChangePostOperations(ExactChange response)
        {
            await PerformPrint(response.Report);

            if (CacheBusinessLogic.SwitchUserOnEachSale)
            {
                NavigateService.Instance.NavigateToLogout();
                MessengerInstance.Send<ForceUserIDMessage>(new ForceUserIDMessage());
            }
        }

        #region Open Cash Drawer

        private void OpenCashDrawerReasonSelected(Reasons reason)
        {
            _reasonForOpenCashDrawer = reason;
            CloseReasonPopup();

            PerformCashDraw();

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
                PerformCashDraw();
            }
        }

        private void PerformCashDraw()
        {
            PerformAction(async () =>
            {
                try
                {
                    base.OpenCashDrawer();
                    await _cashBusinessLogic.OpenCashDrawer(_reasonForOpenCashDrawer.Code);

                    if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "SwitchUserToCashDraw")
                    {
                        CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                        CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                    }
                }
                catch (SwitchUserException ex)
                {
                    ShowNotification(ex.Error.Message,
                       SwitchUserToCashDraw,
                       SwitchUserToCashDraw,
                       ApplicationConstants.ButtonWarningColor);
                }
            });
        }

        private void SwitchUserToCashDraw()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "SwitchUserToCashDraw";
            NavigateService.Instance.NavigateToLogout();
        }

        #endregion
        private void IssueDiscount(FuelDiscount message)
        {
            foreach (SaleLineModel c in message.FuelLines)
            {
                _changedSaleLine = c;
                _changedSaleLine.Discount = string.Format(CultureInfo.InvariantCulture, "{0:0.00}", message.DiscountRate);

                _discountType = message.DiscoutType;
                UpdateItemForFuelDiscount(message);

            }

            PerformAction(async () =>
            {
                await Task.Delay(200);
                var checkoutSummary = await _checkoutBusinessLogic.SaleSummary();
                MessengerInstance.Send(checkoutSummary);
            });

            if (message.Reason == "NO DISCOUNT")
            {
                Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = true });
            }
            else if (_cacheBussinessLogic.isTDRS_FUELDISCSupported)
            {
                Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = true });
            }
            else if (message.CustGrpID == _cacheBussinessLogic.displayCustGrpID)
            {
                Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = true });
            }
            else
            {
                Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = false });
            }

        }
        private void UpdateItemForFuelDiscount(FuelDiscount message)
        {
            var startTime = DateTime.Now;
            try
            {

                _tracker.Restart();
                LoadingService.ShowLoadingStatus(true);
                PerformAction(async () => {
                    string reasoncode = string.Empty;
                    string discountname = string.Empty;

                    if (message.Reason != "NO DISCOUNT")
                    {
                        reasoncode = message.Reason;
                        discountname = message.DiscountName;
                    }

                    var result = await _saleBussinessLogic.UpdateSale(
                        _changedSaleLine.LineNumber,
                        _changedSaleLine.Discount,
                        _discountType,
                        _changedSaleLine.Quantity,
                        _changedSaleLine.Price,
                        reasoncode,
                        discountname);

                    var sale = result.ToModel();
                    MessengerInstance.Send(sale, "UpdateSale");
                    AckrooOutStandingAmtChangeMessage ackAmout = new AckrooOutStandingAmtChangeMessage();
                    ackAmout.NewOutStandingAmount = double.Parse(result.TotalAmount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
                    MessengerInstance.Send<AckrooOutStandingAmtChangeMessage>(ackAmout);
                });



            }
            catch (UserNotAuthorizedException)
            {
                NavigateService.Instance.NavigateToLogin();
            }
            catch (ApiDataException ex)
            {
                ShowNotification(ex.Message, null, null,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
                var endTime = DateTime.Now;
                Log.Info(string.Format("Time taken in update sale  is {0}ms ", (endTime - startTime).TotalMilliseconds));
            }
        }
        /// <summary>
        /// Method for Accept Tenders
        /// </summary>
        private void AcceptTenders()
        {
            double toalAmt = 0;
            if (!double.TryParse(_saleModel.TotalAmount, out toalAmt))
            {
                return;
            }
            //Ackroo Tender
            if (_cacheBussinessLogic.REWARDS_Enabled && toalAmt>0)
            {
                PopupService.IsAckTenderPopOpen = true;
                //PopupService.IsPopupOpen = true;
                MessengerInstance.Send(new AckrooTenderInitMessage());
                MessengerInstance.Send(new AckrooTenderMessage());
            }


            //Fuel Discout logic
            if (_cacheBussinessLogic.isFuelDiscountSupported)
            {

                var fl = (from c in SaleModel.SaleLines
                          where c.Dept == _cacheBussinessLogic.FuelDept
                          select c).ToList();

                if (fl.Count > 0)
                {

                    PopupService.IsFuelDiscountPopupOpen = true;
                    PopupService.IsPopupOpen = true;
                    Messenger.Default.Send<FuelLineInfo>(new FuelLineInfo() { FuelLines = fl });

                }
                else
                {
                    Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = true });
                }

            }
            else
            {
                Messenger.Default.Send<ShowFleetCreditTenders>(new ShowFleetCreditTenders() { DisplayFleetCreditTenders = true });
            }

            _carwashServerDown = false;
             PerformAction(async () =>
             {
                  
                if (_cacheBussinessLogic.HasCarwashProductInSale)
                {
                    if (IsCarwashIntegrated && IsCarwashSupported)
                    {
                        try
                        {
                            var isServerActive = await _carwashBusinessLogic.GetCarwasServerStatus();
                            if (!isServerActive)
                            {
                                ShowNotification(ApplicationConstants.CarwashServerErrorMessage, navigateToAddStock, navigateToAddStock);
                                Log.Info("Message Shown:"+ ApplicationConstants.CarwashServerErrorMessage);
                                 _carwashServerDown = true;
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                 if (!_carwashServerDown)
                 {
                     var skipKickBack = true;
                     if (CacheBusinessLogic.SupportKickback)
                     {
                         skipKickBack = false;

                         try
                         {
                             var response = await _kickBackBusinessLogic.ValidateGasKing();

                             if (response.IsKickBackLinked)
                             {
                                 if (response.PointsReedemed >= CacheBusinessLogic.KickbackRedeemMsg)
                                 {
                                     VerifyKickBack(response.PointsReedemed.ToString(), response.Value);
                                     SetKickBackBalanceInCache(response.Value);
                                 }
                                 else
                                 {
                                     skipKickBack = true;
                                 }
                             }
                             else
                             {
                                 if (!string.IsNullOrEmpty(CacheBusinessLogic.KickBackCardNumber))
                                 {
                                     VerifyKickBack();
                                 }
                                 else
                                 {
                                     OpenKickBackNumberPopup();
                                 }
                             }
                         }
                         catch (ApiDataException ex)
                         {
                             ShowNotification(ex.Error.Message,
                                 ProcessTaxExemption,
                                 ProcessTaxExemption,
                                 ApplicationConstants.ButtonWarningColor);
                         }
                     }
                     else
                     {
                         skipKickBack = true;
                     }

                     if (skipKickBack)
                     {
                         ProcessTaxExemption();
                     }
                 }
                });
            

        }

        private void SetKickBackBalanceInCache(string value)
        {
            CacheBusinessLogic.KickbackAmount = Helper.GetDoubleValue(value);
        }

        private void ProcessTaxExemption()
        {
            _taxExemptionVM = SimpleIoc.Default.GetInstance<TaxExemptionVM>();
            MessengerInstance.Send(_verifyTaxExempt);
        }

        private void navigateToAddStock()
        {
            PopupService.IsPopupOpen = false;
            NavigateService.Instance.NavigateToHome();
        }

        private void OpenKickBackNumberPopup()
        {
            PopupService.CustomKickbackMessage = CacheBusinessLogic.CustomKickbackmsg;
            PopupService.IsKickbackNumberPopupOpen = true;
            PopupService.IsPopupOpen = true;

            PopupService.YesConfirmationCommand = new RelayCommand(() =>
            {
                CacheBusinessLogic.KickBackCardNumber = PopupService.KickBackNumber;
                CloseKickBackNumberPopup();
                VerifyKickBack();
            });

            PopupService.KickBackNumberEnteredCommand = new RelayCommand<object>((s) =>
            {
                CacheBusinessLogic.KickBackCardNumber = PopupService.KickBackNumber;
                KickbackNumberEntered(s);
            });
        }

        private void CloseKickBackNumberPopup()
        {
            PopupService.IsKickbackNumberPopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void WriteOffReasonSelected(Reasons reason)
        {
            _reasonForWriteOff = reason;
            CloseReasonPopup();
            PerformAction(PerformWriteOff);
        }

        private async Task PerformWriteOff()
        {
            var writeOffModel = await _saleBussinessLogic.WriteOff(_reasonForWriteOff);

            WriteToLineDisplay(writeOffModel.LineDisplay);

            var sale = writeOffModel.Sale;

            MessengerInstance.Send(sale.ToModel(), "UpdateSale");

            if (!CacheBusinessLogic.ForcePrintReceipt)
            {
                ShowConfirmationMessage(ApplicationConstants.WriteOffConfirmation,
                   async () =>
                   {
                       await PerformPrint(writeOffModel.Receipt);
                   });
            }
            else
            {
                await PerformPrint(writeOffModel.Receipt);
            }
        }

        /// <summary>
        /// Opens reason pop up and asks for reason for the write off
        /// </summary>
        private void WriteOff()
        {
            CloseCheckoutOptionsPopup();
            PerformAction(async () =>
            {
                await GetReasonListAsync(EntityLayer.ReasonType.writeOff,
                    WriteOffReasonSelectedCommand);
            });
        }

        #region Message
        private void RaiseMessagePopup()
        {
            CloseCheckoutOptionsPopup();

            PerformAction(async () =>
            {
                if (!PopupService.IsPopupOpen)
                {
                    PopupService.ReasonList?.Clear();
                    var messages = await _messageBusinessLogic.GetMessage();

                    foreach (var message in messages)
                    {
                        PopupService.ReasonList.Add(new Reasons
                        {
                            Code = message.Index,
                            Description = message.Caption,
                            Caption = message.ActualMessage
                        });
                    }


                    PopupService.Title = "Message";
                    PopupService.MessageItemClicked = MessageSelectedCommand;
                    PopupService.IsPopupOpen = true;
                    PopupService.IsReasonPopupOpen = true;

                    PopupService.CloseCommand = new RelayCommand(CloseReasonPopup);
                }
            });
        }

        private void MessageSelected(dynamic s)
        {
            CloseReasonPopup();

            GenericReason = string.Empty;
            _selectedMessage = s as Reasons;
            if (_selectedMessage.Code.Equals("0"))
            {
                PopupService.PopupInstance.CloseCommand =
                    new RelayCommand(CloseMessagePopup);

                _saveMessageDelegate = CloseMessagePopup;

                PopupService.PopupInstance.IsMessagePopupOpen = true;
                PopupService.IsPopupOpen = true;
            }
            else
            {
                SaveMessageList(_selectedMessage);
            }
        }

        private void SaveMessage()
        {
            var message = string.Format(ApplicationConstants.MessageConfirmation, GenericReason);

            if (_saveMessageDelegate != null)
            {
                _saveMessageDelegate();
            }

            ShowConfirmationMessage(message,
            () =>
            {
                _selectedMessage.Caption = GenericReason;
                SaveMessageList(_selectedMessage);
            });
        }

        private void CloseMessagePopup()
        {
            PopupService.PopupInstance.IsMessagePopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void SaveMessageList(Reasons selectedMessage)
        {
            PerformAction(async () =>
            {
                await _messageBusinessLogic.AddMessage(selectedMessage.Code,
                    selectedMessage.Caption);
            });
        }

        #endregion


        private void RaiseCashPopup()
        {
            CloseCheckoutOptionsPopup();
            ShowConfirmationMessage(ApplicationConstants.Cash, OpenATMPopup,
               NavigateService.Instance.NavigateToCashDraw, null,
               ApplicationConstants.ButtonFooterColor, ApplicationConstants.ButtonFooterColor,
               ApplicationConstants.CashDrop, ApplicationConstants.CashDraw,
               CacheBusinessLogic.OperatorCanDropCash,
               CacheBusinessLogic.OperatorCanDrawCash);
        }

        private void OpenATMPopup()
        {
            _cashDropReason = "SAFE";
            if (CacheBusinessLogic.AskForCashDropReason)
            {
                ShowConfirmationMessage(
                 ApplicationConstants.CashDropType,
                 () =>
                 {
                     _cashDropReason = "ATM";
                     OpenEnvelopeNumberPopup();
                 },
                 OpenEnvelopeNumberPopup,
                 null,
                 ApplicationConstants.ButtonFooterColor,
                 ApplicationConstants.ButtonFooterColor,
                 ApplicationConstants.ATMDrop,
                 ApplicationConstants.SafeDrop,
                 CacheBusinessLogic.OperatorCanDropCash,
                 CacheBusinessLogic.OperatorCanDrawCash);
            }
            else
            {
                OpenEnvelopeNumberPopup();
            }
        }

        private void OpenEnvelopeNumberPopup()
        {
            if (CacheBusinessLogic.RequireEnvelopNumber)
            {
                PopupService.PopupInstance.IsEnvelopeOpen = true;
                PopupService.IsPopupOpen = true;

                PopupService.PopupInstance.NoConfirmationCommand =
                     PopupService.PopupInstance.CloseCommand = new
                    RelayCommand(() =>
                    {
                        CloseEnvelopePopup();
                    });

                PopupService.PopupInstance.YesConfirmationCommand = new
                    RelayCommand(() =>
                    {
                        EnvelopeNumberEntered();
                    });
            }
            else
            {
                var cashDropEnvelopeModel = new CashDropEnvelopeModel
                {
                    EnvelopeNumber = string.Empty,
                    Reason = _cashDropReason
                };

                NavigateToCashDrop(cashDropEnvelopeModel);
            }
        }

        private void CloseEnvelopePopup()
        {
            EnvelopeNumber = string.Empty;
            PopupService.IsPopupOpen = false;
            PopupService.PopupInstance.IsEnvelopeOpen = false;
        }

        private void EnvelopeNumberEntered()
        {
            if (!string.IsNullOrEmpty(EnvelopeNumber))
            {
                var cashDropEnvelopeModel = new CashDropEnvelopeModel
                {
                    EnvelopeNumber = EnvelopeNumber,
                    Reason = _cashDropReason
                };

                NavigateToCashDrop(cashDropEnvelopeModel);
                CloseEnvelopePopup();
            }
        }

        private void NavigateToCashDrop(CashDropEnvelopeModel cashDropEnvelopeModel)
        {
            NavigateService.Instance.NavigateToCashDrop();
            MessengerInstance.Send<CashDropEnvelopeModel>(cashDropEnvelopeModel,
                "GetCashDropTenders");
        }

        private void EnterPressedOnQuantity(object s)
        {
            if (Helper.IsEnterKey(s))
            {
                ConfirmGiftCardSale();
            }
        }

        private void ConfirmGiftCardSale()
        {
            var price = 0M;
            decimal.TryParse(GiftCard.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out price);
            if (price == 0)
            {
                ShowConfirmationMessage(ApplicationConstants.GiftCardZeroPriceError,
                    AddStockItemForSale);
            }
            else
            {
                AddStockItemForSale();
            }
        }

        private void RaiseCustomerPopup()
        {
            if (CacheBusinessLogic.OperatorCanUseLoyalty)
            {
                ShowConfirmationMessage(ApplicationConstants.Customer,
                    NavigateService.Instance.NavigateToCustomers,
                    NavigateService.Instance.NavigateToLoyaltyCustomers,
                    null,
                    ApplicationConstants.ButtonFooterColor,
                    ApplicationConstants.ButtonFooterColor,
                    ApplicationConstants.CustomerSearch,
                    ApplicationConstants.LoyaltySearch,
                    CacheBusinessLogic.OperatorCanUseCustomer,
                    CacheBusinessLogic.OperatorCanUseLoyalty && CacheBusinessLogic.OperatorCanUseCustomer);
            }
            else
            {
                NavigateService.Instance.NavigateToCustomers();
            }
        }

        private void DiscountChangedWithReason(Reasons reason)
        {
            _reasonForSaleLineUpdate = reason;
            CloseReasonPopup();
            PerformAction(async () => await UpdateItemForSale(ReasonType.discounts));
        }



        private void QuantityChangedWithReason(Reasons reason)
        {
            _reasonForSaleLineUpdate = reason;
            CloseReasonPopup();
            PerformAction(async () => await UpdateItemForSale(ReasonType.refunds));
        }


        private void PriceChangedWithReason(Reasons reason)
        {
            _reasonForSaleLineUpdate = reason;
            CloseReasonPopup();
            PerformAction(async () => await UpdateItemForSale(ReasonType.priceChanges));
        }

        private void DeleteConfirmation(SaleLineModel item)
        {
            if (item.Code == "ACKG" || item.Code == "ACKGCWR" || item.Code == "ACKGCWU")
            {
                ShowNotification("Ackroo sale item can't be deleted.~Delete Sale Line",
                                   null, null, ApplicationConstants.ButtonWarningColor);
                return;
            }
            if (item.Dept == "40")
            {
                ShowNotification("PSInet sale item can't be deleted.~Delete Sale Line",
                                   null, null, ApplicationConstants.ButtonWarningColor);
                return;
            }
            ShowConfirmationMessage(ApplicationConstants.DeleteSaleLine, () =>
            {
                DeleteSaleLine(item);
            });
        }

        private void DeleteSaleLine(SaleLineModel item)
        {
            PerformAction(async () =>
            {
                var startTime = DateTime.Now;
                try
                {
                    _tracker.Restart();
                    var result = await _saleBussinessLogic.RemoveSaleLine(item.LineNumber);
                    MessengerInstance.Send(result.ToModel(), "UpdateSale");
                    SoundService.Instance.PlaySoundFile(SoundTypes.ItemDeleted);
                }
                catch (SwitchUserException ex)
                {
                    ShowNotification(ex.Error.Message,
                        () =>
                        {
                            CacheBusinessLogic.FramePriorSwitchUserNavigation = "SwitchUserToDeleteLine";
                            NavigateService.Instance.NavigateToLogout();
                            MessengerInstance.Send(item.LineNumber,
                                "SwitchUserAndDeleteSaleLine");
                        },
                        null,
                        ApplicationConstants.ButtonWarningColor);
                }
                finally
                {
                    var endTime = DateTime.Now;
                    Log.Info(string.Format("Time taken in delete sale item is {0}ms ", (endTime - startTime).TotalMilliseconds));
                }
            });
        }

        private void DiscountConfirmation(object item)
        {
            var selectedItem = item as SaleLineModel;
            _changedSaleLine = selectedItem.Clone();

            decimal discount = 0M;

            if (decimal.TryParse(_changedSaleLine?.Discount, NumberStyles.Any, CultureInfo.InvariantCulture, out discount) && discount < 0)
            {
                ShowNotification(ApplicationConstants.NegativeDiscountsNotAllowed,
                    null, null, ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                ShowConfirmationMessage(ApplicationConstants.DiscountType, () =>
                    {
                        _discountType = "%";
                        UpdateDiscount();
                    }, () =>
                    {
                        _discountType = "$";
                        UpdateDiscount();
                    });
            }
        }

        private void UpdateDiscount()
        {
            if (_changedSaleLine.AllowDiscoutReason)
            {
                PerformAction(async () => { await GetReasonListAsync(EntityLayer.ReasonType.discounts, DiscountChangedWithReasonCommand); });
            }
            else
            {
                _reasonForSaleLineUpdate = new Reasons();
                PerformAction(async () => await UpdateItemForSale(ReasonType.discounts));
            }
        }

        private void UpdatePrice(object item)
        {
            _changedSaleLine = (item as SaleLineModel).Clone();

            var price = 0M;
            decimal.TryParse(_changedSaleLine?.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out price);

            if (price >= 10000)
            {
                ShowNotification(ApplicationConstants.MaximumPriceError,
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
            else if (price <= -10000)
            {
                // Do Nothing in this scenario
            }
            else if (_changedSaleLine.AllowPriceReason)
            {
                PerformAction(async () => { await GetReasonListAsync(EntityLayer.ReasonType.priceChanges, PriceChangedWithReasonCommand); });
            }
            else
            {
                _reasonForSaleLineUpdate = new Reasons();
                PerformAction(async () => await UpdateItemForSale(ReasonType.priceChanges));
            }
        }

        private void UpdateQuantity(object item)
        {
            var selectedItem = item as SaleLineModel;
            _changedSaleLine = selectedItem.Clone();
            _reasonForSaleLineUpdate = new Reasons();
            SelectedSaleLine.Quantity = _changedSaleLine?.Quantity;
            int changedQuantity = 0;

            if (int.TryParse(_changedSaleLine?.Quantity, out changedQuantity) && changedQuantity < 0 && selectedItem.AllowReturnReason)
            {
                PerformAction(async () =>
                {
                    await GetReasonListAsync(EntityLayer.ReasonType.refunds, QuantityChangedWithReasonCommand);
                });
            }
            else
            {
                PerformAction(async () => await UpdateItemForSale(null));
            }
        }

        private async void AddNewStock()
        {
            VerifyAndAddStockForSale(new AddStockToSaleMessage
            {
                StockCode = _currentStockCodeToProcess,
                Quantity = 1,
                IsManuallyAdded = true
            });
        }

        private void ReturnSale()
        {
            CloseReturnsPopup();
            NavigateService.Instance.NavigateToReturnSale();
        }

        private async Task SuspendSale()
        {
            CloseCheckoutOptionsPopup();
            var response = await _saleBussinessLogic.SuspendSale();

            SaleModel = new SaleModel
            {
                Customer = response.Customer,
                TillNumber = response.Till,
                SaleLines = new ObservableCollection<SaleLineModel>(),
                SaleNumber = response.SaleNumber,
                Summary = string.Empty,
                TotalAmount = string.Empty,
                LineDisplay = response.LineDisplay
            };
            _isSuspendOrVoidDone = true;
            MessengerInstance.Send(SaleModel, "UpdateSale");
        }

        private async Task InitializeNewSale()
        {
            try
            {
                var response = await _saleBussinessLogic.InitializeNewSale();
                var sale = response.ToModel();
                MessengerInstance.Send(sale, "UpdateSale");
            }
            finally
            {
                OperationsCompletedInLogin++;
            }
        }

        private void SetSaleNumberAndCustomerName()
        {
            CustomerName = CacheBusinessLogic.CustomerName = SaleModel.Customer;
            SaleNumber = CacheBusinessLogic.SaleNumber = SaleModel.SaleNumber;
            CacheBusinessLogic.TillNumberForSale = SaleModel.TillNumber;
        }

        private void OpenSuspendSalePage()
        {
            CloseCheckoutOptionsPopup();

            if (!IsSaleEmpty())
            {
                ShowNotification(ApplicationConstants.CannotUnsuspend,
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                NavigateService.Instance.NavigateToUnsuspendedSale();
            }
        }

        private void OpenLoyaltyGiftPopup()
        {
            ShowNotification(ApplicationConstants.LoyaltyGift, NavigateService.Instance.NavigateToGiveXPage, null,
                ApplicationConstants.ButtonFooterColor, ApplicationConstants.GiveX,
                CacheBusinessLogic.CertificateType.Equals("GiveX"));
        }

        private void NavigateToBottleReturns()
        {
            PopupService.IsPopupOpen = false;
            PopupService.IsReturnsPopupOpen = false;
            NavigateService.Instance.NavigateToBottleReturns();
        }

        private void OpenReturnsPopup()
        {
            CloseCheckoutOptionsPopup();
            //  ReInitialize();
            PopupService.IsPopupOpen = true;
            PopupService.IsReturnsPopupOpen = true;
        }

        private void CloseReturnsPopup()
        {
            PopupService.IsPopupOpen = false;
            PopupService.IsReturnsPopupOpen = false;
        }

        private void CloseCheckoutOptionsPopup()
        {
            PopupService.IsPopupOpen = false;
            PopupService.IsCheckoutOptionsOpen = false;
        }

        private void OpenCheckoutOptionsPopup()
        {
            PopupService.IsPopupOpen = true;
            PopupService.IsCheckoutOptionsOpen = true;
            IsMessageInputEnable = CacheBusinessLogic.EnableMsgInput;
            SupportKickBack = CacheBusinessLogic.SupportKickback;
            IsCarwashIntegrated = CacheBusinessLogic.IsCarwashIntegrated;
            IsCarwashSupported = CacheBusinessLogic.IsCarwashSupported;
        }

        /// <summary>
        /// Method to Open void sale pop up
        /// </summary>
        /// <returns></returns>
        private void OpenVoidSalePopup()
        {
            ShowConfirmationMessage(ApplicationConstants.VoidSale,
                VoidSale,
                null,
                null,
                ApplicationConstants.ButtonConfirmationColor,
                ApplicationConstants.ButtonFooterColor);
        }

        /// <summary>
        /// Method to get reasons for voiding sale
        /// </summary>
        private void VoidSale()
        {
            if (CacheBusinessLogic.UseReasonForVoid)
            {
                PerformAction(async () =>
                {
                    await GetReasonListAsync(ReasonType.voidSales, MessageItemClickedCommand);
                });
            }
            else
            {
                _reasonForVoidingSale = new Reasons();
                PerformAction(VoidSaleAsync);
            }
        }

        /// <summary>
        /// Method to get reason list
        /// </summary>
        /// <returns></returns>
        private async Task GetReasonListAsync(ReasonType reasonEnum,
            RelayCommand<Reasons> reasonSelectCommand)
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

                PopupService.CloseCommand = new RelayCommand(CloseReasonPopup);
            }
        }

        /// <summary>
        /// Method to void sale
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        private void VoidSaleWithReason(Reasons reason)
        {
            _reasonForVoidingSale = reason;
            CloseReasonPopup();
            PerformAction(VoidSaleAsync); // Voiding sale with reason
        }

        /// <summary>
        /// Method to void Sale
        /// </summary>
        /// <returns></returns>
        private async Task VoidSaleAsync()
        {
            var startTime = DateTime.Now;
            try
            {
                _tracker.Restart();

                var result = await _saleBussinessLogic.VoidSale(_reasonForVoidingSale);
                SoundService.Instance.PlaySoundFile(SoundTypes.ItemDeleted);
                _isSuspendOrVoidDone = true;
                if (result.LineDisplays != null)
                {
                    foreach (var lineDisplay in result.LineDisplays)
                    {
                        WriteToLineDisplay(lineDisplay);
                    }
                }

                await PerformPrint(result.Receipt);

                MessengerInstance.Send(result.Sale.ToModel(), "UpdateSale");

            }
            finally
            {
                if (_isSwitchUserPerformed)
                {
                    _isSwitchUserPerformed = false;
                    CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                    CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                    MessengerInstance.Send(false, "LoadAllPolicies");
                }
            }
            var endTime = DateTime.Now;
            Log.Info(string.Format("Time taken in void sale  is {0}ms ", (endTime - startTime).TotalMilliseconds));
        }


        private void CompleteVoidSale(bool reason)
        {
            _isSwitchUserPerformed = true;
            PerformAction(ValidateVoidSale);
        }

        #region Add Stock to Sale
        /// <summary>
        /// Verifies and Adds the given stock item to the ongoing sale
        /// </summary>
        public async void VerifyAndAddStockForSale(AddStockToSaleMessage stock)
       {
            _startTime = DateTime.Now;
            var startTime = DateTime.Now;

            if (stock != null)
            {
                _stock = new StockModel
                {
                    IsManuallyAdded = stock.IsManuallyAdded,
                    Quantity = stock.Quantity,
                    StockCode = stock.StockCode
                };
            }

            await PerformActionWithoutLoader(async () =>
            {
                try
                {
                   var response = await VerifyStock();

                    if (response != null && response.GetType() == typeof(VerifyStock))
                    {
                        _verifyStockModel = (VerifyStock)response;
                        if (_verifyStockModel != null)
                        {
                            ShowQuantityError();
                        }
                        else
                        {
                            ResetNewSaleLine();
                        }
                    }
                    else if (response != null && response.GetType() == typeof(EntityLayer.Entities.Sale.Sale))
                    {
                        var sale = ((EntityLayer.Entities.Sale.Sale)response).ToModel();
                        UpdateSale(sale);
                        SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                        NavigateService.Instance.NavigateToHome();
                    }
                }
                finally
                {
                    var endTime = DateTime.Now;
                    _log.Info(string.Format("Time Taken In Verify Stock  is {0}ms", (endTime - startTime).TotalMilliseconds));
                }
            });
        }

        private void ResetNewSaleLine()
        {
            NewSaleLine.Code = string.Empty;
        }

        /// <summary>
        /// Adds the stock item to the ongoing sale
        /// </summary>
        private async void AddStockItemForSale()
        {
            var startTime = DateTime.Now;
            try
            {
                var result = await _saleBussinessLogic.AddStockToSale(_stock.StockCode,
                    _stock.Quantity, CacheBusinessLogic.IsReturn,
                    new EntityLayer.Entities.Sale.GiftCard
                    {
                        CardNumber = GiftCard.CardNumber,
                        GiftNumber = GiftCard.GiftNumber,
                        Price = GiftCard.Price,
                        Quantity = GiftCard.Quantity
                    }, _stock.IsManuallyAdded);
                SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                var sale = result.ToModel();
                UpdateSale(sale);
                NavigateService.Instance.NavigateToHome();
            }
            catch (UserNotAuthorizedException)
            {
                NavigateService.Instance.NavigateToLogin();
            }
            catch (ApiDataException ex)
            {
                ShowNotification(ex.Message, NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome, ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message, NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                var endTime = DateTime.Now;

                Log.Info(string.Format("Time Taken In Add Stock is : {0}",
                    (endTime - startTime).TotalMilliseconds));

                _endTime = DateTime.Now;
                Log.Info(string.Format("Time Taken In Verify and Add Stock is : {0}",
                    (_endTime - _startTime).TotalMilliseconds));
            }
        }

        /// <summary>
        /// Updates the sale line to the ongoing sale
        /// </summary>
        private async Task UpdateItemForSale(ReasonType? reasonType)
        {
            var startTime = DateTime.Now;
            try
            {
                _tracker.Restart();
                LoadingService.ShowLoadingStatus(true);
                var result = await _saleBussinessLogic.UpdateSale(
                    _changedSaleLine.LineNumber, _changedSaleLine.Discount,
                    _discountType, _changedSaleLine.Quantity, _changedSaleLine.Price,
                    string.IsNullOrEmpty(_reasonForSaleLineUpdate.Code) ? string.Empty :
                    _reasonForSaleLineUpdate.Code.ToString(),
                    string.IsNullOrEmpty(reasonType.ToString()) ? string.Empty : reasonType.ToString());

                var sale = result.ToModel();
                MessengerInstance.Send(sale, "UpdateSale");
                NavigateService.Instance.NavigateToHome();
            }
            catch (UserNotAuthorizedException)
            {
                NavigateService.Instance.NavigateToLogin();
            }
            catch (ApiDataException ex)
            {
                ShowNotification(ex.Message, null, null,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
                var endTime = DateTime.Now;
                Log.Info(string.Format("Time taken in update sale  is {0}ms ", (endTime - startTime).TotalMilliseconds));
            }
        }

        private async Task<object> VerifyStock()
        {
            try
            {
                return await _saleBussinessLogic
                    .VerifyStockForSale(_stock.StockCode, _stock.Quantity, new EntityLayer.Entities.Sale.GiftCard
                    {
                        CardNumber = GiftCard.CardNumber,
                        GiftNumber = GiftCard.GiftNumber,
                        Price = GiftCard.Price,
                        Quantity = GiftCard.Quantity
                    }, _stock.IsManuallyAdded);
            }
            catch (UserNotAuthorizedException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    ShowNotification(ex.Message,
                    NavigateService.Instance.NavigateToLogin,
                    NavigateService.Instance.NavigateToLogin,
                    ApplicationConstants.ButtonWarningColor);
                    ResetNewSaleLine();
                }
                return null;
            }
            catch (ApiDataException ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    ShowNotification(ex.Message,
                        ResetNewSaleLine,
                        ResetNewSaleLine,
                        ApplicationConstants.ButtonWarningColor);
                }
                return null;
            }
        }

        private void ShowQuantityError()
        {
            if (!string.IsNullOrEmpty(
                _verifyStockModel.QuantityMessage?.Message))
            {
                ShowConfirmationMessage(_verifyStockModel.QuantityMessage.Message,
                    ShowRegularPriceError,
                  () =>
                  {
                      NavigateService.Instance.NavigateToHome();
                      SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                  }
                  ,
                  () =>
                  {
                      NavigateService.Instance.NavigateToHome();
                      SoundService.Instance.PlaySoundFile(SoundTypes.stockFound);
                  });
                ResetNewSaleLine();
            }
            else
            {
                ShowRegularPriceError();
            }
        }

        private void ShowRegularPriceError()
        {
            if (!string.IsNullOrEmpty(
                _verifyStockModel.RegularPriceMessage?.Message))
            {
                ShowNotification(
                    _verifyStockModel.RegularPriceMessage.Message,
                    CheckStockCanBeAddedManually,
                    CheckStockCanBeAddedManually,
                    ApplicationConstants.ButtonWarningColor);
                ResetNewSaleLine();
            }
            else
            {
                CheckStockCanBeAddedManually();
            }
        }

        private void CheckStockCanBeAddedManually()
        {
            if (_stock.IsManuallyAdded && !_verifyStockModel.CanManuallyEnterProduct
                && !string.IsNullOrEmpty(_verifyStockModel.ManuallyEnterMessage))
            {
                ShowNotification(_verifyStockModel.ManuallyEnterMessage,
                    ResetNewSaleLine,
                    ResetNewSaleLine,
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                CheckForAddStockPage();
            }
        }

        private void CheckForAddStockPage()
        {
            if (_verifyStockModel.AddStockPage.OpenAddStockPage)
            {
                NavigateService.Instance.NavigateToAddStock();
                MessengerInstance.Send(
                    _verifyStockModel.AddStockPage.StockCode, "AddSale");
                ResetNewSaleLine();
            }
            else
            {
                CheckForRestricitons();
            }
        }

        private void CheckForRestricitons()
        {
            if (_verifyStockModel.RestrictionPage.OpenRestrictionPage)
            {
                ShowConfirmationMessage(_verifyStockModel.RestrictionPage.Description,
                    CheckForGiftCertificates,
                    NavigateService.Instance.NavigateToHome,
                    NavigateService.Instance.NavigateToHome,
                    ApplicationConstants.ButtonConfirmationColor,
                    ApplicationConstants.ButtonWarningColor,
                    ApplicationConstants.Confirmed,
                    ApplicationConstants.NotConfirmed);
                ResetNewSaleLine();
            }
            else
            {
                CheckForGiftCertificates();
            }
        }

        private void CheckForGiftCertificates()
        {
            if (_verifyStockModel.GiftCertificatePage.OpenGiftCertificatePage)
            {
                GiftCard.Price = _verifyStockModel.GiftCertificatePage.RegularPrice;
                GiftCard.Quantity = 1;
                GiftCard.GiftNumber = CacheBusinessLogic.GiftNumber != 0 ?
                    CacheBusinessLogic.GiftNumber : _verifyStockModel.GiftCertificatePage.GiftNumber;
                NavigateService.Instance.NavigateToGiftCard();
                ResetNewSaleLine();
            }
            else
            {
                CheckForGiveX();
            }
        }

        private void CheckForGiveX()
        {
            if (_verifyStockModel.GiveXPage.OpenGiveXPage)
            {
                CacheBusinessLogic.IsGiveXCalledFromAddStock = true;
                NavigateService.Instance.NavigateToGiveXPage();
                MessengerInstance.Send(_stock, "AddGivexCardForSaleLine");
                ResetNewSaleLine();
            }
            else
            {
                CheckForPSInet();
            }
        }
        private void CheckForPSInet()
        {
            if (_verifyStockModel.PSInetPage.OpenPSInetPage)
            {
                NavigateService.Instance.NavigateToPSInetPage();
                Messenger.Default.Send<PSMessage>(new PSMessage()
                {
                    UPCNumber = _verifyStockModel.PSInetPage.StockCode,
                    SaleAmount = _verifyStockModel.PSInetPage.RegularPrice,
                    CurrentSale = SaleModel
                });
            }
            else
            {
                AddStockItemForSale();
            }
        }
        #endregion

        private bool IsSaleEmpty()
        {
            return !(SaleModel?.SaleLines?.Count > 1);
        }

        private void UpdateSaleSuspensionFlags()
        {
            if (CacheBusinessLogic.OperatorCanSuspendOrUnsuspendSales)
            {
                if (!CacheBusinessLogic.IsCurrentSaleEmpty)
                {
                    OperatorCanSuspendSale = CacheBusinessLogic.OperatorCanSuspendOrUnsuspendSales;
                }
                else if (CacheBusinessLogic.SuspendEmptySales)
                {
                    OperatorCanSuspendSale = CacheBusinessLogic.OperatorCanSuspendOrUnsuspendSales;
                }
                OperatorCanUnsuspendSales = true;
            }
        }

        //private async void SetTheValueOfCarwashProducts()
        //{
        //    var saleModel = new SaleModel();

        //    try
        //    {
        //        var sale = await _saleBussinessLogic.GetSaleBySaleNumber(_cacheBussinessLogic.TillNumber, SaleNumber);
        //        _cacheBussinessLogic.HasAnyCarwashProductInSale = sale.HasCarwashProducts;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
           
        //}
    }
}
