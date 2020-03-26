using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class GetVendorCouponSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _checkoutRestClient;
        private readonly string _tenderCode;

        public GetVendorCouponSerializeAction(ICheckoutRestClient checkoutRestClient,
            string tenderCode) 
            : base("GetVendorCoupon")
        {
            _checkoutRestClient = checkoutRestClient;
            _tenderCode = tenderCode;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _checkoutRestClient.GetVendorCoupon(_tenderCode);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var vendorCoupon = new DeSerializer().MapVendorCoupon(data);
                    return new Mapper().MapVendorCoupon(vendorCoupon);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
