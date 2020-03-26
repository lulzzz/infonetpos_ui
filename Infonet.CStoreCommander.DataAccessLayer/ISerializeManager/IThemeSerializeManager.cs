using Infonet.CStoreCommander.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IThemeSerializeManager
    {
        Task<Theme> GetActiveTheme();
    }
}
