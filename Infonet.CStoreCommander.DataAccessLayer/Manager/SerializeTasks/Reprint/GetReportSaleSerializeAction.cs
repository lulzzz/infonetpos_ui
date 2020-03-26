using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reprint
{
    public class GetReportSaleSerializeAction : SerializeAction
    {
        private readonly IReprintRestClient _reprintRestClient;
        private readonly string _reportType;
        private readonly string _date;

        public GetReportSaleSerializeAction(IReprintRestClient reprintRestClient,
            string reportType, string date)
            : base("GetReportSale")
        {
            _reprintRestClient = reprintRestClient;
            _reportType = reportType;
            _date = date;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _reprintRestClient.GetReprintSales(_reportType, _date);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var reportSaleContract = new DeSerializer().MapReprintReportSale(data);
                    return new Mapper().MapReprintReportSale(reportSaleContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
