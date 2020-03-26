using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface ISoundBusinessLogic
    {
        Task<List<Sounds>> GetSounds();
    }
}
