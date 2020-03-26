using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class AddBasketSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly AddBasketContract _addBasketContract;

        public AddBasketSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int activePump, string basketValue) : base("AddBasket")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _addBasketContract = new AddBasketContract
            {
                activePump = activePump,
                basketValue = basketValue,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                registerNumber = _cacheManager.RegisterNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var addBasketContract = JsonConvert.SerializeObject(_addBasketContract);
            var content = new StringContent(addBasketContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.AddBasket(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
