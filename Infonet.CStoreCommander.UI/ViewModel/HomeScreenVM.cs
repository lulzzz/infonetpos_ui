using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Infonet.CStoreCommander.UI.ViewModel
{
    public class HomeScreenVM : VMBase
    {
        #region Private variables
        private readonly InfonetLog _log;
#if DEBUG
        private const string BaseUrl = "http://localhost:52287/signalr";
        //private const string BaseUrl = "http://Infonet.CStoreService.com/signalr";
#else
        private const string BaseUrl = "http://Infonet.CStoreService.com/signalr";
#endif
        private List<PropaneGrade> _grades;
        private int _numberOfPumpsRows;
        private string _trainingCaption;
        private BigPumpsModel _bigpump;
        private bool _isBigPumpVisible;
        private readonly IPolicyBussinessLogic _policyBussinessLogic;
        private readonly ILoginBussinessLogic _loginBussinessLogic;
        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;
        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;
        private readonly ISoundBusinessLogic _soundBusinessLogic;
        private readonly ISystemBusinessLogic _systemBusinessLogic;
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private SignalRService _signalrService;

        private bool _isEmergencyPopupOpen;
        private bool _isPaneOpen;
        private int _till;
        private int _shift;
        private bool _isPaymentEnable;
        private bool _isUserInSaleSummary;
        private int _numberOfPumpsInARow;
        public string PumpMessage;
        private bool _isStopEnabled;
        private bool _isResumeEnabled;
        private bool _isFuelPriceEnabled;
        private bool _isFuelPriceEnabledResponse;
        private bool _isTierLevelEnabled;
        private bool _isFuelOnlySystem;
        private bool _isPosOnlySystem;
        private InitializeFuelPump _initialPumpStatus;
        private DispatcherTimer _headOfficeNotificationTimer;
        private DispatcherTimer _pumpStatusTimer;
        private int _notificationCounter;
        private PumpMessage _headOfficeNotificationResponse;
        private bool _isErrorVisible;
        private DispatcherTimer _checkErrorTimer;
        private bool _isFuelPumpOptionEnable;
        private bool _isLogoutEnable;
        private bool _isShiftDateVisible;
        private string _shiftDate;
        private bool _logoutButtonForFuelOnlySystem;
        private const int _maxRetriesForConnectingSignalr = 10;
        private int _retriesDoneForConnectingSignalr = 0;
        private ObservableCollection<PumpDetailModel> _pumpDetails;
        private int _pumpClickDelayInSeconds;
        private DateTime _lastBasketAddedTime = DateTime.MinValue;
        #endregion

        public HubConnection conn { get; set; }
        public IHubProxy proxy { get; set; }

        #region Public Variables
        public bool IsBigPumpVisible
        {
            get { return _isBigPumpVisible; }
            set
            {
                if (_isBigPumpVisible != value)
                {
                    _isBigPumpVisible = value;
                    RaisePropertyChanged(nameof(IsBigPumpVisible));
                }
            }
        }
        public BigPumpsModel BigPump
        {
            get { return _bigpump; }
            set
            {
                if (value != _bigpump)
                {
                    _bigpump = value;
                    RaisePropertyChanged(nameof(BigPumpOperations));
                }
            }
        }

        public int NumberOfPumpRows
        {
            get { return _numberOfPumpsRows; }
            set
            {
                if (value != _numberOfPumpsRows)
                {
                    _numberOfPumpsRows = value;
                    RaisePropertyChanged(nameof(NumberOfPumpRows));
                }
            }
        }
        public string TrainingCaption
        {
            get { return _trainingCaption; }
            set
            {
                if (value != _trainingCaption)
                {
                    _trainingCaption = value;
                    RaisePropertyChanged(nameof(TrainingCaption));
                }
            }
        }

        public bool IsPosOnlySystem
        {
            get { return _isPosOnlySystem; }
            set
            {
                if (_isPosOnlySystem != value)
                {
                    _isPosOnlySystem = value;
                    RaisePropertyChanged(nameof(IsPosOnlySystem));
                }
            }
        }
        public bool IsFuelOnlySystem
        {
            get { return _isFuelOnlySystem; }
            set
            {
                if (_isFuelOnlySystem != value)
                {
                    _isFuelOnlySystem = value;
                    RaisePropertyChanged(nameof(IsFuelOnlySystem));
                }
            }
        }
        public bool IsTierLevelEnabled
        {
            get { return _isTierLevelEnabled; }
            set
            {
                _isTierLevelEnabled = value;
                RaisePropertyChanged(nameof(IsTierLevelEnabled));
            }
        }
        public bool IsFuelPriceEnabled
        {
            get { return _isFuelPriceEnabled; }
            set
            {
                if (_isFuelPriceEnabled != value)
                {
                    _isFuelPriceEnabled = value;
                    RaisePropertyChanged(nameof(IsFuelPriceEnabled));
                }
            }
        }
        public int NumberOfPumpsInARow
        {
            get { return _numberOfPumpsInARow; }
            set
            {
                _numberOfPumpsInARow = value;
                RaisePropertyChanged(nameof(NumberOfPumpsInARow));
            }
        }
        public bool IsResumeEnabled
        {
            get { return _isResumeEnabled; }
            set
            {
                if (value != _isResumeEnabled)
                {
                    _isResumeEnabled = value;
                    RaisePropertyChanged(nameof(IsResumeEnabled));
                }
            }
        }
        public bool IsStopEnabled
        {
            get { return _isStopEnabled; }
            set
            {
                if (_isStopEnabled != value)
                {
                    _isStopEnabled = value;
                    RaisePropertyChanged(nameof(IsStopEnabled));
                }
            }
        }
        public bool IsPaymentEnable
        {
            get { return _isPaymentEnable; }
            set
            {
                _isPaymentEnable = value;
                RaisePropertyChanged(nameof(IsPaymentEnable));
            }
        }
        public bool IsUserNotInSaleSummary
        {
            get { return _isUserInSaleSummary; }
            set
            {
                if (_isUserInSaleSummary != value)
                {
                    IsLogoutEnable = value;
                    _isUserInSaleSummary = value;
                    RaisePropertyChanged(nameof(IsUserNotInSaleSummary));
                    EnableDisablePaymentButton(null);
                }
            }
        }
        public bool IsEmergencyPopupOpen
        {
            get { return _isEmergencyPopupOpen; }
            set
            {
                _isEmergencyPopupOpen = value;
                RaisePropertyChanged(nameof(IsEmergencyPopupOpen));
            }
        }
        public int Sale { get; set; }
        public string Customer { get; set; }
        //public string ShiftDate => string.Format("Shift Date: {0}", CacheBusinessLogic.ShiftDate);

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                RaisePropertyChanged(nameof(IsPaneOpen));
                if (!_isPaneOpen)
                {
                    MessengerInstance.Send(new SetFocusOnGridMessage { });
                }
            }
        }
        public int Till
        {
            get { return _till; }
            set
            {
                _till = value;
                RaisePropertyChanged(nameof(Till));
            }
        }
        public int Shift
        {
            get { return _shift; }
            set
            {
                _shift = value;
                RaisePropertyChanged(nameof(Shift));
            }
        }
        public bool LogoutButtonForFuelOnlySystem
        {
            get { return _logoutButtonForFuelOnlySystem; }
            set
            {
                _logoutButtonForFuelOnlySystem = value;
                RaisePropertyChanged(nameof(LogoutButtonForFuelOnlySystem));
            }
        }

        public string ShiftDate
        {
            get { return _shiftDate; }
            set
            {
                _shiftDate = value;
                RaisePropertyChanged(nameof(ShiftDate));
            }
        }

        public bool IsShiftDateVisible
        {
            get { return _isShiftDateVisible; }
            set
            {
                _isShiftDateVisible = value;
                RaisePropertyChanged(nameof(IsShiftDateVisible));

                if (_isShiftDateVisible)
                {
                    if (!string.IsNullOrEmpty(CacheBusinessLogic.ShiftDate))
                    {
                        ShiftDate = DateTime.Parse(CacheBusinessLogic.ShiftDate, CultureInfo.InvariantCulture).ToString("MMM dd,yyyy", CultureInfo.InvariantCulture);
                    }
                }
            }
        }

        public bool IsLogoutEnable
        {
            get { return _isLogoutEnable; }
            set
            {
                _isLogoutEnable = value;
                RaisePropertyChanged(nameof(IsLogoutEnable));
            }
        }

        public bool IsFuelPumpOptionEnable
        {
            get { return _isFuelPumpOptionEnable; }
            set
            {
                if (_isFuelPumpOptionEnable != value)
                {
                    _isFuelPumpOptionEnable = value;
                    RaisePropertyChanged(nameof(IsFuelPumpOptionEnable));
                }
            }
        }

        public bool IsErrorVisible
        {
            get { return _isErrorVisible; }
            set
            {
                if (value != _isErrorVisible)
                {
                    _isErrorVisible = value;
                    RaisePropertyChanged(nameof(IsErrorVisible));
                }
            }
        }
        #endregion

        #region Public Collections
        public ObservableCollection<PumpDetailModel> PumpDetails
        {
            get { return _pumpDetails; }
            set
            {
                if (value != _pumpDetails)
                {
                    _pumpDetails = value;
                    RaisePropertyChanged(nameof(PumpDetails));
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand OpenEmergencyPopupCommand { get; private set; }
        public RelayCommand ResumeAllPumpsCommand { get; private set; }
        public RelayCommand CloseEmergencyPopupCommand { get; private set; }
        public RelayCommand StopAllPumpsCommand { get; private set; }
        public RelayCommand<object> PumpInteractionCommand { get; private set; }
        public RelayCommand<object> ToggleSplitViewCommand { get; private set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand OpenMaintenanceWindowCommand { get; private set; }
        public RelayCommand OpenPaymentOptionsCommand { get; private set; }
        public RelayCommand OpenReportsCommand { get; private set; }
        public RelayCommand ShowErrorsCommand { get; set; }
        public RelayCommand OpenPumpOptionsPopupCommand { get; set; }
        public RelayCommand ClosePumpOptionsPopupCommand { get; set; }
        public RelayCommand<object> SaveSelectedPropaneGradeItemCommand { get; private set; }
        public RelayCommand<object> AddBasketCommand { get; set; }
        public RelayCommand<object> CurrentStackCommand { get; set; }
        public RelayCommand<object> AuthorizePumpCommand { get; set; }

        public RelayCommand HideBigPumpCommand { get; set; }

        #endregion

        public HomeScreenVM(IPolicyBussinessLogic policyBussinessLogic,
            ILoginBussinessLogic loginBussinessLogic,
            IReasonListBussinessLogic reasonListBussinessLogic,
            IFuelPumpBusinessLogic fuelPumpBusinessLogic,
            ISoundBusinessLogic soundBusinessLogic,
            ISystemBusinessLogic systemBusinessLogic,
            ISaleBussinessLogic saleBussinessLogic)
        {
            _policyBussinessLogic = policyBussinessLogic;
            _loginBussinessLogic = loginBussinessLogic;
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            _soundBusinessLogic = soundBusinessLogic;
            _systemBusinessLogic = systemBusinessLogic;
            _saleBussinessLogic = saleBussinessLogic;

            _log = InfonetLogManager.GetLogger<HomeScreenVM>();
            UnregisterMessages();
            RegisterMessages();
            InitalizeCommands();
            InitalizeData();
        }

        private void UnregisterMessages()
        {
            MessengerInstance.Unregister<ResetPumpOptionMessage>(this, ResetPumpOptions);
            MessengerInstance.Unregister<bool>(this, "LoadAllPolicies", LoadAllPolicies);
            MessengerInstance.Unregister<bool>(this, "UserNavigatedToSaleSummaryPage",
                IsUserInSaleSummaryScreen);
            MessengerInstance.Unregister<bool>(this, "EnableHamburgerIcon",
                SwitchToggleButton);
            MessengerInstance.Unregister<int>(this, "SetPrepayStatus", SetPrepayStatus);
            MessengerInstance.Unregister<bool>(this, "ErrorsCleared", ErrorsCleared);
            MessengerInstance.Unregister<EnableDisablePaymentButtonMessage>(this, EnableDisablePaymentButton);
            MessengerInstance.Unregister<PumpOptionRemoveMessage>(this, RemovePumpOptions);
            MessengerInstance.Unregister<FuelOnlySystemMessage>(this, FuelOnlySystem);
        }

        private void ErrorsCleared(bool obj)
        {
            IsErrorVisible = false;
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<EnableDisablePaymentButtonMessage>(this, EnableDisablePaymentButton);
            MessengerInstance.Register<bool>(this, "ErrorsCleared", ErrorsCleared);
            MessengerInstance.Register<ResetPumpOptionMessage>(this, ResetPumpOptions);
            MessengerInstance.Register<bool>(this, "LoadAllPolicies", LoadAllPolicies);
            MessengerInstance.Register<bool>(this, "UserNavigatedToSaleSummaryPage",
                IsUserInSaleSummaryScreen);
            MessengerInstance.Register<bool>(this, "EnableHamburgerIcon",
                SwitchToggleButton);
            MessengerInstance.Register<int>(this, "SetPrepayStatus", SetPrepayStatus);
            MessengerInstance.Register<object>(this, "InitializeFuelPump", InitializeFuelPumps);
            MessengerInstance.Register<PumpOptionRemoveMessage>(this, RemovePumpOptions);
            MessengerInstance.Register<EnableFuelOptionButtonMessage>(this, EnableFuelOptionButton);
            MessengerInstance.Register<InitializeFuelPump>(this, MapPumpsStatus);
            MessengerInstance.Register<FuelOnlySystemMessage>(this, FuelOnlySystem);
        }

        private void FuelOnlySystem(FuelOnlySystemMessage obj)
        {
            IsFuelOnlySystem = true;
            IsPosOnlySystem = false;
        }

        private void MapPumpsStatus(InitializeFuelPump obj)
        {
            PumpSetup(obj);
        }

        private void EnableFuelOptionButton(EnableFuelOptionButtonMessage message)
        {
            IsFuelPumpOptionEnable = message.EnableFuelOptionButton;
        }

        private void EnableDisablePaymentButton(EnableDisablePaymentButtonMessage message)
        {
            IsPaymentEnable = CacheBusinessLogic.IsCurrentSaleEmpty && !CacheBusinessLogic.IsReturn && IsUserNotInSaleSummary
                && !CacheBusinessLogic.IsFuelOnlySystem;
        }

        private void InitializeFuelPumps(object obj)
        {
            InitializeFuelPump();
        }

        #region Pumps

        public async void SignalR()
        {
            if (_signalrService == null)
            {
                _signalrService = new SignalRService(BaseUrl);
            }

            await Task.Delay(250);
            conn = new HubConnection(BaseUrl);
            _log.Info(string.Format("Creating hub proxy with :{0}", BaseUrl));
            proxy = conn.CreateHubProxy("PumpStatusHub");


            _log.Info("Starting Connection");

            await PerformActionWithoutLoader(async () =>
            {
                try
                {
                    conn.Start(new LongPollingTransport()).Wait();
                    _log.Info("Connection started");
                    await proxy.Invoke("OpenPortReading");//, UserName, TextBoxMessage.Text);

                    conn.Closed -= OnDisconnected;
                    conn.Closed += OnDisconnected;

                    conn.ConnectionSlow -= OnConnectionSlow;
                    conn.ConnectionSlow += OnConnectionSlow;

                    conn.Error -= OnError;
                    conn.Error += OnError;

                    _log.Info("Port invoked");
                    proxy.On<string>("ReadUdpData", OnMessage);
                    proxy.On<string>("KeepConnectionAlive", KeepAlive);
                }
                catch (HttpRequestException ex)
                {
                    ShowNotification("Unable to connect to server: Start server before connecting clients.",
                        null,
                        null, ApplicationConstants.ButtonWarningColor);

                    _log.Info("Unable to connect to server: Start server before connecting clients.");
                    _log.Info(ex.Message);
                }
                catch (Exception ex)
                {
                    _log.Info(ex.Message);
                    SignalR();
                }
            }, false);

            PumpStatusTimerTick(null, null);
        }

        private void OnError(Exception obj)
        {
            _log.Info("Error reported on signalr ", obj);
        }

        private void OnConnectionSlow()
        {
            _log.Info("Slow connection identified!!!");
        }

        private void OnDisconnected()
        {
            if (_retriesDoneForConnectingSignalr > _maxRetriesForConnectingSignalr)
            {
                _log.Info("Max retries done for reconnecting!!!");
            }
            else
            {
                _log.Info(string.Format("Client disconnected for {0} time from SignalR server, trying to reconnect!!!.", ++_retriesDoneForConnectingSignalr));
                SignalR();
            }
        }

        private void KeepAlive(string obj)
        {
            // This only is to keep connection alive
        }

        public void Broadcast(string msg)
        {
            proxy.Invoke("Send", msg);
        }

        private async void OnMessage(string pumpStatusContract)
        {
            _log.Info("Message Received for processing of signalR");
            await Windows.ApplicationModel.Core.CoreApplication.MainView.
                Dispatcher.RunAsync(CoreDispatcherPriority.High, (DispatchedHandler)(() =>
                {
                    _log.Info(string.Format("Message Received from signalr {0}", pumpStatusContract));
                    var pumpStatus = PumpsUtility.GetPumpStatus(pumpStatusContract);
                    if (pumpStatus.Pumps != null)
                    {
                        _initialPumpStatus = pumpStatus;
                        MapPumps(pumpStatus);
                    }
                    else
                    {
                        MapPumps(_initialPumpStatus);
                    }

                    if (pumpStatus?.Pumps?.Count > 0)
                    {
                        SetEmergencyButttonVisibility(pumpStatus);
                    }
                }));
        }

        // TODO: For showing prepay text on pump after add prepay
        private void SetPrepayStatus(int pumpId)
        {
            if (PumpDetails.First(x => x.PumpNumber == pumpId) != null)
            {
                var pump = PumpDetails.FirstOrDefault(x => x.PumpNumber == pumpId);
                pump.PayPumpOrPrepay = "Prepay";
            }
        }

        private async Task InitializeFuelPump()
        {
            PerformAction(async () =>
            {
                _log.Info("Initializing pumps API CALL ");
                _initialPumpStatus = await _fuelPumpBusinessLogic.InitializeFuelPump(true, CacheBusinessLogic.TillNumberForSale);
                _log.Info("Setting Pumps");
                PumpSetup(_initialPumpStatus);
                SetNumberOfPumpInRow();
            });
        }

        private void MapPumps(InitializeFuelPump response)
        {
            _log.Info("Mapping pumps");
            if (PumpDetails != null && response.Pumps?.Count > 0)
            {
                foreach (var pump in response.Pumps)
                {
                    var pumpOptions = GetPumpOptions(response, pump);
                    var tempPump = GetPumpByID(pump.PumpId);

                    tempPump.PumpNumber = pump.PumpId;
                    tempPump.PumpButtonCaption = pump.PumpButtonCaption;
                    tempPump.BasketLabelCaption = pump.BasketLabelCaption;
                    tempPump.BasketButtonCaption = pump.BasketButtonCaption;
                    tempPump.EnableBasketBotton = pump.EnableBasketButton;
                    tempPump.PayPumpOrPrepay = pump.PrepayText;
                    tempPump.PumpOptions = new ObservableCollection<string>(pumpOptions);
                    tempPump.SelectedPumpOptionIndex = -1;

                    if (nameof(SoundTypes.stopped) == tempPump.Status.ToLower()
                        && !pump.Status.ToLower().Equals(nameof(SoundTypes.stopped)))
                    {
                        --SoundService.Instance.StoppedSoundCount;
                    }


                    if (!pump.Status.ToUpper().Equals(tempPump.Status))
                    {
                        if (tempPump.PumpButtonCaption == ApplicationConstants.Stopped)
                        {
                            PlayPumpSound(SoundTypes.resume.ToString(), pump.PumpId.ToString());
                        }
                        else
                        {
                            PlayPumpSound(pump.Status, pump.PumpId.ToString());
                        }

                        tempPump.Status = pump.Status.ToUpper();
                    }
                }
            }
            MapBigPump(response.BigPumps);
        }

        private void PlayPumpSound(string status, string pumpId = default(string))
        {
            switch (status.ToLower())
            {
                case nameof(SoundTypes.calling):
                    SoundService.Instance.PlaySoundFileForCalling(pumpId);
                    break;
                case nameof(SoundTypes.Help):
                    SoundService.Instance.PlaySoundFile(SoundTypes.Help, pumpId);
                    break;
                case nameof(SoundTypes.finished):
                    SoundService.Instance.PlaySoundFile(SoundTypes.finished);
                    break;
                case nameof(SoundTypes.payatPumpDone):
                    SoundService.Instance.PlaySoundFile(SoundTypes.payatPumpDone);
                    break;
                case nameof(SoundTypes.payatPumpStarted):
                    SoundService.Instance.PlaySoundFile(SoundTypes.payatPumpStarted);
                    break;
                case nameof(SoundTypes.pumpAuthorized):
                    SoundService.Instance.PlaySoundFile(SoundTypes.pumpAuthorized);
                    break;
                case nameof(SoundTypes.pumpError):
                    SoundService.Instance.PlaySoundFile(SoundTypes.pumpError);
                    break;
                case nameof(SoundTypes.resume):
                    SoundService.Instance.PlaySoundFile(SoundTypes.resume);
                    break;
                case nameof(SoundTypes.stopped):
                    ++SoundService.Instance.StoppedSoundCount;
                    SoundService.Instance.PlaySoundFile(SoundTypes.stopped);
                    break;
            }

            if (status.ToLower() != nameof(SoundTypes.calling))
            {
                SoundService.Instance.RemoveCallingQueue(pumpId);
            }
        }

        private void PumpSetup(InitializeFuelPump response)
        {
            if (response.Pumps?.Count > 0)
            {
                if (PumpDetails == null)
                {
                    PumpDetails = new ObservableCollection<PumpDetailModel>();
                }
                SetEmergencyButttonVisibility(response);
                SoundService.Instance.StoppedSoundCount = 0;
                foreach (var pump in response.Pumps)
                {
                    var pumpOptions = GetPumpOptions(response, pump);
                    var pumpToUpdate = PumpDetails.FirstOrDefault(x => x.PumpNumber == pump.PumpId);
                    if (pumpToUpdate == null)
                    {
                        pumpToUpdate = new PumpDetailModel();
                        PumpDetails.Add(pumpToUpdate);
                    }

                    pumpToUpdate.PumpButtonCaption = pump.PumpButtonCaption;
                    pumpToUpdate.PumpNumber = pump.PumpId;
                    pumpToUpdate.Status = pump.Status;
                    pumpToUpdate.BasketLabelCaption = pump.BasketLabelCaption;
                    pumpToUpdate.EnableBasketBotton = pump.EnableBasketButton;
                    pumpToUpdate.PayPumpOrPrepay = pump.PrepayText;
                    pumpToUpdate.BasketButtonCaption = pump.BasketButtonCaption;
                    pumpToUpdate.PumpOptions = new ObservableCollection<string>(pumpOptions);
                    pumpToUpdate.SelectedPumpOptionIndex = -1;
                    pumpToUpdate.SelectedCurrentStackIndex = -1;
                    pumpToUpdate.CanCashierAuthorize = pump.CanCashierAuthorize;


                    if (pumpToUpdate.Status.ToLower() == nameof(SoundTypes.stopped))
                    {
                        ++SoundService.Instance.StoppedSoundCount;
                        SoundService.Instance.PlaySoundFile(SoundTypes.resume);
                    }
                    else if (pumpToUpdate.Status.ToLower() == nameof(SoundTypes.resume))
                    {
                        --SoundService.Instance.StoppedSoundCount;
                        SoundService.Instance.PlaySoundFile(SoundTypes.stopped);
                    }
                    else if (pumpToUpdate.Status.ToLower() == nameof(SoundTypes.calling))
                    {
                        SoundService.Instance.PlaySoundFileForCalling(pumpToUpdate.PumpNumber.ToString());
                    }
                }

                if (SoundService.Instance.StoppedSoundCount < 0)
                {
                    SoundService.Instance.StoppedSoundCount = 0;
                }

                MapBigPump(response.BigPumps);
            }
        }


        private void MapBigPump(List<BigPumps> bigPumps)
        {
            if (bigPumps?.Count > 0)
            {
                var bigpump = bigPumps.FirstOrDefault();

                BigPump.IsPumpVisible = string.IsNullOrEmpty(bigpump.IsPumpVisible) ? false :
                    bigpump.IsPumpVisible.Equals("true") ? true : false;
                BigPump.Amount = bigpump.Amount;
                BigPump.PumpId = bigpump.PumpId;
                BigPump.PumpLabel = bigpump.PumpLabel;
                BigPump.PumpMessage = bigpump.PumpMessage;
            }
        }

        private void SetEmergencyButttonVisibility(InitializeFuelPump response)
        {
            IsResumeEnabled = response.IsResumeButtonEnabled;
            IsFuelPriceEnabled = response.IsFuelPriceEnabled && CacheBusinessLogic.AreFuelPricesSaved;
            _isFuelPriceEnabledResponse = response.IsFuelPriceEnabled;
            IsTierLevelEnabled = response.IsTierLevelEnabled;
            IsStopEnabled = response.IsStopButtonEnabled;
        }

        private async void PumpInteraction(dynamic s)
        {
            // TODO: appropriate task to perform 

            var selectedPump = s as PumpDetailModel;

            if (selectedPump.SelectedPumpOptionIndex >= 0)
            {
                var selectedInteractionsOptions = selectedPump.PumpOptions.
                    ElementAt(selectedPump.SelectedPumpOptionIndex);

                ResetPumpOptionsIndex(selectedPump);

                MessengerInstance.Send(new SetFocusOnGridMessage { });
                await ExecutePumpOperation(selectedPump, selectedInteractionsOptions);

                MessengerInstance.Send(new SetFocusOnGridMessage { });
            }
        }

        private async Task ExecutePumpOperation(PumpDetailModel selectedPump, string selectedInteractionsOptions)
        {
            if (selectedInteractionsOptions.Equals(ApplicationConstants.Prepay))
            {
                SetPrepay(selectedPump);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.SwitchPrepay))
            {
                SwitchPrepay(selectedPump);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.DeletePrepay))
            {
                DeletePrepay(selectedPump.PumpNumber);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Stop))
            {
                await StopPump(selectedPump.PumpNumber);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Resume))
            {
                await ResumePump(selectedPump.PumpNumber);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.BigPump))
            {
                BigPumpOperations(selectedPump.PumpNumber);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Manual))
            {
                AddManualFuel(selectedPump);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Finish))
            {
                Finish();
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Authorize))
            {
                AuthorizePumpFromOptions(selectedPump.PumpNumber);
            }
            else if (selectedInteractionsOptions.Equals(ApplicationConstants.Deauthorize))
            {
                DeauthorizePumpFromOptions(selectedPump.PumpNumber);
            }
        }

        private void ResetPumpOptions(ResetPumpOptionMessage obj)
        {
            if (PumpDetails != null && _initialPumpStatus.Pumps?.Count > 0)
            {
                foreach (var pump in _initialPumpStatus.Pumps)
                {
                    var pumpOptions = GetPumpOptions(_initialPumpStatus, pump);

                    var tempPump = GetPumpByID(pump.PumpId);
                    tempPump.PumpOptions = new ObservableCollection<string>(pumpOptions);
                }
            }
        }

        private void RemovePumpOptions(PumpOptionRemoveMessage pumpOptionRemoveMessage)
        {
            if (PumpDetails != null && _initialPumpStatus.Pumps?.Count > 0)
            {
                foreach (var pump in _initialPumpStatus.Pumps)
                {
                    var pumpOptions = GetPumpOptions(_initialPumpStatus, pump);

                    if (_initialPumpStatus.IsFinishEnabled && pumpOptionRemoveMessage.RemoveFinishOption)
                    {
                        pumpOptions.Remove(ApplicationConstants.Finish);
                    }

                    if (_initialPumpStatus.IsManualEnabled && pumpOptionRemoveMessage.RemoveManualOption)
                    {
                        pumpOptions.Remove(ApplicationConstants.Manual);
                    }

                    if (_initialPumpStatus.IsPrepayEnabled && CacheBusinessLogic.IsPrePayOn &&
                        pumpOptionRemoveMessage.RemovePrepayOption)
                    {
                        if (string.IsNullOrEmpty(pump.PrepayText))
                        {
                            pumpOptions.Remove(ApplicationConstants.Prepay);
                        }
                        else
                        {
                            if (pump.PrepayText.ToUpper() == "PREPAY")
                            {
                                pumpOptions.Remove(ApplicationConstants.Prepay);
                            }
                            else
                            {
                                pumpOptions.Remove(ApplicationConstants.DeletePrepay);
                                pumpOptions.Remove(ApplicationConstants.SwitchPrepay);
                            }
                        }
                    }
                    var tempPump = GetPumpByID(pump.PumpId);
                    tempPump.PumpOptions = new ObservableCollection<string>(pumpOptions);
                }
            }
        }

        private async void BigPumpOperations(int pumpId)
        {
            await PerformActionWithoutLoader(async () =>
            {
                var response = await _fuelPumpBusinessLogic.GetPumpAction(pumpId, false, false);

                BigPump.IsPumpVisible = response.IsPumpVisible;
                BigPump.Amount = response.Amount;
                BigPump.PumpId = response.PumpId;
                BigPump.PumpLabel = response.PumpLabel;
                BigPump.PumpMessage = response.PumpMessage;

                IsBigPumpVisible = BigPump == null ? false : BigPump.IsPumpVisible;
            }, false);
        }

        private void Finish()
        {
            NavigateService.Instance.NavigateToFinish();
            IsFuelPumpOptionEnable = false;
            RemovePumpOptions(new PumpOptionRemoveMessage
            {
                RemoveManualOption = true,
                RemoveFinishOption = true
            });
        }

        private void AddManualFuel(PumpDetailModel selectedPump)
        {
            NavigateService.Instance.NavigateToManualFuelSale();
            IsFuelPumpOptionEnable = false;
            MessengerInstance.Send(new FuelManualPumpMessage
            {
                PumpId = selectedPump.PumpNumber
            });

            RemovePumpOptions(new PumpOptionRemoveMessage
            {
                RemoveManualOption = true,
                RemoveFinishOption = true
            });
        }

        #region Pump Operations

        private async Task ResumePump(int pumpId)
        {
            await PerformActionWithoutLoader(async () =>
            {
                var response = await _fuelPumpBusinessLogic.
                GetPumpAction(pumpId, false, true);
            }, false);

        }

        private async Task StopPump(int pumpId)
        {
            await PerformActionWithoutLoader(async () =>
            {
                var response = await _fuelPumpBusinessLogic.
                GetPumpAction(pumpId, true, false);
            }, false);

        }

        private void DeletePrepay(int pumpId)
        {
            PerformAction(async () =>
            {
                var checkoutSummary = await _fuelPumpBusinessLogic.DeletePrepay(pumpId);
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send(new CloseKeyboardMessage());
                MessengerInstance.Send(checkoutSummary);
            });
        }

        private void SwitchPrepay(PumpDetailModel selectedPump)
        {
            NavigateService.Instance.NavigateToPrePay();
            IsFuelPumpOptionEnable = false;
            RemovePumpOptions(new PumpOptionRemoveMessage
            {
                RemoveManualOption = true,
                RemoveFinishOption = true
            });

            var switchPrepayMessage = new PrepayMessage
            {
                IsPrepay = false,
                SelectedPumpID = selectedPump.PumpNumber,
                TotalPumps = PumpDetails.Count
            };

            MessengerInstance.Send<PrepayMessage>(switchPrepayMessage, "SetPrepayMessage");
        }

        private void SetPrepay(PumpDetailModel selectedPump)
        {
            NavigateService.Instance.NavigateToPrePay();
            IsFuelPumpOptionEnable = false;
            RemovePumpOptions(new PumpOptionRemoveMessage
            {
                RemoveManualOption = true,
                RemoveFinishOption = true
            });

            var prepayMessage = new PrepayMessage
            {
                IsPrepay = true,
                SelectedPumpID = selectedPump.PumpNumber,
                TotalPumps = PumpDetails.Count
            };

            MessengerInstance.Send<PrepayMessage>(prepayMessage, "SetPrepayMessage");
        }

        private async void AuthorizePump(dynamic pumpId)
        {
            var pump = GetPumpByID(pumpId);

            if (pump.Status.ToUpper() == PumpStatuses.CALLING.ToString())
            {
                MessengerInstance.Send(new SetFocusOnGridMessage { });

                await PerformActionWithoutLoader(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.GetPumpAction(pumpId, false, false);
                    PlayPumpSound(SoundTypes.pumpAuthorized.ToString());
                }, false);
                MessengerInstance.Send(new SetFocusOnGridMessage { });
            }
        }

        private async void AuthorizePumpFromOptions(dynamic pumpId)
        {
            var pump = GetPumpByID(pumpId);

            if (pump.Status.ToUpper() == PumpStatuses.IDLE.ToString() && pump.CanCashierAuthorize)
            {
                await PerformActionWithoutLoader(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.GetPumpAction(pumpId, false, false);
                    PlayPumpSound(SoundTypes.pumpAuthorized.ToString());
                }, false);
            }
        }

        private async void DeauthorizePumpFromOptions(int pumpId)
        {
            var pump = GetPumpByID(pumpId);

            if (pump.Status.ToUpper() == PumpStatuses.AUTHORIZED.ToString()
                && pump.CanCashierAuthorize)
            {
                await PerformActionWithoutLoader(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.GetPumpAction(pumpId, false, false);
                }, false);
            }
        }

        private PumpDetailModel GetPumpByID(dynamic pumpId)
        {
            return PumpDetails.First(x => x.PumpNumber == pumpId);
        }

        #endregion

        private void ResetPumpOptionsIndex(PumpDetailModel selectedPump)
        {
            try
            {
                selectedPump.SelectedPumpOptionIndex = -1;
            }
            catch (Exception ex)
            {
                _log.Error("Exception while resetting pump option combobox index ");
                _log.Error(ex.Message);
            }
        }

        private void ResetCurrentStackIndex(PumpDetailModel selectedPump)
        {
            try
            {
                selectedPump.SelectedCurrentStackIndex = -1;
            }
            catch (Exception ex)
            {
                _log.Error("Exception while resetting stacked combobox index ");
                _log.Error(ex.Message);
            }
        }

        private void AddBasket(object pumpId)
        {
            // Return if any of the modal popup is open
            if (PopupService.PopupInstance.IsPopupOpen || (DateTime.Now - _lastBasketAddedTime).TotalMilliseconds <= _pumpClickDelayInSeconds * 1000)
            {
                return;
            }

            if (NavigateService.Instance.FirstFrame != null && NavigateService.Instance.FirstFrame.Content != null)
            {
                if (NavigateService.Instance.FirstFrame.Content.GetType().Name == "SaleGrid")
                {
                    PerformActionWithoutLoader(async () =>
                    {
                        var pump = GetPumpByID((int)pumpId);
                        var basketValue = pump.BasketButtonCaption;

                        if (!string.IsNullOrEmpty(basketValue))
                        {
                            _lastBasketAddedTime = DateTime.Now;
                            var response = await _fuelPumpBusinessLogic.AddBasket((int)pumpId, basketValue);
                            MessengerInstance.Send(response.ToModel(), "UpdateSale");
                        }
                    });
                }
            }
        }

        private void CurrentStack(object args)
        {
            // Return if any of the modal popup is open
            if (PopupService.PopupInstance.IsPopupOpen || (DateTime.Now - _lastBasketAddedTime).TotalMilliseconds <= _pumpClickDelayInSeconds * 1000)
            {
                return;
            }

            if (NavigateService.Instance.FirstFrame != null && NavigateService.Instance.FirstFrame.Content != null)
            {
                if (NavigateService.Instance.FirstFrame.Content.GetType().Name == "SaleGrid")
                {
                    var selectedPump = args as PumpDetailModel;

                    if (selectedPump.SelectedCurrentStackIndex >= 0)
                    {
                        var selectedStackedValue = selectedPump.Stacked.
                            ElementAt(selectedPump.SelectedCurrentStackIndex);

                        ResetCurrentStackIndex(selectedPump);

                        if (selectedStackedValue != null)
                        {
                            var stackedValue = selectedStackedValue.Content;

                            PerformActionWithoutLoader(async () =>
                            {
                                _lastBasketAddedTime = DateTime.Now;
                                var response = await _fuelPumpBusinessLogic.AddBasket(selectedPump.PumpNumber,
                                    stackedValue);
                                MessengerInstance.Send(response.ToModel(), "UpdateSale");
                            });
                        }
                    }
                    MessengerInstance.Send(new SetFocusOnGridMessage { });
                }
            }
        }

        private void OpenEmergencyPopup()
        {
            // Closing all popups because Emergency popup can be opened if a popup is already opened
            PopupService.PopupInstance.CloseCurrentPopupWithStateSave();
            PopupService.PopupInstance.IsEmergencyPopupOpen = true;
        }

        private void CloseEmergencyPopup()
        {
            PopupService.PopupInstance.IsEmergencyPopupOpen = false;
            PopupService.PopupInstance.RestoreLastOpenedPopup();
        }

        private async void ResumeAllPumps()
        {
            await PerformActionWithoutLoader(async () =>
            {
                await _fuelPumpBusinessLogic.ResumeAllPumps();
            }, false);
            PopupService.PopupInstance.RestoreLastOpenedPopup();
        }

        private async void StopAllPumps()
        {
            await PerformActionWithoutLoader(async () =>
            {
                await _fuelPumpBusinessLogic.StopAllPumps();
            }, false);
            PopupService.PopupInstance.RestoreLastOpenedPopup();
        }

        private List<string> GetPumpOptions(InitializeFuelPump response, PumpStatus pump)
        {
            var pumpOptions = new List<string>();

            if (!CacheBusinessLogic.IsFuelOnlySystem)
            {
                if (response.IsFinishEnabled)
                {
                    pumpOptions.Add(ApplicationConstants.Finish);
                }
                if (response.IsManualEnabled)
                {
                    pumpOptions.Add(ApplicationConstants.Manual);
                }


                if (pump.Status.ToUpper() != PumpStatuses.INACTIVE.ToString() &&
                    pump.Status.ToUpper() != PumpStatuses.AUTHORIZED.ToString() &&
                    pump.Status.ToUpper() != PumpStatuses.STOPPED.ToString())
                {
                    if (response.IsPrepayEnabled && CacheBusinessLogic.IsPrePayOn)
                    {
                        if (string.IsNullOrEmpty(pump.PrepayText))
                        {
                            pumpOptions.Add(ApplicationConstants.Prepay);
                        }
                        else
                        {
                            if (pump.PrepayText.ToUpper() == "PREPAY")
                            {
                                pumpOptions.Add(ApplicationConstants.Prepay);
                            }
                            else
                            {
                                pumpOptions.Add(ApplicationConstants.DeletePrepay);
                                pumpOptions.Add(ApplicationConstants.SwitchPrepay);
                            }
                        }
                    }
                    pumpOptions.Add(ApplicationConstants.Authorize);
                }
            }
            if (pump.Status.ToUpper().Equals(PumpStatuses.STOPPED.ToString()))
            {
                pumpOptions.Add(ApplicationConstants.Resume);
            }
            else if (!pump.Status.ToString().Equals(PumpStatuses.INACTIVE.ToString()))
            {
                pumpOptions.Add(ApplicationConstants.Stop);
            }

            if (pump.Status.ToUpper().Equals(PumpStatuses.AUTHORIZED.ToString()))
            {
                pumpOptions.Add(ApplicationConstants.Deauthorize);
            }

            if (pump.Status.ToUpper().Equals(PumpStatuses.PUMPING.ToString()))
            {
                pumpOptions.Add(ApplicationConstants.BigPump);
            }
            else
            {
                if (BigPump != null && !string.IsNullOrEmpty(BigPump.PumpId)
                    && BigPump.PumpId.Equals(pump.PumpId.ToString()))
                {
                    HideBigPump();
                }
            }
            return pumpOptions;
        }
        #endregion

        private void SwitchToggleButton(bool enableHamburgerIcon)
        {
            IsUserNotInSaleSummary = enableHamburgerIcon;
        }

        private void IsUserInSaleSummaryScreen(bool isUserNotInSaleSummaryPage)
        {
            IsUserNotInSaleSummary = isUserNotInSaleSummaryPage;
        }

        private void LoadAllPolicies(bool isRefresh)
        {
            PerformAction(async () =>
            {
                var timer = new Stopwatch();
                timer.Restart();
                try
                {
                    await _policyBussinessLogic.GetAllPolicies(isRefresh);

                    // call to initialize sale
                    var response = await _saleBussinessLogic.InitializeNewSale();
                    var sale = response.ToModel();
                    MessengerInstance.Send(sale, "UpdateSale");

                    StartFreezeTimer();
                    SetupDipInputTimer();
                    MessengerInstance.Send(new LoadHotCategoriesMessage());
                    if (!isRefresh)
                    {
                        var loginPolicy = await _loginBussinessLogic.GetLoginPolicyAsync();
                        CacheBusinessLogic.LoginPolicies = loginPolicy;
                    }
                    else
                    {
                        await _systemBusinessLogic.GetAndSaveRegisterSettings();
                        await VerifyPeripheralsConnected();
                        NavigateService.Instance.NavigateToHome();
                        MessengerInstance.Send(new object { }, "InitializeFuelPump");
                    }
                }
                finally
                {
                    if (IsSwitchUserStarted)
                    {
                        OperationsCompletedInSwitchUser++;
                    }

                    timer.Stop();
                    Log.Info(string.Format("Time taken in refresh is {0}ms ", timer.ElapsedMilliseconds));
                }
            });
        }

        private async void SetSystem()
        {
            LogoutButtonForFuelOnlySystem = false;

            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                LogoutButtonForFuelOnlySystem = true;
                IsFuelOnlySystem = true;
                IsPosOnlySystem = false;
                IsUserNotInSaleSummary = false;
                await InitializeFuelPump();
                SignalR();
            }
            else if (CacheBusinessLogic.IsPosOnlySystem)
            {
                IsFuelOnlySystem = false;
                IsPosOnlySystem = true;
            }
            else
            {
                IsFuelOnlySystem = true;
                IsPosOnlySystem = true;
                await InitializeFuelPump();
                SignalR();
            }
        }

        private void InitalizeData()
        {
            Till = CacheBusinessLogic.TillNumber;
            Shift = CacheBusinessLogic.ShiftNumber;
            TrainingCaption = CacheBusinessLogic.TrainerCaption;
            _notificationCounter = 0;
            _pumpClickDelayInSeconds = CacheBusinessLogic.ClickDelayForPumps;
            IsShiftDateVisible = CacheBusinessLogic.LoginPolicies.UseShifts;
            SetSystem();
            SetupDipInputTimer();
            SetUpHeadOfficeNotification();
            GetSounds();
            SetupPumpStatusTimer();
            SetCheckErrorTimer();
            CheckError(null, null);
        }

        private void SetCheckErrorTimer()
        {
            if (_checkErrorTimer == null)
            {
                _checkErrorTimer = new DispatcherTimer();

                _checkErrorTimer.Interval = new TimeSpan(0, 2, 0);

                _checkErrorTimer.Tick -= CheckError;
                _checkErrorTimer.Tick += CheckError;
                _checkErrorTimer.Start();
            }
        }

        private async void CheckError(object sender, object e)
        {
            await PerformActionWithoutLoader(async () =>
            {
                var response = await _fuelPumpBusinessLogic.CheckError();
                IsErrorVisible = response;
            }, false);
        }

        private void GetSounds()
        {
            PerformAction(async () =>
            {
                var sounds = await _soundBusinessLogic.GetSounds();
                SoundService.Instance.Sounds = sounds;
                SoundService.Instance.SetStopSoundToMedia();
            });
        }

        #region HeadOfficeNotification

        private void SetUpHeadOfficeNotification()
        {
            if (_headOfficeNotificationTimer == null && CacheBusinessLogic.SupportFuelPriceFromHO)
            {
                _headOfficeNotificationTimer = new DispatcherTimer();

                PerformAction(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.GetHeadOfficeMessage();

                    _headOfficeNotificationResponse = response;
                    _headOfficeNotificationTimer.Interval = new TimeSpan(0,
                        CacheBusinessLogic.FuelPriceNotificationTimeInterval, 0);

                    _headOfficeNotificationTimer.Tick -= HeadOfficeNotificationTimerTick;
                    _headOfficeNotificationTimer.Tick += HeadOfficeNotificationTimerTick;
                    _headOfficeNotificationTimer.Start();
                });
            }
        }

        private void OpenRequiredPageOfHeadOfficeNotification(string pageType, int option)
        {
            switch (pageType)
            {
                case "UserChange":
                    NavigateService.Instance.NavigateToLogout();
                    break;
                case "PumpGroupPrice":
                case "PumpPrice":
                    NavigateService.Instance.NavigateToFuelPricingPage();
                    _notificationCounter = 0;
                    _headOfficeNotificationResponse = new PumpMessage();
                    break;
                case "ResetCounter":
                    _notificationCounter = 0;
                    _headOfficeNotificationResponse = new PumpMessage();
                    break;
                default:
                    break;
            }
        }

        private async void HeadOfficeNotificationTimerTick(object sender, object e)
        {
            if (string.IsNullOrEmpty(_headOfficeNotificationResponse?.Message))
            {
                await PerformActionWithoutLoader(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.GetHeadOfficeMessage();

                    _headOfficeNotificationResponse = response;
                });
            }

            if (!string.IsNullOrEmpty(_headOfficeNotificationResponse?.Message))
            {
                if (!PopupService.PopupInstance.IsPopupOpen)
                {
                    if ((_notificationCounter == 0 || _notificationCounter <= CacheBusinessLogic.FuelPriceNotificationCount) &&
                        NavigateService.Instance.MasterFrame.Content.GetType().Name == "HomeScreen" &&
                        NavigateService.Instance.FirstFrame.Content.GetType().Name == "SaleGrid")
                    {
                        ++_notificationCounter;

                        if (_headOfficeNotificationResponse.MessageType == "36")
                        {
                            ShowConfirmationMessage(_headOfficeNotificationResponse.Message,
                                () =>
                                {
                                    UpdateFuelPrice(1);
                                },
                                () =>
                                {
                                    UpdateFuelPrice(2);
                                    ResetHeadOfficeNotificationPopup();
                                },
                                () =>
                                {
                                    UpdateFuelPrice(2);
                                    ResetHeadOfficeNotificationPopup();
                                });
                        }
                        else
                        {
                            ShowConfirmationMessage(_headOfficeNotificationResponse.Message,
                                () =>
                                {
                                    UpdateFuelPrice(1);
                                },
                                () =>
                                {
                                    UpdateFuelPrice(2);
                                    ResetHeadOfficeNotificationPopup();
                                },
                                () =>
                                {
                                    UpdateFuelPrice(2);
                                    ResetHeadOfficeNotificationPopup();
                                },
                                ApplicationConstants.ButtonConfirmationColor,
                                ApplicationConstants.ButtonWarningColor, ApplicationConstants.Yes, ApplicationConstants.No, true,
                                true, ApplicationConstants.Cancel, true, () =>
                                {
                                    UpdateFuelPrice(3);
                                });
                        }

                    }
                    else
                    {
                        _headOfficeNotificationTimer.Interval = new TimeSpan(0,
                            0, 2);
                    }
                }
                else
                {
                    _headOfficeNotificationTimer.Interval = new TimeSpan(0,
                        0, 2);
                }
            }
        }

        private void ResetHeadOfficeNotificationPopup()
        {
            _headOfficeNotificationTimer.Interval = new TimeSpan(0,
                CacheBusinessLogic.FuelPriceNotificationTimeInterval, 0);
            _headOfficeNotificationTimer.Start();
        }

        private void UpdateFuelPrice(int option)
        {
            PerformAction(async () =>
            {
                var response = await _fuelPumpBusinessLogic.UpdateFuelPrice(option, _notificationCounter);
                OpenRequiredPageOfHeadOfficeNotification(response, option);

            });
        }

        #endregion

        private void InitalizeCommands()
        {
            CurrentStackCommand = new RelayCommand<object>(CurrentStack);
            OpenEmergencyPopupCommand = new RelayCommand(OpenEmergencyPopup);
            ResumeAllPumpsCommand = new RelayCommand(ResumeAllPumps);
            CloseEmergencyPopupCommand = new RelayCommand(CloseEmergencyPopup);
            StopAllPumpsCommand = new RelayCommand(StopAllPumps);
            PumpInteractionCommand = new RelayCommand<object>(PumpInteraction);
            ToggleSplitViewCommand = new RelayCommand<object>(ToggleSplitView);
            LogoutCommand = new RelayCommand(NavigateService.Instance.NavigateToLogout);
            OpenMaintenanceWindowCommand = new RelayCommand(OpenMaintenanceWindow);
            OpenPaymentOptionsCommand = new RelayCommand(OpenPaymentOptions);
            OpenReportsCommand = new RelayCommand(OpenReports);
            ShowErrorsCommand = new RelayCommand(OpenErrorPage);
            AddBasketCommand = new RelayCommand<object>(AddBasket);
            
            OpenPumpOptionsPopupCommand = new RelayCommand(() =>
            {
                IsFuelPriceEnabled = _isFuelPriceEnabledResponse && CacheBusinessLogic.AreFuelPricesSaved;
                ShowConfirmationMessage(ApplicationConstants.PumpOptions,
                    OpenFuelPricingPage,
                    OpenTierLevelPage,
                    null,
                    ApplicationConstants.ButtonFooterColor,
                    ApplicationConstants.ButtonFooterColor,
                    ApplicationConstants.FuelPrice,
                    ApplicationConstants.TierLevel,
                    IsFuelPriceEnabled,
                    IsTierLevelEnabled,
                    ApplicationConstants.PropaneGrade,
                    !CacheBusinessLogic.IsFuelOnlySystem,
                    OpenPropaneGrade,
                    ApplicationConstants.ButtonFooterColor);

                if (!PopupService.PopupInstance.IsPopupOpen)
                {
                    PopupService.PopupInstance.IsPopupOpen = true;
                    PopupService.PopupInstance.IsPumpOptionsPopupOpen = true;
                }
            });

            ClosePumpOptionsPopupCommand = new RelayCommand(ClosePumpOptionsPopup);
            SaveSelectedPropaneGradeItemCommand = new RelayCommand<object>(PropaneGradeReasonSelected);
            AuthorizePumpCommand = new RelayCommand<object>(AuthorizePump);
            HideBigPumpCommand = new RelayCommand(async () =>
            {
                await HideBigPump();
            });
        }

        private async Task HideBigPump()
        {
            if (IsBigPumpVisible)
            {
                IsBigPumpVisible = false;
                await PerformActionWithoutLoader(async () =>
                {
                    var response = await _fuelPumpBusinessLogic.StopBroadcast();
                }, false);
            }
        }

        private void OpenTierLevelPage()
        {
            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                IsLogoutEnable = true;
                IsPosOnlySystem = true;
            }

            ClosePumpOptionsPopup();
            NavigateService.Instance.NavigateToTierlevelPage();
        }

        private void OpenFuelPricingPage()
        {
            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                IsLogoutEnable = true;
                IsPosOnlySystem = true;
            }
            ClosePumpOptionsPopup();
            NavigateService.Instance.NavigateToFuelPricingPage();
        }

        private void OpenPropaneGrade()
        {
            ClosePumpOptionsPopup();
            PerformAction(async () =>
            {
                await GetReasonListAsync(ReasonType.voidSales,
                SaveSelectedPropaneGradeItemCommand);
                RemovePumpOptions(new PumpOptionRemoveMessage
                {
                    RemoveManualOption = true,
                    RemoveFinishOption = true
                });
            });
        }

        private async Task GetReasonListAsync(ReasonType reasonEnum,
          RelayCommand<object> reasonSelectCommand)
        {
            if (!PopupService.IsPopupOpen)
            {
                PopupService.ReasonList?.Clear();

                _grades = await _fuelPumpBusinessLogic.LoadPropaneGrade();

                var response = (from r in _grades
                                select new Reasons
                                {
                                    Code = r.Id.ToString(),
                                    Description = r.FullName
                                }).ToList();


                foreach (var reason in _grades)
                {
                    PopupService.ReasonList.Add(new Reasons
                    {
                        Code = reason.Id.ToString(),
                        Description = reason.FullName
                    });
                }

                PopupService.Title = ApplicationConstants.PropaneGrade;
                PopupService.MessageItemClicked = reasonSelectCommand;
                PopupService.IsPopupOpen = true;
                PopupService.IsReasonPopupOpen = true;

                PopupService.CloseCommand = new RelayCommand(CloseReasonPopup);
            }
        }

        private void ClosePumpOptionsPopup()
        {
            PopupService.PopupInstance.IsPopupOpen = false;
            PopupService.PopupInstance.IsPumpOptionsPopupOpen = false;
        }

        private void PropaneGradeReasonSelected(dynamic reason)
        {
            CloseReasonPopup();
            PopupService.PopupInstance.IsPopupOpen = false;
            PopupService.PopupInstance.IsPumpOptionsPopupOpen = false;
            NavigateService.Instance.NavigateToPropaneGrade();
            MessengerInstance.Send(reason.Code, "SetGradeId");
        }

        private void OpenErrorPage()
        {
            NavigateService.Instance.NavigateToErrorPage();
            IsPaneOpen = false;
        }

        private void OpenReports()
        {
            NavigateService.Instance.NavigateToReports();
            IsPaneOpen = false;
        }

        private void OpenPaymentOptions()
        {
            NavigateService.Instance.NavigateToPayment();
            IsPaneOpen = false;
        }

        private void OpenMaintenanceWindow()
        {
            NavigateService.Instance.NavigateToMaintainence();
            IsPaneOpen = false;
        }

        private void ToggleSplitView(dynamic args)
        {
            IsPaneOpen = !IsPaneOpen;
        }

        private void SetNumberOfPumpInRow()
        {
            if (PumpDetails != null)
            {
                if (CacheBusinessLogic.PumpSpace == 1)
                {
                    NumberOfPumpRows = PumpDetails.Count <= 8 ? 1 : 2;
                    NumberOfPumpsInARow = 12;
                }
                else
                {
                    NumberOfPumpRows = PumpDetails.Count <= 15 ? 1 : 2;

                    if ((PumpDetails.Count & 1) == 1)
                    {
                        NumberOfPumpsInARow = (PumpDetails.Count / 2) + 1;
                    }
                    else
                    {
                        NumberOfPumpsInARow = PumpDetails.Count / 2;
                    }
                }
            }
        }

        public void ResetVM()
        {
            IsPaneOpen = false;
            BigPump = new BigPumpsModel();
            IsFuelPumpOptionEnable = true;
            _pumpClickDelayInSeconds = CacheBusinessLogic.ClickDelayForPumps;
            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                IsPosOnlySystem = false;
            }
            else
            {
                IsUserNotInSaleSummary = true;
                switch (CacheBusinessLogic.FramePriorSwitchUserNavigation)
                {
                    case "Reports":
                        NavigateService.Instance.NavigateToReports();
                        NavigateService.Instance.NavigateToTillAuditReport();
                        MessengerInstance.Send<bool>(true,
                         "SelectTillAuditTab");
                        break;

                    case "PaymentByFleet":
                        NavigateService.Instance.NavigateToPayment();
                        MessengerInstance.Send<SelectFleetTabMessage>(new SelectFleetTabMessage(),
                         "SelectPaymentByFleetTab");
                        break;

                    case "SwitchUser":
                        CacheBusinessLogic.PreviousAuthKey = CacheBusinessLogic.AuthKey;
                        CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                        NavigateService.Instance.NavigateToHome();
                        break;

                    case "PaymentByAccount":
                        NavigateService.Instance.NavigateToSaleSummary();
                        break;

                    case "PostPayMaintenance":
                        NavigateService.Instance.NavigateToMaintainence();
                        break;

                    case "SwitchUserToCashDraw":
                    default:
                        NavigateService.Instance.NavigateToHome();
                        break;
                }
            }
        }

        private void SetupPumpStatusTimer()
        {
            if (_pumpStatusTimer == null)
            {
                _pumpStatusTimer = new DispatcherTimer();
            }

            _pumpStatusTimer.Interval = new TimeSpan(0, 1, 0);

            _pumpStatusTimer.Tick -= PumpStatusTimerTick;
            _pumpStatusTimer.Tick += PumpStatusTimerTick;
            _pumpStatusTimer.Start();
        }

        private async void PumpStatusTimerTick(object sender, object e)
        {
            await PerformActionWithoutLoader(async () =>
            {
                var response = await _fuelPumpBusinessLogic.InitializeFuelPump(false, CacheBusinessLogic.TillNumberForSale);
                _initialPumpStatus = response;
                PumpSetup(_initialPumpStatus);
            }, false);
        }

        #region DipInput

        public void SetupDipInputTimer()
        {
            if (CacheBusinessLogic.SupportDipInput)
            {
                if (DipInputTimer == null)
                {
                    DipInputTimer = new DispatcherTimer();
                }

                var interval = (int)(CacheBusinessLogic.DipInputTime - DateTime.Now)
                    .TotalSeconds;

                if (interval <= 0)
                {
                    DipInputTimer.Interval = new TimeSpan(0, 5, 0);
                }
                else
                {
                    DipInputTimer.Interval = new TimeSpan(0, 0, interval);
                }

                DipInputTimer.Tick -= DipInputTimerTick;
                DipInputTimer.Tick += DipInputTimerTick;
                DipInputTimer.Start();
            }
        }

        private void DipInputTimerTick(object sender, object e)
        {
            int interval = 300;

            if (NavigateService.Instance.MasterFrame != null && NavigateService.Instance.FirstFrame != null &&
               NavigateService.Instance.MasterFrame.Content != null && NavigateService.Instance.FirstFrame.Content != null)
            {
                if (NavigateService.Instance.MasterFrame.Content.GetType().Name == "HomeScreen" &&
                     NavigateService.Instance.FirstFrame.Content.GetType().Name != "SalesSummary" &&
                     NavigateService.Instance.FirstFrame.Content.GetType().Name != "DipInput")
                {
                    if ((DateTime.Now.Date - CacheBusinessLogic.DipInputTime.Date).Days > 0)
                    {
                        interval = (int)(CacheBusinessLogic.DipInputTime.AddDays(
                            (DateTime.Now.Date - CacheBusinessLogic.DipInputTime.Date).Days) - DateTime.Now).
                            TotalSeconds;
                    }
                    else
                    {
                        ShowNotification(ApplicationConstants.DipReadingWarning,
                         () =>
                         {
                             NavigateService.Instance.NavigateToDipReading();
                             DipInputTimer.Stop();
                         },
                         () =>
                         {
                             NavigateService.Instance.NavigateToDipReading();
                             DipInputTimer.Stop();
                         },
                          ApplicationConstants.ButtonWarningColor);
                    }
                    DipInputTimer.Interval = new TimeSpan(0, 0, interval);
                }
                else
                {
                    DipInputTimer.Interval = new TimeSpan(0, 0, 2);
                }
            }
            else
            {
                DipInputTimer.Interval = new TimeSpan(0, 0, 2);
            }
        }

        #endregion

    }
}