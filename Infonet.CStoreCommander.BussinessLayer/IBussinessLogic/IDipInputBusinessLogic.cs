using Infonet.CStoreCommander.EntityLayer.Entities.DipInput;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IDipInputBusinessLogic
    {
        Task<List<DipInput>> GetDipInput();

        Task<Report> GetDipInputReport();

        Task<List<DipInput>> SaveDipInput(List<DipInput> dipInputs);
    }
}
