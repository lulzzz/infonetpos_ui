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
    public class UpdateTenderForCashDropSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly UpdateTenderPost _updateTender;

        public UpdateTenderForCashDropSerializeAction(ICashRestClient cashRestClient,
            UpdateTenderPost updateTender,
            ICacheManager cacheManager)
            : base("UpdateTenderForCashDrop")
        {
            _cashRestClient = cashRestClient;
            _updateTender = updateTender;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var updateTenderContract = new UpdateTenderPostContract()
            {
                tenders = new List<TenderContract>(),
                dropReason = _updateTender.DropReason,
                tillNumber = _cacheManager.TillNumber,
                saleNumber = _cacheManager.SaleNumber
            };

            foreach(var tender in _updateTender.Tenders)
            {
                updateTenderContract.tenders.Add(new TenderContract
                {
                    amountEntered = tender.AmountEntered,
                    tenderCode = tender.TenderCode
                });
            }

            var completeCashDrawModel = JsonConvert.SerializeObject(updateTenderContract);
            var content = new StringContent(completeCashDrawModel, Encoding.UTF8, ApplicationJSON);

            var response = await _cashRestClient.UpdateTenderForCashdrop(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var updatedTendersGet = new DeSerializer().MapUpdatedTendersGet(data);
                    return new Mapper().MapUpdatedTendersGet(updatedTendersGet);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
