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
    public class VerifyBasePricesSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly FuelPrices _fuelPrices;

        public VerifyBasePricesSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            FuelPrices fuelPrices)
            : base("VerifyBasePrices")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _fuelPrices = fuelPrices;
        }

        protected async override Task<object> OnPerform()
        {
            var payLoad = new FuelPricesContract
            {
                fuelPrices = _fuelPrices != null ?
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
                 }).ToList() : new List<FuelPriceContract>(),
                isReadTotalizerChecked = _fuelPrices.IsReadTotalizerChecked,
                isReadTankDipChecked = _fuelPrices.IsReadTankDipChecked,
                isPricesToDisplayChecked = _fuelPrices.IsPricesToDisplayChecked,
                isTaxExemptionVisible = _fuelPrices.IsTaxExemptionVisible,
                canReadTotalizer = _fuelPrices.CanReadTotalizer,
                canSelectPricesToDisplay = _fuelPrices.CanSelectPricesToDisplay,
                caption = _fuelPrices.Caption,
                tillNumber = _cacheManager.TillNumberForSale
            };

            var contract = JsonConvert.SerializeObject(payLoad);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.VerifyBasePrices(content);
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
