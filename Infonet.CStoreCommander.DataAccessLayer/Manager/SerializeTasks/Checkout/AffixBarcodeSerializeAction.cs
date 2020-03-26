using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class AffixBarcodeSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _cardNumber;
        private readonly string _barCode;

        public AffixBarcodeSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string cardNumber, string barCode) : base("AffixBarcode")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _cardNumber = cardNumber;
            _barCode = barCode;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.AffixBarcode(_cardNumber,
                _barCode);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapAffixBarcode(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
