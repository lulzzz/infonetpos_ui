using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class LoadGradeSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly int _pumpId;
        private readonly bool _switchPrepay;
        private readonly int _tillNumber;

        public LoadGradeSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            int pumpId, bool switchPrepay, int tillNumber)
            : base("LoadGrade")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _pumpId = pumpId;
            _switchPrepay = switchPrepay;
            _tillNumber = tillNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.LoadGrade(_pumpId, _switchPrepay, _tillNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapGrades(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
