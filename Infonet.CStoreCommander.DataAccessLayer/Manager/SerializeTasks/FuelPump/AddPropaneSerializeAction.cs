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
    public class AddPropaneSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly AddPropane _addPropane;

        public AddPropaneSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int gradeId, int pumpId, string propaneValue, 
            bool isAmount) : base("AddPropane")
        {
            _fuelPumpRestClient = fuelPumpRestClient;

            _addPropane = new AddPropane
            {
                gradeId = gradeId,
                isAmount = isAmount,
                propaneValue = propaneValue,
                pumpId = pumpId,
                registerNumber = cacheManager.RegisterNumber,
                saleNumber = cacheManager.SaleNumber,
                tillNumber = cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_addPropane);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _fuelPumpRestClient.AddPropane(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSales = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(suspendedSales);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
