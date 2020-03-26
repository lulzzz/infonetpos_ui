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
    public class AddPrepaySerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly AddPrepayContract _addPrepayContract;

        public AddPrepaySerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int activePump, string amount, string fuelGrade,
            bool isAmountCash)
            : base("AddPrepay")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _addPrepayContract = new AddPrepayContract
            {
                activePump = activePump,
                registerNumber = _cacheManager.RegisterNumber,
                saleNumber = _cacheManager.SaleNumber,
                shiftNumber = _cacheManager.ShiftNumber,
                tillNumber = _cacheManager.TillNumber,
                amount = amount,
                fuelGrade = fuelGrade,
                isAmountCash = isAmountCash
            };
        }

        protected async override Task<object> OnPerform()
        {
            var deleteContract = JsonConvert.SerializeObject(_addPrepayContract);
            var content = new StringContent(deleteContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.AddPrepay(content);
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
