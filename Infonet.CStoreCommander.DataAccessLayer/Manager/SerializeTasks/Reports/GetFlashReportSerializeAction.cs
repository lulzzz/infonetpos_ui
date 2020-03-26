using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetFlashReportSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;

        public GetFlashReportSerializeAction(IReportRestClient restClient) 
            : base("GetFlashReport")
        {
            _restClient = restClient;
        }

        protected async  override Task<object> OnPerform()
        {
            var response = await _restClient.GetFlashReport();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var flashReportContract = new DeSerializer().MapFlashReport(data);
                    return new Mapper().MapFlashReport(flashReportContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
