using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice
{
    public class SavePricesToDisplaySerializeAction : SerializeAction
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;
        private readonly List<string> _grades;
        private readonly List<string> _tiers;
        private readonly List<string> _levels;

        public SavePricesToDisplaySerializeAction(IFuelPriceRestClient fuelPriceRestClient,
            List<string> grades, List<string> tiers, List<string> levels)
            : base("SavePricesToDisplay")
        {
            _fuelPriceRestClient = fuelPriceRestClient;
            _grades = grades;
            _levels = levels;
            _tiers = tiers;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                selectedGrades = _grades,
                selectedTiers = _tiers,
                selectedLevels = _levels
            };

            var payload = JsonConvert.SerializeObject(contract);
            var content = new StringContent(payload, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPriceRestClient.SavePricesToDisplay(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSuccess(data);
                    return result.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
