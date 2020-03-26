using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPump;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPump
{
    public class SetGroupBasePriceSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly SetGroupFuelPriceContract _fuelPriceContract;

        public SetGroupBasePriceSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
            ICacheManager cacheManager, List<Price> fuelPrices, int row) : base("SetGroupBasePrice")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;

            _fuelPriceContract = new SetGroupFuelPriceContract
            {
                row = row,
                prices = (from f in fuelPrices
                          select new FuelPriceContract
                          {
                              cashPrice = f.CashPrice,
                              creditPrice = f.CreditPrice,
                              grade = f.Grade,
                              row = f.Row,
                              taxExemptedCashPrice = f.TaxExemptedCashPrice,
                              taxExemptedCreditPrice = f.TaxExemptedCreditPrice
                          }).ToList()
            };
        }

        protected async override Task<object> OnPerform()
        {
            var setContract = JsonConvert.SerializeObject(_fuelPriceContract);
            var content = new StringContent(setContract, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPumpRestClient.SetGroupBasePrice(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapFuelPrices(data);
                    var entity = new Mapper().MapFuelPrices(contract);
                    return entity;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
