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
    public class RemoveVendorCouponSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly RemoveVendorCouponContract _removeVendorCouponContract;

        public RemoveVendorCouponSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string tenderCode,
            string couponNumber)
            : base("AddVendorCouponSerializeAction")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;

            _removeVendorCouponContract = new RemoveVendorCouponContract
            {
                couponNumber = couponNumber,
                saleNumber = _cacheManager.SaleNumber,
                tenderCode = tenderCode,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var payload = JsonConvert.SerializeObject(_removeVendorCouponContract);
            var content = new StringContent(payload, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.RemoveVendorCoupon(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tenderSummary = new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(tenderSummary);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
