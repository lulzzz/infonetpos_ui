using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Stock
{
    public class PriceCheckVM : VMBase
    {
        private bool _showPriceChange;
        private bool _showPriceCheck;
        private ObservableCollection<PriceTypeModel> _productList;
        private readonly IStockBussinessLogic _stockBussinessLogic;
        private string _stockCode;
        private StockPriceModel _stockPriceModel;
        private int _selectedPriceIndex;
        private int _selectedPriceUnitIndex;
        private bool _showDates;
        private bool _showQuantityAndPrice;
        private bool _showPrices;
        private bool _showFromToPrice;
        private bool _isEditButtonEnabled;
        private string _defaultSelectedSpecialPrice;
        private string _column2Header;
        private bool _showOnlyPrices;
        private bool _isStockCodeEnabled;
        private bool _isEditButtonPressed;
        private int _currentpriceTypeIndex = 0;
        private bool _showPriceUnits;
        private bool _showGrids;
        private bool _showEditButton;
        private bool _showSaveButton;

        public bool ShowSaveButton
        {
            get { return _showSaveButton; }
            set
            {
                _showSaveButton = value;
                RaisePropertyChanged(nameof(ShowSaveButton));
            }
        }

        public bool ShowEditButton
        {
            get { return _showEditButton; }
            set
            {
                _showEditButton = value && CacheBusinessLogic.OperatorCanChangePrice;
                RaisePropertyChanged(nameof(ShowEditButton));
            }
        }

        public bool ShowGrids
        {
            get { return _showGrids; }
            set
            {
                _showGrids = value;
                RaisePropertyChanged(nameof(ShowGrids));
            }
        }

        public bool ShowPriceUnits
        {
            get { return _showPriceUnits; }
            set
            {
                _showPriceUnits = value;
                RaisePropertyChanged(nameof(ShowPriceUnits));
            }
        }

        public bool IsEditButtonPressed
        {
            get { return _isEditButtonPressed; }
            set
            {
                _isEditButtonPressed = value;
                RaisePropertyChanged(nameof(IsEditButtonPressed));
            }
        }

        public bool IsStockCodeEnabled
        {
            get { return _isStockCodeEnabled; }
            set
            {
                _isStockCodeEnabled = value;
                RaisePropertyChanged(nameof(IsStockCodeEnabled));
            }
        }

        public bool ShowOnlyPrices
        {
            get { return _showOnlyPrices; }
            set
            {
                _showOnlyPrices = value;
                RaisePropertyChanged(nameof(ShowOnlyPrices));
            }
        }

        public List<string> PriceUnits { get; set; }

        public bool IsEditButtonEnabled
        {
            get { return _isEditButtonEnabled; }
            set
            {
                _isEditButtonEnabled = value;
                RaisePropertyChanged(nameof(IsEditButtonEnabled));
            }
        }

        public bool ShowFromToPrice
        {
            get { return _showFromToPrice; }
            set
            {
                _showFromToPrice = value;
                RaisePropertyChanged(nameof(ShowFromToPrice));
            }
        }

        public bool ShowPrices
        {
            get { return _showPrices; }
            set
            {
                _showPrices = value;
                RaisePropertyChanged(nameof(ShowPrices));
            }
        }

        public bool ShowQuantityAndPrice
        {
            get { return _showQuantityAndPrice; }
            set
            {
                _showQuantityAndPrice = value;
                RaisePropertyChanged(nameof(ShowQuantityAndPrice));
            }
        }

        public bool ShowDates
        {
            get { return _showDates; }
            set
            {
                _showDates = value;
                RaisePropertyChanged(nameof(ShowDates));
            }
        }

        public int SelectedPriceUnitIndex
        {
            get { return _selectedPriceUnitIndex; }
            set
            {
                _selectedPriceUnitIndex = value;
                RaisePropertyChanged(nameof(SelectedPriceUnitIndex));
                ChangeGridColumnHeaderValue();
            }
        }

        public string Column2Header
        {
            get { return _column2Header; }
            set
            {
                _column2Header = value;
                RaisePropertyChanged(nameof(Column2Header));
            }
        }

        public int SelectedPriceIndex
        {
            get { return _selectedPriceIndex; }
            set
            {
                if (_selectedPriceIndex != value)
                {
                    _selectedPriceIndex = value;

                    if (StockPriceModel?.SpecialPriceTypes != null && _selectedPriceIndex > -1)
                    {
                        ControlUIWithSelectedPriceIndex();
                    }
                }
                RaisePropertyChanged(nameof(SelectedPriceIndex));
            }
        }

        public StockPriceModel StockPriceModel
        {
            get { return _stockPriceModel; }
            set
            {
                _stockPriceModel = value;
                RaisePropertyChanged(nameof(StockPriceModel));
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                RaisePropertyChanged(nameof(StockCode));
            }
        }

        public bool ShowPriceCheck
        {
            get { return _showPriceCheck; }
            set
            {
                _showPriceCheck = value;
                RaisePropertyChanged(nameof(ShowPriceCheck));
            }
        }

        public bool ShowPriceChange
        {
            get { return _showPriceChange; }
            set
            {
                _showPriceChange = value;
                RaisePropertyChanged(nameof(ShowPriceChange));
            }
        }

        public RelayCommand PlusCommand { get; set; }
        public RelayCommand ChangePriceCommand { get; set; }
        public RelayCommand<object> MinusCommand { get; set; }
        public RelayCommand<object> SearchByStockCodeCommand { get; set; }
        public RelayCommand CheckPriceCommand { get; set; }
        public RelayCommand EditPriceCommand { get; set; }

        public ObservableCollection<PriceTypeModel> PriceTypeList
        {
            get { return _productList; }
            set
            {
                _productList = value;
                RaisePropertyChanged(nameof(PriceTypeList));
            }
        }

        public PriceCheckVM(IStockBussinessLogic stockBussinessLogic)
        {
            _stockBussinessLogic = stockBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            EditPriceCommand = new RelayCommand(() => EditPrice(true));
            ChangePriceCommand = new RelayCommand(() => PerformAction(PriceChangeAsync));
            PlusCommand = new RelayCommand(AddProduct);
            MinusCommand = new RelayCommand<object>((s) => RemoveProduct(s));
            CheckPriceCommand = new RelayCommand(() => PerformAction(CheckPriceStockCode));
            SearchByStockCodeCommand = new RelayCommand<object>((args) => CheckPriceStockCode(args));
        }

        private void EditPrice(bool isEditButtonPressed)
        {
            ShowEditButton = !isEditButtonPressed;
            ShowSaveButton = isEditButtonPressed;
            IsStockCodeEnabled = !isEditButtonPressed;
            ShowPriceChange = IsEditButtonPressed = ShowPriceUnits = ShowOnlyPrices = isEditButtonPressed;
            ShowPriceCheck = !isEditButtonPressed;
            if ((StockPriceModel?.SpecialPriceTypes.ElementAt(SelectedPriceIndex)).Equals(ApplicationConstants.RegularPriceText))
            {
                ShowPriceUnits = false;
            }
        }

        private bool IsAllFieldsEntered()
        {
            var gridPrices = new List<GridPrices>();

            foreach (var priceType in PriceTypeList)
            {
                if (ShowPrices && string.IsNullOrEmpty(priceType.Column1))
                {
                    ShowNotification(ApplicationConstants.EnterAValue,
                        null, null, ApplicationConstants.ButtonWarningColor);
                    return false;
                }
                else if (ShowQuantityAndPrice && (string.IsNullOrEmpty(priceType.Column1) || string.IsNullOrEmpty(priceType.Column2)))
                {
                    ShowNotification(ApplicationConstants.EnterAValue,
                        null, null, ApplicationConstants.ButtonWarningColor);
                    return false;
                }
                else if (ShowFromToPrice && (string.IsNullOrEmpty(priceType.Column1) ||
                    string.IsNullOrEmpty(priceType.Column2) || string.IsNullOrEmpty(priceType.Column3)))
                {
                    ShowNotification(ApplicationConstants.EnterAValue,
                        null, null, ApplicationConstants.ButtonWarningColor);
                    return false;
                }
            }
            return true;
        }

        private List<GridPrices> GetGridPrices()
        {
            var gridPrices = new List<GridPrices>();

            foreach (var priceType in PriceTypeList)
            {
                gridPrices.Add(new GridPrices
                {
                    Column1 = priceType.Column1,
                    Column2 = priceType.Column2,
                    Column3 = priceType.Column3
                });

            }
            return gridPrices;
        }

        private async Task PriceChangeAsync()
        {
            if (IsAllFieldsEntered())
            {
                if (SelectedPriceIndex >= 0 &&
                      !(StockPriceModel?.SpecialPriceTypes.ElementAt(SelectedPriceIndex)).Equals(ApplicationConstants.RegularPriceText))
                {
                    var ChangePrice = new ChangePrice()
                    {
                        Fromdate = StockPriceModel.FromDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                        Todate = StockPriceModel.ToDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                        StockCode = StockCode,
                        RegisterNumber = CacheBusinessLogic.RegisterNumber,
                        TillNumber = CacheBusinessLogic.TillNumber,
                        SaleNumber = CacheBusinessLogic.SaleNumber,
                        RegularPrice = decimal.Parse(StockPriceModel.RegularPriceText, CultureInfo.InvariantCulture),
                        IsEndDate = StockPriceModel.IsEndDateChecked,
                        PerDollarChecked = PriceUnits.ElementAt(_selectedPriceUnitIndex).Equals("$"),
                        PriceType = StockPriceModel?.SpecialPriceTypes.ElementAt(SelectedPriceIndex),
                        GridPricesContract = GetGridPrices()
                    };

                    var response = await _stockBussinessLogic.EditPrice(ChangePrice, false);

                    WriteToLineDisplay(response.LineDisplay);
                    MapStockPriceModel(response);
                    EditPrice(false);
                }
                else
                {
                    var ChangePrice = new ChangePrice()
                    {
                        StockCode = StockCode,
                        RegisterNumber = CacheBusinessLogic.RegisterNumber,
                        TillNumber = CacheBusinessLogic.TillNumber,
                        SaleNumber = CacheBusinessLogic.SaleNumber,
                        RegularPrice = decimal.Parse(StockPriceModel.RegularPriceText, CultureInfo.InvariantCulture)
                    };

                    var response = await _stockBussinessLogic.EditPrice(ChangePrice, true);
                    WriteToLineDisplay(response.LineDisplay);
                    MapStockPriceModel(response);
                    EditPrice(false);
                }
            }
        }

        private void CheckPriceStockCode(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                PerformAction(CheckPriceStockCode, "txtStockCode");
            }
        }

        private async Task CheckPriceStockCode()
        {
            try
            {
                if (!string.IsNullOrEmpty(StockCode))
                {
                    var response = await _stockBussinessLogic.CheckStockPrice(StockCode);
                    if (response != null)
                    {
                        if (!string.IsNullOrEmpty(response.Message))
                        {
                            ShowNotification(response.Message,
                                null, null, ApplicationConstants.ButtonWarningColor);
                        }
                        WriteToLineDisplay(response.LineDisplay);
                        MapStockPriceModel(response);
                        ShowEditButton = true;
                    }
                }
            }
            catch (Exception)
            {
                ResetVM();
                throw;
            }
        }

        private void MapStockPriceModel(StockPrice response)
        {
            try
            {
                StockPriceModel.SpecialPriceTypes = MapSepcialPrices(response.SpecialPriceTypes);
            }
            catch (Exception)
            {
            }
            finally
            {
                StockPriceModel.Description = string.Format("{0} (#{1})", response.Description, StockCode);
                StockPriceModel.AvailableQuantity = response.AvailableQuantity;
                StockPriceModel.ToDate = string.IsNullOrEmpty(response.ToDate) ? DateTimeOffset.Now :
                    Convert.ToDateTime(response.ToDate, CultureInfo.InvariantCulture);
                StockPriceModel.FromDate = string.IsNullOrEmpty(response.FromDate) ? DateTimeOffset.Now :
                    Convert.ToDateTime(response.FromDate, CultureInfo.InvariantCulture);
                StockPriceModel.RegularPriceText = response.RegularPriceText;
                StockPriceModel.IsAddButtonVisible = response.IsAddButtonVisible && IsEditButtonPressed;
                StockPriceModel.IsChangePriceEnable = response.IsChangePriceEnable;
                StockPriceModel.IsRemoveButtonVisible = response.IsRemoveButtonVisible && IsEditButtonPressed;
                StockPriceModel.IsToDateVisible = response.IsToDateVisible && response.IsSpecialPricingVisible;
                StockPriceModel.TaxExemptPrice = response.TaxExemptPrice;
                StockPriceModel.TaxExemptAvailable = response.TaxExemptAvailable;
                StockPriceModel.IsTaxExemptVisible = response.IsTaxExemptVisible;
                StockPriceModel.VendorId = response.VendorId;
                StockPriceModel.AvailableQuantity = response.AvailableQuantity;
                _defaultSelectedSpecialPrice = StockPriceModel.PriceTypeText = response.PriceTypeText;
                SelectedPriceIndex = string.IsNullOrEmpty(response.PriceTypeText) ? -1 :
                    response.SpecialPriceTypes.IndexOf(response.PriceTypeText);


                ControlUIElementVisibility();

                if (response.XForPrice.Columns > 0)
                {
                    MapPriceTypeModelWithReponse(response.XForPrice);
                }
                else if (response.IncrementalPrice.Columns > 0)
                {
                    MapPriceTypeModelWithReponse(response.IncrementalPrice);
                }
                else if (response.FirstUnitPrice.Columns > 0)
                {
                    MapPriceTypeModelWithReponse(response.FirstUnitPrice);
                }
                else if (response.SalePrice.Columns > 0)
                {
                    MapPriceTypeModelWithReponse(response.SalePrice);
                }
                else
                {
                    ResetPriceTypeList();
                }

                if (response.IsPerDollarChecked)
                {
                    SelectedPriceUnitIndex = PriceUnits.IndexOf("$");
                }
                else if (response.IsPerPercentageChecked)
                {
                    SelectedPriceUnitIndex = PriceUnits.IndexOf("%");
                }
            }
        }

        private void ResetPriceTypeList()
        {
            PriceTypeList.Clear();

            PriceTypeList.Add(new PriceTypeModel
            {
                Id = _currentpriceTypeIndex++
            });
        }

        private void ControlUIElementVisibility()
        {
            IsEditButtonEnabled = ShowOnlyPrices =
             StockPriceModel.IsActiveVendorPrice =
             StockPriceModel.IsAvQtyVisible =
             StockPriceModel.IsPriceVisible =
             StockPriceModel.IsSpecialPricingVisible = true;
        }

        private void ControlUIWithSelectedPriceIndex()
        {
            var dropDownValue = StockPriceModel?.SpecialPriceTypes.ElementAt(SelectedPriceIndex);

            if (dropDownValue.ToUpper() == ApplicationConstants.FirstUnitPrice.ToUpper() || 
                dropDownValue.ToUpper() == ApplicationConstants.XPrice.ToUpper())
            {
                ControlDatesVisibility(true);
                ControlQuantityAndPriceVisibility(true);
                ControlPriceVisibility(false);
                ControlFromToPriceVisibility(false);
                ControlPriceUnitsVisibility(IsEditButtonPressed);
                ControlAddAndMinusButtonVisible(true);
            }
            if (dropDownValue.ToUpper() == ApplicationConstants.IncrementalPrice.ToUpper())
            {
                ControlPriceVisibility(false);
                ControlQuantityAndPriceVisibility(false);
                ControlFromToPriceVisibility(true);
                ControlDatesVisibility(true);
                ControlPriceUnitsVisibility(IsEditButtonPressed);
                ControlAddAndMinusButtonVisible(true);
            }
            if (dropDownValue.ToUpper() == ApplicationConstants.SalePrice.ToUpper())
            {
                ControlDatesVisibility(true);
                ControlPriceVisibility(true);
                ControlFromToPriceVisibility(false);
                ControlQuantityAndPriceVisibility(false);
                ControlPriceUnitsVisibility(IsEditButtonPressed);
                ControlAddAndMinusButtonVisible(false);
            }
            if (dropDownValue.ToUpper() == ApplicationConstants.RegularPrice.ToUpper())
            {
                ControlPriceVisibility(false);
                ControlFromToPriceVisibility(false);
                ControlDatesVisibility(false);
                ControlQuantityAndPriceVisibility(false);
                ControlPriceUnitsVisibility(false);
                ControlAddAndMinusButtonVisible(false);
            }
            ResetPriceTypeList();
        }

        internal void ControlAddAndMinusButtonVisible(bool isAddAndMinusButtonVisible)
        {
            StockPriceModel.IsRemoveButtonVisible =
             StockPriceModel.IsAddButtonVisible = isAddAndMinusButtonVisible;
        }

        private void ControlPriceUnitsVisibility(bool isDateVisible)
        {
            ShowPriceUnits = isDateVisible;
        }

        private void ControlPriceVisibility(bool isPricesVisible)
        {
            ShowPrices = isPricesVisible;
        }

        private void ControlFromToPriceVisibility(bool isFromToPriceVisible)
        {
            ShowFromToPrice = isFromToPriceVisible;
        }

        private void ControlDatesVisibility(bool showdates)
        {
            ShowGrids = ShowDates = showdates;
        }

        private void ControlQuantityAndPriceVisibility(bool isShowQuantityAndPriceVisible)
        {
            ShowQuantityAndPrice = isShowQuantityAndPriceVisible;
        }

        private ObservableCollection<string> MapSepcialPrices(List<string> specialPriceTypes)
        {
            var tempSpecialPrices = new ObservableCollection<string>();
            foreach (var specialPrice in specialPriceTypes)
            {
                tempSpecialPrices.Add(specialPrice);
            }
            return tempSpecialPrices;
        }

        private void ChangeGridColumnHeaderValue()
        {
            if (PriceUnits.ElementAt(SelectedPriceUnitIndex) == "%")
            {
                Column2Header = ApplicationConstants.Percent;
            }
            else
            {
                Column2Header = ApplicationConstants.Prices;
            }
        }

        private void AddProduct()
        {
            PriceTypeList.Add(new PriceTypeModel
            {
                Id = _currentpriceTypeIndex++
            });
        }

        private void RemoveProduct(dynamic s)
        {
            if (PriceTypeList.Count > 0)
            {
                var selectedPriceType = PriceTypeList.First(x => x.Id == s);
                PriceTypeList.Remove(selectedPriceType);
                --_currentpriceTypeIndex;
            }
        }

        private void MapPriceTypeModelWithReponse(dynamic priceTypes)
        {
            PriceTypeList.Clear();
            foreach (var row in priceTypes.CommonGrids)
            {
                var priceType = new PriceTypeModel
                {
                    Id = _currentpriceTypeIndex++,
                    Column1 = row.Column1,
                    Column2 = row.Column2,
                    Column3 = row.Column3
                };
                PriceTypeList.Add(priceType);
            }
        }

        internal void ResetVM()
        {
            IsStockCodeEnabled = true;
            StockCode = string.Empty;
            SelectedPriceIndex = StockPriceModel?.SpecialPriceTypes?.Count > 0 ? 0 : -1;
            PriceUnits = new List<string> { "$", "%" };
            SelectedPriceUnitIndex = 0;
            IsEditButtonEnabled = IsEditButtonPressed = ShowOnlyPrices = ShowPriceChange = false;
            ShowPriceCheck = true;
            _defaultSelectedSpecialPrice = string.Empty;
            ControlDatesVisibility(false);
            ControlQuantityAndPriceVisibility(false);
            ControlPriceVisibility(false);
            ControlFromToPriceVisibility(false);
            StockPriceModel = new StockPriceModel();
            ChangeGridColumnHeaderValue();
            PriceTypeList = new ObservableCollection<PriceTypeModel>();
            ShowEditButton = false;
            ShowSaveButton = false;
        }
    }
}
