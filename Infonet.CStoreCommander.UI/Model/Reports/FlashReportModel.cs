using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Reports
{
    public class FlashReportModel : ViewModelBase
    {
        private string _department;
        private string _description;
        private string _netSales;

        public string NetSales
        {
            get { return _netSales; }
            set
            {
                _netSales = value;
                RaisePropertyChanged(nameof(NetSales));
            }
        }


        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }


        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                RaisePropertyChanged(nameof(Department));
            }
        }

    }
}
