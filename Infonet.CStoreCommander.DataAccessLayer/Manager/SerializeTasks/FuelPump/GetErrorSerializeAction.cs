using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class GetErrorSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;

        public GetErrorSerializeAction(IFuelPumpRestClient fuelPumpRestClient) 
            : base("GetError")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPumpRestClient.GetError();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapFuelPumpError(data);
                    var decodedString = Convert.FromBase64String(contract);
                    return Encoding.UTF8.GetString(decodedString);
                   
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
