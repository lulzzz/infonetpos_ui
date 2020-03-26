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
    internal class GetDownloadedFilesSerializeAction : SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        public GetDownloadedFilesSerializeAction(IPaymentSourceRestClient PaymentSourceRestClient)
            :base("GetDownloadedFilesAsync")
        {
            _restClient = PaymentSourceRestClient;
        }
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetDownloadedFilesAsync();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    //return new DeSerializer().MapPSLogo(data);
                    return Convert.ToBoolean(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
