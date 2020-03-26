using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.KickBack;
using System.Threading.Tasks;
using System;
using Infonet.CStoreCommander.EntityLayer.Entities.Kickback;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class KickBackSerializeManager : SerializeManager, IKickBackSerializeManager
    {
        private readonly IKickBackRestClient _kickBackRestClient;

        public KickBackSerializeManager(IKickBackRestClient kickBackRestClient)
        {
            _kickBackRestClient = kickBackRestClient;
        }

        public async Task<KickBackBalancePoint> CheckKickBackBalance(string cardNumber)
        {
            var action = new CheckKickBackBalanceSerializeAction(_kickBackRestClient, cardNumber);

            await PerformTask(action);

            return (KickBackBalancePoint)action.ResponseValue;
        }

        public async Task<bool> CheckKickBackResponse(bool userResponse)
        {
            var action = new CheckKickBackResponseSerializeAction(_kickBackRestClient, userResponse);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<ValidateGasKing> ValidateGasKing()
        {
            var action = new ValidateGasKingSerializeAction(_kickBackRestClient);

            await PerformTask(action);

            return (ValidateGasKing)action.ResponseValue;
        }

        public async Task<VerifyKickback> VerifyKickBack(string pointCardNumber, string phoneNumber)
        {
            var action = new VerifyKickBackSerializeAction(_kickBackRestClient,
              pointCardNumber, phoneNumber);

            await PerformTask(action);

            return (VerifyKickback)action.ResponseValue;
        }
    }
}
