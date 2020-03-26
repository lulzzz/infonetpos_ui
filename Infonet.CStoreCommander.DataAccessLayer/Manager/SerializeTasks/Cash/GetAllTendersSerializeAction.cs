using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    public class GetAllTendersSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;
        private readonly string _transactionType;
        private readonly bool _billTillClose;
        private readonly string _dropreason;

        public GetAllTendersSerializeAction(
            ICashRestClient cashRestClient,
            string transactionType,
            bool billTillClose,
            string dropreason)
            : base("")
        {
            _transactionType = transactionType;
            _billTillClose = billTillClose;
            _dropreason = dropreason;
            _cashRestClient = cashRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _cashRestClient.GetAllTenders(_transactionType,
                _billTillClose, _dropreason);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tenderContract = new DeSerializer().MapTenders(data);
                    return new Mapper().MapTenders(tenderContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
