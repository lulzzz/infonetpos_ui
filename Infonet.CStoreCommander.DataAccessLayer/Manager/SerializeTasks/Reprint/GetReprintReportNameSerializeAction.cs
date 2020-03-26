using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reprint
{
    public class GetReprintReportNameSerializeAction : SerializeAction
    {
        private readonly IReprintRestClient _reprintRestClient;

        public GetReprintReportNameSerializeAction(IReprintRestClient reprintRestClient) 
            : base("GetReprintName")
        {
            _reprintRestClient = reprintRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _reprintRestClient.GetReprintReportName();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var reportNameContract = new DeSerializer().MapReprintReportName(data);
                    return new Mapper().MapReprintReportName(reportNameContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
