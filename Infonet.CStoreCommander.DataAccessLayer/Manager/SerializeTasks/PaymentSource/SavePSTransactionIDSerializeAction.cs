using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.PaymentSource
{
    public class SavePSTransactionIDSerializeAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        private int _TILL_NUM;
        private int _SALE_NO;
        private int _LINE_NUM;
        private string _TransID;



        public SavePSTransactionIDSerializeAction(IPaymentSourceRestClient _IPaymentSourceRestClient,
             int TILL_NUM, int SALE_NO, int LINE_NUM, string TransID )
            :base("SavePSTransactionIDAsync")
        {
            _restClient = _IPaymentSourceRestClient;
            _TILL_NUM = TILL_NUM;
            _SALE_NO = SALE_NO;
            _LINE_NUM = LINE_NUM;
            _TransID = TransID;

        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.SavePSTransactionIDAsync(_TILL_NUM, _SALE_NO, _LINE_NUM, _TransID);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Convert.ToBoolean(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
