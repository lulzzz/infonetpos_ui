using GalaSoft.MvvmLight;
using Infonet.CStoreCommander.EntityLayer.Entities;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infonet.CStoreCommander.UI.Model.Sale
{
    public class SaleModel : ViewModelBase
    {
        private int _tillNumber;
        private int _saleNumber;
        private string _customer;
        private string _totalAmount;
        private string _summary;
        private bool _enableExactChange;
        private bool _hasCarwashProducts;

        public SaleModel()
        {
            SaleLines = new ObservableCollection<SaleLineModel>();
        }

        private ObservableCollection<SaleLineModel> _saleLines;

        public LineDisplayModel LineDisplay { get; set; }

        public bool EnableWriteOffButton { get; set; }

        public bool EnableExactChange
        {
            get { return _enableExactChange; }
            set
            {
                if (_enableExactChange != value)
                {
                    _enableExactChange = value;
                    RaisePropertyChanged(nameof(EnableExactChange));
                }
            }
        }

        public int TillNumber
        {
            get { return _tillNumber; }
            set
            {
                if (_tillNumber != value)
                {
                    _tillNumber = value;
                    RaisePropertyChanged(nameof(TillNumber));
                }
            }
        }

        public int SaleNumber
        {
            get { return _saleNumber; }
            set
            {
                if (_saleNumber != value)
                {
                    _saleNumber = value;
                    RaisePropertyChanged(nameof(SaleNumber));
                }
            }
        }

        public string Customer
        {
            get { return _customer; }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    RaisePropertyChanged(nameof(Customer));
                }
            }
        }

        public string TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    RaisePropertyChanged(nameof(TotalAmount));
                }
            }
        }

        public string Summary
        {
            get { return _summary; }
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    RaisePropertyChanged(nameof(Summary));
                }
            }
        }

        public ObservableCollection<SaleLineModel> SaleLines
        {
            get { return _saleLines; }
            set
            {
                _saleLines = value;
                RaisePropertyChanged(nameof(SaleLines));
            }
        }


        public bool HasCarwashProducts
        {
            get { return _hasCarwashProducts; }
            set
            {
                _hasCarwashProducts = value;
                RaisePropertyChanged(nameof(HasCarwashProducts));
            }
        }
        
        public List<Error> SaleLineError { get; set; }
            = new List<Error>();
    }

    public class SaleLineModel : ViewModelBase
    {
        #region Private Variable
        private string _code;
        private string _description;
        private string _quantity;
        private string _price;
        private string _discount;
        private string _amount;
        private string _dept;
        private bool _allowPriceChange;
        private bool _allowQuantityChange;
        private bool _allowDiscountChange;
        private bool _allowDiscoutReason;
        private bool _allowPriceReason;
        private bool _allowReturnReason;
        private bool _allowStockCodeChange;
        private int _lineNumber;
        #endregion

        public string Dept
        {
            get { return _dept; }
            set
            {
                if (_dept != value)
                {
                    _dept = value;
                    RaisePropertyChanged(nameof(Dept));
                }

            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged(nameof(Amount));
                }
            }
        }

        public string Discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    RaisePropertyChanged(nameof(Discount));
                }
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    RaisePropertyChanged(nameof(Price));
                }
            }
        }

        public string Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    RaisePropertyChanged(nameof(Code));
                }
            }
        }

        public bool AllowPriceChange
        {
            get { return _allowPriceChange; }
            set
            {
                if (_allowPriceChange != value)
                {
                    _allowPriceChange = value;
                    RaisePropertyChanged(nameof(AllowPriceChange));
                }
            }
        }

        public bool AllowQuantityChange
        {
            get { return _allowQuantityChange; }
            set
            {
                if (_allowQuantityChange != value)
                {
                    _allowQuantityChange = value;
                    RaisePropertyChanged(nameof(AllowQuantityChange));
                }
            }
        }

        public bool AllowDiscountChange
        {
            get { return _allowDiscountChange; }
            set
            {
                if (_allowDiscountChange != value)
                {
                    _allowDiscountChange = value;
                    RaisePropertyChanged(nameof(AllowDiscountChange));
                }
            }
        }

        public bool AllowDiscoutReason
        {
            get { return _allowDiscoutReason; }
            set
            {
                if (_allowDiscoutReason != value)
                {
                    _allowDiscoutReason = value;
                    RaisePropertyChanged(nameof(AllowDiscoutReason));
                }
            }
        }

        public bool AllowPriceReason
        {
            get { return _allowPriceReason; }
            set
            {
                if (_allowPriceReason != value)
                {
                    _allowPriceReason = value;
                    RaisePropertyChanged(nameof(AllowPriceReason));
                }
            }
        }

        public bool AllowReturnReason
        {
            get { return _allowReturnReason; }
            set
            {
                if (_allowReturnReason != value)
                {
                    _allowReturnReason = value;
                    RaisePropertyChanged(nameof(AllowReturnReason));
                }
            }
        }

        public bool AllowStockCodeChange
        {
            get { return _allowStockCodeChange; }
            set
            {
                if (_allowStockCodeChange != value)
                {
                    _allowStockCodeChange = value;
                    RaisePropertyChanged(nameof(AllowStockCodeChange));
                }
            }
        }

        public int LineNumber
        {
            get { return _lineNumber; }
            set
            {
                if (_lineNumber != value)
                {
                    _lineNumber = value;
                    RaisePropertyChanged(nameof(LineNumber));
                }
            }
        }

        public SaleLineModel Clone()
        {
            return new SaleLineModel
            {
                AllowDiscountChange = AllowDiscountChange,
                AllowDiscoutReason = AllowDiscoutReason,
                AllowPriceChange = AllowPriceChange,
                AllowPriceReason = AllowPriceReason,
                AllowQuantityChange = AllowQuantityChange,
                AllowReturnReason = AllowReturnReason,
                AllowStockCodeChange = AllowStockCodeChange,
                Amount = Amount,
                Code = Code,
                Description = Description,
                Discount = Discount,
                LineNumber = LineNumber,
                Price = Price,
                Quantity = Quantity
            };
        }
    }
}
