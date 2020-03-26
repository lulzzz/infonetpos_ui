using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.FuelPrice
{
    public class IncrementModel : ViewModelBase
    {
        private int _row;
        private int _gradeId;
        private string _grade;
        private string _cash;
        private string _credit;

        public int Row
        {
            get
            {
                return _row;
            }

            set
            {
                if (_row != value)
                {
                    _row = value;
                    RaisePropertyChanged(nameof(Row));
                }
            }
        }

        public int GradeId
        {
            get
            {
                return _gradeId;
            }

            set
            {
                if (_gradeId != value)
                {
                    _gradeId = value;
                    RaisePropertyChanged(nameof(GradeId));
                }
            }
        }

        public string Grade
        {
            get
            {
                return _grade;
            }

            set
            {
                if (_grade != value)
                {
                    _grade = value;
                    RaisePropertyChanged(nameof(Grade));
                }
            }
        }

        public string Cash
        {
            get
            {
                return _cash;
            }

            set
            {
                if (_cash != value)
                {
                    _cash = value;
                    RaisePropertyChanged(nameof(Cash));
                }
            }
        }

        public string Credit
        {
            get
            {
                return _credit;
            }

            set
            {
                if (_credit != value)
                {
                    _credit = value;
                    RaisePropertyChanged(nameof(Credit));
                }
            }
        }
    }

    public class DifferenceModel : ViewModelBase
    {
        private int _row;
        private int _tierId;
        private int _levelId;
        private string _tierLevel;
        private string _cash;
        private string _credit;

        public int Row
        {
            get
            {
                return _row;
            }

            set
            {
                if (_row != value)
                {
                    _row = value;
                    RaisePropertyChanged(nameof(Row));
                }
            }
        }

        public int TierId
        {
            get
            {
                return _tierId;
            }

            set
            {
                if (_tierId != value)
                {
                    _tierId = value;
                    RaisePropertyChanged(nameof(TierId));
                }
            }
        }

        public string TierLevel
        {
            get
            {
                return _tierLevel;
            }

            set
            {
                if (_tierLevel != value)
                {
                    _tierLevel = value;
                    RaisePropertyChanged(nameof(TierLevel));
                }
            }
        }

        public string Cash
        {
            get
            {
                return _cash;
            }

            set
            {
                if (_cash != value)
                {
                    _cash = value;
                    RaisePropertyChanged(nameof(Cash));
                }
            }
        }

        public string Credit
        {
            get
            {
                return _credit;
            }

            set
            {
                if (_credit != value)
                {
                    _credit = value;
                    RaisePropertyChanged(nameof(Credit));
                }
            }
        }

        public int LevelId
        {
            get
            {
                return _levelId;
            }

            set
            {
                if (_levelId != value)
                {
                    _levelId = value;
                    RaisePropertyChanged(nameof(LevelId));
                }
            }
        }
    }
}
