using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    class GetCashButtonsSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;

        public GetCashButtonsSerializeAction(ICashRestClient cashRestClient) 
            : base("GetCashButtons")
        {
            _cashRestClient = cashRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _cashRestClient.GetCashButtons();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var cashButtons = new DeSerializer().MapCashButtons(data);
                    return new Mapper().MapCashButtons(cashButtons);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
