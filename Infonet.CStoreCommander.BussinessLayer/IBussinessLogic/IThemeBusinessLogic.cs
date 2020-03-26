using Infonet.CStoreCommander.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IThemeBusinessLogic
    {
        Task<Theme> GetActiveTheme();
    }
}
