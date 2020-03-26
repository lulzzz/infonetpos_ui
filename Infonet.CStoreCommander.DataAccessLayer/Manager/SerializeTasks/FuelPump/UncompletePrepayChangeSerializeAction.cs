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
    public class UncompletePrepayChangeSerializeAction : SerializeAction
    {
        private readonly IFuelPumpRestClient _fuelPumpRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly UncompletePrepayChangePostContract _uncompletePrepayChangePostContract;

        public UncompletePrepayChangeSerializeAction(IFuelPumpRestClient fuelPumpRestClient,
             ICacheManager cacheManager, string finishAmount, string finishPrice,
             string finishQty, int gradeId, string positionId, string prepayAmount,
             int pumpId, int saleNumber)
            : base("UncompletePrepay")
        {
            _fuelPumpRestClient = fuelPumpRestClient;
            _cacheManager = cacheManager;
            _uncompletePrepayChangePostContract = new UncompletePrepayChangePostContract
            {
                finishAmount = finishAmount,
                finishPrice = finishPrice,
                finishQty = finishQty,
                gradeId = gradeId,
                positionId = positionId,
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
            var response = await _fuelPumpRestClient.UncompletePrepayChange(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapUncompletePrepayChange(data);
                    return new Mapper().MapUncompletePrepayChange(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
