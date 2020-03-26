using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.FuelPrice;
using System.Collections.ObjectModel;
using Infonet.CStoreCommander.UI.Utility;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Globalization;
using Infonet.CStoreCommander.UI.Messages;

namespace Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing
{
    public class PriceIncrementsVM : VMBase
    {
        private readonly IFuelPriceBusinessLogic _fuelPriceBusinessLogic;

        private bool _taxExempt;

        private bool _isCreditEnabled;
        private IncrementModel _selectedIncrement;
        private DifferenceModel _selectedDifference;

        private ObservableCollection<IncrementModel> _increments;
        private ObservableCollection<DifferenceModel> _differences;

        public RelayCommand SetIncrementCommand { get; private set; }
        public RelayCommand SetDecrementCommand { get; private set; }
        public RelayCommand LoadPricesCommand { get; private set; }
        public RelayCommand LoadTaxExemptPricesCommand { get; private set; }

        public bool IsCreditEnabled
        {
            get
            {
                return _isCreditEnabled;
            }
            set
            {
                if (_isCreditEnabled != value)
                {
                    _isCreditEnabled = value;
                    RaisePropertyChanged(nameof(IsCreditEnabled));
                }
            }
        }

        public IncrementModel SelectedIncrement
        {
            get
            {
                return _selectedIncrement;
            }
            set
            {
                if (_selectedIncrement != value)
                {
                    _selectedIncrement = value;
                    RaisePropertyChanged(nameof(SelectedIncrement));
                }
            }
        }

        public DifferenceModel SelectedDifference
        {
            get
            {
                return _selectedDifference;
            }
            set
            {
                if (_selectedDifference != value)
                {
                    _selectedDifference = value;
                    RaisePropertyChanged(nameof(SelectedDifference));
                }
            }
        }

        public ObservableCollection<IncrementModel> Increments
        {
            get
            {
                return _increments;
            }
            set
            {
                if (_increments != value)
                {
                    _increments = value;
                    RaisePropertyChanged(nameof(Increments));
                }
            }
        }

        public ObservableCollection<DifferenceModel> Differences
        {
            get
            {
                return _differences;
            }
            set
            {
                if (_differences != value)
                {
                    _differences = value;
                    RaisePropertyChanged(nameof(Differences));
                }
            }
        }

        public PriceIncrementsVM(IFuelPriceBusinessLogic fuelPriceBusinessLogic)
        {
            _fuelPriceBusinessLogic = fuelPriceBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoadPricesCommand = new RelayCommand(() => PerformAction(LoadPrices));
            LoadTaxExemptPricesCommand = new RelayCommand(() => PerformAction(LoadTaxExemptPrices));
            SetIncrementCommand = new RelayCommand(() => PerformAction(SetIncrement));
            SetDecrementCommand = new RelayCommand(() => PerformAction(SetDecrement));
        }

        private async Task SetDecrement()
        {
            try
            {
                Convert.ToDecimal(SelectedDifference?.Cash, CultureInfo.InvariantCulture);
                Convert.ToDecimal(SelectedDifference?.Credit, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // Eating exception when prices are not in correct format
                return;
            }

            var result = await _fuelPriceBusinessLogic.SetPriceDecrement(SelectedDifference?.ToEntity(), _taxExempt);
            var model = result?.Price.ToModel();
            var data = Differences.FirstOrDefault(x => x.Row == result?.Price.Row);
            var index = Differences.IndexOf(data);
            if (index != -1)
            {
                Differences[index] = model;
            }

            MessengerInstance.Send(new FuelPriceReportMessage { Report = result?.Report.ReportContent });
        }

        private async Task SetIncrement()
        {
            try
            {
                Convert.ToDecimal(SelectedIncrement?.Cash, CultureInfo.InvariantCulture);
                Convert.ToDecimal(SelectedIncrement?.Credit, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // Eating exception when prices are not in correct format
                return;
            }

            var result = await _fuelPriceBusinessLogic.SetPriceIncrement(SelectedIncrement?.ToEntity(), _taxExempt);
            var model = result?.Price.ToModel();
            var data = Increments.FirstOrDefault(x => x.Row == result?.Price.Row);
            var index = Increments.IndexOf(data);
            if (index != -1)
            {
                Increments[index] = model;
            }

            MessengerInstance.Send(new FuelPriceReportMessage { Report = result?.Report.ReportContent });
        }

        private async Task LoadTaxExemptPrices()
        {
            _taxExempt = true;
            await LoadPriceIncrements();
        }

        private async Task LoadPriceIncrements()
        {
            var prices = await _fuelPriceBusinessLogic.LoadPriceIncrementsAndDecrements(_taxExempt);

            Increments = new ObservableCollection<IncrementModel>(from p in prices.PriceIncrements
                                                                  select new IncrementModel
                                                                  {
                                                                      Cash = p.Cash,
                                                                      Credit = p.Credit,
                                                                      Grade = p.Grade,
                                                                      GradeId = p.GradeId,
                                                                      Row = p.Row
                                                                  });

            Differences = new ObservableCollection<DifferenceModel>(from p in prices.PriceDecrements
                                                                    select new DifferenceModel
                                                                    {
                                                                        Cash = p.Cash,
                                                                        Credit = p.Credit,
                                                                        LevelId = p.LevelId,
                                                                        Row = p.Row,
                                                                        TierId = p.TierId,
                                                                        TierLevel = p.TierLevel
                                                                    });

            IsCreditEnabled = prices.IsCreditEnabled;
        }

        private async Task LoadPrices()
        {
            _taxExempt = false;
            await LoadPriceIncrements();
        }

        internal void ReInitialize()
        {
            SelectedDifference = null;
            SelectedIncrement = null;
            Increments = null;
            Differences = null;
        }
    }
}
