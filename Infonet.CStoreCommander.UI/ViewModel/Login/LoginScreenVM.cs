using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Login;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Login
{
    /// <summary>
    /// View model for Login screen
    /// </summary>
    public class LoginScreenVM : VMBase
    {
        private readonly InfonetLog _log =
            InfonetLogManager.GetLogger<LoginScreenVM>();

        private readonly ILoginBussinessLogic _loginBussinessLogic;
        private readonly IPolicyBussinessLogic _policyBussinessLogic;
        private readonly ISystemBusinessLogic _systemBusinessLogic;

        private int _selectedTillIndex;
        private bool _showSingleShift;
        private string _shiftText;

        public string ShiftText
        {
            get { return _shiftText; }
            set
            {
                _shiftText = value;
                RaisePropertyChanged(nameof(ShiftText));
            }
        }


        public bool ShowSingleShift
        {
            get { return _showSingleShift; }
            set
            {
                _showSingleShift = value;
                RaisePropertyChanged(nameof(ShowSingleShift));

                if (value == true)
                {
                    IsShiftVisible = false;
                }
            }
        }

        public int SelectedTillIndex
        {
            get { return _selectedTillIndex; }
            set
            {
                if (_selectedTillIndex != value)
                {
                    _selectedTillIndex = value;
                    RaisePropertyChanged(nameof(SelectedTillIndex));

                    if (value >= 0)
                    {
                        TillsSelected(LoginModel.TillNumbers?.ElementAt(value));
                    }
                }
            }
        }

        #region Private Variables
        private bool _isLoginEnabled;
        private bool _isTillEnabled;
        private bool _isTillVisible;
        private bool _isShiftEnabled;
        private bool _isShiftVisible;
        private bool _isUsernameEnabled;
        private bool _isPasswordEnabled;
        private bool _isShiftPopupOpen;
        private bool _isTillFloatEnabled;
        private bool _isTillFloatVisible;
        private int _invalidLoginAttempts;

        private ActiveShifts _activeShiftsModel;

        private const int MaxInvalidAttemps = 3;
        #endregion

        #region Public Variables
        public bool IsPasswordEnabled
        {
            get { return _isPasswordEnabled; }
            set
            {
                _isPasswordEnabled = value;
                RaisePropertyChanged(nameof(IsPasswordEnabled));
            }
        }

        public bool IsUserNameEnabled
        {
            get { return _isUsernameEnabled; }
            set
            {
                _isUsernameEnabled = value;
                RaisePropertyChanged(nameof(IsUserNameEnabled));
            }
        }

        public LoginModel LoginModel { get; set; }
        = new LoginModel { TillFloat = "0.00" };

        public bool IsTillFloatEnabled
        {
            get { return _isTillFloatEnabled; }
            set
            {
                _isTillFloatEnabled = value;
                IsTillFloatVisible = true;
                RaisePropertyChanged(nameof(IsTillFloatEnabled));
            }
        }

        public bool IsLoginEnabled
        {
            get { return _isLoginEnabled; }
            set
            {
                _isLoginEnabled = value;
                RaisePropertyChanged(nameof(IsLoginEnabled));
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

        public bool IsShiftPopupOpen
        {
            get { return _isShiftPopupOpen; }
            set
            {
                _isShiftPopupOpen = value;
                RaisePropertyChanged(nameof(IsShiftPopupOpen));
            }
        }

        public bool IsTillVisible
        {
            get { return _isTillVisible; }
            set
            {
                _isTillVisible = value;
                RaisePropertyChanged(nameof(IsTillVisible));
            }
        }

        public bool IsShiftVisible
        {
            get { return _isShiftVisible; }
            set
            {
                _isShiftVisible = value;
                RaisePropertyChanged(nameof(IsShiftVisible));
                if (IsShiftVisible)
                {
                    ShowSingleShift = false;
                }
            }
        }

        public bool IsTillFloatVisible
        {
            get { return _isTillFloatVisible; }
            set
            {
                _isTillFloatVisible = value;
                RaisePropertyChanged(nameof(IsTillFloatVisible));
            }
        }

        public Dictionary<string, int> Shifts { get; set; }
            = new Dictionary<string, int>();

        public string UserFieldName;
        #endregion

        #region Commands
        public RelayCommand GetTillsCommand { get; private set; }
        public RelayCommand<object> PasswordCompletedCommand { get; private set; }
        public RelayCommand<object> TillFloatCompletedCommand { get; private set; }
        public RelayCommand<object> TillFloatGotFocusCommand { get; private set; }
        public RelayCommand<object> TillsSelectedCommand { get; private set; }
        public RelayCommand<object> ShiftsSelectedCommand { get; private set; }
        public RelayCommand LoginUserCommand { get; private set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginBussinessLogic"></param>
        /// <param name="policyBussinessLogic"></param>
        public LoginScreenVM(ILoginBussinessLogic loginBussinessLogic,
            IPolicyBussinessLogic policyBussinessLogic,
            ISystemBusinessLogic systemBusinessLogic)
        {
            _loginBussinessLogic = loginBussinessLogic;
            _policyBussinessLogic = policyBussinessLogic;
            _systemBusinessLogic = systemBusinessLogic;
            InitializeCommands();
            InitalizeData();
        }

        private void InitializeCommands()
        {
            LoginUserCommand = new RelayCommand(RaiseShiftConfirmationPopup);
            TillFloatCompletedCommand = new RelayCommand<object>(TillFloatCompleted);
            PasswordCompletedCommand = new RelayCommand<object>(PasswordCompleted);
            GetTillsCommand = new RelayCommand(GetTills);
            TillsSelectedCommand = new RelayCommand<object>(TillsSelected);
            ShiftsSelectedCommand = new RelayCommand<object>(ShiftsSelected);
            TillFloatGotFocusCommand = new RelayCommand<object>(TillFloatGotFocus);
        }

        private void TillFloatCompleted(dynamic args)
        {
            if (Helper.IsEnterKey(args) && IsLoginEnabled)
            {
                LoginUser();
            }
        }

        private void InitalizeData()
        {
            // clearing KickBackCardNumber cache for those cases when POS closed forcefully
            CacheBusinessLogic.KickBackCardNumber = null;

            LoginModel.KeypadFormat = CacheBusinessLogic.LoginPolicies.KeypadFormat;

            if (CacheBusinessLogic.LoginPolicies.WindowsLogin)
            {
                CacheBusinessLogic.Password = CacheBusinessLogic.UserName = string.Empty;
                IsPasswordEnabled = IsUserNameEnabled = false;
                GetUserName();
                GetPassword();
            }
            else
            {
                IsPasswordEnabled = IsUserNameEnabled = true;
            }
        }

        private async void GetUserName()
        {
            try
            {
                LoginModel.UserName = CacheBusinessLogic.UserName = await new WindowManager().GetCurrentUserName();
            }
            catch (WindowsManagerException ex)
            {
                ShowNotification(ex.Message,
                      null,
                      null,
                      ApplicationConstants.ButtonWarningColor);
            }
        }

        public void GetPassword()
        {
            PerformAction(async () =>
            {
                try
                {
                    bool isUserNameAndPasswordNotEmpty = false;

#if DEBUG
                    CacheBusinessLogic.UserName = "optimus80";
                    LoginModel.UserName = "optimus80";
#endif

                    LoginModel.Password = CacheBusinessLogic.Password = await _loginBussinessLogic.GetPassword();

                    SaveUsernameAndPassword(ref isUserNameAndPasswordNotEmpty);

                    PopulateTillsAsync();
                }
                catch (ApiDataException ex)
                {
                    ShowNotification(
                    ex.Error.Message,
                    LogOffSystem,
                    LogOffSystem, ApplicationConstants.ButtonWarningColor);
                }
            });
        }


        private void RaiseShiftConfirmationPopup()
        {
            if (CacheBusinessLogic.LoginPolicies.AutoShiftPick)
            {
                if (CacheBusinessLogic.LoginPolicies.UseShifts)
                {
                    ShowSingleShift = true;
                    ShiftText = LoginModel.Shifts?.FirstOrDefault();
                }

                LoginUser();
            }
            else
            {
                var message = string.Format(ApplicationConstants.ShiftConfirmation,
                    CacheBusinessLogic.ShiftNumber, CacheBusinessLogic.ShiftDate);
                ShowConfirmationMessage(message, LoginUser, () =>
                {
                    if (CacheBusinessLogic.LoginPolicies.UseShifts)
                    {
                        if (LoginModel.Shifts.Count > 1)
                        {
                            IsShiftEnabled = true;
                            IsLoginEnabled = false;
                        }
                    }
                    else
                    {
                        ShutDownApplication();
                    }
                }, () =>
             {
                 if (CacheBusinessLogic.LoginPolicies.UseShifts)
                 {
                     if (LoginModel.Shifts.Count > 1)
                     {
                         IsShiftEnabled = true;
                         IsLoginEnabled = false;
                     }
                 }
                 else
                 {
                     ShutDownApplication();
                 }
             });
            }
        }

        private async void TillsSelected(dynamic s)
        {
            if (s is int)
            {
                IsTillEnabled = false;
                CacheBusinessLogic.TillNumber = s;
                IsTillFloatVisible = IsTillFloatEnabled = CacheBusinessLogic.LoginPolicies.ProvideTillFloat;

                if (CacheBusinessLogic.LoginPolicies.UseShifts)
                {
                    await GetShiftsAsync();

                    if (LoginModel.Shifts != null)
                    {
                        if (LoginModel.Shifts.Count == 1)
                        {
                            if (LoginModel.Shifts.FirstOrDefault() != "0")
                            {
                                ShowSingleShift = true;
                                ShiftText = LoginModel.Shifts.FirstOrDefault();
                            }
                            IsLoginEnabled = true;
                        }
                        else
                        {
                            IsShiftVisible = true;
                            IsShiftEnabled = true;
                        }
                    }
                }
                else
                {
                    IsShiftVisible = false;

                    CacheBusinessLogic.ShiftNumber = 0;
                    CacheBusinessLogic.ShiftDate = string.Empty;
                    IsLoginEnabled = true;
                }
            }
        }


        private void PopulateTillsAsync()
        {
            PerformAction(async () =>
            {
                try
                {
                    ActiveTills tillsModel = null;
                    IsUserNameEnabled = IsPasswordEnabled = false;
                    if (!CacheBusinessLogic.LoginPolicies.UsePredefinedTillNumber)
                    {
                        tillsModel = await _loginBussinessLogic.GetTillsAsync();
                        LoginModel.TillNumbers = tillsModel.Tills;
                        IsTillEnabled = true;
                        LoginModel.TillFloat = tillsModel.CashFloat.ToString("0.00", CultureInfo.InvariantCulture);

                        if (tillsModel.IsTrainer)
                        {
                            if (!tillsModel.ForceTill)
                            {
                                ShowNotification(tillsModel.Message, () =>
                                {
                                    if (LoginModel.TillNumbers.Count == 1)
                                    {
                                        SelectedTillIndex = 0;
                                    }
                                }, () =>
                                {
                                    if (LoginModel.TillNumbers.Count == 1)
                                    {
                                        SelectedTillIndex = 0;
                                    }
                                }, ApplicationConstants.ButtonWarningColor);
                            }
                        }

                        // Force user for the last used till
                        if (tillsModel.ForceTill)
                        {
                            ShowNotification(tillsModel.Message, LoginUser, LoginUser, ApplicationConstants.ButtonWarningColor);
                            return;
                        }


                        if (LoginModel.TillNumbers.Count == 1)
                        {
                            SelectedTillIndex = 0;
                        }

                    }
                    else
                    {
                        var tills = await new Helper().GetTillsOffline();
                        LoginModel.TillNumbers = tills;
                        tillsModel = await _loginBussinessLogic.GetTillsAsync();
                        CacheBusinessLogic.TillNumber = tills.FirstOrDefault();

                        if (tillsModel.IsTrainer)
                        {
                            ShowNotification(tillsModel.Message, () =>
                            {
                                SelectedTillIndex = 0;
                            },
                            () =>
                            {
                                SelectedTillIndex = 0;
                            }, ApplicationConstants.ButtonWarningColor);
                        }
                        else
                        {
                            SelectedTillIndex = 0;
                        }
                    }
                    IsTillVisible = true;

                }
                catch (UserAlreadyLoggedOnException ex)
                {
                    ShowNotification(ex.Error.Message, ResetLogin, ResetLogin, ApplicationConstants.ButtonWarningColor);
                }
                catch (InternalServerException ex)
                {
                    ShowNotification(
                        ex.Error.Message,
                        ShutDownApplication,
                        ShutDownApplication, ApplicationConstants.ButtonWarningColor);
                }
                catch (ApiDataException ex)
                {
                    if (ex.Message.Contains("Invalid  Password") ||
                        ex.Message.Contains("No Such User"))
                    {  // Checking for the invalid attempts and exiting the application
                        _invalidLoginAttempts++;
                    }
                    if (_invalidLoginAttempts == MaxInvalidAttemps)
                    {
                        ShowNotification(ApplicationConstants.MaxLoginAttemptsReached,
                            ShutDownApplication, ShutDownApplication,
                            ApplicationConstants.ButtonWarningColor);
                        return;
                    }

                    IsPasswordEnabled = IsUserNameEnabled = true;

                    ShowNotification(
                        ex.Error.Message,
                        ex.Error.ShutDownPos ? ShutDownApplication : (Action)ResetLogin,
                        ex.Error.ShutDownPos ? ShutDownApplication : (Action)ResetLogin,
                        ApplicationConstants.ButtonWarningColor);

                    return;
                }
                finally
                {
                    MessengerInstance.Send(new CloseKeyboardMessage());
                }
            });
        }

        internal void ResetLogin()
        {
            var startTime = DateTime.Now;
            _log.Info("ResetLogin method call started");
            LoginModel.UserName = string.Empty;
            LoginModel.Password = string.Empty;
            LoginModel.TillNumbers?.Clear();
            LoginModel.Shifts?.Clear();
            Shifts?.Clear();
            IsUserNameEnabled = true;
            IsPasswordEnabled = true;

            var endTime = DateTime.Now;
            _log.Info(string.Format("ResetLogin method call ended in {0}ms",
                (endTime - startTime).TotalMilliseconds));
        }

        private void PopulateShifts(ActiveShifts shifts)
        {
            _activeShiftsModel = shifts;
            var startTime = DateTime.Now;
            _log.Info("PopulateShifts method call started");
            LoginModel.Shifts = new List<string>();


            foreach (var shift in _activeShiftsModel.Shifts)
            {
                LoginModel.Shifts.Add(shift.DisplayFormat);
                if (!Shifts.ContainsKey(shift.DisplayFormat))
                {
                    Shifts.Add(shift.DisplayFormat, shift.ShiftNumber);
                }
            }

            LoginModel.TillFloat = _activeShiftsModel.CashFloat.ToString("0.00", CultureInfo.InvariantCulture);
            var endTime = DateTime.Now;
            MessengerInstance.Send(new CloseKeyboardMessage());
            _log.Info(string.Format("PopulateShifts method call ended in {0}ms",
                (endTime - startTime).TotalMilliseconds));
        }

        private async Task GetShiftsAsync()
        {
            var startTime = DateTime.Now;
            _log.Info("GetShiftsAsync method call started");
            try
            {
                LoadingService.ShowLoadingStatus(true);
                var shifts = await _loginBussinessLogic.GetShiftsAsync();

                PopulateShifts(shifts);

                if (shifts.ForceShift)
                {
                    LoginUser();
                    return;
                }
                if (CacheBusinessLogic.UseShiftForTheDay)
                {
                    ShowConfirmationMessage(ApplicationConstants.ShiftUsedForTheDay, null, ShutDownApplication, ShutDownApplication);
                }
            }
            catch (UserAlreadyLoggedOnException ex)
            {
                ShowNotification(ex.Error.Message, ResetLogin, ResetLogin, ApplicationConstants.ButtonWarningColor);
            }
            catch (InternalServerException ex)
            {
                ShowNotification(ex.Error.Message, ShutDownApplication, ShutDownApplication, ApplicationConstants.ButtonWarningColor);
            }
            catch (ApiDataException ex)
            {
                ShowNotification(
                    ex.Error.Message,
                    () =>
                    {
                        if (ex.Error.ShutDownPos)
                        {
                            ShutDownApplication();
                        }
                        else
                        {
                            ResetUIElements();
                        }
                    },
                    () =>
                    {
                        if (ex.Error.ShutDownPos)
                        {
                            ShutDownApplication();
                        }
                        else
                        {
                            ResetUIElements();
                        }
                    },
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception ex)
            {
                Log.Info(Message, ex);
                ShowNotification(ApplicationConstants.SomethingBadHappned,
                    null,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
            }
            var endTime = DateTime.Now;
            _log.Info(string.Format("GetShiftsAsync method call ended in {0}ms",
                (endTime - startTime).TotalMilliseconds));
        }


        private void ResetUIElements()
        {
            PerformAction(() =>
            {
                ResetLogin();

                IsShiftVisible = false;
                IsShiftEnabled = false;

                IsTillFloatEnabled = false;
                IsTillFloatVisible = false;

                IsTillVisible = false;
                IsTillEnabled = false;

                return Task.FromResult<bool>(true);
            }, UserFieldName);
        }

        private void ShiftsSelected(dynamic selectedShiftNumber)
        {
            if (selectedShiftNumber is string)
            {
                var startTime = DateTime.Now;
                _log.Info("ShiftsSelected method call started");

                IsShiftEnabled = false;
                IsLoginEnabled = true;
                IsShiftVisible = true;

                int shiftNumber = 0;

                if (Shifts.TryGetValue(selectedShiftNumber, out shiftNumber))
                {
                    CacheBusinessLogic.ShiftNumber = shiftNumber;
                    CacheBusinessLogic.ShiftDate = _activeShiftsModel.Shifts
                        .FirstOrDefault(x => x.ShiftNumber == shiftNumber).ShiftDate;
                }
                var endTime = DateTime.Now;
                _log.Info(string.Format("ShiftsSelected method call ended in {0}ms",
                    (endTime - startTime).TotalMilliseconds));
            }
        }

        private void LoginUser()
        {
            PerformAction(async () =>
            {
                var startTime = DateTime.Now;
                _log.Info("LoginUser method call started");
                try
                {
                    CacheBusinessLogic.CashFloat = LoginModel.TillFloat;
                    var login = await _loginBussinessLogic.LoginAsync();
                    await _policyBussinessLogic.GetAllPolicies(false);
                    await _systemBusinessLogic.GetAndSaveRegisterSettings();

                    await VerifyPeripheralsConnected();

                    StartFreezeTimer();
                    NavigateService.Instance.RedirectToHome();
                }
                catch (InternalServerException ex)
                {
                    ShowNotification(ex.Error.Message, ShutDownApplication, ShutDownApplication, ApplicationConstants.ButtonWarningColor);
                }
                catch (ApiDataException ex)
                {
                    ShowNotification(
                        ex.Error.Message,
                        ex.Error.ShutDownPos ? ShutDownApplication : (Action)null,
                        ex.Error.ShutDownPos ? ShutDownApplication : (Action)null,
                        ApplicationConstants.ButtonWarningColor);
                }
                finally
                {
                    var endTime = DateTime.Now;
                    LoginStartTime = DateTime.Now;
                    OperationsCompletedInLogin++;
                    //_log.Info(string.Format("Time Taken In Login (Get All Policies) is {0}ms", (endTime - startTime).TotalMilliseconds));
                    MessengerInstance.Send(new CloseKeyboardMessage());
                }
            });

        }

        private void GetTills()
        {
            var startTime = DateTime.Now;
            _log.Info("GetTills method call started");
            bool isUserNameAndPasswordNotEmpty = false;

            SaveUsernameAndPassword(ref isUserNameAndPasswordNotEmpty);

            if (isUserNameAndPasswordNotEmpty)
            {
                PopulateTillsAsync();
            }
            var endTime = DateTime.Now;
            _log.Info(string.Format("GetTills method call ended in {0}ms",
                (endTime - startTime).TotalMilliseconds));
        }

        public void PasswordCompleted(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                GetTills();
            }
        }

        private void TillFloatGotFocus(dynamic args)
        {
            args.OriginalSource.SelectAll();
        }

        private void SaveUsernameAndPassword(ref bool isUserNameAndPasswordNotEmpty)
        {
            if (!string.IsNullOrEmpty(LoginModel.UserName) &&
                  !string.IsNullOrEmpty(LoginModel.Password))
            {
                CacheBusinessLogic.UserName = LoginModel.UserName;
                CacheBusinessLogic.Password = LoginModel.Password;
                isUserNameAndPasswordNotEmpty = true;
            }
        }

        internal void ResetVM()
        {
            SelectedTillIndex = -1;
        }
    }
}
