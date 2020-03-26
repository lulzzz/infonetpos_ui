using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.PaymentSource
{
    public class GetPSProductsSerializeAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        public GetPSProductsSerializeAction(IPaymentSourceRestClient PaymentSourceRestClient)
            :base("GetPSProductsAsync")
        {
            _restClient = PaymentSourceRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetPSProductsAsync();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapPSProds(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
