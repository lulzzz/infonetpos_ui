using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    public class PriceCheckByCodeSerializeAction : SerializeAction
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly string _stockCode;

        public PriceCheckByCodeSerializeAction(IStockRestClient stockRestClient,
            string stockCode)
            : base("PriceCheck")
        {
            _stockCode = stockCode;
            _stockRestClient = stockRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _stockRestClient.CheckPriceByCode(_stockCode);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapStockPrice(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
