using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing
{
    public class PricesToDisplayVM : VMBase
    {
        private readonly IFuelPriceBusinessLogic _fuelPriceBusinessLogic;

        private ObservableCollection<string> _grades;
        private ObservableCollection<string> _tiers;
        private ObservableCollection<string> _levels;

        private string _grade1;
        private string _grade2;
        private string _grade3;
        private string _grade4;

        private string _tier1;
        private string _tier2;
        private string _tier3;
        private string _tier4;

        private string _level1;
        private string _level2;
        private string _level3;
        private string _level4;

        private bool _isRow1Enabled;
        private bool _isRow2Enabled;
        private bool _isRow3Enabled;
        private bool _isRow4Enabled;

        public ObservableCollection<string> Grades
        {
            get
            {
                return _grades;
            }
            set
            {
                if (_grades != value)
                {
                    _grades = value;
                    RaisePropertyChanged(nameof(Grades));
                }
            }
        }

        public ObservableCollection<string> Tiers
        {
            get
            {
                return _tiers;
            }
            set
            {
                if (_tiers != value)
                {
                    _tiers = value;
                    RaisePropertyChanged(nameof(Tiers));
                }
            }
        }

        public ObservableCollection<string> Levels
        {
            get
            {
                return _levels;
            }
            set
            {
                if (_levels != value)
                {
                    _levels = value;
                    RaisePropertyChanged(nameof(Levels));
                }
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }

        public string Grade1
        {
            get
            {
                return _grade1;
            }
            set
            {
                if (_grade1 != value)
                {
                    _grade1 = value;
                    RaisePropertyChanged(nameof(Grade1));
                }
            }
        }

        public string Grade2
        {
            get
            {
                return _grade2;
            }
            set
            {
                if (_grade2 != value)
                {
                    _grade2 = value;
                    RaisePropertyChanged(nameof(Grade2));
                }
            }
        }

        public string Grade3
        {
            get
            {
                return _grade3;
            }
            set
            {
                if (_grade3 != value)
                {
                    _grade3 = value;
                    RaisePropertyChanged(nameof(Grade3));
                }
            }
        }

        public string Grade4
        {
            get
            {
                return _grade4;
            }
            set
            {
                if (_grade4 != value)
                {
                    _grade4 = value;
                    RaisePropertyChanged(nameof(Grade4));
                }
            }
        }

        public string Tier1
        {
            get
            {
                return _tier1;
            }
            set
            {
                if (_tier1 != value)
                {
                    _tier1 = value;
                    RaisePropertyChanged(nameof(Tier1));
                }
            }
        }

        public string Tier2
        {
            get
            {
                return _tier2;
            }
            set
            {
                if (_tier2 != value)
                {
                    _tier2 = value;
                    RaisePropertyChanged(nameof(Tier2));
                }
            }
        }

        public string Tier3
        {
            get
            {
                return _tier3;
            }
            set
            {
                if (_tier3 != value)
                {
                    _tier3 = value;
                    RaisePropertyChanged(nameof(Tier3));
                }
            }
        }

        public string Tier4
        {
            get
            {
                return _tier4;
            }
            set
            {
                if (_tier4 != value)
                {
                    _tier4 = value;
                    RaisePropertyChanged(nameof(Tier4));
                }
            }
        }

        public string Level1
        {
            get
            {
                return _level1;
            }
            set
            {
                if (_level1 != value)
                {
                    _level1 = value;
                    RaisePropertyChanged(nameof(Level1));
                }
            }
        }

        public string Level2
        {
            get
            {
                return _level2;
            }
            set
            {
                if (_level2 != value)
                {
                    _level2 = value;
                    RaisePropertyChanged(nameof(Level2));
                }
            }
        }

        public string Level3
        {
            get
            {
                return _level3;
            }
            set
            {
                if (_level3 != value)
                {
                    _level3 = value;
                    RaisePropertyChanged(nameof(Level3));
                }
            }
        }

        public string Level4
        {
            get
            {
                return _level4;
            }
            set
            {
                if (_level4 != value)
                {
                    _level4 = value;
                    RaisePropertyChanged(nameof(Level4));
                }
            }
        }

        public bool IsRow1Enabled
        {
            get
            {
                return _isRow1Enabled;
            }
            set
            {
                if (_isRow1Enabled != value)
                {
                    _isRow1Enabled = value;
                    RaisePropertyChanged(nameof(IsRow1Enabled));
                }
            }
        }

        public bool IsRow2Enabled
        {
            get
            {
                return _isRow2Enabled;
            }
            set
            {
                if (_isRow2Enabled != value)
                {
                    _isRow2Enabled = value;
                    RaisePropertyChanged(nameof(IsRow2Enabled));
                }
            }
        }

        public bool IsRow3Enabled
        {
            get
            {
                return _isRow3Enabled;
            }
            set
            {
                if (_isRow3Enabled != value)
                {
                    _isRow3Enabled = value;
                    RaisePropertyChanged(nameof(IsRow3Enabled));
                }
            }
        }

        public bool IsRow4Enabled
        {
            get
            {
                return _isRow4Enabled;
            }
            set
            {
                if (_isRow4Enabled != value)
                {
                    _isRow4Enabled = value;
                    RaisePropertyChanged(nameof(IsRow4Enabled));
                }
            }
        }

        public PricesToDisplayVM(IFuelPriceBusinessLogic fuelPriceBusinessLogic)
        {
            _fuelPriceBusinessLogic = fuelPriceBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoadCommand = new RelayCommand(() => PerformAction(Load));
            SaveCommand = new RelayCommand(() => PerformAction(SavePrices));
        }

        private async Task SavePrices()
        {
            await _fuelPriceBusinessLogic.SavePricesToDisplay(new List<string>
            {
                Grade1, Grade2, Grade3, Grade4
            },
            new List<string>
            {
                Tier1, Tier2, Tier3, Tier4
            }, new List<string>
            {
                Level1, Level2, Level3, Level4
            });
        }

        private async Task Load()
        {
            var data = await _fuelPriceBusinessLogic.LoadPricesToDisplay();

            Grades = new ObservableCollection<string>(data.Grades);
            Tiers = new ObservableCollection<string>(data.Tiers);
            Levels = new ObservableCollection<string>(data.Levels);

            Grade1 = data.GradesState[0].SelectedValue;
            Grade2 = data.GradesState[1].SelectedValue;
            Grade3 = data.GradesState[2].SelectedValue;
            Grade4 = data.GradesState[3].SelectedValue;

            Tier1 = data.TiersState[0].SelectedValue;
            Tier2 = data.TiersState[1].SelectedValue;
            Tier3 = data.TiersState[2].SelectedValue;
            Tier4 = data.TiersState[3].SelectedValue;

            Level1 = data.LevelsState[0].SelectedValue;
            Level2 = data.LevelsState[1].SelectedValue;
            Level3 = data.LevelsState[2].SelectedValue;
            Level4 = data.LevelsState[3].SelectedValue;

            IsRow1Enabled = data.GradesState[0].IsEnabled;
            IsRow2Enabled = data.GradesState[1].IsEnabled;
            IsRow3Enabled = data.GradesState[2].IsEnabled;
            IsRow4Enabled = data.GradesState[3].IsEnabled;
        }

        internal void ReInitialize()
        {
            Grade1 = Grade2 = Grade3 = Grade4 = Level1 = Level2 = Level3 = Level4 = Tier1 = Tier2 = Tier3 = Tier4 = string.Empty;
        }
    }
}
