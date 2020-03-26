using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash
{
    public class CompleteCashDrawSerializeAction : SerializeAction
    {
        private readonly ICashRestClient _cashRestClient;
        private readonly CompleteCashDraw _completeCashDraw;

        public CompleteCashDrawSerializeAction(ICashRestClient cashRestClient,
            CompleteCashDraw completeCashDraw)
            : base("CompleteCashDraw")
        {
            _completeCashDraw = completeCashDraw;
            _cashRestClient = cashRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var completeCashDrawContract = MapCompleteCashDrawContract();
            var completeCashDrawModel = JsonConvert.SerializeObject(completeCashDrawContract);
            var content = new StringContent(completeCashDrawModel, Encoding.UTF8, ApplicationJSON);

            var response = await _cashRestClient.CompleteCashDraw(content);
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

        private CompleteCashDrawContract MapCompleteCashDrawContract()
        {
            var completeCashdrawContract = new CompleteCashDrawContract
            {
                amount = _completeCashDraw.Amount.ToString(CultureInfo.InvariantCulture),
                drawReason = _completeCashDraw.DrawReason.ToString(),
                registerNumber = _completeCashDraw.RegisterNumber,
                tillNumber = _completeCashDraw.TillNumber,
                coins = new List<CurrencyContract>(),
                bills = new List<CurrencyContract>()
            };

            foreach (var coin in _completeCashDraw.Coins)
            {
                completeCashdrawContract.coins.Add(
                    new CurrencyContract
                    {
                        currencyName = coin.CurrencyName,
                        quantity = coin.Quantity,
                        value = coin.Value.ToString(CultureInfo.InvariantCulture)
                    });
            }

            foreach (var bill in _completeCashDraw.Bills)
            {
                completeCashdrawContract.bills.Add(
                    new CurrencyContract
                    {
                        currencyName = bill.CurrencyName,
                        quantity = bill.Quantity,
                        value = bill.Value.ToString(CultureInfo.InvariantCulture)
                    });
            }
            return completeCashdrawContract;
        }
    }
}
