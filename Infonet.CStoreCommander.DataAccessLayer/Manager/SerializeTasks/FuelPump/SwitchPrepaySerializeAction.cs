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
    public class SwitchPrepaySerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly SwitchPrepayContract _switchPrepayContract;

        public SwitchPrepaySerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int activePump, int newPumpId) 
            : base("SwitchPrepay")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _switchPrepayContract = new SwitchPrepayContract
            {
                activePump = activePump,
                newPumpId = newPumpId,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var switchContract = JsonConvert.SerializeObject(_switchPrepayContract);
            var content = new StringContent(switchContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.SwitchPrepay(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var success = new DeSerializer().MapBool(data);
                    return success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
