using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class VerifyGroupBasePricesSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly FuelPrices _fuelPrices;

        public VerifyGroupBasePricesSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            FuelPrices fuelPrices)
            : base("VerifyGroupBasePrices")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _fuelPrices = fuelPrices;
        }

        protected async override Task<object> OnPerform()
        {
            var payLoad = _fuelPrices != null ?
                (from f in _fuelPrices.Prices
                 select new FuelPriceContract
                 {
                     cashPrice = f.CashPrice,
                     creditPrice = f.CreditPrice,
                     grade = f.Grade,
                     gradeId = f.GradeId,
                     level = f.Level,
                     levelId = f.LevelId,
                     taxExemptedCashPrice = f.TaxExemptedCashPrice,
                     taxExemptedCreditPrice = f.TaxExemptedCreditPrice,
                     tier = f.Tier,
                     tierId = f.TierId,
                     row = f.Row
                 }).ToList() : new List<FuelPriceContract>();

            var contract = JsonConvert.SerializeObject(payLoad);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.VerifyGroupBasePrices(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContract = new DeSerializer().MapSuccess(data);
                    return responseContract?.success;
                case HttpStatusCode.Conflict:
                    throw PumpsOfflineException(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
