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
    public class SaveGroupBasePricesSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly FuelPrices _fuelPrices;

        public SaveGroupBasePricesSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            FuelPrices fuelPrices)
            : base("SaveGroupBasePrices")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _fuelPrices = fuelPrices;
        }

        protected async override Task<object> OnPerform()
        {
            var payLoad = new GroupFuelPricesPayLoadContract
            {
                tillNumber = _cacheManager.TillNumberForSale,
                isPricesToDisplayChecked = _fuelPrices.IsPricesToDisplayChecked,
                isReadTankDipChecked = _fuelPrices.IsReadTankDipChecked,
                isReadTotalizerChecked = _fuelPrices.IsReadTotalizerChecked,
                groupFuelPrices = _fuelPrices != null ?
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
                 }).ToList() : new List<FuelPriceContract>()
            };

            var contract = JsonConvert.SerializeObject(payLoad);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.SaveGroupBasePrices(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var errorContract = new DeSerializer().MapErrorWithCaption(data);
                    return new Mapper().MapErrorWithCaption(errorContract);
                case HttpStatusCode.Conflict:
                    throw PumpsOfflineException(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
