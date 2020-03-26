using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface ISystemBusinessLogic
    {
        Task GetAndSaveRegisterSettings();
    }
}
