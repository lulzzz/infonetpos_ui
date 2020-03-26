using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class UncompleteDeleteSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly int _pumpId;
        public UncompleteDeleteSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
             ICacheManager cacheManager, int pumpId) : base("UncompleteDelete")
        {
            _pumpId = pumpId;
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.UncompleteDelete(_pumpId);
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
