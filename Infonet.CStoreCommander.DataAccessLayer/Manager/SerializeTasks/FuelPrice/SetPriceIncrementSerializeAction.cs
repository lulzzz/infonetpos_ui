using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.FuelPrice;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice
{
    public class SetPriceIncrementSerializeAction : SerializeAction
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;
        private readonly PriceIncrement _priceIncrement;
        private readonly bool _taxExempt;

        public SetPriceIncrementSerializeAction(IFuelPriceRestClient fuelPriceRestClient,
            PriceIncrement priceIncrement,
            bool taxExempt)
            : base("SetPriceIncrement")
        {
            _fuelPriceRestClient = fuelPriceRestClient;
            _priceIncrement = priceIncrement;
            _taxExempt = taxExempt;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                price = new
                {
                    row = _priceIncrement.Row,
                    grade = _priceIncrement.Grade,
                    gradeId = _priceIncrement.GradeId,
                    cash = _priceIncrement.Cash,
                    credit = _priceIncrement.Credit
                },
                taxExempt = _taxExempt
            };

            var payload = JsonConvert.SerializeObject(contract);
            var content = new StringContent(payload, Encoding.UTF8, ApplicationJSON);
            var response = await _fuelPriceRestClient.SetPriceIncrement(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSetPriceIncrement(data);
                    return new Mapper().MapSetPriceIncrement(result);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
