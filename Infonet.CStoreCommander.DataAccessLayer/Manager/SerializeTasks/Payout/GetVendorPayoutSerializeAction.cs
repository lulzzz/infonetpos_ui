using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payout
{
    public class GetVendorPayoutSerializeAction : SerializeAction
    {
        private readonly IPayoutRestClient _payoutRestClient;
        private readonly ICacheManager _cacheManager;

        public GetVendorPayoutSerializeAction(IPayoutRestClient payoutRestClient,
            ICacheManager cacheManager) : base("GetVendorPayout")
        {
            _cacheManager = cacheManager;
            _payoutRestClient = payoutRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _payoutRestClient.GetVendorPayout();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapVendorPayout(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
