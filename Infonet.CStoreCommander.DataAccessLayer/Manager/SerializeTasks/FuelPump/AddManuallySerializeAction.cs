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
    public class AddManuallySerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly AddFuelManuallyContract _payload;

        public AddManuallySerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            int pumpId,
            string amount,
            bool isCashSelected,
            string grade) : base("AddManually")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _payload = new AddFuelManuallyContract
            {
                amount = amount,
                grade = grade,
                isCashSelected = isCashSelected,
                pumpId = pumpId,
                registerNumber = _cacheManager.RegisterNumber,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale
            };
        }

        protected async override Task<object> OnPerform()
        {
            var contract = JsonConvert.SerializeObject(_payload);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.AddManually(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContract = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(responseContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
