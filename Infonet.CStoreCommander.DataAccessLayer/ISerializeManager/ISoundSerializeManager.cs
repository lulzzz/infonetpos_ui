using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ISoundSerializeManager
    {
        Task<List<Sounds>> GetSounds();
    }
}
