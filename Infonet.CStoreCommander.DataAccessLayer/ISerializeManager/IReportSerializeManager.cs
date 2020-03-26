using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IReportSerializeManager
    {
        //Tony 03/19/2019
        Task<List<string>> GetReceiptHeader();
        //end
        Task<List<Department>> GetAllDepartment();

        Task<List<Till>> GetAllTill();

        Task<List<Shift>> GetAllShift();

        Task<Report> GetSaleCountReport(int shiftNumber,
            int tillNumber, string departmentId);

        Task<FlashReport> GetFalshReport();

        Task<Report> GetTillAuditReport();

        Task<Report> GetKickBackReport(double kickbackPoints);
    }
}
