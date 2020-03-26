using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice
{
    public class SetPriceDecrementSerializeAction : SerializeAction
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;
        private readonly PriceDecrement _priceDecrement;
        private readonly bool _taxExempt;

        public SetPriceDecrementSerializeAction(IFuelPriceRestClient fuelPriceRestClient,
            PriceDecrement priceDecrement,
            bool taxExempt)
            : base("SetPriceDecrement")
        {
            _fuelPriceRestClient = fuelPriceRestClient;
            _priceDecrement = priceDecrement;
            _taxExempt = taxExempt;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                price = new
                {
                    row = _priceDecrement.Row,
                    tierId = _priceDecrement.TierId,
                    levelId = _priceDecrement.LevelId,
                    tierLevel = _priceDecrement.TierLevel,
                    cash = _priceDecrement.Cash,
                    credit = _priceDecrement.Credit
                },
                taxExempt = _taxExempt
            };

            var payload = JsonConvert.SerializeObject(contract);
            var content = new StringContent(payload, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPriceRestClient.SetPriceDecrement(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSetPriceDecrement(data);
                    return new Mapper().MapSetPriceDecrement(result);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
