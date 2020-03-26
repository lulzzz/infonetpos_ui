using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class VerifyStockSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly int _saleNumber;
        private readonly int _tillNumber;
        private readonly int _registerNumber;
        private readonly string _stockCode;
        private readonly float _quantity;
        private readonly GiftCard _giftCard;
        private readonly bool _isReturn;
        private readonly bool _isManuallyAdded;

        public VerifyStockSerializeAction(ISaleRestClient saleRestClient,
             int saleNumber, int tillNumber,
             int registerNumber,
                string stockCode, float quantity, GiftCard giftcard, bool isReturn, bool isManuallyAdded)
            : base("VerifyStock")
        {
            _saleRestClient = saleRestClient;
            _saleNumber = saleNumber;
            _tillNumber = tillNumber;
            _registerNumber = registerNumber;
            _stockCode = stockCode;
            _quantity = quantity;
            _giftCard = giftcard;
            _isReturn = isReturn;
            _isManuallyAdded = isManuallyAdded;
        }

        protected override async Task<object> OnPerform()
        {
            var addStockToSale = new Mapper().MapAddStockToSale(_tillNumber,
                _saleNumber, _registerNumber, _stockCode, _quantity,
                _isReturn, _isManuallyAdded, _giftCard);
            var reason = JsonConvert.SerializeObject(addStockToSale);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _saleRestClient.VerifyStock(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSales = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(suspendedSales);
                case HttpStatusCode.PartialContent:
                    var verifyStock = new DeSerializer().MapVerifyStock(data);
                    return new Mapper().MapVerifyStock(verifyStock);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
