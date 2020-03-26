using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Model.Stock;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model.Cash
{
    public class CashDrawTypesModel
    {
        public ObservableCollection<ProductDataModel> Coins { get; set; }
            = new ObservableCollection<ProductDataModel>();

        public ObservableCollection<ProductDataModel> Bills { get; set; }
            = new ObservableCollection<ProductDataModel>();
    }

    public class CurrencyModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private string _description;
        private int _quantity;
        private decimal _value;
        private string _stockCode;

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                RaisePropertyChanged(nameof(StockCode));
            }
        }
        public decimal Value
        {
            get { return _value; }
            set { _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
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
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }
    }
}
