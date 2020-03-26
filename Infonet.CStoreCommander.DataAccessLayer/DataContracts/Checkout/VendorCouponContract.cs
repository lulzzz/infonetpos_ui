using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class VendorCouponContract
    {
        public string defaultCoupon { get; set; }
        public List<SaleVendorCouponContract> saleVendorCoupons { get; set; }
    }

    public class AddVendorCouponContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string tenderCode { get; set; }
        public string couponNumber { get; set; }
        public string serialNumber { get; set; }
    }

    public class RemoveVendorCouponContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string tenderCode { get; set; }
        public string couponNumber { get; set; }
    }

    public class PaymentByVendorCouponContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string tenderCode { get; set; }
    }
}