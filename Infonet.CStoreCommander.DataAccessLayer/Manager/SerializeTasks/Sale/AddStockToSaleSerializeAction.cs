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
    public class AddStockToSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly int _saleNumber;
        private readonly int _tillNumber;
        private readonly int _registerNumber;
        private readonly string _stockCode;
        private readonly float _quantity;
        private readonly bool _isReturn;
        private readonly bool _isManuallyAdded;
        private readonly GiftCard _giftCard;

        public AddStockToSaleSerializeAction(ISaleRestClient saleRestClient,
            int saleNumber, int tillNumber,
            int registerNumber,
                string stockCode,
                float quantity,
                bool isReturn,
                GiftCard giftCard, bool isManuallyAdded)
            : base("AddStockToSale")
        {
            _saleRestClient = saleRestClient;
            _saleNumber = saleNumber;
            _tillNumber = tillNumber;
            _stockCode = stockCode;
            _registerNumber = registerNumber;
            _quantity = quantity;
            _isReturn = isReturn;
            _giftCard = giftCard;
            _isManuallyAdded = isManuallyAdded;
        }

        protected override async Task<object> OnPerform()
        {
            var addStockToSale = new Mapper().MapAddStockToSale(_tillNumber,
                _saleNumber, _registerNumber, _stockCode, _quantity,
                _isReturn, _isManuallyAdded, _giftCard);
            var reason = JsonConvert.SerializeObject(addStockToSale);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _saleRestClient.AddStockToSale(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSales = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(suspendedSales);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
