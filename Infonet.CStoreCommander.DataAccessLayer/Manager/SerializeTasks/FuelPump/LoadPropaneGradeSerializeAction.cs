using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class LoadPropaneGradeSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public LoadPropaneGradeSerializeAction(IFuelPumpRestClient fuelPumpRestClient) 
            : base("LoadPropaneGrade")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.LoadPropaneGrade();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapPropaneGrades(data);
                    return new Mapper().MapPropaneGrades(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
