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
    public class GetPSLogosSerializeAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        public GetPSLogosSerializeAction(IPaymentSourceRestClient _IPaymentSourceRestClient)
            :base("GetPSLogosAsync")
        {
            _restClient = _IPaymentSourceRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetPSLogosAsync();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return DeSerializer.MapPSLogos(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
