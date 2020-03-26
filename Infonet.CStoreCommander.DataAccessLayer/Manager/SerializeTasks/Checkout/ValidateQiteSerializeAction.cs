using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class ValidateQiteSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _bandMember;

        public ValidateQiteSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string bandMember) : base("ValidateQite")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _bandMember = bandMember;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.ValidateQite(_cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale, _cacheManager.ShiftNumber,
                _cacheManager.RegisterNumber, _bandMember);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var qiteContract = new DeSerializer().MapValidateQite(data);
                    return new Mapper().MapQiteValidate(qiteContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
