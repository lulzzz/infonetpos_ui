using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infonet.CStoreCommander.UI.ViewModel.Login
{
    public class LogoutScreenVM : VMBase
    {
        #region Private Variables
        private bool _isTillEnabled;
        private bool _isShiftEnabled;
        private string _selectedShift;
        private int _selectedTill;
        private List<int> _tillNumbers;
        private List<string> _shifts;
        private string _userName;
        private string _password;
        private bool _isSwitchEnabled;
        private bool _isLogoutEnabled;
        private bool _isFreezeTillEnabled;
        private bool _requirePasswordOnChangeUser;
        private ICommand _backToHomeNavigationCommand;
        private int _invalidLoginAttempts;
        private const int MaxInvalidAttemps = 3;
        private bool _isVoidSaleToBeCompleted;
        private int _lineNumberToBeDeleted;
        private ValidateTillClose _validateTillCloseResponse;
        protected bool? _apiResponseForReadTotalizer;
        protected bool? _apiResponseForReadTankDip;
        #endregion

        #region Public Variables
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                EnabledUIControls();
                RaisePropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                EnabledUIControls();
                RaisePropertyChanged(nameof(Password));
            }
        }
        public List<int> TillNumbers
        {
            get { return _tillNumbers; }
            set
            {
                _tillNumbers = value;
                RaisePropertyChanged(nameof(TillNumbers));
            }
        }
        public List<string> Shifts
        {
            get { return _shifts; }
            set
            {
                _shifts = value;
                RaisePropertyChanged(nameof(Shifts));
            }
        }
        public bool IsFreezeTillEnabled
        {
            get { return _isFreezeTillEnabled; }
            set
            {
                _isFreezeTillEnabled = value;
                RaisePropertyChanged(nameof(IsFreezeTillEnabled));
            }
        }
        public bool IsLogoutEnabled
        {
            get { return _isLogoutEnabled; }
            set
            {
                _isLogoutEnabled = value;
                RaisePropertyChanged(nameof(IsLogoutEnabled));
            }
        }
        public bool IsSwitchEnabled
        {
            get { return _isSwitchEnabled; }
            set
            {
                _isSwitchEnabled = value;
                RaisePropertyChanged(nameof(IsSwitchEnabled));
            }
        }
        public int SelectedTill
        {
            get { return _selectedTill; }
            set
            {
                _selectedTill = value;
                RaisePropertyChanged(nameof(SelectedTill));
            }
        }
        public string SelectedShift
        {
            get { return _selectedShift; }
            set
            {
                _selectedShift = value;
                RaisePropertyChanged(nameof(SelectedShift));
            }
        }
        public bool IsShiftEnabled
        {
            get { return _isShiftEnabled; }
            set
            {
                _isShiftEnabled = value;
                RaisePropertyChanged(nameof(IsShiftEnabled));
            }
        }
        public bool IsTillEnabled
        {
            get { return _isTillEnabled; }
            set
            {
                _isTillEnabled = value;
                RaisePropertyChanged(nameof(IsTillEnabled));
            }
        }
        #endregion

        public ICommand BackToHomeNavigationCommand
        {
            get { return _backToHomeNavigationCommand; }
            set
            {
                _backToHomeNavigationCommand = value;
                RaisePropertyChanged(nameof(BackToHomeNavigationCommand));
            }
        }

        public RelayCommand SwitchUserCommand { get; set; }
        public RelayCommand LogoutUserCommand { get; set; }
        public RelayCommand<object> EnterPressedOnUserNameCommand { get; set; }
        public RelayCommand<object> EnterPressedOnPasswordCommand { get; set; }
        public RelayCommand<object> PasswordCompletedCommand { get; private set; }
        public RelayCommand FreezeCommand { get; private set; }

        public bool RequirePasswordOnChangeUser
        {
            get { return _requirePasswordOnChangeUser; }
            set
            {
                _requirePasswordOnChangeUser = value;
                RaisePropertyChanged(nameof(RequirePasswordOnChangeUser));
            }
        }

        protected readonly ILogoutBussinessLogic _logoutBussinesslogic;
        protected readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;

        private BottleReturnSaleModel _bottleReturnSale;
        private FuelPrices _fuelPrices;
        private SetPostPayMessage _setPostPayMessage;
        private int _pumpId;

        /// <summary>
        /// Constructor for LogoutScreenVM
        /// </summary>
        /// <param name="logoutBussinesslogic"></param>
        public LogoutScreenVM(ILogoutBussinessLogic logoutBussinesslogic,
            IFuelPumpBusinessLogic fuelPumpBusinessLogic)
        {
            _logoutBussinesslogic = logoutBussinesslogic;
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            InitializeData();
            InitializeCommands();
            UnregisterMessages();
            RegisterMessages();
        }

        private void UnregisterMessages()
        {
            MessengerInstance.Unregister<BottleReturnSaleModel>(this,
                 "SwitchUserAndCompleteBottleReturn",
                 SendToBottleReturnOnSaleComplete);
            MessengerInstance.Unregister<Reasons>(this,
                "SwitchUserAndVoidSale",
                SendToVoidSaleOnSwitchUser);
            MessengerInstance.Unregister<int>(this,
                "SwitchUserAndDeleteSaleLine",
                SendToDeleteSaleLineOnSwitchUser);
            MessengerInstance.Unregister<SaveFuelPricesMessage>(this,
                SaveFuelPrices);
            MessengerInstance.Unregister<ForceUserIDMessage>(this,
               ForceUserID);
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<BottleReturnSaleModel>(this,
                "SwitchUserAndCompleteBottleReturn",
                SendToBottleReturnOnSaleComplete);
            MessengerInstance.Register<Reasons>(this,
                "SwitchUserAndVoidSale",
                SendToVoidSaleOnSwitchUser);
            MessengerInstance.Register<int>(this,
                "SwitchUserAndDeleteSaleLine",
                SendToDeleteSaleLineOnSwitchUser);
            MessengerInstance.Register<SaveFuelPricesMessage>(this,
                SaveFuelPrices);
            MessengerInstance.Register<ForceUserIDMessage>(this,
               ForceUserID);
            MessengerInstance.Register<AddManualFuelMessage>(this, AddManualFuel);
            MessengerInstance.Register<SetPostPayMessage>(this, SetPostPay);
        }

        private void SetPostPay(SetPostPayMessage obj)
        {
            _setPostPayMessage = obj;
            _bottleReturnSale = null;
            _isVoidSaleToBeCompleted = false;
            _lineNumberToBeDeleted = 0;
            _fuelPrices = null;
            _pumpId = 0;
        }

        private void AddManualFuel(AddManualFuelMessage obj)
        {
            _bottleReturnSale = null;
            _isVoidSaleToBeCompleted = false;
            _lineNumberToBeDeleted = 0;
            _fuelPrices = null;
            _pumpId = obj.PumpId;
            _setPostPayMessage = null;
        }

        private void ForceUserID(ForceUserIDMessage message)
        {
            if (CacheBusinessLogic.SwitchUserOnEachSale)
            {
                IsLogoutEnabled = IsFreezeTillEnabled = false;
            }
        }

        private void SaveFuelPrices(SaveFuelPricesMessage message)
        {
            _fuelPrices = message?.FuelPrices;
            _bottleReturnSale = null;
            _isVoidSaleToBeCompleted = false;
            _lineNumberToBeDeleted = 0;
            _pumpId = 0;
            _setPostPayMessage = null;
        }

        private void SendToDeleteSaleLineOnSwitchUser(int saleLine)
        {
            _fuelPrices = null;
            _bottleReturnSale = null;
            _isVoidSaleToBeCompleted = false;
            _lineNumberToBeDeleted = saleLine;
            _pumpId = 0;
            _setPostPayMessage = null;
        }

        private void SendToVoidSaleOnSwitchUser(Reasons reasons)
        {
            _fuelPrices = null;
            _bottleReturnSale = null;
            _isVoidSaleToBeCompleted = true;
            _lineNumberToBeDeleted = 0;
            _pumpId = 0;
            _setPostPayMessage = null;
        }

        private void SendToBottleReturnOnSaleComplete(BottleReturnSaleModel sale)
        {
            // TODO: Ipsit - Not the proper way of injecting the Bottle return dependency 
            // in the code for messenging
            _bottleReturnSale = sale;
            _isVoidSaleToBeCompleted = false;
            _lineNumberToBeDeleted = 0;
            _fuelPrices = null;
            _pumpId = 0;
            _setPostPayMessage = null;
        }

        /// <summary>
        /// Method to initialize required data
        /// </summary>
        private void InitializeData()
        {
            IsLogoutEnabled = IsSwitchEnabled = false;
            IsFreezeTillEnabled = true;
            Password = UserName = string.Empty;
            SelectedTill = CacheBusinessLogic.TillNumber;
            RequirePasswordOnChangeUser = CacheBusinessLogic.RequirePasswordOnChangeUser;
        }

        /// <summary>
        /// Method to Initialize commands
        /// </summary>
        private void InitializeCommands()
        {
            LogoutUserCommand = new RelayCommand(() => PerformAction(LogoutUser));
            EnterPressedOnPasswordCommand = new RelayCommand<object>(EnterPressedOnPassword);
            EnterPressedOnUserNameCommand = new RelayCommand<object>(EnterPressedOnUserName);
            PasswordCompletedCommand = new RelayCommand<object>(PasswordCompleted);
            SwitchUserCommand = new RelayCommand(() => PerformAction(SwitchUser));
            FreezeCommand = new RelayCommand(NavigateService.Instance.NavigateToFreeze);
            BackToHomeNavigationCommand = new RelayCommand(() =>
            {
                CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                NavigateService.Instance.RedirectToHome();
            });
        }

        private async Task LogoutUser()
        {
            try
            {
                _validateTillCloseResponse = await _logoutBussinesslogic.ValidateClosetill();

                if (!string.IsNullOrEmpty(_validateTillCloseResponse.PrepayMessage.Message))
                {
                    ShowConfirmationMessage(_validateTillCloseResponse.PrepayMessage.Message,
                        CheckForSuspendMessage,
                        NavigateService.Instance.RedirectToHome,
                        NavigateService.Instance.RedirectToHome);
                }
                else
                {
                    CheckForSuspendMessage();
                }
            }
            catch (ApiDataException ex)
            {
                _log.Info(ex.Message, ex);

                ShowNotification(ex.Message,
                    NavigateService.Instance.RedirectToHome,
                    NavigateService.Instance.RedirectToHome,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void CheckForSuspendMessage()
        {
            if (!string.IsNullOrEmpty(_validateTillCloseResponse.SuspendSaleMessage.Message))
            {
                ShowNotification(_validateTillCloseResponse.SuspendSaleMessage.Message,
                   CheckForEndSaleSessionMessage,
                   NavigateService.Instance.RedirectToHome,
                   ApplicationConstants.ButtonConfirmationColor);
            }
            else
            {
                CheckForEndSaleSessionMessage();
            }
        }

        private void CheckForEndSaleSessionMessage()
        {
            if (!string.IsNullOrEmpty(_validateTillCloseResponse.EndSaleSessionMessage.Message))
            {
                ShowConfirmationMessage(_validateTillCloseResponse.EndSaleSessionMessage.Message,
                    EndShift,
                    NavigateService.Instance.RedirectToHome,
                    NavigateService.Instance.RedirectToHome);
            }
            else
            {
                CheckForCloseTillMessage();
            }
        }

        private void EndShift()
        {
            bool response = false;

            PerformAction(async () =>
            {
                response = await _logoutBussinesslogic.EndShift();

                if (response)
                {
                    PerformCloseApplication();
                }
            });
        }

        private void CheckForCloseTillMessage()
        {
            if (!string.IsNullOrEmpty(_validateTillCloseResponse.CloseTillMessage.Message))
            {
                ShowConfirmationMessage(_validateTillCloseResponse.CloseTillMessage.Message,
                    CheckForReadTotalizerMessage,
                    EndShift,
                     NavigateService.Instance.RedirectToHome,
                    ApplicationConstants.ButtonConfirmationColor,
                     ApplicationConstants.ButtonWarningColor,
                    ApplicationConstants.Yes,
                    ApplicationConstants.No,
                    true, true, ApplicationConstants.Cancel, true,
                    NavigateService.Instance.RedirectToHome,
                    ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                CheckForReadTotalizerMessage();
            }
        }

        private void CheckForReadTotalizerMessage()
        {
            if (!string.IsNullOrEmpty(_validateTillCloseResponse.ReadTotalizerMessage.Message))
            {
                if (!string.IsNullOrEmpty(_validateTillCloseResponse.CloseTillMessage.Message))
                {
                    ShowConfirmationMessage(_validateTillCloseResponse.ReadTotalizerMessage.Message,
                      CallPumpReadTotalizer,
                      CheckForReadTankDip,
                      CheckForReadTankDip);
                }
                else
                {
                    ShowConfirmationMessage(_validateTillCloseResponse.ReadTotalizerMessage.Message,
                   CallPumpReadTotalizer,
                   CheckForReadTankDip,
                   CheckForReadTankDip,
                   ApplicationConstants.ButtonConfirmationColor,
                    ApplicationConstants.ButtonWarningColor,
                   ApplicationConstants.Yes,
                   ApplicationConstants.No,
                   true, true, ApplicationConstants.Cancel, true,
                   NavigateService.Instance.RedirectToHome,
                   ApplicationConstants.ButtonWarningColor);
                }
            }
            else
            {
                CheckForReadTankDip();
            }
        }

        private void CallPumpReadTotalizer()
        {
            PerformAction(async () =>
            {
                try
                {
                    _apiResponseForReadTotalizer = await _fuelPumpBusinessLogic.ReadTotalizer();
                }
                catch (ApiDataException)
                {
                    _apiResponseForReadTotalizer = false;
                }
                finally
                {
                    CheckForReadTankDip();
                }
            });
        }

        private void CheckForReadTankDip()
        {
            if (!string.IsNullOrEmpty(_validateTillCloseResponse.TankDipMessage.Message))
            {
                ShowConfirmationMessage(_validateTillCloseResponse.TankDipMessage.Message,
                 CallReadTankDip,
                 PerformEndShiftOrTillClose,
                 PerformEndShiftOrTillClose);
            }
            else if (_validateTillCloseResponse.ProcessTankDip)
            {
                CallReadTankDip();
            }
            else
            {
                PerformEndShiftOrTillClose();
            }
        }

        private void PerformEndShiftOrTillClose()
        {
            if (string.IsNullOrEmpty(_validateTillCloseResponse.CloseTillMessage.Message) &&
               !string.IsNullOrEmpty(_validateTillCloseResponse.ReadTotalizerMessage.Message)
               && _apiResponseForReadTotalizer == null)
            {
                EndShift();
            }
            else
            {
                PerformAction(async () =>
                {
                    var response = await _logoutBussinesslogic.CloseTill();
                    NavigateService.Instance.NavigateToCloseTill();

                    var closeTillMessage = new CloseTillMessage
                    {
                        ApiResponseForReadTankDip = _apiResponseForReadTankDip,
                        ApiResponseForReadTotalizer = _apiResponseForReadTotalizer
                    };

                    MessengerInstance.Send<CloseTillMessage>(closeTillMessage, "CloseTillMessage");
                    MessengerInstance.Send(response, "SetCloseTill");

                });
            }
        }

        private void CallReadTankDip()
        {
            PerformAction(async () =>
            {
                _apiResponseForReadTankDip = await _logoutBussinesslogic.ReadTankDip();
                PerformEndShiftOrTillClose();
            });
        }

        protected void PerformCloseApplication()
        {
            // Releasing printer just before log off
            new PeripheralsService().ReleasePrinter();
            if (CacheBusinessLogic.LoginPolicies.WindowsLogin)
            {
                LogOffSystem();
            }
            else
            {
                ShutDownApplication();
            }
        }

        public void PasswordCompleted(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                PerformAction(SwitchUser);
            }
        }

        private void EnterPressedOnPassword(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                PerformAction(SwitchUser);
            }
        }

        private void EnterPressedOnUserName(dynamic args)
        {
            if (!RequirePasswordOnChangeUser)
            {
                EnterPressedOnPassword(args);
            }
        }

        /// <summary>
        /// Method to switch user
        /// </summary>
        /// <returns></returns>
        private async Task SwitchUser()
        {
            var startTime = DateTime.Now;
            IsSwitchUserStarted = true;
            SwitchUserStartTime = DateTime.Now;
            try
            {
                var flag = IsSwitchUserInitiatedForUnathorizedAccess();

                var response = await _logoutBussinesslogic.SwitchUserAsync(UserName, Password, flag);
                _invalidLoginAttempts = 0;
                // TODO: Move code of saving auth key to BL

                // TODO: If user closes without Switch user then it will create issue
                CacheBusinessLogic.AuthKey = response;

                switch (CacheBusinessLogic.FramePriorSwitchUserNavigation)
                {
                    case "Reports":
                    case "PaymentByFleet":
                    case "PaymentByAccount":
                    case "SwitchUserToCashDraw":
                        NavigateService.Instance.RedirectToHome();
                        MessengerInstance.Send(new CloseKeyboardMessage());
                        break;

                    case "PerformManualSale":
                        MessengerInstance.Send(false, "LoadAllPolicies");
                        await Task.Delay(1000);
                        NavigateService.Instance.RedirectToHome();
                        NavigateService.Instance.NavigateToManualFuelSale();
                        MessengerInstance.Send(new FuelManualPumpMessage
                        {
                            PumpId = _pumpId
                        });
                        break;
                    default:
                        {
                            MessengerInstance.Send(false, "LoadAllPolicies");
                            if (_lineNumberToBeDeleted != 0)
                            {
                                MessengerInstance.Send(_lineNumberToBeDeleted, "CompleteDeleteSaleLine");
                                _lineNumberToBeDeleted = 0;
                            }
                            else if (_bottleReturnSale != null)
                            {
                                MessengerInstance.Send(_bottleReturnSale, "CompleteBottleReturn");
                                _bottleReturnSale = null;
                            }
                            else if (_isVoidSaleToBeCompleted)
                            {
                                MessengerInstance.Send(_isVoidSaleToBeCompleted, "CompleteVoidSale");
                                _isVoidSaleToBeCompleted = false;
                            }
                            else if (_fuelPrices != null)
                            {
                                MessengerInstance.Send(_fuelPrices);
                                _fuelPrices = null;
                            }
                            else if (_setPostPayMessage != null)
                            {
                                MessengerInstance.Send(_setPostPayMessage.Value, "SetPostPay");
                                _setPostPayMessage = null;
                            }
                        }
                        NavigateService.Instance.RedirectToHome();
                        MessengerInstance.Send(new CloseKeyboardMessage());
                        break;
                }

            }
            catch (ApiDataException ex)
            {
                if (ex.Message.Contains("Invalid  Password") ||
                       ex.Message.Contains("No Such User"))
                {
                    if (++_invalidLoginAttempts == MaxInvalidAttemps)
                    {
                        ShowNotification(ApplicationConstants.MaxLoginAttemptsReached,
                            ShutDownApplication, ShutDownApplication,
                            ApplicationConstants.ButtonWarningColor);
                        return;
                    }
                }

                ShowNotification(ex.Message, ReInitialize, ReInitialize,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                var endTime = DateTime.Now;
                if (IsSwitchUserStarted)
                {
                    OperationsCompletedInSwitchUser++;
                }
            }
        }

        private bool IsSwitchUserInitiatedForUnathorizedAccess()
        {
            return CacheBusinessLogic.FramePriorSwitchUserNavigation == "Reports" ||
                CacheBusinessLogic.FramePriorSwitchUserNavigation == "PaymentByFleet" ||
                CacheBusinessLogic.FramePriorSwitchUserNavigation == "PaymentByAccount" ||
                _lineNumberToBeDeleted != 0 ||
                _bottleReturnSale != null ||
                _pumpId != 0 ||
                _setPostPayMessage != null ||
                _isVoidSaleToBeCompleted;
        }

        /// <summary>
        /// Method to check if user name or password is empty
        /// </summary>
        /// <returns></returns>
        private bool IsUserNameOrPasswordNotEmpty()
        {
            return (!string.IsNullOrEmpty(UserName) && !RequirePasswordOnChangeUser) ||
                   (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
        }

        /// <summary>
        /// Method to enable IsSwitchUser  Property if UserName and password are not empty
        /// </summary>
        private void EnabledUIControls()
        {
            if (IsUserNameOrPasswordNotEmpty() ||
                (!string.IsNullOrEmpty(UserName) && !CacheBusinessLogic.RequirePasswordOnChangeUser))
            {
                IsSwitchEnabled = true;
            }
            else
            {
                IsSwitchEnabled = false;
            }
        }

        internal void ReInitialize()
        {
            IsLogoutEnabled = true;
            UserName = Password = string.Empty;
            IsSwitchEnabled = false;
            IsFreezeTillEnabled = true;
            _apiResponseForReadTankDip = _apiResponseForReadTotalizer = null;
            RequirePasswordOnChangeUser = CacheBusinessLogic.RequirePasswordOnChangeUser;
            if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "Reports" ||
                CacheBusinessLogic.FramePriorSwitchUserNavigation == "SwitchUser")
            {
                IsLogoutEnabled = false;
                IsFreezeTillEnabled = false;
            }
        }
    }
}
