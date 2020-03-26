using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class InitializeFuelPumpSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly bool _isInitializingPump;
        private readonly int _tillNumber;

        public InitializeFuelPumpSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            bool isInitializingPump,
            int tillNumber)
            : base("InitializeFuelPump")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _isInitializingPump = isInitializingPump;
            _tillNumber = tillNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = new HttpResponseMessage();

            if (_isInitializingPump)
            {
                response = await _fuelPumpRestClient.InitializeFuelPump();
            }
            else
            {
                response = await _fuelPumpRestClient.GetPumpStatus(_tillNumber);
            }

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapInitializeFuelPump(data);
                    return new Mapper().MapInitializeFuelPump(contract);
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
