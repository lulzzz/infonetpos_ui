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
    public class UncompleteOverPaymentSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly UncompletePrepayChangePostContract _uncompletePrepayChangePostContract;

        public UncompleteOverPaymentSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
             ICacheManager cacheManager,
            int pumpId, string finishAmount, string finishQuantity, string finishPrice,
            string prepayAmount, int positionId, int gradeId, int saleNumber)
            : base("UncompleteOverPayment")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _uncompletePrepayChangePostContract = new UncompletePrepayChangePostContract
            {
                finishAmount = finishAmount,
                finishPrice = finishPrice,
                finishQty = finishQuantity,
                gradeId = gradeId,
                positionId = positionId.ToString(),
                prepayAmount = prepayAmount,
                pumpId = pumpId,
                saleNum = saleNumber,
                tillNumber = cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var uncompletePrepayContract = JsonConvert.SerializeObject(_uncompletePrepayChangePostContract);
            var content = new StringContent(uncompletePrepayContract, Encoding.UTF8, ApplicationJSON);

            var response = await _fuelPumpRestClient.UncompleteOverPayment(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapOverPayment(data);
                    return new Mapper().MapOverPayment(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
