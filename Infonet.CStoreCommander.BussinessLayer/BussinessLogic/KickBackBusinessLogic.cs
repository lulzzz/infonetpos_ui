using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;
using System.Threading.Tasks;
using System;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class KickBackBusinessLogic : IKickBackBusinessLogic
    {
        private IKickBackSerializeManager _kickBackSeralizemanager;

        public KickBackBusinessLogic(IKickBackSerializeManager kickBackSeralizeManager)
        {
            _kickBackSeralizemanager = kickBackSeralizeManager;
        }

        public async Task<KickBackBalancePoint> CheckKickBackbalance(string pointCardNumber)
        {
            return await _kickBackSeralizemanager.CheckKickBackBalance(pointCardNumber);
        }

        public async Task<bool> CheckKickBackResponse(bool userResponse)
        {
            return await _kickBackSeralizemanager.CheckKickBackResponse(userResponse);
        }

        public async Task<ValidateGasKing> ValidateGasKing()
        {
            return await _kickBackSeralizemanager.ValidateGasKing();
        }

        public async Task<VerifyKickback> VerifyKickBack(string pointCardNumber, string phoneNumber)
        {
            return await _kickBackSeralizemanager.VerifyKickBack(pointCardNumber, phoneNumber);
        }
    }
}
