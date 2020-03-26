using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing
{
    public class AddManualVM : VMBase
    {
        #region Private Variables
        private bool _isPaymentModeEnabled;
        private List<string> _paymentModes;
        private string _selectedPaymentMode;
        private string _paymentModeHeading;
        private bool _isGradeEnabled;
        private List<string> _grades;
        private string _selectedGrade;
        private bool _isAddFuelEnabled;
        private ObservableCollection<Model.Cash.CashButtonModel> _cashButtons;

        private int _pumpId;
        private decimal _amount;

        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;
        #endregion

        #region Properties
        public bool IsPaymentModeEnabled
        {
            get
            {
                return _isPaymentModeEnabled;
            }
            set
            {
                if (_isPaymentModeEnabled != value)
                {
                    _isPaymentModeEnabled = value && CacheBusinessLogic.SupportCashCreditpricing;
                    RaisePropertyChanged(nameof(IsPaymentModeEnabled));
                }
            }
        }

        public List<string> PaymentModes
        {
            get
            {
                return _paymentModes;
            }
            set
            {
                if (_paymentModes != value)
                {
                    _paymentModes = value;
                    RaisePropertyChanged(nameof(PaymentModes));
                }
            }
        }

        public string SelectedPaymentMode
        {
            get
            {
                return _selectedPaymentMode;
            }
            set
            {
                if (_selectedPaymentMode != value)
                {
                    _selectedPaymentMode = value;
                    ValidateDetails();
                    RaisePropertyChanged(nameof(SelectedPaymentMode));
                }
            }
        }

        public string PaymentModeHeading
        {
            get
            {
                return _paymentModeHeading;
            }
            set
            {
                if (_paymentModeHeading != value)
                {
                    _paymentModeHeading = value;
                    RaisePropertyChanged(nameof(PaymentModeHeading));
                }
            }
        }

        public bool IsGradeEnabled
        {
            get
            {
                return _isGradeEnabled;
            }
            set
            {
                if (_isGradeEnabled != value)
                {
                    _isGradeEnabled = value;
                    RaisePropertyChanged(nameof(IsGradeEnabled));
                }
            }
        }

        public List<string> Grades
        {
            get
            {
                return _grades;
            }
            set
            {
                if (_grades != value)
                {
                    _grades = value;
                    RaisePropertyChanged(nameof(Grades));
                }
            }
        }

        public string SelectedGrade
        {
            get
            {
                return _selectedGrade;
            }
            set
            {
                if (_selectedGrade != value)
                {
                    _selectedGrade = value;
                    ValidateDetails();
                    RaisePropertyChanged(nameof(SelectedGrade));
                }
            }
        }

        public bool IsAddFuelEnabled
        {
            get
            {
                return _isAddFuelEnabled;
            }
            set
            {
                if (_isAddFuelEnabled != value)
                {
                    _isAddFuelEnabled = value;
                    RaisePropertyChanged(nameof(IsAddFuelEnabled));
                }
            }
        }

        public ObservableCollection<CashButtonModel> CashButtons
        {
            get
            {
                return _cashButtons;
            }
            set
            {
                if (_cashButtons != value)
                {
                    _cashButtons = value;
                    RaisePropertyChanged(nameof(CashButtons));
                }
            }
        }
        #endregion

        #region Commands
        public RelayCommand AddFuelCommand { get; private set; }
        public RelayCommand<object> SetAmountCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CheckUserCanPerformManualSalesCommand { get; set; }
        #endregion

        public AddManualVM(IFuelPumpBusinessLogic fuelPumpBusinessLogic)
        {
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            RegisterMessages();
            InitializeCommands();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<FuelManualPumpMessage>(this, LoadGrades);
        }

        private void LoadGrades(FuelManualPumpMessage message)
        {
            PerformAction(async () =>
            {
                _pumpId = message.PumpId;
                UpdatePaymentHeading();
                await LoadGrades();
            });
        }

        private void UpdatePaymentHeading()
        {
            if (_amount != 0)
            {
                PaymentModeHeading = string.Format(ApplicationConstants.ForPumpTwoOption, _pumpId, _amount.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                PaymentModeHeading = string.Format(ApplicationConstants.ForPumpOneOption, _pumpId);
            }
        }

        public void InitializeCommands()
        {
            BackCommand = new RelayCommand(() =>
            {
                // Resetting the auth key in case user was not authorized to perform manual sales
                CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                MessengerInstance.Send(false, "LoadAllPolicies");
                PerformHomeNavigation();
            });
            AddFuelCommand = new RelayCommand(() => PerformAction(AddFuel));
            SetAmountCommand = new RelayCommand<object>(SetAmount);
            CheckUserCanPerformManualSalesCommand = new RelayCommand(CheckUserCanPerformManualSales);
        }

        private void CheckUserCanPerformManualSales()
        {
            if (!CacheBusinessLogic.UserCanPerformManualSales)
            {
                ShowConfirmationMessage(ApplicationConstants.UserCannotPerformManual,
                    SwitchUserAndPerformManualSale,
                    () =>
                    {
                        if (CacheBusinessLogic.IsFuelOnlySystem)
                        {
                            MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
                        }
                        NavigateService.Instance.NavigateToHome();
                    },
                    () =>
                    {
                        if (CacheBusinessLogic.IsFuelOnlySystem)
                        {
                            MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
                        }
                        NavigateService.Instance.NavigateToHome();
                    });
            }
        }

        private void SwitchUserAndPerformManualSale()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "PerformManualSale";
            NavigateService.Instance.NavigateToLogout();
            MessengerInstance.Send(new AddManualFuelMessage
            {
                PumpId = _pumpId
            });
        }

        private void ValidateDetails()
        {
            IsAddFuelEnabled = !string.IsNullOrEmpty(SelectedGrade) && !string.IsNullOrEmpty(SelectedPaymentMode);
        }

        private void SetAmount(object obj)
        {
            _amount = 0;
            decimal.TryParse(obj?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out _amount);
            if (_amount != 0)
            {
                IsGradeEnabled = true;
                IsPaymentModeEnabled = true;
            }
            UpdatePaymentHeading();
        }

        private async Task AddFuel()
        {
            var sale = await _fuelPumpBusinessLogic.AddManually(_pumpId, _amount.ToString(CultureInfo.InvariantCulture),
                (SelectedPaymentMode?.Equals("Cash")).HasValue ? (SelectedPaymentMode?.Equals("Cash")).Value : true,
                SelectedGrade);
            MessengerInstance.Send(sale.ToModel(), "UpdateSale");
            // Resetting the auth key in case user was not authorized to perform manual sales
            CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
            MessengerInstance.Send(false, "LoadAllPolicies");
            PerformHomeNavigation();
        }

        private void PerformHomeNavigation()
        {
            if (CacheBusinessLogic.IsFuelOnlySystem)
            {
                MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
            }
            NavigateService.Instance.NavigateToHome();
            MessengerInstance.Send<ResetPumpOptionMessage>(null);
            MessengerInstance.Send<EnableFuelOptionButtonMessage>(new EnableFuelOptionButtonMessage
            {
                EnableFuelOptionButton = true
            });
        }

        public void ReInitialize()
        {
            IsPaymentModeEnabled = false;
            IsGradeEnabled = false;
            PaymentModes = new List<string> { "Cash", "Credit" };
            PerformAction(GetCashButtons);
            SelectedPaymentMode = "Cash";
            SelectedGrade = string.Empty;
            _amount = 0;
        }

        private async Task LoadGrades()
        {
            Grades = await _fuelPumpBusinessLogic.LoadGrades(_pumpId, false, CacheBusinessLogic.TillNumberForSale);
        }

        private async Task GetCashButtons()
        {
            var cashButtons = await _fuelPumpBusinessLogic.GetManualCashButtons();
            CashButtons?.Clear();
            if (cashButtons?.Count > 0)
            {
                CashButtons = new ObservableCollection<CashButtonModel>(
                    from c in cashButtons
                    select new CashButtonModel
                    {
                        Button = c.Button,
                        Value = c.Value
                    });
            }
        }
    }
}
