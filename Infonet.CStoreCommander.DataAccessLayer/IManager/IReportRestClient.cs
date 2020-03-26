using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IReportRestClient
    {
        Task<HttpResponseMessage> GetReceiptHeader();
        Task<HttpResponseMessage> GetAllDepartment();

        Task<HttpResponseMessage> GetAllTill();

        Task<HttpResponseMessage> GetAllShift();

        Task<HttpResponseMessage> GetShiftCountReport(string departmentId,
            int tillNumber, int shiftNumber);

        Task<HttpResponseMessage> GetFlashReport();

        Task<HttpResponseMessage> GetTillAuditReport();

        Task<HttpResponseMessage> GetKickBackReport(double kickBackBalancePoints);
    }
}
