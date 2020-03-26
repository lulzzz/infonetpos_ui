using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions
{
    public class PrepayVM : VMBase
    {
        private ObservableCollection<string> _gradeTypes;
        private int _selectedGradeIndex;
        private bool _isSetPrepayVisible;
        private bool _isSwitchprepayVisible;
        private IFuelPumpBusinessLogic _fuelBusinessLogic;
        private int _pumpId;
        private string _title;
        private string _amount;
        private ObservableCollection<string> _cashTypes;
        private int _selectedCashIndex;
        private bool _isSetPrepayEnable;
        private bool _isSwitchPrepayEnable;
        private ObservableCollection<string> _pumpList;
        private int _selectedPumpNumberIndex;
        private bool _isPumpListVisible;
        private bool _isGradeVisible;
        private bool _isAmountVisible;
        private bool _isCashTypeEnable;
        private bool _isKeyPadVisible;
        private ObservableCollection<Model.Cash.CashButtonModel> _cashButtons;

        public bool IsCashTypeEnable
        {
            get { return _isCashTypeEnable; }
            set
            {
                if (value != _isCashTypeEnable)
                {
                    _isCashTypeEnable = value;
                    RaisePropertyChanged(nameof(IsCashTypeEnable));
                }
            }
        }


        public bool IsAmountVisible
        {
            get { return _isAmountVisible; }
            set
            {
                if (_isAmountVisible != value)
                {
                    _isAmountVisible = value;
                    RaisePropertyChanged(nameof(IsAmountVisible));
                }
            }
        }


        public bool IsGradeVisible
        {
            get { return _isGradeVisible; }
            set
            {
                if (value != _isGradeVisible)
                {
                    _isGradeVisible = value;
                    RaisePropertyChanged(nameof(IsGradeVisible));
                }
            }
        }


        public bool IsPumpListVisible
        {
            get { return _isPumpListVisible; }
            set
            {
                if (value != _isPumpListVisible)
                {
                    _isPumpListVisible = value;
                    RaisePropertyChanged(nameof(IsPumpListVisible));
                }
            }
        }


        public int SelectedPumpNumberIndex
        {
            get { return _selectedPumpNumberIndex; }
            set
            {
                if (_selectedPumpNumberIndex != value)
                {
                    _selectedPumpNumberIndex = value;
                    RaisePropertyChanged(nameof(SelectedPumpNumberIndex));
                    IsPrepayButtonEnabled();
                }
            }
        }


        public ObservableCollection<string> PumpList
        {
            get { return _pumpList; }
            set
            {
                if (_pumpList != value)
                {
                    _pumpList = value;
                    RaisePropertyChanged(nameof(PumpList));
                }
            }
        }


        public bool IsSwitchPrepayEnable
        {
            get { return _isSwitchPrepayEnable; }
            set
            {
                if (_isSwitchPrepayEnable != value)
                {
                    _isSwitchPrepayEnable = value;
                    RaisePropertyChanged(nameof(IsSwitchPrepayEnable));
                }
            }
        }


        public bool IsSetPrepayEnable
        {
            get { return _isSetPrepayEnable; }
            set
            {
                if (value != _isSetPrepayEnable)
                {
                    _isSetPrepayEnable = value;
                    RaisePropertyChanged(nameof(IsSetPrepayEnable));
                }
            }
        }


        public int SelectedCashIndex
        {
            get { return _selectedCashIndex; }
            set
            {
                if (_selectedCashIndex != value)
                {
                    _selectedCashIndex = value;
                    RaisePropertyChanged(nameof(SelectedCashIndex));
                    IsPrepayButtonEnabled();
                }
            }
        }
        public ObservableCollection<string> CashTypes
        {
            get { return _cashTypes; }
            set
            {
                if (_cashTypes != value)
                {
                    _cashTypes = value;
                    RaisePropertyChanged(nameof(CashTypes));
                }
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                    IsPrepayButtonEnabled();
                }
            }
        }
        public string PrepayTitle
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(PrepayTitle));
                }
            }
        }
        public bool IsSwitchPrepayVisible
        {
            get { return _isSwitchprepayVisible; }
            set
            {
                if (_isSwitchprepayVisible != value)
                {
                    _isSwitchprepayVisible = value;
                    RaisePropertyChanged(nameof(IsSwitchPrepayVisible));
                }
            }
        }
        public bool IsSetPrepayVisible
        {
            get { return _isSetPrepayVisible; }
            set
            {
                if (_isSetPrepayVisible != value)
                {
                    _isSetPrepayVisible = value;
                    RaisePropertyChanged(nameof(IsSetPrepayVisible));
                }
            }
        }
        public int SelectedGradeIndex
        {
            get { return _selectedGradeIndex; }
            set
            {
                if (_selectedGradeIndex != value)
                {
                    _selectedGradeIndex = value;
                    RaisePropertyChanged(nameof(SelectedGradeIndex));
                    IsPrepayButtonEnabled();
                }
            }
        }
        public ObservableCollection<string> GradeTypes
        {
            get { return _gradeTypes; }
            set
            {
                if (value != _gradeTypes)
                {
                    _gradeTypes = value;
                    RaisePropertyChanged(nameof(GradeTypes));
                }
            }
        }

        public bool IsKeyPadVisible
        {
            get { return _isKeyPadVisible; }
            set
            {
                if (_isKeyPadVisible != value)
                {
                    _isKeyPadVisible = value;
                    RaisePropertyChanged(nameof(IsKeyPadVisible));
                }
            }
        }

        public ObservableCollection<CashButtonModel> CashButtons
        {
            get { return _cashButtons; }
            set
            {
                if (_cashButtons != value)
                {
                    _cashButtons = value;
                }
            }
        }

        public RelayCommand SetPrepayCommand { get; set; }
        public RelayCommand SwitchPrepayCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand OpenKeyPadCommand { get; set; }
        public RelayCommand HideKeyPadCommand { get; set; }
        public RelayCommand<string> SetAmountCommand { get; set; }

        public PrepayVM(IFuelPumpBusinessLogic fuelBusinessLogic)
        {
            _fuelBusinessLogic = fuelBusinessLogic;
            UnRegisterMessages();
            RegisterMessages();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SetPrepayCommand = new RelayCommand(() => PerformAction(SetPrepay));
            SwitchPrepayCommand = new RelayCommand(() => PerformAction(SwitchPrepay));
            BackCommand = new RelayCommand(PerformHomeNavigation);
            OpenKeyPadCommand = new RelayCommand(() => { IsKeyPadVisible = true; });
            HideKeyPadCommand = new RelayCommand(() => { IsKeyPadVisible = false; });
            SetAmountCommand = new RelayCommand<string>(SetAmount);
        }

        private void SetAmount(string param)
        {
            var amount = 0M;
            decimal.TryParse(param, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
            Amount = amount.ToString(CultureInfo.InvariantCulture);
            IsKeyPadVisible = false;
        }

        private async Task SwitchPrepay()
        {
            var newPumpId = Convert.ToInt32(PumpList.ElementAt(SelectedPumpNumberIndex), CultureInfo.InvariantCulture);
            var response = await _fuelBusinessLogic.SwitchPrepay(_pumpId, newPumpId);
            PerformHomeNavigation();
        }

        private async Task SetPrepay()
        {
            var isAmountCash = false;
            var grade = GradeTypes.ElementAt(SelectedGradeIndex);
            if (!CacheBusinessLogic.SupportCashCreditpricing)
            {
                isAmountCash = true;
            }
            else
            {
                isAmountCash = CashTypes.ElementAt(SelectedCashIndex).Equals("Cash");
            }
            var response = await _fuelBusinessLogic.AddPrepay(_pumpId,
                Amount, grade, isAmountCash);
            MessengerInstance.Send(response.ToModel(), "UpdateSale");
            PerformHomeNavigation();
        }

        private void PerformHomeNavigation()
        {
            NavigateService.Instance.NavigateToHome();
            MessengerInstance.Send<ResetPumpOptionMessage>(null);
            MessengerInstance.Send<EnableFuelOptionButtonMessage>(new EnableFuelOptionButtonMessage
            {
                EnableFuelOptionButton = true
            });
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<PrepayMessage>(this, "SetPrepayMessage", SetPrepayOrSwitchPrepay);
        }

        private void UnRegisterMessages()
        {
            MessengerInstance.Unregister<PrepayMessage>(this, "SetPrepayMessage", SetPrepayOrSwitchPrepay);
        }

        private void SetPrepayOrSwitchPrepay(PrepayMessage prepayMessage)
        {
            _pumpId = prepayMessage.SelectedPumpID;
            if (prepayMessage.IsPrepay)
            {
                PrepayTitle = string.Format(ApplicationConstants.SetPrepayTitle, _pumpId);
                SetControlsForPrepay();
            }
            else
            {
                PrepayTitle = string.Format(ApplicationConstants.SwitchPrepayTitle, _pumpId);
                SetControlsForSwitchPrepay();
                SetNumberOfPumps(prepayMessage.TotalPumps);
            }
            PerformAction(LoadGrades);
        }

        private void SetControlsForPrepay()
        {
            HideSwitchPrepayControls();
            ShowPrepayControls();
        }

        private void SetControlsForSwitchPrepay()
        {
            HidePrepayControls();
            ShowSwitchPrepayControls();
        }

        private void SetNumberOfPumps(int numberOfPumps)
        {
            PumpList.Clear();
            for (int i = 1; i <= numberOfPumps; i++)
            {
                PumpList.Add(i.ToString());
            }

            PumpList.Remove(_pumpId.ToString());
        }

        internal void ResetVM()
        {
            PerformAction(GetCashButtons);
            SelectedPumpNumberIndex = -1;
            SelectedGradeIndex = -1;
            IsKeyPadVisible = false;
            GradeTypes = new ObservableCollection<string>();
            PumpList = new ObservableCollection<string>();
            CashTypes = new ObservableCollection<string>()
            {
                "Cash",
                "Credit"
            };

            SelectedCashIndex = 0;
            Amount = "0.00";
            IsCashTypeEnable = CacheBusinessLogic.SupportCashCreditpricing;
        }

        private async Task LoadGrades()
        {
            var tempGrades = await _fuelBusinessLogic.LoadGrades(_pumpId, true, CacheBusinessLogic.TillNumberForSale);

            GradeTypes = new ObservableCollection<string>(tempGrades);

            if (GradeTypes.Count > 0)
            {
                SelectedGradeIndex = 0;
            }
        }

        private async Task GetCashButtons()
        {
            var cashButtons = await _fuelBusinessLogic.GetPrepayCashButtons();
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

        private void IsPrepayButtonEnabled()
        {

            if (IsSwitchPrepayVisible && SelectedPumpNumberIndex >= 0)
            {
                IsSwitchPrepayEnable = true;
            }
            else
            {
                IsSwitchPrepayEnable = false;
            }

            var amount = 0M;

            if (!string.IsNullOrEmpty(Amount))
            {
                decimal.TryParse(Amount, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
            }

            if (SelectedGradeIndex >= 0 &&
                amount > 0M && IsSetPrepayVisible)
            {
                if (CacheBusinessLogic.SupportCashCreditpricing)
                {
                    IsSetPrepayEnable = SelectedCashIndex >= 0 ? true : false;
                }
                else
                {
                    IsSetPrepayEnable = true;
                }
            }
            else
            {
                IsSetPrepayEnable = false;
            }
        }

        #region Visibility Controllers
        private void HideSwitchPrepayControls()
        {
            IsSwitchPrepayVisible = false;
            IsPumpListVisible = false;
        }

        private void ShowPrepayControls()
        {
            IsSetPrepayVisible = true;
            IsAmountVisible = true;
            IsGradeVisible = true;
        }

        private void ShowSwitchPrepayControls()
        {
            IsSwitchPrepayVisible = true;
            IsPumpListVisible = true;
        }

        private void HidePrepayControls()
        {
            IsSetPrepayVisible = false;
            IsAmountVisible = false;
            IsGradeVisible = false;
        }
        #endregion
    }
}
