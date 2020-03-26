using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class AckrooBusinessLogic : IAckrooBusinessLogic
    {
        private readonly IAckrooSerializeManager _serializeManager;
        public AckrooBusinessLogic(IAckrooSerializeManager serializeManager)
        {
            _serializeManager = serializeManager;
        }
        public async Task<string> GetAckrooStockCode()
        {
            return await _serializeManager.GetAckrooStockCode();
        }
        public async Task<List<Carwash>> GetCarwashCategories()
        {
            return await _serializeManager.GetCarwashCategories();
        }
        public async Task<string> GetAckrooCarwashStockCode(string sDesc)
        {
            return await _serializeManager.GetAckrooCarwashStockCode(sDesc);
        }
    }
}
