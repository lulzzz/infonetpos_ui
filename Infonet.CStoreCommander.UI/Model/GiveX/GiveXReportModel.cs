using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.GiveX
{
    public class GiveXReportModel : ViewModelBase
    {
        private string _id;
        private string _cashOut;
        private string _batchDate;
        private string _batchTime;
        private string _report;

        public string Report
        {
            get { return _report; }
            set
            {
                if (value != _report)
                {
                    _report = value;
                    RaisePropertyChanged(nameof(Report));
                }
            }
        }

        public string BatchTime
        {
            get { return _batchTime; }
            set
            {
                if (value != _batchTime)
                {
                    _batchTime = value;
                    RaisePropertyChanged(nameof(BatchTime));
                }
            }
        }


        public string BatchDate
        {
            get { return _batchDate; }
            set
            {
                if (value != _batchDate)
                {
                    _batchDate = value;
                    RaisePropertyChanged(nameof(BatchDate));
                }
            }
        }


        public string CashOut
        {
            get { return _cashOut; }
            set
            {
                if (value != _cashOut)
                {
                    _cashOut = value;
                    RaisePropertyChanged(nameof(CashOut));
                }
            }
        }


        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    RaisePropertyChanged(nameof(Id));
                }
            }
        }
    }
}
