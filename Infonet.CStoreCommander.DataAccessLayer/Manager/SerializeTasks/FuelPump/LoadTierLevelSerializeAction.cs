using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class LoadTierLevelSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public LoadTierLevelSerializeAction(IFuelPumpRestClient fuelPumpRestClient)
            : base("LoadTierLevel")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.LoadTierLevel();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapTierLevel(data);
                    return new Mapper().MapTierLevel(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
