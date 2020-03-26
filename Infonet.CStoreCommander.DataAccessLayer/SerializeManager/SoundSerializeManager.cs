using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class SoundSerializeManager : SerializeManager,
        ISoundSerializeManager
    {
        private readonly ISoundRestClient _soundRestClient;

        public SoundSerializeManager(ISoundRestClient soundRestClient)
        {
            _soundRestClient = soundRestClient;
        }

        public async Task<List<Sounds>> GetSounds()
        {
            var action = new GetSoundsSerializeAction(_soundRestClient);

            await PerformTask(action);

            return (List<Sounds>)action.ResponseValue;
        }
    }
}
