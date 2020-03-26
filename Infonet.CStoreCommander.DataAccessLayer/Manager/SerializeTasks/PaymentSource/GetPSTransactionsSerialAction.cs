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
    public class GetPSTransactionsSerialAction: SerializeAction
    {
        private IPaymentSourceRestClient _restClient;
        private int _pastdays;
        private int _SaleNumber;
        private int _TillNumber;
        public GetPSTransactionsSerialAction(IPaymentSourceRestClient restClient,  int TillNumber, int SaleNumber, int PastDays)
            : base("GetPSTransactionsAsync")
        {
            _restClient = restClient;
            _TillNumber = TillNumber;
            _SaleNumber = SaleNumber;
            _pastdays = PastDays;

        }
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetPSTransactionsAsync(_TillNumber, _SaleNumber, _pastdays);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapPSTransactions(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
