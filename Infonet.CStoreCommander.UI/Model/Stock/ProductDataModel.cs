using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model.Stock
{
    public class ProductDataModel : ViewModelBase
    {
        private string _name;
        private ImageSource _image;
        private int _quantity;
        private int _id;
        private int _defaultQuantity;
        private string _stockCode;
        private string _description;
        private decimal _price;
        private decimal _value;

       

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                if (_stockCode != value)
                {
                    _stockCode = value;
                    RaisePropertyChanged(nameof(StockCode));
                }
            }
        }

        public int DefaultQuantity
        {
            get { return _defaultQuantity; }
            set
            {
                if (_defaultQuantity != value)
                {
                    _defaultQuantity = value;
                    RaisePropertyChanged(nameof(DefaultQuantity));
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public ImageSource ImageSource
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    RaisePropertyChanged(nameof(ImageSource));
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    RaisePropertyChanged(nameof(Price));
                }
            }
        }

        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }
    }
}
