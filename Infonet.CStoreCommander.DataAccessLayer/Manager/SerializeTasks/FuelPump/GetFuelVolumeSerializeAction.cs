using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class GetFuelVolumeSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly AddPropane _addPropane;

        public GetFuelVolumeSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, int gradeId, int pumpId, string propaneValue) : base("GetFuelVolume")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _addPropane = new AddPropane
            {
                gradeId = gradeId,
                propaneValue = propaneValue,
                pumpId = pumpId,
                registerNumber = cacheManager.RegisterNumber,
                saleNumber = cacheManager.SaleNumber,
                tillNumber = cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_addPropane);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _fuelPumpRestClient.GetFuelVolume(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var volume = new DeSerializer().MapString(data);
                    return volume;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
