using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Model.Stock;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model.Sale
{
    public class BottleModel : ViewModelBase
    {
        private string _pageName;
        private int _pageId;
        private ObservableCollection<ProductDataModel> _bottleDetails;

        public string PageName
        {
            get { return _pageName; }
            set
            {
                _pageName = value;
                RaisePropertyChanged(nameof(PageName));
            }
        }

        public int PageId
        {
            get { return _pageId; }
            set
            {
                _pageId = value;
                RaisePropertyChanged(nameof(PageId));
            }
        }

        public ObservableCollection<ProductDataModel> BottleDetails
        {
            get { return _bottleDetails; }
            set
            {
                _bottleDetails = value;
                RaisePropertyChanged(nameof(BottleDetails));
            }
        }
    }

    public class BottleDetailModel : ViewModelBase
    {
        private ImageSource _image;
        private int _quantity;
        private int _defaultQuantity;
        private string _stockCode;
        private string _description;
        private decimal _price;
        private decimal _amount;

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

        public int DefaultQuantity
        {
            get { return _defaultQuantity; }
            set
            {
                _defaultQuantity = value;
                RaisePropertyChanged(nameof(DefaultQuantity));
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

        public ImageSource ImageSource
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }
    }
}
