using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Payout
{
    public class VendorModel : ViewModelBase
    {
        private string _code;
        private string _name;

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
