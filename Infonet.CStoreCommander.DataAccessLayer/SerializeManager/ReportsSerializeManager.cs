using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class ReportsSerializeManager : SerializeManager, IReportSerializeManager
    {
        private readonly IReportRestClient _restClient;

        public ReportsSerializeManager(IReportRestClient reportRestClient)
        {
            _restClient = reportRestClient;
        }
        //Tony 03/10/2019
        public async Task<List<string>> GetReceiptHeader()
        {
            var action = new GetReceiptHeaderSerializeAction(_restClient);
            await PerformTask(action);
            return (List<string>)action.ResponseValue;
        }
        //end
        public async Task<List<Department>> GetAllDepartment()
        {
            var action = new GetAllDepartmentSerializeAction(_restClient);

            await PerformTask(action);

            return (List<Department>)action.ResponseValue;
        }

        public async Task<List<Shift>> GetAllShift()
        {
            var action = new GetAllShiftSerializeAction(_restClient);

            await PerformTask(action);

            return (List<Shift>)action.ResponseValue;
        }

        public async Task<List<Till>> GetAllTill()
        {
            var action = new GetAllTillSerializeAction(_restClient);

            await PerformTask(action);

            return (List<Till>)action.ResponseValue;
        }


        public async Task<Report> GetSaleCountReport(int shiftNumber,
            int tillNumber, string departmentId)
        {
            var action = new GetSaleCountReportSerializeAction(_restClient,
                shiftNumber, tillNumber, departmentId);

            await PerformTask(action);

            return (Report)action.ResponseValue;
        }

        public async Task<FlashReport> GetFalshReport()
        {
            var action = new GetFlashReportSerializeAction(_restClient);

            await PerformTask(action);

            return (FlashReport)action.ResponseValue;
        }

        public async Task<Report> GetTillAuditReport()
        {
            var action = new GetTillAuditReportSerializeAction(_restClient);

            await PerformTask(action);

            return (Report)action.ResponseValue;
        }

        public async Task<Report> GetKickBackReport(double kickbackPoints)
        {
            var action = new GetKickBackBalanceReportSerializeAction(_restClient, kickbackPoints);

            await PerformTask(action);

            return (Report)action.ResponseValue;            
        }
    }
}
