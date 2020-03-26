using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IReprintRestClient
    {
        Task<HttpResponseMessage> GetReprintReportName();

        Task<HttpResponseMessage> GetReprintSales(string reportType,
            string date);

        Task<HttpResponseMessage> GetReprintReport(string saleNumber,string saleDate,
           string reportType);
    }
}
