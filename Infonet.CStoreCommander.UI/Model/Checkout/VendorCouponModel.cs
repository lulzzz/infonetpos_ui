using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class VendorCouponModel : ViewModelBase
    {
        private string _serialNumber;
        private string _coupon;

        public string Coupon
        {
            get { return _coupon; }
            set
            {
                if (value != _coupon)
                {
                    _coupon = value;
                    RaisePropertyChanged(nameof(Coupon));
                }
            }
        }


        public string SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                    RaisePropertyChanged(nameof(SerialNumber));
                }
            }
        }
    }

}
