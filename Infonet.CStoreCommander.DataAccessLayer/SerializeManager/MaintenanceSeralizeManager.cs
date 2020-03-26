using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Settings.Maintenance;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class MaintenanceSeralizeManager : SerializeManager, IMaintenanceSeralizeManager
    {
        private readonly IMaintenanceRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public MaintenanceSeralizeManager(IMaintenanceRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to change password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>success model</returns>
        public async Task<Success> ChangePassword(string password, string confirmPassword)
        {
            var action = new ChangePasswordSerializeAction(_restClient,
                 _cacheManager, password, confirmPassword);

            await PerformTask(action);

            return (Success)action.ResponseValue;
        }

        public async Task<List<Report>> CloseBatch()
        {
            var action = new CloseBatchSerializeAction(_restClient,
           _cacheManager);

            await PerformTask(action);

            return (List<Report>)action.ResponseValue;
        }

        public async Task<Error> Initialize()
        {
            var action = new InitializeSerializeAction(_restClient,
            _cacheManager);

            await PerformTask(action);

            return (Error)action.ResponseValue;
        }

        public async Task<bool> SetPrepayOrPostPay(bool isOn, bool isPrepay)
        {
            var action = new SetPrepayOrPostpaySerializeAction(_restClient,
            isOn, isPrepay);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }
    }
}
