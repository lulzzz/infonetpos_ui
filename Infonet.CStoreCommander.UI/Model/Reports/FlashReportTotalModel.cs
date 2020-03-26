using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Reports
{
    public class FlashReportTotalModel : ViewModelBase
    {
        private string _productSales;
        private string _lineDiscounts;
        private string _invoiceDiscount;
        private string _salesAfterDiscount;
        private string _taxes;
        private string _charges;
        private string _refunded;
        private string _totalsReceipts;

        public string TotalsReceipts
        {
            get { return _totalsReceipts; }
            set
            {
                _totalsReceipts = value;
                RaisePropertyChanged(nameof(TotalsReceipts));
            }
        }


        public string Refunded
        {
            get { return _refunded; }
            set
            {
                _refunded = value;
                RaisePropertyChanged(nameof(Refunded));
            }
        }


        public string Charges
        {
            get { return _charges; }
            set
            {
                _charges = value;
                RaisePropertyChanged(nameof(Charges));
            }
        }


        public string Taxes
        {
            get { return _taxes; }
            set
            {
                _taxes = value;
                RaisePropertyChanged(nameof(Taxes));
            }
        }


        public string SalesAfterDiscount
        {
            get { return _salesAfterDiscount; }
            set
            {
                _salesAfterDiscount = value;
                RaisePropertyChanged(nameof(SalesAfterDiscount));
            }
        }


        public string InvoiceDiscount
        {
            get { return _invoiceDiscount; }
            set
            {
                _invoiceDiscount = value;
                RaisePropertyChanged(nameof(InvoiceDiscount));
            }
        }


        public string LineDiscounts
        {
            get { return _lineDiscounts; }
            set
            {
                _lineDiscounts = value;
                RaisePropertyChanged(nameof(LineDiscounts));
            }
        }


        public string ProductSales
        {
            get { return _productSales; }
            set
            {
                _productSales = value;
                RaisePropertyChanged(nameof(ProductSales));
            }
        }

    }
}
