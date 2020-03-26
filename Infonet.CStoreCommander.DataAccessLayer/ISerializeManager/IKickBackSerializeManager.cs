using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IKickBackSerializeManager
    {
        Task<VerifyKickback> VerifyKickBack(string pointCardNumber, string phoneNumber);

        Task<bool> CheckKickBackResponse(bool userResponse);

        Task<KickBackBalancePoint> CheckKickBackBalance(string cardNumber);

        Task<ValidateGasKing> ValidateGasKing();
    }
}
