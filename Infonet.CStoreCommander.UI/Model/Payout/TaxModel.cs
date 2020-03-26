using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.Model.Payout
{
    public class TaxModel : ViewModelBase
    {
        private string _code;
        private string _amount;
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = Helper.SelectAllDecimalValue(value, _amount);
                RaisePropertyChanged(nameof(Amount));
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
