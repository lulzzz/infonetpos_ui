using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class ValidateAITESerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _cardNumber;
        private readonly string _barCode;
        private readonly bool _isCardNumber;

        public ValidateAITESerializeAction(ICheckoutRestClient restClient,
                ICacheManager cacheManager, string cardNumber,
                string barcode, bool isCardNumber) :
            base("ValidateAITE")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _cardNumber = cardNumber;
            _barCode = barcode;
            _isCardNumber = isCardNumber;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.ValidateAITE(
                _cacheManager.SaleNumber, _cacheManager.TillNumberForSale,
                _cacheManager.ShiftNumber, _cacheManager.RegisterNumber,
                _cardNumber, _barCode, _isCardNumber ? 2 : 1);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var validateAite = new DeSerializer().MapValidateAITE(data);
                    return new Mapper().MapValidateAITE(validateAite);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
