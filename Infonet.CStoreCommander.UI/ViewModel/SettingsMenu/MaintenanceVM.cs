using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.View.Settings.MaintenanceOptions;
using System;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.SettingsMenu
{
    public class MaintenanceVM : VMBase
    {
        private readonly InfonetLog _log;
        private string _selectedTab;
        private bool _isPrePayOn;
        private bool _isPostPayOn;
        private bool _isPayAtPumpOn;
        private readonly IMaintenanceBussinessLogic _maintenanceBussinessLogic;
        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;

        private bool _isUpdatedByUser = true;

        public bool IsPayAtPumpOn
        {
            get { return _isPayAtPumpOn; }
            set
            {
                if (value != IsPayAtPumpOn)
                {
                    CacheBusinessLogic.IsPayAtPumpOn = _isPayAtPumpOn = value;
                    RaisePropertyChanged(nameof(IsPayAtPumpOn));
                }
            }
        }

        public bool IsPostPayOn
        {
            get { return _isPostPayOn; }
            set
            {
                if (value != _isPostPayOn)
                {
                    CacheBusinessLogic.IsPostPayOn = _isPostPayOn = value;
                    RaisePropertyChanged(nameof(IsPostPayOn));
                }
            }
        }

        public bool IsPrepayOn
        {
            get { return _isPrePayOn; }
            set
            {
                if (value != _isPrePayOn)
                {
                    CacheBusinessLogic.IsPrePayOn = _isPrePayOn = value;
                    RaisePropertyChanged(nameof(IsPrepayOn));
                    PerformActionWithoutLoader(async () =>
                    {
                        var response = await _fuelPumpBusinessLogic.InitializeFuelPump(true, CacheBusinessLogic.TillNumberForSale);
                        MessengerInstance.Send(response);
                    }, false);
                }
            }
        }

        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));
            }
        }

        public RelayCommand LoginTabPressedCommand { get; private set; }
        public RelayCommand InitializeCommand { get; private set; }
        public RelayCommand<object> PrepaySwitchedCommand { get; set; }
        public RelayCommand<object> PostpaySwitchedCommand { get; set; }

        internal void ResetVM()
        {
            SelectedTab = MaintenanceTabs.ServicePump.ToString();
            IsPrepayOn = CacheBusinessLogic.IsPrePayOn;
            IsPostPayOn = CacheBusinessLogic.IsPostPayOn;
            IsPayAtPumpOn = CacheBusinessLogic.IsPayAtPumpOn;
            _isUpdatedByUser = true;
        }

        public RelayCommand ServicePumpTabPressedCommand { get; private set; }
        public RelayCommand BankingTabPressedCommand { get; private set; }
        public RelayCommand PolicyTabPressedCommand { get; private set; }
        public RelayCommand RefreshPoliciesCommand { get; private set; }
        public RelayCommand CloseBatchCommand { get; private set; }

        public MaintenanceVM(IMaintenanceBussinessLogic maintenanceBussinessLogic,
            IFuelPumpBusinessLogic fuelPumpBusinessLogic)
        {
            _log = InfonetLogManager.GetLogger<MaintenanceVM>();
            InitializeCommands();
            _maintenanceBussinessLogic = maintenanceBussinessLogic;
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<bool>(this, "SetPostPay", SetPostPayAfterAuthorization);
        }

        private void SetPostPayAfterAuthorization(bool obj)
        {
            PerformAction(async () =>
            {
                try
                {
                    await SetPostpay(obj);
                    MessengerInstance.Send(false, "LoadAllPolicies");
                }
                finally
                {
                    CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                    CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                }
            });
        }

        private void InitializeCommands()
        {
            RefreshPoliciesCommand = new RelayCommand(() =>
            {
                MessengerInstance.Send(true, "LoadAllPolicies");
            });
            LoginTabPressedCommand = new RelayCommand(LoginTabPressed);
            ServicePumpTabPressedCommand = new RelayCommand(ServicePumpTabPressed);
            BankingTabPressedCommand = new RelayCommand(BankingTabPressed);
            PolicyTabPressedCommand = new RelayCommand(PolicyTabPressed);
            CloseBatchCommand = new RelayCommand(() => PerformAction(CloseBatch));
            InitializeCommand = new RelayCommand(() => PerformAction(Initialize));
            PrepaySwitchedCommand = new RelayCommand<object>((args) => SetPrepay(args));
            PostpaySwitchedCommand = new RelayCommand<object>((args) => PerformAction(async () => { await SetPostpay(args); }));
        }

        private void SetPrepay(dynamic args)
        {
            PerformAction(async () =>
            {
                try
                {
                    if (_isUpdatedByUser)
                    {
                        _isUpdatedByUser = false;
                        var response = await _maintenanceBussinessLogic.SetPrepayOrPostPay(!args, true);
                        CacheBusinessLogic.IsPrePayOn = IsPrepayOn = !args;
                    }
                }
                catch (Exception)
                {
                    CacheBusinessLogic.IsPrePayOn = IsPrepayOn = args;
                    throw;
                }
                finally
                {
                    _isUpdatedByUser = true;
                }
            });
        }

        private async Task SetPostpay(dynamic value)
        {
            try
            {
                if (_isUpdatedByUser)
                {
                    _isUpdatedByUser = false;
                    var response = await _maintenanceBussinessLogic.SetPrepayOrPostPay(!value, false);
                    CacheBusinessLogic.IsPostPayOn = IsPostPayOn = !value;
                }
            }
            catch (SwitchUserException ex)
            {
                ShowNotification(ex.Error.Message,
                    () =>
                    {
                        CacheBusinessLogic.FramePriorSwitchUserNavigation = "PostPayMaintenance";
                        NavigateService.Instance.NavigateToLogout();
                        MessengerInstance.Send(new SetPostPayMessage
                        {
                            Value = value
                        });
                    },
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception)
            {
                CacheBusinessLogic.IsPostPayOn = IsPostPayOn = value;
                throw;
            }
            finally
            {
                _isUpdatedByUser = true;
            }
        }

        private async Task Initialize()
        {
            var response = await _maintenanceBussinessLogic.Initialize();
            if (!string.IsNullOrEmpty(response.Message))
            {
                ShowNotification(ApplicationConstants.InitializeMessage,
               () =>
               {
                   ShowInitializeMessage(response.Message);
               },
               () =>
                {
                    ShowInitializeMessage(response.Message);
                },
                ApplicationConstants.ButtonWarningColor);
            }
        }

        private void ShowInitializeMessage(string message)
        {
            ShowNotification(message,
                   NavigateService.Instance.NavigateToHome,
                   NavigateService.Instance.NavigateToHome,
                   ApplicationConstants.ButtonWarningColor);
        }

        private async Task CloseBatch()
        {
            var response = await _maintenanceBussinessLogic.CloseBatch();

            await PerformPrint(response);
        }

        private void LoginTabPressed()
        {
            SelectedTab = MaintenanceTabs.Login.ToString();
            NavigateService.Instance.NavigateMaintenanceFrame(typeof(SwitchUserOrChangePassword));
        }

        private void BankingTabPressed()
        {
            SelectedTab = MaintenanceTabs.Banking.ToString();
            NavigateService.Instance.NavigateMaintenanceFrame(typeof(Banking));
        }

        private void ServicePumpTabPressed()
        {
            SelectedTab = MaintenanceTabs.ServicePump.ToString();
            NavigateService.Instance.NavigateMaintenanceFrame(typeof(ServicePump));
        }

        private void PolicyTabPressed()
        {
            SelectedTab = MaintenanceTabs.Policy.ToString();
            NavigateService.Instance.NavigateMaintenanceFrame(typeof(Policy));
        }

    }
}
