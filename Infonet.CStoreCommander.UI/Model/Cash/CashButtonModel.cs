using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Cash
{
    public class CashButtonModel : ViewModelBase
    {
        private string _button;
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }


        public string Button
        {
            get { return _button; }
            set
            {
                _button = value;
                RaisePropertyChanged(nameof(Button));
            }
        }

    }
}
