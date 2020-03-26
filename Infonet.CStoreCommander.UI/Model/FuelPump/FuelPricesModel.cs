using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.FuelPump
{
    public class FuelPricesModel : ViewModelBase
    {
        private ObservableCollection<FuelPriceModel> _prices;
        private bool _isTaxExemptionVisible;
        private bool _isTaxExemptionInVisible;
        private bool _isGroupedTaxExemptionVisible;
        private bool _isGroupedTaxExemptionInVisible;
        private bool _isReadTotalizerEnabled;
        private bool _isReadTotalizerChecked;
        private bool _isPricesToDisplayEnabled;
        private bool _isPricesToDisplayChecked;
        private bool _isReadTankDipEnabled;
        private bool _isReadTankDipChecked;
        private bool _canReadTotalizer;
        private bool _canSelectPricesToDisplay;
        private bool _isExitEnabled;
        private bool _isErrorEnabled;
        private string _caption;
        private bool _isCashPriceEnabled;
        private bool _isCreditPriceEnabled;
        private bool _isTaxExemptedCashPriceEnabled;
        private bool _isTaxExemptedCreditPriceEnabled;
        private string _report;
        private bool _isGrouped;

        public bool IsGrouped
        {
            get
            {
                return _isGrouped;
            }
            set
            {
                _isGrouped = value;
            }
        }

        public ObservableCollection<FuelPriceModel> Prices
        {
            get
            {
                return _prices;
            }
            set
            {
                if (_prices != value)
                {
                    _prices = value;
                    RaisePropertyChanged(nameof(Prices));
                }
            }
        }

        public bool IsTaxExemptionVisible
        {
            get
            {
                return _isTaxExemptionVisible;
            }
            set
            {
                IsTaxExemptionInVisible = !IsGrouped && !value;
                IsGroupedTaxExemptionInVisible = IsGrouped && !value;
                IsGroupedTaxExemptionVisible = IsGrouped && value;
                _isTaxExemptionVisible = !IsGrouped && value;
                RaisePropertyChanged(nameof(IsTaxExemptionVisible));
            }
        }

        public bool IsTaxExemptionInVisible
        {
            get
            {
                return _isTaxExemptionInVisible;
            }
            private set
            {
                if (_isTaxExemptionInVisible != value)
                {
                    _isTaxExemptionInVisible = value;
                    RaisePropertyChanged(nameof(IsTaxExemptionInVisible));
                }
            }
        }

        public bool IsGroupedTaxExemptionInVisible
        {
            get
            {
                return _isGroupedTaxExemptionInVisible;
            }
            private set
            {
                if (_isGroupedTaxExemptionInVisible != value)
                {
                    _isGroupedTaxExemptionInVisible = value;
                    RaisePropertyChanged(nameof(IsGroupedTaxExemptionInVisible));
                }
            }
        }

        public bool IsGroupedTaxExemptionVisible
        {
            get
            {
                return _isGroupedTaxExemptionVisible;
            }
            private set
            {
                if (_isGroupedTaxExemptionVisible != value)
                {
                    _isGroupedTaxExemptionVisible = value;
                    RaisePropertyChanged(nameof(IsGroupedTaxExemptionVisible));
                }
            }
        }

        public bool IsReadTotalizerEnabled
        {
            get
            {
                return _isReadTotalizerEnabled;
            }
            set
            {
                if (_isReadTotalizerEnabled != value)
                {
                    _isReadTotalizerEnabled = value;
                    RaisePropertyChanged(nameof(IsReadTotalizerEnabled));
                }
            }
        }

        public bool IsReadTotalizerChecked
        {
            get
            {
                return _isReadTotalizerChecked;
            }
            set
            {
                if (_isReadTotalizerChecked != value)
                {
                    _isReadTotalizerChecked = value;
                    RaisePropertyChanged(nameof(IsReadTotalizerChecked));
                }
            }
        }

        public bool IsPricesToDisplayEnabled
        {
            get
            {
                return _isPricesToDisplayEnabled;
            }
            set
            {
                if (_isPricesToDisplayEnabled != value)
                {
                    _isPricesToDisplayEnabled = value;
                    RaisePropertyChanged(nameof(IsPricesToDisplayEnabled));
                }
            }
        }

        public bool IsPricesToDisplayChecked
        {
            get
            {
                return _isPricesToDisplayChecked;
            }
            set
            {
                if (_isPricesToDisplayChecked != value)
                {
                    _isPricesToDisplayChecked = value;
                    RaisePropertyChanged(nameof(IsPricesToDisplayChecked));
                }
            }
        }

        public bool IsReadTankDipEnabled
        {
            get
            {
                return _isReadTankDipEnabled;
            }
            set
            {
                if (_isReadTankDipEnabled != value)
                {
                    _isReadTankDipEnabled = value;
                    RaisePropertyChanged(nameof(IsReadTankDipEnabled));
                }
            }
        }

        public bool IsReadTankDipChecked
        {
            get
            {
                return _isReadTankDipChecked;
            }
            set
            {
                if (_isReadTankDipChecked != value)
                {
                    _isReadTankDipChecked = value;
                    RaisePropertyChanged(nameof(IsReadTankDipChecked));
                }
            }
        }

        public bool CanReadTotalizer
        {
            get
            {
                return _canReadTotalizer;
            }
            set
            {
                if (_canReadTotalizer != value)
                {
                    _canReadTotalizer = value;
                    RaisePropertyChanged(nameof(CanReadTotalizer));
                }
            }
        }

        public bool CanSelectPricesToDisplay
        {
            get
            {
                return _canSelectPricesToDisplay;
            }
            set
            {
                if (_canSelectPricesToDisplay != value)
                {
                    _canSelectPricesToDisplay = value;
                    RaisePropertyChanged(nameof(CanSelectPricesToDisplay));
                }
            }
        }

        public bool IsExitEnabled
        {
            get
            {
                return _isExitEnabled;
            }
            set
            {
                if (_isExitEnabled != value)
                {
                    _isExitEnabled = value;
                    RaisePropertyChanged(nameof(IsExitEnabled));
                }
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return _isErrorEnabled;
            }
            set
            {
                if (_isErrorEnabled != value)
                {
                    _isErrorEnabled = value;
                    RaisePropertyChanged(nameof(IsErrorEnabled));
                }
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    RaisePropertyChanged(nameof(Caption));
                }
            }
        }

        public bool IsCashPriceEnabled
        {
            get
            {
                return _isCashPriceEnabled;
            }
            set
            {
                if (_isCashPriceEnabled != value)
                {
                    _isCashPriceEnabled = value;
                    RaisePropertyChanged(nameof(IsCashPriceEnabled));
                }
            }
        }

        public bool IsCreditPriceEnabled
        {
            get
            {
                return _isCreditPriceEnabled;
            }
            set
            {
                if (_isCreditPriceEnabled != value)
                {
                    _isCreditPriceEnabled = value;
                    RaisePropertyChanged(nameof(IsCreditPriceEnabled));
                }
            }
        }

        public bool IsTaxExemptedCashPriceEnabled
        {
            get
            {
                return _isTaxExemptedCashPriceEnabled;
            }
            set
            {
                if (_isTaxExemptedCashPriceEnabled != value)
                {
                    _isTaxExemptedCashPriceEnabled = value;
                    RaisePropertyChanged(nameof(IsTaxExemptedCashPriceEnabled));
                }
            }
        }

        public bool IsTaxExemptedCreditPriceEnabled
        {
            get
            {
                return _isTaxExemptedCreditPriceEnabled;
            }
            set
            {
                if (_isTaxExemptedCreditPriceEnabled != value)
                {
                    _isTaxExemptedCreditPriceEnabled = value;
                    RaisePropertyChanged(nameof(IsTaxExemptedCreditPriceEnabled));
                }
            }
        }

        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                if (_report != value)
                {
                    _report = value;
                    RaisePropertyChanged(nameof(Report));
                }
            }
        }
    }

    public class FuelPriceModel : ViewModelBase
    {
        private int _row;
        private string _grade;
        private short _gradeId;
        private string _tier;
        private short _tierId;
        private string _level;
        private short _levelId;
        private string _cashPrice;
        private string _creditPrice;
        private string _taxExemptedCashPrice;
        private string _taxExemptedCreditPrice;

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

        public short GradeId
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

        public string Tier
        {
            get
            {
                return _tier;
            }
            set
            {
                if (_tier != value)
                {
                    _tier = value;
                    RaisePropertyChanged(nameof(Tier));
                }
            }
        }

        public short TierId
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

        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    RaisePropertyChanged(nameof(Level));
                }
            }
        }

        public short LevelId
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

        public string CashPrice
        {
            get
            {
                return _cashPrice;
            }
            set
            {
                if (_cashPrice != value)
                {
                    _cashPrice = value;
                    RaisePropertyChanged(nameof(CashPrice));
                }
            }
        }

        public string CreditPrice
        {
            get
            {
                return _creditPrice;
            }
            set
            {
                if (_creditPrice != value)
                {
                    _creditPrice = value;
                    RaisePropertyChanged(nameof(CreditPrice));
                }
            }
        }

        public string TaxExemptedCashPrice
        {
            get
            {
                return _taxExemptedCashPrice;
            }
            set
            {
                if (_taxExemptedCashPrice != value)
                {
                    _taxExemptedCashPrice = value;
                    RaisePropertyChanged(nameof(TaxExemptedCashPrice));
                }
            }
        }

        public string TaxExemptedCreditPrice
        {
            get
            {
                return _taxExemptedCreditPrice;
            }
            set
            {
                if (_taxExemptedCreditPrice != value)
                {
                    _taxExemptedCreditPrice = value;
                    RaisePropertyChanged(nameof(TaxExemptedCreditPrice));
                }
            }
        }

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
    }
}
