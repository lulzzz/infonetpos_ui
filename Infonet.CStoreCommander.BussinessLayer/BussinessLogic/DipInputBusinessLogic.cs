using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class DipInputBusinessLogic : IDipInputBusinessLogic
    {
        private readonly IDipInputSerializeManager _serializeManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        public DipInputBusinessLogic(IDipInputSerializeManager serializeManager,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _serializeManager = serializeManager;
            _reportsBusinessLogic = reportsBusinessLogic;
        }

        public async Task<List<DipInput>> GetDipInput()
        {
            return await _serializeManager.GetDipInput();
        }

        public async Task<Report> GetDipInputReport()
        {
            var report =  await _serializeManager.GetDipInputReport();
            await _reportsBusinessLogic.SaveReport(report);
            return report;
        }

        public async Task<List<DipInput>> SaveDipInput(List<DipInput> dipInputs)
        {
            return await _serializeManager.SaveDipInput(dipInputs);
        }
    }
}
