using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class ReadTotalizerSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;

        public ReadTotalizerSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager) 
            : base("ReadTotalizer")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.ReadTotalizer(_cacheManager.TillNumberForSale);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapSuccess(data);
                    return contract.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
