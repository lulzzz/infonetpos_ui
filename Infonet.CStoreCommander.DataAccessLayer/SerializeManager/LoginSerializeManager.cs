using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Login;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class LoginSerializeManager : SerializeManager, ILoginSerializeManager
    {
        private readonly ILoginRestClient _loginRestClient;
        private readonly ICacheManager _cacheManager;

        public LoginSerializeManager(ILoginRestClient loginRestClient,
            ICacheManager cacheManager)
        {
            _loginRestClient = loginRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<ActiveTills> GetTills()
        {
            var action = new GetTillsSerializeAction(_loginRestClient, _cacheManager);

            await PerformTask(action);

            return (ActiveTills)action.ResponseValue;
        }

        public async Task<ActiveShifts> GetActiveShifts()
        {
            var action = new GetShiftsSerializeAction(_loginRestClient, _cacheManager);

            await PerformTask(action);

            return (ActiveShifts)action.ResponseValue;
        }

        public async Task<LoginPolicy> GetLoginPolicy()
        {
            var action = new GetLoginPoliciesSerializeAction(_loginRestClient, _cacheManager);

            await PerformTask(action);

            return (LoginPolicy)action.ResponseValue;
        }

        public async Task<Login> Login()
        {
            var action = new LoginSerializeAction(_loginRestClient, _cacheManager);

            await PerformTask(action);

            return (Login)action.ResponseValue;
        }

        public async Task<string> GetPassword()
        {
            var action = new GetPasswordSerializeAction(_loginRestClient, _cacheManager);

            await PerformTask(action);

            return (string)action.ResponseValue;
        }
    }
}
