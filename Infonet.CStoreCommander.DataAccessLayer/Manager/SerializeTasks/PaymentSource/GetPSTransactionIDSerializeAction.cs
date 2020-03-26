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
    public class GetPSTransactionIDSerializeAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        public GetPSTransactionIDSerializeAction(IPaymentSourceRestClient PaymentSourceRestClient)
            :base("GetPSTransactionIDAsync")
        {
            _restClient = PaymentSourceRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetPSTransactionIDAsync();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapString(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
