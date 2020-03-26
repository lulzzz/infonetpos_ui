using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetAllTillSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;

        public GetAllTillSerializeAction(IReportRestClient restClient)
            : base("GetAllTill")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetAllTill();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tillContract = new DeSerializer().MapTill(data);
                    return new Mapper().MapTill(tillContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
