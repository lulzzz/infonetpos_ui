using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    class OpenCashDrawerSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;
        private readonly ICacheManager _cacheManager;
        private OpenCashDrawerContract _openCashDrawer;

        public OpenCashDrawerSerializeAction(ICashRestClient cashRestClient,
            ICacheManager cacheManager,string openDrawerReason) : base("OpenCashDrawer")
        {
            _cacheManager = cacheManager;
            _cashRestClient = cashRestClient;
            _openCashDrawer = new OpenCashDrawerContract
            {
                dropReason = openDrawerReason,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_openCashDrawer)
                 , Encoding.UTF8, ApplicationJSON);

            var response = await _cashRestClient.OpenCashDrawer(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSuccess(data).success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
