using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Sale
{
    public class BottleReturnSaleModel : ViewModelBase
    {
        private ObservableCollection<BottleReturnSaleLineModel> _saleLines;
        private decimal _totalAmount;

        public ObservableCollection<BottleReturnSaleLineModel> SaleLines
        {
            get { return _saleLines; }
            set
            {
                _saleLines = value;
                RaisePropertyChanged(nameof(SaleLines));
            }
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                RaisePropertyChanged(nameof(TotalAmount));
            }
        }

        public byte Register { get; set; }

        public int TillNumber { get; set; }

        public int SaleNumber { get; set; }

        public BottleReturnSaleModel()
        {
            SaleLines = new ObservableCollection<BottleReturnSaleLineModel>();
        }
    }

    public class BottleReturnSaleLineModel : ViewModelBase
    {
        private string _stockCode;
        private int _lineNumber;
        private string _description;
        private int _quantity;
        private string _price;
        private decimal _discount;
        private string _amount;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public decimal Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                RaisePropertyChanged(nameof(Discount));
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                RaisePropertyChanged(nameof(StockCode));
            }
        }

        public int LineNumber
        {
            get { return _lineNumber; }
            set
            {
                _lineNumber = value;
                RaisePropertyChanged(nameof(LineNumber));
            }
        }
    }
}
