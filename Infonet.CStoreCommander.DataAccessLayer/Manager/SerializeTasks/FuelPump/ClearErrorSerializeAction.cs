using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class ClearErrorSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public ClearErrorSerializeAction(IFuelPumpRestClient fuelPumpRestClient) 
            : base("ClearError")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.ClearError();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapBool(data);
                    return result;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
