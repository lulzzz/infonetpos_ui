using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports
{
    public class GetSaleCountReportSerializeAction : SerializeAction
    {
        private readonly IReportRestClient _restClient;
        private readonly int _shiftNumber;
        private readonly int _tillNumber;
        private readonly string _departmentId;

        public GetSaleCountReportSerializeAction(IReportRestClient restClient,
            int shiftNumber, int tillNumber, string departmentId)
            : base("GetSaleCountReport")
        {
            _restClient = restClient;
            _shiftNumber = shiftNumber;
            _tillNumber = tillNumber;
            _departmentId = departmentId;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetShiftCountReport(_departmentId,
                _tillNumber, _shiftNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleCountReportContract = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(saleCountReportContract);
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
