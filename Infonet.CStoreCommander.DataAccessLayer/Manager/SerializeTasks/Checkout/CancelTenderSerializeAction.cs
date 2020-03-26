using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    class CancelTenderSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _transactionType;

        public CancelTenderSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager, string transactionType)
            : base("CancelTender")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _transactionType = transactionType;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.CancelTenders(_transactionType);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var newSale = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(newSale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
