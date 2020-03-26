using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice
{
    public class LoadPricesToDisplaySerializeAction : SerializeAction
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;

        public LoadPricesToDisplaySerializeAction(IFuelPriceRestClient fuelPriceRestClient)
            : base("LoadPricesToDisplay")
        {
            _fuelPriceRestClient = fuelPriceRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPriceRestClient.LoadPricesToDisplay();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var prices = new DeSerializer().MapPricesToDisplay(data);
                    return new Mapper().MapPricesToDisplay(prices);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
