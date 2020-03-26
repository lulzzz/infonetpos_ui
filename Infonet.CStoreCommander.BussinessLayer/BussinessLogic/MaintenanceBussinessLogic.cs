using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Maintenance Screens
    /// </summary>
    public class MaintenanceBussinessLogic : IMaintenanceBussinessLogic
    {
        private readonly IMaintenanceSeralizeManager _seralizeManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seralizeManager"></param>
        public MaintenanceBussinessLogic(IMaintenanceSeralizeManager seralizeManager,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _seralizeManager = seralizeManager;
            _reportsBusinessLogic = reportsBusinessLogic;
        }

        /// <summary>
        /// Method to change password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Success model</returns>
        public async Task<Success> ChangePassword(string password, string confirmPassword)
        {
            return await _seralizeManager.ChangePassword(password, confirmPassword);
        }

        public async Task<List<Report>> CloseBatch()
        {
            var reports = await _seralizeManager.CloseBatch();

            foreach (var report in reports)
            {
                if (report.ReportName.Contains(ReportType.EodDetailsFile))
                {
                    await _reportsBusinessLogic.SaveReport(report);
                }
            }
            return reports;
        }

        public async Task<Error> Initialize()
        {
            return await _seralizeManager.Initialize();
        }

        public async Task<bool> SetPrepayOrPostPay(bool isOn, bool isPrepay)
        {
            return await _seralizeManager.SetPrepayOrPostPay(isOn, isPrepay);
        }
    }
}
