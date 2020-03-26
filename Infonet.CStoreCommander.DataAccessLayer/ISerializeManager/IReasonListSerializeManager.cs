using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IReasonListSerializeManager
    {
        /// <summary>
        /// Method to get reasons list
        /// </summary>
        /// <returns></returns>
        Task<ReasonsList> GetReasonList(string reason);
    }
}
