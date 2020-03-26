using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IPolicyBussinessLogic
    {
        /// <summary>
        /// Method to get all policies
        /// </summary>
        /// <returns></returns>
        Task<Policy> GetAllPolicies(bool isRefresh);
    }
}
