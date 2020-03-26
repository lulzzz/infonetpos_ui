using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.FuelPump
{
    public class UncompleteSaleModel : ViewModelBase
    {
        private int _pumpId;
        private int _positionId;
        private int _saleNumber;
        private string _prepayAmount;
        private string _prepayVolume;
        private string _usedAmount;
        private string _usedVolume;
        private int _grade;
        private string _unitPrice;
        private int _salePosition;
        private int _saleGrade;
        private string _regPrice;
        private int _mop;
        
        public string UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value != _unitPrice)
                {
                    _unitPrice = value;
                    RaisePropertyChanged(nameof(UnitPrice));
                }
            }
        }

        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value != _grade)
                {
                    _grade = value;
                    RaisePropertyChanged(nameof(Grade));
                }
            }
        }

        public string UsedVolume
        {
            get { return _usedVolume; }
            set
            {
                if (value != _usedVolume)
                {
                    _usedVolume = value;
                    RaisePropertyChanged(nameof(UsedVolume));
                }
            }
        }

        public string UsedAmount
        {
            get { return _usedAmount; }
            set
            {
                if (value != _usedAmount)
                {
                    _usedAmount = value;
                    RaisePropertyChanged(nameof(UsedAmount));
                }
            }
        }

        public string PrepayVolume
        {
            get { return _prepayVolume; }
            set
            {
                if (value != _prepayVolume)
                {
                    _prepayVolume = value;
                    RaisePropertyChanged(nameof(PrepayVolume));
                }
            }
        }

        public string PrepayAmount
        {
            get { return _prepayAmount; }
            set
            {
                if (value != _prepayAmount)
                {
                    _prepayAmount = value;
                    RaisePropertyChanged(nameof(PrepayAmount));
                }
            }
        }

        public int SaleNumber
        {
            get { return _saleNumber; }
            set
            {
                if (value != _saleNumber)
                {
                    _saleNumber = value;
                    RaisePropertyChanged(nameof(SaleNumber));
                }
            }
        }

        public int PositionId
        {
            get { return _positionId; }
            set
            {
                if (value != _positionId)
                {
                    _positionId = value;
                    RaisePropertyChanged(nameof(PositionId));
                }
            }
        }

        public int PumpId
        {
            get { return _pumpId; }
            set
            {
                if (value != _pumpId)
                {
                    _pumpId = value;
                    RaisePropertyChanged(nameof(PumpId));
                }
            }
        }
      
        public int Mop
        {
            get { return _mop; }
            set
            {
                if (_mop != value)
                {
                    _mop = value;
                    RaisePropertyChanged(nameof(Mop));
                }
            }
        }

        public string RegPrice
        {
            get { return _regPrice; }
            set
            {
                if (value != _regPrice)
                {
                    _regPrice = value;
                    RaisePropertyChanged(nameof(RegPrice));
                }
            }
        }

        public int SaleGrade
        {
            get { return _saleGrade; }
            set
            {
                if (value != _saleGrade)
                {
                    _saleGrade = value;
                    RaisePropertyChanged(nameof(SaleGrade));
                }
            }
        }

        public int SalePosition
        {
            get { return _salePosition; }
            set
            {
                if (value != _salePosition)
                {
                    _salePosition = value;
                    RaisePropertyChanged(nameof(SalePosition));
                }
            }
        }
    }

    public class UncompletePrepayModel : ViewModelBase
    {
        private bool _isDeleteEnabled;
        private bool _isChangeEnabled;
        private bool _isOverPaymentEnabled;
        private bool _isDeleteVisible;
        private string _caption;

        public string Caption
        {
            get { return _caption; }
            set
            {
                if (value != _caption)
                {
                    _caption = value;
                    RaisePropertyChanged(nameof(Caption));
                }
            }
        }

        public bool IsDeleteVisible
        {
            get { return _isDeleteVisible; }
            set
            {
                if (value != _isDeleteVisible)
                {
                    _isDeleteVisible = value;
                    RaisePropertyChanged(nameof(IsDeleteVisible));
                }
            }
        }

        public bool IsOverPaymentEnabled
        {
            get { return _isOverPaymentEnabled; }
            set
            {
                if (value != _isOverPaymentEnabled)
                {
                    _isOverPaymentEnabled = value;
                    RaisePropertyChanged(nameof(IsOverPaymentEnabled));
                }
            }
        }

        public bool IsChangeEnabled
        {
            get { return _isChangeEnabled; }
            set
            {
                if (value != _isChangeEnabled)
                {
                    _isChangeEnabled = value;
                    RaisePropertyChanged(nameof(IsChangeEnabled));
                }
            }
        }

        public bool IsDeleteEnabled
        {
            get { return _isDeleteEnabled; }
            set
            {
                if (value != _isDeleteEnabled)
                {
                    _isDeleteEnabled = value;
                    RaisePropertyChanged(nameof(IsDeleteEnabled));
                }
            }
        }

        public ObservableCollection<UncompleteSaleModel> UncompleteSale { get; set; }
        = new ObservableCollection<UncompleteSaleModel>();
    }
}
