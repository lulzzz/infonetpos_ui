using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.EntityLayer.Entities;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.Logout
{

    public class CloseTillModel : ViewModelBase
    {
        private bool _showBillCoins;
        private bool _showEnteredField;
        private bool _showSystemField;
        private bool _showDifferenceField;
        private string _total;

        public string Total
        {
            get { return _total; }
            set
            {
                if (value != _total)
                {
                    _total = value;
                    RaisePropertyChanged(nameof(Total));
                }
            }
        }

        public bool ShowDifferenceField
        {
            get { return _showDifferenceField; }
            set
            {
                if (value != _showDifferenceField)
                {
                    _showDifferenceField = value;
                    RaisePropertyChanged(nameof(ShowDifferenceField));
                }
            }
        }
        public bool ShowSystemField
        {
            get { return _showSystemField; }
            set
            {
                if (value != _showSystemField)
                {
                    _showSystemField = value;
                    RaisePropertyChanged(nameof(ShowSystemField));
                }
            }
        }
        public bool ShowEnteredField
        {
            get { return _showEnteredField; }
            set
            {
                if (value != _showEnteredField)
                {
                    _showEnteredField = value;
                    RaisePropertyChanged(nameof(ShowEnteredField));
                }
            }
        }
        public bool ShowBillCoins
        {
            get { return _showBillCoins; }
            set
            {
                if (value != _showBillCoins)
                {
                    _showBillCoins = value;
                    RaisePropertyChanged(nameof(ShowBillCoins));
                }
            }
        }

        public LineDisplayModel LineDisplay { get; set; }
        public ObservableCollection<BillCoinsModel> BillCoins { get; set; }
        public ObservableCollection<CloseTillTendersModel> Tenders { get; set; }
    }

    public class BillCoinsModel : ViewModelBase
    {
        private string _description;
        private string _value;
        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = string.IsNullOrEmpty(Amount) ? string.Empty : value;
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

    }

    public class CloseTillTendersModel : ViewModelBase
    {
        private string _tender;
        private string _count;
        private string _entered;
        private string _system;
        private string _difference;

        public string Difference
        {
            get { return _difference; }
            set
            {
                if (_difference != value)
                {
                    _difference = value;
                    RaisePropertyChanged(nameof(Difference));
                }
            }
        }
        public string System
        {
            get { return _system; }
            set
            {
                if (_system != value)
                {
                    _system = value;
                    RaisePropertyChanged(nameof(System));
                }
            }
        }

        public string Entered
        {
            get { return _entered; }
            set
            {
                if (_entered != value)
                {
                    _entered = value;
                    RaisePropertyChanged(nameof(Entered));
                }
            }
        }


        public string Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    RaisePropertyChanged(nameof(Count));
                }
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
