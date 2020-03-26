using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.Model.FuelPump;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions.PropaneGrade
{
    public class PropaneGradeVM : VMBase
    {
        private int _selectedPumpIndex;
        private int _gradeId;
        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;
        private readonly ICashBusinessLogic _cashBussinessLogic;
        private ObservableCollection<PropaneModel> _pumps;
        private string _volume;
        private string _amount;
        private bool _isAcceptEnabled;
        private ObservableCollection<CashButtonModel> _savedCashButtons;
        private ObservableCollection<CashButtonModel> _cashButtons;
        private bool _isAmountEditTrue;
        private string _volumeString;
        private bool _isVolumeStringVisible;

        private bool _isEditingAmount;

        public bool IsVolumeStringVisible
        {
            get { return _isVolumeStringVisible; }
            set
            {
                if (_isVolumeStringVisible != value)
                {
                    _isVolumeStringVisible = value;
                    RaisePropertyChanged(nameof(IsVolumeStringVisible));
                }
            }
        }

        public string VolumeString
        {
            get { return _volumeString; }
            set
            {
                if (value != _volumeString)
                {
                    _volumeString = string.IsNullOrEmpty(value) ? string.Empty : value + "L";
                    RaisePropertyChanged(nameof(VolumeString));
                }
            }
        }


        public bool IsAmountEditTrue
        {
            get { return _isAmountEditTrue; }
            set
            {
                _isAmountEditTrue = value;
                RaisePropertyChanged(nameof(IsAmountEditTrue));
            }
        }


        public ObservableCollection<CashButtonModel> CashButtons
        {
            get { return _cashButtons; }
            set
            {
                _cashButtons = value;
                RaisePropertyChanged(nameof(CashButtons));
            }
        }

        public bool IsAcceptEnabled
        {
            get { return _isAcceptEnabled; }
            set
            {
                if (_isAcceptEnabled != value)
                {
                    _isAcceptEnabled = value;
                    RaisePropertyChanged(nameof(IsAcceptEnabled));
                }
            }
        }


        public string Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    _amount = Helper.SelectAllDecimalValue(value, _amount);
                    _volume = string.IsNullOrEmpty(_amount) ? _volume : string.Empty;
                    IsAcceptEnabled = (!string.IsNullOrEmpty(_amount) || !string.IsNullOrEmpty(_volume)) &&
                        SelectedPumpIndex >= 0;

                    IsVolumeStringVisible = !string.IsNullOrEmpty(Amount);
                    RaisePropertyChanged(nameof(Amount));
                    RaisePropertyChanged(nameof(Volume));
                }
            }
        }


        public string Volume
        {
            get { return _volume; }
            set
            {
                if (value != _volume)
                {
                    _volume = Helper.SelectAllDecimalValue(value, _volume);
                    _amount = string.IsNullOrEmpty(_volume) ? _amount : string.Empty;
                    IsAcceptEnabled = (!string.IsNullOrEmpty(_amount) || !string.IsNullOrEmpty(_volume)) &&
                        SelectedPumpIndex >= 0;
                    RaisePropertyChanged(nameof(Volume));
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }


        public ObservableCollection<PropaneModel> Pumps
        {
            get { return _pumps; }
            set
            {
                if (_pumps != value)
                {
                    _pumps = value;
                    RaisePropertyChanged(nameof(Pumps));
                }
            }
        }

        public RelayCommand AddPropaneCommand { get; set; }
        public RelayCommand<object> SetAmountCommand { get; set; }
        public RelayCommand NumberPadClearCommand { get; set; }
        public RelayCommand AmountGotFocusCommand { get; set; }
        public RelayCommand VolumeGotFocusCommand { get; set; }
        public RelayCommand<object> VolumeEnteredCommand { get; set; }

        public int SelectedPumpIndex
        {
            get { return _selectedPumpIndex; }
            set
            {
                if (_selectedPumpIndex != value)
                {
                    _selectedPumpIndex = value;
                    RaisePropertyChanged(nameof(SelectedPumpIndex));
                    IsAcceptEnabled = (!string.IsNullOrEmpty(_amount) || !string.IsNullOrEmpty(_volume)) &&
                        SelectedPumpIndex >= 0;
                }
            }
        }

        public PropaneGradeVM(IFuelPumpBusinessLogic fuelPumpBusinessLogic,
            ICashBusinessLogic cashBussinessLogic)
        {
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            _cashBussinessLogic = cashBussinessLogic;
            InitializeCommand();
            UnRegisterMessage();
            RegisterMessages();
            CashButtons = new ObservableCollection<CashButtonModel>();
            _savedCashButtons = new ObservableCollection<CashButtonModel>();
            PerformAction(GetCashButtons);
        }

        private async Task GetCashButtons()
        {
            var response = await _cashBussinessLogic.GetCashButtons();
            _savedCashButtons.Clear();
            foreach (var button in response)
            {
                _savedCashButtons.Add(new CashButtonModel
                {
                    Button = button.Button,
                    Value = button.Value
                });
            }
        }

        private void UnRegisterMessage()
        {
            MessengerInstance.Unregister<string>(this, "SetGradeId", SetGradeId);
            MessengerInstance.Unregister<string>(this, "CurrencyTapped", CurrencyTapped);
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<string>(this, "SetGradeId", SetGradeId);
            MessengerInstance.Register<string>(this, "CurrencyTapped", CurrencyTapped);
        }

        private void SetGradeId(string gradeId)
        {
            _gradeId = Convert.ToInt32(gradeId, CultureInfo.InvariantCulture);
            LoadPropanePumps();
        }

        private void InitializeCommand()
        {
            AddPropaneCommand = new RelayCommand(AddPropane);

            AmountGotFocusCommand = new RelayCommand(() =>
            {
                CashButtons = new ObservableCollection<CashButtonModel>(_savedCashButtons);
                _isEditingAmount = true;
                IsAmountEditTrue = false;
                NavigateService.Instance.NavigateToPropaneGradeAmountNumberPad();
                MessengerInstance.Send(true, "ResetNumberPadVM");
                MessengerInstance.Send(Amount.ToString(), "SetQuantiyUsingNumberPad");
            });

            VolumeGotFocusCommand = new RelayCommand(() =>
            {
                CashButtons = null;
                _isEditingAmount = false;
                IsAmountEditTrue = false;
                NavigateService.Instance.NavigateToPropaneGradeAmountNumberPad();
                MessengerInstance.Send(true, "ResetNumberPadVM");
                MessengerInstance.Send(Volume.ToString(), "SetQuantiyUsingNumberPad");
            });

            SetAmountCommand = new RelayCommand<object>((s) => GetFuelVolume(s.ToString(), false));
            NumberPadClearCommand = new RelayCommand(ResetNumberPadValues);
            VolumeEnteredCommand = new RelayCommand<object>((args) => VolumeEntered(args));
        }

        private void AddPropane()
        {
            PerformAction(async () =>
            {
                try
                {
                    var amount = string.IsNullOrEmpty(Amount) ? Volume : Amount;
                    var selectedPumpId = Pumps.ElementAt(SelectedPumpIndex).Id;
                    var response = await _fuelPumpBusinessLogic.AddPropane(_gradeId, selectedPumpId, amount,
                         !string.IsNullOrEmpty(Amount));
                    MessengerInstance.Send(response.ToModel(), "UpdateSale");
                    NavigateService.Instance.NavigateToHome();
                }
                catch (Exception)
                {
                    _volume = _amount = string.Empty;
                    VolumeString = null;
                    RaisePropertyChanged(nameof(Volume));
                    RaisePropertyChanged(nameof(Amount));
                    throw;
                }
            });
        }

        private void VolumeEntered(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                MessengerInstance.Send(new CloseKeyboardMessage());
                AddPropane();
            }
        }

        private void ResetNumberPadValues()
        {
            NavigateService.Instance.ClearSecondFrame();
            IsAmountEditTrue = true;
        }

        private void CurrencyTapped(string currencyValue)
        {
            if (NavigateService.Instance.SecondFrame.Content.GetType().Name == "AmountNumberPad")
            {
                var amount = 0M;
                var currency = 0M;
                decimal.TryParse(Amount, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
                decimal.TryParse(currencyValue, NumberStyles.Any, CultureInfo.InvariantCulture, out currency);

                amount += currency;

                GetFuelVolume(amount.ToString(CultureInfo.InvariantCulture), true);
            }
        }

        private void GetFuelVolume(string currencyValue, bool isCashButtonPressed)
        {
            if (_isEditingAmount)
            {
                PerformAction(async () =>
                {
                    var selectedPumpId = Pumps.ElementAt(SelectedPumpIndex).Id;
                    if (!isCashButtonPressed)
                    {
                        VolumeString = await _fuelPumpBusinessLogic.GetFuelVolume(_gradeId, selectedPumpId, currencyValue);
                    }
                    Amount = currencyValue;
                    if (!isCashButtonPressed)
                    {
                        ResetNumberPadValues();
                    }
                });
            }
            else
            {
                Volume = currencyValue;
                ResetNumberPadValues();
                AddPropane();
            }
        }

        private void LoadPropanePumps()
        {
            PerformAction(async () =>
            {
                var response = await _fuelPumpBusinessLogic.LoadPropanePumps(_gradeId);
                if (response?.Count > 0)
                {
                    var pumps = (from i in response
                                 select new PropaneModel
                                 {
                                     Id = i.Id,
                                     Name = i.Name,
                                     PositionId = i.PositionId
                                 }).ToList();

                    Pumps = new ObservableCollection<PropaneModel>(pumps);

                    if (Pumps.Count > 0)
                    {
                        SelectedPumpIndex = 0;
                    }
                }
            });
        }

        public void ResetVM()
        {
            IsAmountEditTrue = true;
            SelectedPumpIndex = -1;
            Pumps = new ObservableCollection<PropaneModel>();
            IsAcceptEnabled = false;
            _volume = _amount = string.Empty;
            VolumeString = null;
        }
    }
}
