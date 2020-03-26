using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Reprint
{
    public class ReportTypesModel : ViewModelBase
    {
        private string _reportName;
        private string _reportType;
        private bool _isReportEnabled;
        private bool _isDateEnbled;

        public bool IsDateEnabled
        {
            get { return _isDateEnbled; }
            set
            {
                _isDateEnbled = value;
                RaisePropertyChanged(nameof(IsDateEnabled));
            }
        }


        public bool IsReportEnabled
        {
            get { return _isReportEnabled; }
            set
            {
                _isReportEnabled = value;
                RaisePropertyChanged(nameof(IsReportEnabled));
            }
        }


        public string ReportName
        {
            get { return _reportName; }
            set
            {
                _reportName = value;
                RaisePropertyChanged(nameof(ReportName));
            }
        }

        public string ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                if (_reportType != value)
                {
                    _reportType = value;
                    RaisePropertyChanged(nameof(ReportType));
                }
            }
        }
    }
}
