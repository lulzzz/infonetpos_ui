using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class CheckErrorSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public CheckErrorSerializeAction(IFuelPumpRestClient fuelPumpRestClient) : base("CheckError")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.CheckError();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContract = new DeSerializer().MapBool(data);
                    return responseContract;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
