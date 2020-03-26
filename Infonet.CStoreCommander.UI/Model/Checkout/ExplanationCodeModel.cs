using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class ExplanationCodeModel : ViewModelBase
    {
        private int _code;
        private string _reason;

        public int Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }

        public string Reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                RaisePropertyChanged(nameof(Reason));
            }
        }
    }
}
