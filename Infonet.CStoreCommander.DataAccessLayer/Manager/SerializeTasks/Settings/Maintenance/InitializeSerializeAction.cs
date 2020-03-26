using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Settings.Maintenance;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Settings.Maintenance
{
    public class InitializeSerializeAction : SerializeAction
    {
        private readonly IMaintenanceRestClient _maintenanceRestClient;
        private readonly CloseBatchContract _closeBatchContract;

        public InitializeSerializeAction(IMaintenanceRestClient maintenanceRestClient,
            ICacheManager cacheManager) 
            : base("Initialize")
        {
            _maintenanceRestClient = maintenanceRestClient;
            _closeBatchContract = new CloseBatchContract
            {
                posId = cacheManager.LoginPolicies.PosID,
                registerNumber = cacheManager.RegisterNumber,
                saleNumber = cacheManager.SaleNumber,
                tillNumber = cacheManager.TillNumber
            };

        }

        protected async override Task<object> OnPerform()
        {
            var changePassword = JsonConvert.SerializeObject(_closeBatchContract);
            var content = new StringContent(changePassword, Encoding.UTF8, ApplicationJSON);
            var response = await _maintenanceRestClient.Initialize(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    var parsedData = new DeSerializer().MapError(data);
                    return new Mapper().MapError(parsedData);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
