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
    public class CardSwipeSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private CardSwipeContract _cardSwipe;

        public CardSwipeSerializeAction(ICheckoutRestClient checkoutRestClient,
            ICacheManager cacheManager,string cardNumber, string transactionType)
            :base("SwipeCard")
        {
            _cacheManager = cacheManager;
            _restClient = checkoutRestClient;
            _cardSwipe = new CardSwipeContract
            {
                cardNumber = cardNumber,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                transactionType = transactionType
            };
        }

        protected async override Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_cardSwipe)
                , Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.GetCardInformation(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tenderInformation = new DeSerializer().MapGetCardInformation(data);
                    return new Mapper().MapCardTenderInformation(tenderInformation);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
