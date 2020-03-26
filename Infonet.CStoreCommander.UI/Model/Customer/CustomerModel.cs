using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Customer
{
    public class CustomerModel : ViewModelBase
    {
        private string _code;
        private string _name;
        private string _phoneNumber;
        private string _loyaltyNumber;

        public string LoyaltyNumber
        {
            get { return _loyaltyNumber; }
            set
            {
                _loyaltyNumber = value;
                RaisePropertyChanged(nameof(LoyaltyNumber));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
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

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }
    }
}
