using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class GetPumpActionSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly int _pumpId;
        private readonly bool _isStopPressed;
        private readonly bool _isResumePressed;

        public GetPumpActionSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            int pumpId, bool isStopPressed, bool isResumePressed)
            : base("GetPumpAction")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _pumpId = pumpId;
            _isStopPressed = isStopPressed;
            _isResumePressed = isResumePressed;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.GetPumpAction(_pumpId, _isStopPressed, _isResumePressed);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var pumpAction = new DeSerializer().MapPumpAction(data);
                    return new Mapper().MapPumpAction(pumpAction);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
