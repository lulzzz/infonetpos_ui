using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class SaveBasePricesSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly FuelPricesContract _contract;

        public SaveBasePricesSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, FuelPrices prices) : base("SaveBasePrices")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _contract = new FuelPricesContract
            {
                isReadTotalizerChecked = prices.IsReadTotalizerChecked,
                isPricesToDisplayChecked = prices.IsPricesToDisplayChecked,
                isReadTankDipChecked = prices.IsReadTankDipChecked,
                tillNumber = _cacheManager.TillNumberForSale,
                fuelPrices = (from f in prices.Prices
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
                             }).ToList()
            };
        }

        protected async override Task<object> OnPerform()
        {
            var deleteContract = JsonConvert.SerializeObject(_contract);
            var content = new StringContent(deleteContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.SaveBasePrices(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapErrorWithCaption(data);
                    return new Mapper().MapErrorWithCaption(contract);
                case HttpStatusCode.Conflict:
                    throw PumpsOfflineException(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
