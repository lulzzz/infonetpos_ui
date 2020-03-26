using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Cash
{
    public class CashDrawModel : ViewModelBase
    {
        private string _tender;
        private decimal _amount;
        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
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

        public string Tender
        {
            get { return _tender; }
            set
            {
                _tender = value;
                RaisePropertyChanged(nameof(Tender));
            }
        }

    }
}
