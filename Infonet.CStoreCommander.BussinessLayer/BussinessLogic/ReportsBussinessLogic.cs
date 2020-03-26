using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class ReportsBussinessLogic : IReportsBussinessLogic
    {
        private readonly IReportSerializeManager _serializeManager;
        private readonly IStorageService _storageService;
        private readonly ICacheBusinessLogic _cacheManager;

        public ReportsBussinessLogic(IReportSerializeManager serializeManager,
            IStorageService storageService,
             ICacheBusinessLogic cacheManager)
        {
            _serializeManager = serializeManager;
            _storageService = storageService;
            _cacheManager = cacheManager;
        }
        //Tony 03/19/2019
        public async Task<List<string>> GetReceiptHeader()
        {
            return await _serializeManager.GetReceiptHeader();
        }
        //end
        public async Task<List<Department>> GetAllDepartment()
        {
            return await _serializeManager.GetAllDepartment();
        }

        public async Task<List<Shift>> GetAllShift()
        {
            return await _serializeManager.GetAllShift();
        }

        public async Task<List<Till>> GetAllTill()
        {
            return await _serializeManager.GetAllTill();
        }

        public async Task<Report> GetSaleCountReport(int shiftNumber,
            int tillNumber, string departmentID)
        {
            var report = await _serializeManager.GetSaleCountReport(shiftNumber, tillNumber, departmentID);
            await SaveReport(report);
            return report;
        }

        public async Task<FlashReport> GetFlashReport()
        {
            var flashReport = await _serializeManager.GetFalshReport();
            await SaveReport(flashReport.Report);
            return flashReport;
        }

        public async Task<List<string>> GetReceipt(string reportName)
        {
            var folder = _storageService.LocalFolder;
            if (!string.IsNullOrEmpty(reportName))
            {
                var reportFile = await folder.GetFileAsync(reportName);
                var lines = await FileIO.ReadLinesAsync(reportFile);
                return lines.ToList();
            }

            return new List<string>();
        }

        public async Task<Report> GetTillAuditReport()
        {
            var report = await _serializeManager.GetTillAuditReport();
            await SaveReport(report);
            return report;
        }

        public async Task SaveReport(Report report)
        {
            if (report != null && report.ReportName != null && report.ReportContent != null)
            {
                var folder = _storageService.LocalFolder;
                var reportFile = await folder.CreateFileAsync(report.ReportName,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(reportFile, report.ReportContent);

                switch (report.ReportName)
                {
                    case ReportType.CashDropFile:
                    case ReportType.CashDrawFile:
                    case ReportType.ReceiptFile:
                    case ReportType.ArPayFile:
                    case ReportType.PayoutFile:
                    case ReportType.BottleReturnFile:
                    case ReportType.TaxExemptionFile:
                    case ReportType.ReprintFile:
                    case ReportType.PumpTestFile:
                    case ReportType.RunAwayFile:
                    case ReportType.PrepayFile:
                    case ReportType.PaymentFile:
                    case ReportType.PriceFile:
                    case ReportType.KickBackReport:
                        _cacheManager.LastPrintReport = report.ReportName;
                        break;
                }
            }
        }

        public async Task<Report> GetKickBackReport(double kickBackBalancePoints)
        {
            var report = await _serializeManager.GetKickBackReport(kickBackBalancePoints);
            await SaveReport(report);
            return report;
        }
    }
}
