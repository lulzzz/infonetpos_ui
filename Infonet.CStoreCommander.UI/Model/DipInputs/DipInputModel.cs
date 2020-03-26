using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.Model.DipInputs
{
    public class DipInputModel : ViewModelBase
    {
        private string _tankId;
        private string _grade;
        private string _gradeId;
        private string _dipValue;

        public string DipValue
        {
            get { return _dipValue; }
            set
            {
                _dipValue = Helper.SelectDecimalValue(value, _dipValue);
                RaisePropertyChanged(nameof(DipValue));
            }
        }


        public string GradeId
        {
            get { return _gradeId; }
            set
            {
                _gradeId = value;
                RaisePropertyChanged(nameof(GradeId));
            }
        }


        public string Grade
        {
            get { return _grade; }
            set
            {
                _grade = value;
                RaisePropertyChanged(nameof(Grade));
            }
        }


        public string TankId
        {
            get { return _tankId; }
            set
            {
                _tankId = value;
                RaisePropertyChanged(nameof(TankId));
            }
        }

    }
}