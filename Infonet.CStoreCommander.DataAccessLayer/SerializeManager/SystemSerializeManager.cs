using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.System;
using Infonet.CStoreCommander.EntityLayer.Entities.System;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class SystemSerializeManager : SerializeManager, ISystemSerializeManager
    {
        private readonly ISystemRestClient _restClient;

        public SystemSerializeManager(ISystemRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<Register> GetRegisterSettings(byte registerNumber)
        {
            var action = new GetRegisterSettingsSerializeAction(_restClient, registerNumber);
            await PerformTask(action);
            return (Register)action.ResponseValue;
        }
    }
}
