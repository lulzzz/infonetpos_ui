using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Model.Stock;

namespace Infonet.CStoreCommander.UI.Model.HotCategory
{
    public class HotProductModel : ViewModelBase
    {
        private string _pageName;
        private int _pageId;
        private ObservableCollection<ProductDataModel> _productDetails;

        public string PageName
        {
            get { return _pageName; }
            set
            {
                if (_pageName != value)
                {
                    _pageName = value;
                    RaisePropertyChanged(nameof(PageName));
                }
            }
        }

        public int PageId
        {
            get { return _pageId; }
            set
            {
                if (_pageId != value)
                {
                    _pageId = value;
                    RaisePropertyChanged(nameof(PageId));
                }
            }
        }

        public ObservableCollection<ProductDataModel> ProductDetails
        {
            get { return _productDetails; }
            set
            {
                _productDetails = value;
                RaisePropertyChanged(nameof(ProductDetails));
            }
        }

        public HotProductModel()
        {
            ProductDetails = new ObservableCollection<ProductDataModel>();
        }
    }

    public class HotProductDetailModel : ViewModelBase
    {
        private string _name;
        private ImageSource _image;
        private int _quantity;
        private int _id;
        private int _defaultQuantity;
        private string _stockCode;
        private string _description;
        private int _fontSizeSelector;

        public int FontSizeSelector
        {
            get { return _fontSizeSelector; }
            set
            {
                _fontSizeSelector = value;
                RaisePropertyChanged(nameof(FontSizeSelector));
            }
        }

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

                    if (value <= 99 && value >= -9)
                    {
                        FontSizeSelector = 1;
                    }
                    else if ((value >= 100 && value <= 999) || (value >= -10 && (value <= -99)))
                    {
                        FontSizeSelector = 2;
                    }
                    else
                    {
                        FontSizeSelector = 3;
                    }
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
    }
}
