using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class WriteOffSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly Reasons _reason;

        public WriteOffSerializeAction(
            ISaleRestClient saleRestClient,
            ICacheManager cacheManager,
            Reasons reason)
            : base("WriteOff")
        {
            _saleRestClient = saleRestClient;
            _cacheManager = cacheManager;
            _reason = reason;
        }

        /// <summary>
        /// Completes the Write off
        /// </summary>
        /// <returns>Response from the API</returns>
        protected override async Task<object> OnPerform()
        {
            var writeOffPayLoad = new WriteOffPayload
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                writeOffReason = _reason.Code.ToString()
            };

            var payload = JsonConvert.SerializeObject(writeOffPayLoad);
            var content = new StringContent(payload, 
                Encoding.UTF8, ApplicationJSON);
            var response = await _saleRestClient.WriteOff(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapWriteOff(data);
                    return new Mapper().MapWriteOff(result);
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
