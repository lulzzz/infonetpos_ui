using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Ackroo;
using Infonet.CStoreCommander.EntityLayer.Entities.Ackroo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class AckrooSerializeManager : SerializeManager, IAckrooSerializeManager
    {
        private readonly IAckrooRestClient _restClient;
        public AckrooSerializeManager(IAckrooRestClient restClient)
        {
            _restClient = restClient;
        }
        public async Task<string> GetAckrooStockCode()
        {
            var action = new GetAckrooStockCodeSerializeAction(_restClient);
            await PerformTask(action);
            return (string)action.ResponseValue;
        }
        public async Task<List<Carwash>> GetCarwashCategories()
        {
            var action = new GetCarwashCategoriesSerializeAction(_restClient);
            await PerformTask(action);
            return (List<Carwash>)action.ResponseValue;
        }
        public async Task<string> GetAckrooCarwashStockCode(string sDesc)
        {
            var action = new GetAckrooCarwashStockCodeSerializeAction(_restClient, sDesc);
            await PerformTask(action);
            return (string)action.ResponseValue;
        }
    }
}
