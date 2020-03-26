using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class PaymentByCouponSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _checkoutRestClient;
        private readonly ICacheManager _cacheManager;
        private CouponContract _couponContract;

        public PaymentByCouponSerializeAction(ICheckoutRestClient checkoutRestClient,
            ICacheManager cacheManager, string couponCode,
            string tenderCode) : base("PaymentByCoupon")
        {
            _checkoutRestClient = checkoutRestClient;
            _cacheManager = cacheManager;
            _couponContract = new CouponContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                transactionType = "Sale",
                couponNumber = couponCode,
                tenderCode = tenderCode,
                blTillClose = false
            };
        }

        protected async override Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_couponContract)
                , Encoding.UTF8, ApplicationJSON);
            var response = await _checkoutRestClient.PaymentByCoupon(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var paymentByCoupon = new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(paymentByCoupon);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
