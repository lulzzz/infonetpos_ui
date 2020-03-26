using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IPolicySerializeManager
    {
        Task<Policy> GetAllPolicies(bool isRefresh);
    }
}
