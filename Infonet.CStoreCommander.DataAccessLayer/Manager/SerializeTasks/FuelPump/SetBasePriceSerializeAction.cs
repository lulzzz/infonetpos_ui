using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class SetBasePriceSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly Price _fuelPrice;

        public SetBasePriceSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager,
            Price fuelPrice)
            : base("SetBasePrice")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _fuelPrice = fuelPrice;
        }

        protected async override Task<object> OnPerform()
        {
            var payLoad = new FuelPriceContract
            {
                cashPrice = _fuelPrice.CashPrice,
                creditPrice = _fuelPrice.CreditPrice,
                grade = _fuelPrice.Grade,
                gradeId = _fuelPrice.GradeId,
                level = _fuelPrice.Level,
                levelId = _fuelPrice.LevelId,
                taxExemptedCashPrice = _fuelPrice.TaxExemptedCashPrice,
                taxExemptedCreditPrice = _fuelPrice.TaxExemptedCreditPrice,
                tier = _fuelPrice.Tier,
                tierId = _fuelPrice.TierId,
                row = _fuelPrice.Row
            };

            var contract = JsonConvert.SerializeObject(payLoad);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.SetBasePrice(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var responseContract = new DeSerializer().MapFuelPrice(data);
                    return new Mapper().MapFuelPrice(responseContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
