using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Ackroo
{
    public class GetAckrooStockCodeSerializeAction: SerializeAction
    {
        private readonly IAckrooRestClient _IAckrooRestClient;
        public GetAckrooStockCodeSerializeAction(IAckrooRestClient restClient)
            :base("GetAckrooStockCode")
        {
            _IAckrooRestClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _IAckrooRestClient.GetAckrooStockCode();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapString(data);
                    return contract;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
