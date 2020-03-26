using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class SaleSummaryModel : ViewModelBase
    {
        private ObservableCollection<SaleSummaryLineModel> _lines;
        private string _summary1;
        private string _summary2;

        public string Summary1
        {
            get { return _summary1; }
            set
            {
                _summary1 = value;
                RaisePropertyChanged(nameof(Summary1));
            }
        }

        public string Summary2
        {
            get { return _summary2; }
            set
            {
                _summary2 = value;
                RaisePropertyChanged(nameof(Summary2));
            }
        }

        public ObservableCollection<SaleSummaryLineModel> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                RaisePropertyChanged(nameof(Lines));
            }
        }
    }

    public class SaleSummaryLineModel : ViewModelBase
    {
        private string _name;
        private string _amount;
        private string _value;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
    }
}
