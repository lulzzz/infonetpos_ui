using Infonet.CStoreCommander.DataAccessLayer.DataContracts.GiveX;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX
{
    public class CloseBatchSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public CloseBatchSerializeAction(IGiveXRestClient giveXRestClient,
            ICacheManager cacheManager)
            : base("CloseBatch")
        {
            _cacheManager = cacheManager;
            _restClient = giveXRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var giveXContract = new GiveXCardContract
            {
                tillNumber = _cacheManager.TillNumber,
                saleNumber = _cacheManager.SaleNumber
            };

            var giveXCardContract = JsonConvert.SerializeObject(giveXContract);
            var content = new StringContent(giveXCardContract, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.CloseBatch(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
