using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetAllShiftSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;

        public GetAllShiftSerializeAction(IReportRestClient restClient)
            : base("GetAllShift")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetAllShift();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var ShiftContract = new DeSerializer().MapShift(data);
                    return new Mapper().MapShift(ShiftContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
