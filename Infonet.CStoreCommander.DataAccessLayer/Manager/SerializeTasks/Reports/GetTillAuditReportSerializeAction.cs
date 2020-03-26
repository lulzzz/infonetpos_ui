using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetTillAuditReportSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;

        public GetTillAuditReportSerializeAction(IReportRestClient restClient) 
            : base("GetTillAuditReport")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetTillAuditReport();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var reportContract = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(reportContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
