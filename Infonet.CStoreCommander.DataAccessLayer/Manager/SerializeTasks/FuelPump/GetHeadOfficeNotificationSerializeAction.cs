using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class GetHeadOfficeNotificationSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public GetHeadOfficeNotificationSerializeAction(IFuelPumpRestClient fuelPumpRestClient) 
            : base("GetHeadOfficeNotification")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.GetHeadOfficeNotification();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapPumpMessage(data);
                    return new Mapper().MapPumpMessage(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
