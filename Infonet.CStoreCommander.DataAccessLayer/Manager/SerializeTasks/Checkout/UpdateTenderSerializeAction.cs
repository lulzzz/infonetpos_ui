using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class UpdateTenderSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _code;
        private readonly decimal? _amount;
        private readonly string _transactionType;
        private readonly bool _isAmountEnteredManually;

        public UpdateTenderSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager, string code,
            decimal? amount,
            string transactionType,
            bool isAmountEnteredManually)
            : base("UpdateTender")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _code = code;
            _amount = amount;
            _transactionType = transactionType;
            _isAmountEnteredManually = isAmountEnteredManually;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.UpdateTender(
                _cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale,
               _code,
               _amount,
               _transactionType, _isAmountEnteredManually);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tendersSummary = new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(tendersSummary);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
