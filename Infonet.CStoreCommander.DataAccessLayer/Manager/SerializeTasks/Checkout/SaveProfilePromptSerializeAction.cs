using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class SaveProfilePromptSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly PaymentByFleetContract _paymentByFleetContract;

        public SaveProfilePromptSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string cardNumber, string profileId,
            Dictionary<string, string> prompts)
            : base("SaveProfilePrompt")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _paymentByFleetContract = new PaymentByFleetContract
            {
                cardNumber = cardNumber,
                profileId = profileId,
                prompts = (from p in prompts
                          select new PromptContract
                          {
                              promptAnswer = p.Value,
                              promptMessage = p.Key
                          }).ToList(),
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var paymentByFleetContract = JsonConvert.SerializeObject(_paymentByFleetContract);
            var content = new StringContent(paymentByFleetContract, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.SaveProfilePrompt(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var poNumber = new DeSerializer().MapPONumber(data);
                    return poNumber;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
