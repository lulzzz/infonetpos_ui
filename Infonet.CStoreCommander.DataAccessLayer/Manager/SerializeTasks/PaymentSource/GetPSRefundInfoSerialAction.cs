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
   public class GetPSRefundInfoSerialAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        private string _TransID;
        private int _SaleNumber;
        private int _TillNumber;
        public GetPSRefundInfoSerialAction(IPaymentSourceRestClient restClient, string TransactionID,int SaleNumber,int TillNumber)
            :base("GetPSRefundInfoAsync")
        {
            _restClient = restClient;
            _TransID = TransactionID;
            _SaleNumber = SaleNumber;
            _TillNumber = TillNumber;
        }

        protected  override async Task<object> OnPerform()
        {
            var response = await _restClient.GetPSRefundInfoAsync(_TransID, _SaleNumber, _TillNumber);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return DeSerializer.MapPSRefundInfo(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
