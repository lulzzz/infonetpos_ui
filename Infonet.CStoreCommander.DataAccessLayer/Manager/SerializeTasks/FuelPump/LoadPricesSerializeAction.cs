using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class LoadPricesSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly bool _grouped;

        public LoadPricesSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            bool grouped)
            : base("LoadPrices")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _grouped = grouped;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.LoadPrices(_grouped);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapFuelPrices(data);
                    var entity = new Mapper().MapFuelPrices(contract);
                    entity.IsGrouped = _grouped;
                    return entity;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
