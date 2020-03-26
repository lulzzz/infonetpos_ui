using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Policies
    /// </summary>
    public class PolicyBussinessLogic : IPolicyBussinessLogic
    {
        private readonly IPolicySerializeManager _seralizeManager;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seralizeManager">Policy serialize manager</param>
        public PolicyBussinessLogic(IPolicySerializeManager seralizeManager,
            ICacheBusinessLogic cacheBusinessLogic)
        {
            _seralizeManager = seralizeManager;
            _cacheBusinessLogic = cacheBusinessLogic;
        }

        /// <summary>
        /// Returns all policies
        /// </summary>
        /// <returns>Policy model</returns>
        public async Task<Policy> GetAllPolicies(bool isRefresh)
        {
            var policies =  await _seralizeManager.GetAllPolicies(isRefresh);
            _cacheBusinessLogic.SetAllPolicies(policies);
            return policies;
        }
    }
}
