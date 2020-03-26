using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    public class CompleteCashDropSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;
        private readonly CompleteCashDrop _completeCashDrop;

        public CompleteCashDropSerializeAction(ICashRestClient cashRestClient,
            CompleteCashDrop completeCashDrop)
            : base("CompleteCashDrop")
        {
            _cashRestClient = cashRestClient;
            _completeCashDrop = completeCashDrop;
        }

        protected async override Task<object> OnPerform()
        {
            var completeCashDrawContract = MapCompleteCashDropContract(_completeCashDrop);
            var completeCashDrawModel = JsonConvert.SerializeObject(completeCashDrawContract);
            var content = new StringContent(completeCashDrawModel, Encoding.UTF8, ApplicationJSON);

            var response = await _cashRestClient.CompleteCashCashdrop(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var returnedSale = new DeSerializer().MapReport(data);
                    return new Mapper().MapReport(returnedSale);
                default:
                    return await HandleExceptions(response);
            }
        }

        private CompleteCashDropContract MapCompleteCashDropContract(CompleteCashDrop _completeCashDrop)
        {
            var tempTender = new List<TenderContract>();

            foreach (var tender in _completeCashDrop.Tenders)
            {
                tempTender.Add(new TenderContract
                {
                    amountEntered = tender.AmountEntered,
                    tenderCode = tender.TenderCode
                });
            }

            var completeCashDropContract = new CompleteCashDropContract
            {
                dropReason = _completeCashDrop.DropReason,
                envelopeNumber = _completeCashDrop.EnvelopeNumber,
                registerNumber =_completeCashDrop.RegisterNumber,
                tillNumber = _completeCashDrop.TillNumber,
                tenders = tempTender
            };

            return completeCashDropContract;
        }
    }
}
