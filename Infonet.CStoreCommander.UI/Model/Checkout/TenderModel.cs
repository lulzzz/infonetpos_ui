using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class TenderModel : ViewModelBase
    {
        private string _code;
        private string _name;
        private string _class;
        private string _amountEntered;
        private string _amountValue;
        private bool _isEnabled;
        private decimal _maximumValue;
        private decimal _minimumValue;
        private ImageSource _image;

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Class
        {
            get { return _class; }
            set
            {
                _class = value;
                RaisePropertyChanged(nameof(Class));
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

        public string AmountValue
        {
            get { return _amountValue; }
            set
            {
                _amountValue = value;
                RaisePropertyChanged(nameof(AmountValue));
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

        public decimal MinimumValue
        {
            get { return _minimumValue; }
            set
            {
                _minimumValue = value;
                RaisePropertyChanged(nameof(MinimumValue));
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

        public bool IsEnabled
        {
            get{return _isEnabled;}
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }
    }
}
