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
   public class GetAckrooCarwashStockCodeSerializeAction: SerializeAction
    {
        private readonly IAckrooRestClient _IAckrooRestClient;
        private string _sDesc;
        public GetAckrooCarwashStockCodeSerializeAction(IAckrooRestClient restClient, string sDesc)
            :base("GetAckrooCarwashStockCode")
        {
            _IAckrooRestClient = restClient;
            _sDesc = sDesc;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _IAckrooRestClient.GetAckrooCarwashStockCode(_sDesc);
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
