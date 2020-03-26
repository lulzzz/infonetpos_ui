using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IReasonListBussinessLogic
    {
        Task<ReasonsList> GetReasonListAsync(string reason);
    }
}
