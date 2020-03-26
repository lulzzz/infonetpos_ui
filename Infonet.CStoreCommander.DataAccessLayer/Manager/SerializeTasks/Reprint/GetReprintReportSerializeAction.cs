using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reprint
{
    public class GetReprintReportSerializeAction : SerializeAction
    {
        private readonly IReprintRestClient _reprintRestClient;
        private readonly string _saleDate;
        private readonly string _reportType;
        private readonly string _saleNumber;

        public GetReprintReportSerializeAction(IReprintRestClient reprintRestClient,
            string saleNumber,string saleDate, string reportType) 
            : base("GetReprintReport")
        {
            _reprintRestClient = reprintRestClient;
            _saleDate = saleDate;
            _reportType = reportType;
            _saleNumber = saleNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _reprintRestClient.GetReprintReport(_saleNumber, _saleDate, _reportType);

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var reportNameContract = new DeSerializer().MapReports(data);
                    return new Mapper().MapReports(reportNameContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
