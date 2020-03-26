using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IReportsBussinessLogic
    {
        //Tony 03/19/2019
        Task<List<string>> GetReceiptHeader();
        //end
        Task<List<Department>> GetAllDepartment();

        Task<List<Till>> GetAllTill();

        Task<List<Shift>> GetAllShift();

        Task<Report> GetSaleCountReport(int shiftNumber,
            int tillNumber, string departmentID);

        Task<FlashReport> GetFlashReport();

        Task<Report> GetTillAuditReport();

        Task<List<string>> GetReceipt(string reportName);

        Task SaveReport(Report report);

        Task<Report> GetKickBackReport(double kickBackBalancePoints);
    }
}
