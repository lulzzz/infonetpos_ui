using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class SoundBusinessLogic : ISoundBusinessLogic
    {
        private readonly ISoundSerializeManager _seralizeManager;

        public SoundBusinessLogic(ISoundSerializeManager seralizeManager)
        {
            _seralizeManager = seralizeManager;
        }

        public async Task<List<Sounds>> GetSounds()
        {
            return await _seralizeManager.GetSounds();
        }
    }
}
