using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Stock
{
    public class TaxCodes : ViewModelBase
    {
        private string _taxCode;
        private bool? _isChecked;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        public string TaxCode
        {
            get { return _taxCode; }
            set
            {
                _taxCode = value;
                RaisePropertyChanged(nameof(TaxCode));
            }
        }
    }
}
