using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class UpdateFuelPriceSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly int _option;
        private readonly int _counter;

        public UpdateFuelPriceSerializeAction(IFuelPumpRestClient fuelPumpRestClient, int option, int counter)
            : base("UpdateFuelPrice")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _option = option;
            _counter = counter;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.UpdateFuelPrice(_option,_counter);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var pageName = new DeSerializer().MapString(data);
                    return pageName;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
