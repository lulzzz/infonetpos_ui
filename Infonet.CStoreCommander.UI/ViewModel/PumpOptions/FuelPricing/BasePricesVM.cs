using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.FuelPump;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing
{
    public class BasePricesVM : VMBase
    {
        private FuelPricesModel _fuelPrices;
        private FuelPriceModel _selectedPrice;
        private string _caption;
        private string _caption2;
        private bool _isGrouped;
        private bool _focusOnNewRow;

        #region State saving variables while prices are saved
        private bool _isReadTotalizerEnabled;
        private bool _isPricesToDisplayEnabled;
        private bool _isReadTankDipEnabled;
        private bool _canReadTotalizer;
        private bool _canSelectPricesToDisplay;
        private bool _isExitEnabled;
        private bool _isCashPriceEnabled;
        private bool _isCreditPriceEnabled;
        private bool _isTaxExemptedCashPriceEnabled;
        private bool _isTaxExemptedCreditPriceEnabled;
        #endregion

        private readonly IFuelPumpBusinessLogic _fuelPumpBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        public RelayCommand LoadPricesCommand { get; private set; }
        public RelayCommand ReadTotalizerCommand { get; private set; }
        public RelayCommand<FuelPriceModel> SetPriceCommand { get; private set; }
        public RelayCommand SavePricesCommand { get; private set; }
        public RelayCommand PrintCommand { get; private set; }

        public FuelPricesModel FuelPrices
        {
            get
            {
                return _fuelPrices;
            }
            set
            {
                if (_fuelPrices != value)
                {
                    _fuelPrices = value;
                    RaisePropertyChanged(nameof(FuelPrices));
                }
            }
        }

        public bool IsGrouped
        {
            get
            {
                return _isGrouped;
            }
            set
            {
                if (_isGrouped != value)
                {
                    _isGrouped = value;
                    RaisePropertyChanged(nameof(IsGrouped));
                }
            }
        }

        public FuelPriceModel SelectedPrice
        {
            get
            {
                return _selectedPrice;
            }
            set
            {
                if (_selectedPrice != value)
                {
                    _selectedPrice = value;
                    RaisePropertyChanged(nameof(SelectedPrice));
                }
            }
        }

        public bool FocusOnNewRow
        {
            get
            {
                return _focusOnNewRow;
            }
            set
            {
                _focusOnNewRow = value;
                RaisePropertyChanged(nameof(FocusOnNewRow));
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    RaisePropertyChanged(nameof(Caption));
                }
            }
        }

        public string Caption2
        {
            get { return _caption2; }
            set
            {
                if (_caption2 != value)
                {
                    _caption2 = value;
                    RaisePropertyChanged(nameof(Caption2));
                }
            }
        }

        public BasePricesVM(IFuelPumpBusinessLogic fuelPumpBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _fuelPumpBusinessLogic = fuelPumpBusinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
            InitializeCommands();
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<FuelPrices>(this, CompleteSaveFuelPrices);
            MessengerInstance.Register<FuelPriceReportMessage>(this, UpdateReport);
        }

        private void UpdateReport(FuelPriceReportMessage obj)
        {
            FuelPrices.Report = obj?.Report;
        }

        private async void CompleteSaveFuelPrices(FuelPrices prices)
        {
            FuelPrices = prices.ToModel();
            await SavePrices();
            CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
            CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
            MessengerInstance.Send(false, "LoadAllPolicies");
        }

        private void InitializeCommands()
        {
            LoadPricesCommand = new RelayCommand(() => PerformAction(LoadPrices));
            ReadTotalizerCommand = new RelayCommand(() => PerformAction(ReadTotalizer));
            SetPriceCommand = new RelayCommand<FuelPriceModel>(SetPrice);
            SavePricesCommand = new RelayCommand(() => { SavePrices(); });
            PrintCommand = new RelayCommand(Print);
        }

        private async void Print()
        {
            if (CacheBusinessLogic.AreFuelPricesSaved)
            {
                var report = await _reportsBusinessLogic.GetReceipt(ReportType.PriceFile);
                PerformPrint(report);
            }
        }

        private async Task SavePrices()
        {
            try
            {
                if (IsGrouped)
                {
                    var success = await _fuelPumpBusinessLogic.VerifyGroupBasePrices(FuelPrices.ToEntity());
                    if (success)
                    {
                        // Don't need to wait on Fuel prices page
                        if (!CacheBusinessLogic.StayOnFuelPricePage)
                        {
                            NavigateService.Instance.NavigateToHome();
                            if (CacheBusinessLogic.IsFuelOnlySystem)
                            {
                                MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
                            }
                            SaveBasePrices();
                        }
                        else
                        {
                            await SaveBasePrices();
                        }
                    }
                }
                else
                {
                    var success = await _fuelPumpBusinessLogic.VerifyBasePrices(FuelPrices.ToEntity());
                    if (success)
                    {
                        await SaveBasePrices();
                    }
                }
            }
            catch (PumpsOfflineException ex)
            {
                _log.Info(ex.Message, ex);
                ShowConfirmationMessage(ex.Message,
                    async () => { await SaveBasePrices(); });
            }
            catch (SwitchUserException ex)
            {
                _log.Info(ex.Message, ex);
                ShowNotification(ex.Error.Message,
                    SwitchUserAndSavePrices,
                    null,
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (ApiDataException ex)
            {
                _log.Info(ex.Message, ex);
                ShowNotification(ex.Message, null, null, ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
            }
        }

        private async Task SaveBasePrices()
        {
            try
            {
                DisableControls();
                Caption = ApplicationConstants.SettingInProgress;
                ErrorMessageWithCaption response;
                if (IsGrouped)
                {
                    response = await _fuelPumpBusinessLogic.SaveGroupBasePrices(FuelPrices.ToEntity());
                }
                else
                {
                    response = await _fuelPumpBusinessLogic.SaveBasePrices(FuelPrices.ToEntity());
                }
                PrepareCaptions(response);

                await PerformPrint(new List<Report> { response.PriceReport, response.FuelPriceReport });
            }
            // Catching here to reset Captions in case when any popus is generated
            catch (Exception)
            {
                Caption = Caption2 = string.Empty;
                EnableControls();
                throw;
            }
        }

        private void EnableControls()
        {
            FuelPrices.IsReadTotalizerEnabled = _isReadTotalizerEnabled;
            FuelPrices.IsPricesToDisplayEnabled = _isPricesToDisplayEnabled;
            FuelPrices.IsReadTankDipEnabled = _isReadTankDipEnabled;
            FuelPrices.CanReadTotalizer = _canReadTotalizer;
            FuelPrices.CanSelectPricesToDisplay = _canSelectPricesToDisplay;
            FuelPrices.IsExitEnabled = _isExitEnabled;
            FuelPrices.IsCashPriceEnabled = _isCashPriceEnabled;
            FuelPrices.IsCreditPriceEnabled = _isCreditPriceEnabled;
            FuelPrices.IsTaxExemptedCashPriceEnabled = _isTaxExemptedCashPriceEnabled;
            FuelPrices.IsTaxExemptedCreditPriceEnabled = _isTaxExemptedCreditPriceEnabled;
        }

        private void DisableControls()
        {
            _isReadTotalizerEnabled = FuelPrices.IsReadTotalizerEnabled;
            FuelPrices.IsReadTotalizerEnabled = false;
            _isPricesToDisplayEnabled = FuelPrices.IsPricesToDisplayEnabled;
            FuelPrices.IsPricesToDisplayEnabled = false;
            _isReadTankDipEnabled = FuelPrices.IsReadTankDipEnabled;
            FuelPrices.IsReadTankDipEnabled = false;
            _canReadTotalizer = FuelPrices.CanReadTotalizer;
            FuelPrices.CanReadTotalizer = false;
            _canSelectPricesToDisplay = FuelPrices.CanSelectPricesToDisplay;
            FuelPrices.CanSelectPricesToDisplay = false;
            _isExitEnabled = FuelPrices.IsExitEnabled;
            FuelPrices.IsExitEnabled = false;
            _isCashPriceEnabled = FuelPrices.IsCashPriceEnabled;
            FuelPrices.IsCashPriceEnabled = false;
            _isCreditPriceEnabled = FuelPrices.IsCreditPriceEnabled;
            FuelPrices.IsCreditPriceEnabled = false;
            _isTaxExemptedCashPriceEnabled = FuelPrices.IsTaxExemptedCashPriceEnabled;
            FuelPrices.IsTaxExemptedCashPriceEnabled = false;
            _isTaxExemptedCreditPriceEnabled = FuelPrices.IsTaxExemptedCreditPriceEnabled;
            FuelPrices.IsTaxExemptedCreditPriceEnabled = false;
        }

        private void PrepareCaptions(ErrorMessageWithCaption response)
        {
            if (string.IsNullOrEmpty(response?.Caption) && response?.Caption?.IndexOf(";") == -1)
            {
                Caption = Caption2 = string.Empty;
            }
            else
            {
                var captions = response?.Caption?.Split(';');
                Caption = captions[0];
                Caption2 = captions.Length > 1 ? captions[1] : string.Empty;
            }

            if (!string.IsNullOrEmpty(response?.Error?.Message))
            {
                ShowNotification(response.Error.Message, null, null,
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void SetPrice(FuelPriceModel fuelPrice)
        {
            if (fuelPrice == null)
            {
                return;
            }

            try
            {
                Convert.ToDecimal(fuelPrice.CashPrice, CultureInfo.InvariantCulture);
                Convert.ToDecimal(fuelPrice.CreditPrice, CultureInfo.InvariantCulture);
                if (FuelPrices.IsTaxExemptionVisible)
                {
                    if (fuelPrice.TaxExemptedCashPrice != "N/A")
                    {
                        Convert.ToDecimal(fuelPrice.TaxExemptedCashPrice, CultureInfo.InvariantCulture);
                    }
                    if (fuelPrice.TaxExemptedCreditPrice != "N/A")
                    {
                        Convert.ToDecimal(fuelPrice.TaxExemptedCreditPrice, CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                // Eating exception when prices are not in correct format
                return;
            }

            PerformAction(async () =>
            {
                if (IsGrouped)
                {
                    var reponse = await _fuelPumpBusinessLogic.SetGroupBasePrice(FuelPrices.ToEntity().Prices, fuelPrice.Row);
                    FuelPrices.Prices = reponse.ToModel().Prices;
                    FuelPrices.Report = reponse?.Report.ReportContent;
                }
                else
                {
                    var reponse = await _fuelPumpBusinessLogic.SetBasePrice(fuelPrice.ToEntity());
                    var model = reponse?.ToModel();
                    var price = FuelPrices.Prices.FirstOrDefault(x => x.GradeId == model?.GradeId &&
                        x.TierId == model?.TierId && x.LevelId == model?.LevelId);
                    var index = FuelPrices.Prices.IndexOf(price);
                    if (index != -1)
                    {
                        FuelPrices.Prices[index] = model;
                    }
                }
            });
        }

        private async Task ReadTotalizer()
        {
            var success = await _fuelPumpBusinessLogic.ReadTotalizer();
        }

        private async Task LoadPrices()
        {
            if (IsGrouped && !LoadFuelPrices)
            {
                return;
            }
            try
            {
                var prices = await _fuelPumpBusinessLogic.LoadPrices(IsGrouped);
                prices.IsGrouped = IsGrouped;
                FuelPrices = prices.ToModel();
                Caption = prices.Caption;
            }
            catch (ApiDataException ex)
            {
                ShowNotification(ex.Message,
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
                    },
                    ApplicationConstants.ButtonWarningColor);
            }
        }

        private void SwitchUserAndSavePrices()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "SaveFuelPrices";
            NavigateService.Instance.NavigateToLogout();
            MessengerInstance.Send(new SaveFuelPricesMessage
            {
                FuelPrices = FuelPrices.ToEntity()
            });
        }

        internal void ReInitialize()
        {
            IsGrouped = CacheBusinessLogic.IsFuelPricingGrouped;
            Caption = string.Empty;
            Caption2 = string.Empty;
            if (!(IsGrouped && !LoadFuelPrices))
            {
                FuelPrices = null;
            }
        }
    }
}
