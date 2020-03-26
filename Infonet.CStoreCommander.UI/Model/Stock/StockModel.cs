using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.Model.Stock
{
    public class StockModel : ViewModelBase
    {
        private string _alternateCode;
        private string _stockCode;
        private string _description;
        private string _regularPrice;
        private float _quantity;
        private bool _isManuallyAdded;
        private bool _isAddButtonEnabled;

        public bool IsAddButtonEnabled
        {
            get { return _isAddButtonEnabled; }
            set
            {
                _isAddButtonEnabled = value;
                RaisePropertyChanged(nameof(IsAddButtonEnabled));
            }
        }


        public string RegularPrice
        {
            get { return _regularPrice; }
            set
            {
                _regularPrice = Helper.SelectDecimalValue(value, _regularPrice);
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                     !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(RegularPrice));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                    !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string AlternateCode
        {
            get { return _alternateCode; }
            set
            {
                _alternateCode = value;
                RaisePropertyChanged(nameof(AlternateCode));
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                    !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(StockCode));
            }
        }

        public ObservableCollection<TaxCodes> TaxCodes { get; set; }
            = new ObservableCollection<TaxCodes>();

        public float Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        public bool IsManuallyAdded
        {
            get { return _isManuallyAdded; }
            set { _isManuallyAdded = value; }
        }
    }
}
