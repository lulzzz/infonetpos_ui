using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class CompleteOverLimitsSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _reason;
        private readonly string _explanation;
        private readonly string _location;
        private readonly DateTime _date;

        public CompleteOverLimitsSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager, string reason,
            string explanation, string location, DateTime date)
            : base("CompleteOverLimits")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _reason = reason;
            _explanation = explanation;
            _location = location;
            _date = date;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.CompleteOverLimit(
                _cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale,
                _reason,
                _explanation,
                _location,
                _date);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleSumarry = new DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(saleSumarry);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
