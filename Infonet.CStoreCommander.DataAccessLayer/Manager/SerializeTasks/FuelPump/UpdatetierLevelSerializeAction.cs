using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class UpdateTierLevelSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly UpdateTierLevelContract _updateTierLevelContract;

        public UpdateTierLevelSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            List<int> pumpIds, int tierId, int levelId) : base("UpdateTierLevel")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _updateTierLevelContract = new UpdateTierLevelContract
            {
                pumpIds = pumpIds,
                levelId = levelId,
                tierId = tierId
            };
        }

        protected async override Task<object> OnPerform()
        {
            var updateTierLevelContract = JsonConvert.SerializeObject(_updateTierLevelContract);
            var content = new StringContent(updateTierLevelContract, Encoding.UTF8, ApplicationJSON);

            var response = await _fuelPumpRestClient.UpdateTierLevel(content);
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
