using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Reasons
    /// </summary>
    public class ReasonListBussinessLogic : IReasonListBussinessLogic
    {
        private readonly IReasonListSerializeManager _serializeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager"></param>
        public ReasonListBussinessLogic(IReasonListSerializeManager serializeManager)
        {
            _serializeManager = serializeManager;
        }

        /// <summary>
        /// Returns list of reasons
        /// </summary>
        /// <param name="reason">Type of reasons</param>
        /// <returns>List of reasons</returns>
        public async Task<ReasonsList> GetReasonListAsync(string reason)
        {
            return await _serializeManager.GetReasonList(reason);
        }
    }
}
