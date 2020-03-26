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
    public class GetPSVoucherInfoSerializeAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        private string _prodName;
        public GetPSVoucherInfoSerializeAction(IPaymentSourceRestClient PaymentSourceRestClient, string ProdName)
            : base("GetPSVoucherInfoAsync")
        {
            _restClient = PaymentSourceRestClient;
            _prodName = ProdName;
        }
        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetPSVoucherInfoAsync(_prodName);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return DeSerializer.MapPSVoucherInfo(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
