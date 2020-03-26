using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Logout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    class UpdateTillCloseSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;
        private readonly UpdateTillCloseContract _updateTillCloseContract;

        public UpdateTillCloseSerializeAction(ILogoutRestClient restClient,
            ICacheManager cacheManager, UpdatedTender updatedTender,
            UpdatedBillCoin updatedBillCoin) : base("UpdateTillClose")
        {
            _restClient = restClient;

            _updateTillCloseContract = new UpdateTillCloseContract
            {
                tillNumber = cacheManager.TillNumber,

                updatedBillCoin = new UpdatedBillCoinContract
                {
                    amount = string.IsNullOrEmpty(updatedBillCoin.Amount) ? "0" : updatedBillCoin.Amount,
                    description = updatedBillCoin.Description
                },

                updatedTender = new UpdatedTenderContract
                {
                    entered = string.IsNullOrEmpty(updatedTender.Entered) ? "0" : updatedTender.Entered,
                    name = updatedTender.Name
                }
            };

        }


        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_updateTillCloseContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.UpdateTillClose(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var closeTillContract = new DeSerializer().MapTillClose(data);
                    return new Mapper().MapTillClose(closeTillContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
