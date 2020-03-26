using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model.Cash
{
    public class TenderModel : ViewModelBase
    {
        private string _tenderName;
        private string _tenderCode;
        private string _amountEntered;
        private string _amountValue;
        private decimal _maximumValue;
        private decimal _minimumValue;
        private int _quantity;
        private ImageSource _image;
        private string _tenderClass;

        public string TenderClass
        {
            get { return _tenderClass; }
            set
            {
                _tenderClass = value;
                RaisePropertyChanged(nameof(TenderClass));
            }
        }


        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(nameof(Image));
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


        public decimal MinimumValue
        {
            get { return _minimumValue; }
            set
            {
                _minimumValue = value;
                RaisePropertyChanged(nameof(MinimumValue));
            }
        }
        public decimal MaximumValue
        {
            get { return _maximumValue; }
            set
            {
                _maximumValue = value;
                RaisePropertyChanged(nameof(MaximumValue));
            }
        }
        public string AmountValue
        {
            get { return _amountValue; }
            set
            {
                _amountValue = value;
                RaisePropertyChanged(nameof(AmountValue));
            }
        }
        public string AmountEntered
        {
            get { return _amountEntered; }
            set
            {
                _amountEntered = value;
                RaisePropertyChanged(nameof(AmountEntered));
            }
        }
        public string TenderCode
        {
            get { return _tenderCode; }
            set
            {
                _tenderCode = value;
                RaisePropertyChanged(nameof(TenderCode));
            }
        }
        public string TenderName
        {
            get { return _tenderName; }
            set
            {
                _tenderName = value;
                RaisePropertyChanged(nameof(TenderName));
            }
        }

    }
}
