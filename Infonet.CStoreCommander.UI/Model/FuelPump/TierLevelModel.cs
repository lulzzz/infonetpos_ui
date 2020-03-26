using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.FuelPump
{
    public class TierLevelModel : ViewModelBase
    {
        private string _pageCaption;

        public string PageCaption
        {
            get { return _pageCaption; }
            set
            {
                if (_pageCaption != value)
                {
                    _pageCaption = value;
                    RaisePropertyChanged(nameof(PageCaption));
                }
            }
        }
        public ObservableCollection<PumpTierLevelModel> PumpTierLevels { get; set; }
        public ObservableCollection<TierModel> Tiers { get; set; }
        public ObservableCollection<LevelModel> Levels { get; set; }
    }

    public class PumpTierLevelModel : ViewModelBase
    {
        private int _pumpId;
        private int _tierId;
        private int _levelId;
        private string _tierName;
        private string _levelName;

        public string LevelName
        {
            get { return _levelName; }
            set
            {
                if (_levelName != value)
                {
                    _levelName = value;
                    RaisePropertyChanged(nameof(LevelName));
                }
            }
        }
        public string TierName
        {
            get { return _tierName; }
            set
            {
                if (_tierName != value)
                {
                    _tierName = value;
                    RaisePropertyChanged(nameof(TierName));
                }
            }
        }
        public int LevelId
        {
            get { return _levelId; }
            set
            {
                if (_levelId != value)
                {
                    _levelId = value;
                    RaisePropertyChanged(nameof(LevelId));
                }
            }
        }
        public int TierId
        {
            get { return _tierId; }
            set
            {
                if (_tierId != value)
                {
                    _tierId = value;
                    RaisePropertyChanged(nameof(TierId));
                }
            }
        }
        public int PumpId
        {
            get { return _pumpId; }
            set
            {
                if (_pumpId != value)
                {
                    _pumpId = value;
                    RaisePropertyChanged(nameof(PumpId));
                }
            }
        }
    }

    public class TierModel : ViewModelBase
    {
        private int _tierId;
        private string _tierName;
        private bool? _isChecked;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged(nameof(IsChecked));
                }
            }
        }
        public string TierName
        {
            get { return _tierName; }
            set
            {
                if (_tierName != value)
                {
                    _tierName = value;
                    RaisePropertyChanged(nameof(TierName));
                }
            }
        }
        public int TierId
        {
            get { return _tierId; }
            set
            {
                if (_tierId != value)
                {
                    _tierId = value;
                    RaisePropertyChanged(nameof(TierId));
                }
            }
        }

    }

    public class LevelModel : ViewModelBase
    {
        private int _levelId;
        private string _levelName;
        private bool? _isChecked;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged(nameof(IsChecked));
                }
            }
        }
        public string LevelName
        {
            get { return _levelName; }
            set
            {
                if (_levelName != value)
                {
                    _levelName = value;
                    RaisePropertyChanged(nameof(LevelName));
                }
            }
        }
        public int LevelId
        {
            get { return _levelId; }
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


