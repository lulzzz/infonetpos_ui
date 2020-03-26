using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class VendorCoupon
    {
        public string DefaultCoupon { get; set; }

        public List<SaleVendorCoupon> SaleVendorCoupons { get; set; }
    }
}
