using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class UncompletePrepayLoadSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public UncompletePrepayLoadSerializeAction(IFuelPumpRestClient fuelPumpRestClient) 
            : base("UncompletePrepayLoad")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.UncompletePrepayLoad();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapUncompletePrepayLoad(data);
                    return new Mapper().MapUncompletePrepayLoad(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
