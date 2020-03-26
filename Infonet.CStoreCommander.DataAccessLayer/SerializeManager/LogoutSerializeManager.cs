using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class LogoutSerializeManager : SerializeManager, ILogoutSerializeManager
    {
        private ILogoutRestClient _restClient;
        private ICacheManager _cacheManager;

        public LogoutSerializeManager(ILogoutRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        public async Task<bool> LogoutUser()
        {
            var action = new LogoutUserSerializeAction(_restClient, _cacheManager);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        /// <summary>
        /// Method to switch user
        /// </summary>
        /// <returns></returns>
        public async Task<string> SwitchUser(User userModel)
        {
            var action = new SwitchUserSerializeAction(_restClient,
               userModel);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }

        public async Task<CloseTill> CloseTill()
        {
            var action = new CloseTillSerializeAction(_restClient);

            await PerformTask(action);

            return (CloseTill)action.ResponseValue;
        }

        public async Task<ValidateTillClose> ValidateClosetill()
        {
            var action = new ValidateTillCloseSerializeAction(_restClient);

            await PerformTask(action);

            return (ValidateTillClose)action.ResponseValue;
        }

        public async Task<bool> EndShift()
        {
            var action = new EndShiftSerializeAction(_restClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<bool> ReadTankDip()
        {
            var action = new ReadTankDipSerializeAction(_restClient);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<CloseTill> UpdateCloseTill(UpdatedTender updatedTender, UpdatedBillCoin updatedBillCoin)
        {
            var action = new UpdateTillCloseSerializeAction(_restClient, _cacheManager,
                updatedTender, updatedBillCoin);

            await PerformTask(action);

            return (CloseTill)action.ResponseValue;
        }

        public async Task<FinishTillClose> FinishTillClose(bool? readTankDip, bool? readTotalizer)
        {
            var action = new FinishTillCloseSerializeAction(_restClient,
               readTankDip, readTotalizer);

            await PerformTask(action);

            return (FinishTillClose)action.ResponseValue;
        }
    }
}
