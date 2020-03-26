using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Payment
{
    public class ARCustomerModel : ViewModelBase
    {
        private string _code;
        private string _name;
        private string _phone;
        private string _balance;
        private string _creditLimit;

        public string CreditLimit
        {
            get { return _creditLimit; }
            set
            {
                _creditLimit = value;
                RaisePropertyChanged(nameof(CreditLimit));
            }
        }


        public string Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }


        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged(nameof(Phone));
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
