using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.FuelPrice
{
    public class LoadPriceIncrementsAndDecrementsSerializeAction : SerializeAction
    {
        private readonly IFuelPriceRestClient _fuelPriceRestClient;
        private readonly bool _taxExempt;

        public LoadPriceIncrementsAndDecrementsSerializeAction(IFuelPriceRestClient fuelPriceRestClient, bool taxExempt)
            : base("LoadPriceIncrementsAndDecrements")
        {
            _fuelPriceRestClient = fuelPriceRestClient;
            _taxExempt = taxExempt;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _fuelPriceRestClient.LoadPriceIncrementsAndDecrements(_taxExempt);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var prices = new DeSerializer().MapIncrementsAndDecrements(data);
                    return new Mapper().MapIncrementsAndDecrements(prices);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
