using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IKickBackBusinessLogic
    {
        Task<VerifyKickback> VerifyKickBack(string pointCardNumber, string phoneNumber);

        Task<bool> CheckKickBackResponse(bool userResponse);

        Task<KickBackBalancePoint> CheckKickBackbalance(string pointCardNumber);

        Task<ValidateGasKing> ValidateGasKing();
    }
}
