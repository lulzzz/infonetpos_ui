using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Sale
{
    public class SuspendSaleModel : ViewModelBase
    {
        private int _saleNumber;
        private string _customer;
        private int _till;

        public int Till
        {
            get { return _till; }
            set
            {
                _till = value;
                RaisePropertyChanged(nameof(Till));
            }
        }

        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                RaisePropertyChanged(nameof(Customer));
            }
        }

        public int SaleNumber
        {
            get { return _saleNumber; }
            set
            {
                _saleNumber = value;
                RaisePropertyChanged(nameof(SaleNumber));
            }
        }
    }
}
