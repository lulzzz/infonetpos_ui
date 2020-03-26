using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.Stock
{
    public class StockPriceModel : ViewModelBase
    {
        private string _stockCode;
        private string _description;
        private string _vendorId;
        private string _regularPriceText;
        private bool _isEndDateChecked;
        private string _availableQuantity;
        private ObservableCollection<string> _SpecialPriceTypes;
        private string _priceTypeText;
        private DateTimeOffset _todate;
        private DateTimeOffset _fromDate;
        private bool _isPriceVisible;
        private bool _isAvQtyVisible;
        private bool _isTaxExemptVisible;
        private string _taxExemptPrice;
        private bool _isSpecialPricingVisible;
        private bool _isToDateVisible;
        private bool _isActiveVendorPrice;
        private bool _isPerDollarChecked;
        private bool _isPerPercentageChecked;
        private bool _isAddButtonVisible;
        private bool _isRemoveButtonVisible;
        private bool _isChangePriceEnable;
        private string _taxExemptAvailable;


        public bool IsChangePriceEnable
        {
            get { return _isChangePriceEnable; }
            set
            {
                _isChangePriceEnable = value;
                RaisePropertyChanged(nameof(IsChangePriceEnable));
            }
        }
        public bool IsRemoveButtonVisible
        {
            get { return _isRemoveButtonVisible; }
            set
            {
                _isRemoveButtonVisible = value;
                RaisePropertyChanged(nameof(IsRemoveButtonVisible));
            }
        }
        public bool IsAddButtonVisible
        {
            get { return _isAddButtonVisible; }
            set
            {
                _isAddButtonVisible = value;
                RaisePropertyChanged(nameof(IsAddButtonVisible));
            }
        }
        public bool IsPerPercentageChecked
        {
            get { return _isPerPercentageChecked; }
            set
            {
                _isPerPercentageChecked = value;
                RaisePropertyChanged(nameof(IsPerPercentageChecked));
            }
        }
        public bool IsPerDollarChecked
        {
            get { return _isPerDollarChecked; }
            set
            {
                _isPerDollarChecked = value;
                RaisePropertyChanged(nameof(IsPerDollarChecked));
            }
        }
        public bool IsActiveVendorPrice
        {
            get { return _isActiveVendorPrice; }
            set
            {
                _isActiveVendorPrice = value;
                RaisePropertyChanged(nameof(IsActiveVendorPrice));
            }
        }
        public bool IsToDateVisible
        {
            get { return _isToDateVisible; }
            set
            {
                _isToDateVisible = value;
                RaisePropertyChanged(nameof(IsToDateVisible));
            }
        }
        public bool IsSpecialPricingVisible
        {
            get { return _isSpecialPricingVisible; }
            set
            {
                _isSpecialPricingVisible = value;
                RaisePropertyChanged(nameof(IsSpecialPricingVisible));
            }
        }
        public string TaxExemptPrice
        {
            get { return _taxExemptPrice; }
            set
            {
                _taxExemptPrice = value;
                RaisePropertyChanged(nameof(TaxExemptPrice));
            }
        }
        public bool IsTaxExemptVisible
        {
            get { return _isTaxExemptVisible; }
            set
            {
                _isTaxExemptVisible = value;
                RaisePropertyChanged(nameof(IsTaxExemptVisible));
            }
        }
        public bool IsAvQtyVisible
        {
            get { return _isAvQtyVisible; }
            set
            {
                _isAvQtyVisible = value;
                RaisePropertyChanged(nameof(IsAvQtyVisible));
            }
        }
        public bool IsPriceVisible
        {
            get { return _isPriceVisible; }
            set
            {
                _isPriceVisible = value;
                RaisePropertyChanged(nameof(IsPriceVisible));
            }
        }
        public DateTimeOffset FromDate
        {
            get { return _fromDate; }
            set
            {
                if (_fromDate != value)
                {
                    _fromDate = value;
                    RaisePropertyChanged(nameof(FromDate));
                }
            }
        }
        public DateTimeOffset ToDate
        {
            get { return _todate; }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    RaisePropertyChanged(nameof(ToDate));
                }
            }
        }
        public string PriceTypeText
        {
            get { return _priceTypeText; }
            set
            {
                _priceTypeText = value;
                RaisePropertyChanged(nameof(PriceTypeText));
            }
        }
        public string VendorId
        {
            get { return _vendorId; }
            set
            {
                _vendorId = value;
                RaisePropertyChanged(nameof(VendorId));
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
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
        public string RegularPriceText
        {
            get { return _regularPriceText; }
            set
            {
                _regularPriceText = Helper.SelectAllDecimalValue(value, _regularPriceText);
                RaisePropertyChanged(nameof(RegularPriceText));
            }
        }
        public bool IsEndDateChecked
        {
            get { return _isEndDateChecked; }
            set
            {
                _isEndDateChecked = value;
                RaisePropertyChanged(nameof(IsEndDateChecked));
            }
        }
        public string AvailableQuantity
        {
            get { return _availableQuantity; }
            set
            {
                _availableQuantity = Helper.SelectAllDecimalValue(value, _availableQuantity);
                RaisePropertyChanged(nameof(AvailableQuantity));
            }
        }
        public string TaxExemptAvailable
        {
            get { return _taxExemptAvailable; }
            set
            {
                _taxExemptAvailable = value;
                RaisePropertyChanged(nameof(TaxExemptAvailable));
            }
        }
        public ObservableCollection<string> SpecialPriceTypes
        {
            get { return _SpecialPriceTypes; }
            set
            {
                _SpecialPriceTypes = value;
                RaisePropertyChanged(nameof(SpecialPriceTypes));
            }
        }
        public SalePriceModel SalePrice { get; set; }
        public FirstUnitPrice FirstUnitPrice { get; set; }
        public IncrementalPrice IncrementalPrice { get; set; }
        public FirstUnitPrice XForPrice { get; set; }
    }

    public class IncrementalPrice : ViewModelBase
    {
        private int _columns;
        private string _columnText;
        private string _columnText2;
        private string _columnText3;

        public string ColumnText3
        {
            get { return _columnText3; }
            set
            {
                _columnText3 = value;
                RaisePropertyChanged(nameof(ColumnText3));
            }
        }
        public string ColumnText2
        {
            get { return _columnText2; }
            set
            {
                _columnText2 = value;
                RaisePropertyChanged(nameof(ColumnText2));
            }
        }
        public string ColumnText
        {
            get { return _columnText; }
            set
            {
                _columnText = value;
                RaisePropertyChanged(nameof(ColumnText));
            }
        }
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged(nameof(Columns));
            }
        }
        public List<SalePricesModel> incrementalPriceGrids { get; set; }
    }

    public class FirstUnitPrice : ViewModelBase
    {
        private int _columns;
        private string _columnText;
        private string _columnText2;

        public string ColumnText2
        {
            get { return _columnText2; }
            set
            {
                _columnText2 = value;
                RaisePropertyChanged(nameof(ColumnText2));
            }
        }

        public string ColumnText
        {
            get { return _columnText; }
            set
            {
                _columnText = value;
                RaisePropertyChanged(nameof(ColumnText));
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged(nameof(Columns));
            }
        }
        public ObservableCollection<SalePricesModel> FirstUnitPriceGrids { get; set; }
    }

    public class SalePriceModel : ViewModelBase
    {
        private int _columns;
        private string _columnText;

        public string ColumnText
        {
            get { return _columnText; }
            set
            {
                _columnText = value;
                RaisePropertyChanged(nameof(ColumnText));
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged(nameof(Columns));
            }
        }
        public ObservableCollection<SalePricesModel> SalePrices { get; set; }
    }

    public class SalePricesModel : ViewModelBase
    {
        private string _column1;
        private string _column2;
        private string _column3;

        public string Column1
        {
            get { return _column1; }
            set
            {
                _column1 = Helper.SelectIntegers(value); 
                RaisePropertyChanged(nameof(Column1));
            }
        }
        public string Column2
        {
            get { return _column2; }
            set
            {
                _column2 = Helper.SelectIntegers(value);
                RaisePropertyChanged(nameof(Column2));
            }
        }
        public string Column3
        {
            get { return _column3; }
            set
            {
                _column3 = Helper.SelectIntegers(value);
                RaisePropertyChanged(nameof(Column3));
            }
        }
    }
}
