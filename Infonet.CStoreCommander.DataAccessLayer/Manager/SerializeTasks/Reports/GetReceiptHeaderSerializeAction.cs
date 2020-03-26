using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetReceiptHeaderSerializeAction: SerializeAction
    {
        private readonly IReportRestClient _restClient;
        public GetReceiptHeaderSerializeAction(IReportRestClient restClient)
            :base("GetReceiptHeader")
        {
            _restClient = restClient;
        }
        protected async override Task<object> OnPerform()
        {

            var response = await _restClient.GetReceiptHeader();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return DeSerializer.MapListString(data);
                default:
                    return await HandleExceptions(response);
            }
           


        }
    }
}
