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
    public class DeletePrepaySerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly DeletePrepayContract _addPrepayContract;

        public DeletePrepaySerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int activePump) 
            : base("DeletePrepay")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _addPrepayContract = new DeletePrepayContract
            {
                activePump = activePump,
                registerNumber = _cacheManager.RegisterNumber,
                saleNumber = _cacheManager.SaleNumber,
                shiftNumber = _cacheManager.ShiftNumber,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var addContract = JsonConvert.SerializeObject(_addPrepayContract);
            var content = new StringContent(addContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.DeletePrepay(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract =new  DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
