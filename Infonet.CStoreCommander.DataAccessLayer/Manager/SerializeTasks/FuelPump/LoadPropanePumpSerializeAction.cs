using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class LoadPropanePumpSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly int _gradeId;

        public LoadPropanePumpSerializeAction(IFuelPumpRestClient fuelPumpRestClient, int gradeId) 
            : base("LoadPropanePump")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _gradeId = gradeId;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.LoadPropanePumps(_gradeId);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var propanePumpsContract = new DeSerializer().MapPropanePumps(data);
                    return new Mapper().MapPropanePumps(propanePumpsContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
