using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX
{
    public class GetGiveXReportSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _restClient;
        private readonly string _date;

        public GetGiveXReportSerializeAction(IGiveXRestClient givexRestClient, string date) : base("GetGiveXReport")
        {
            _restClient = givexRestClient;
            _date = date;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetGivexReport(_date);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var giveXReportContract = new DeSerializer().MapGiveXReport(data);
                    return new Mapper().MapGiveXReport(giveXReportContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
