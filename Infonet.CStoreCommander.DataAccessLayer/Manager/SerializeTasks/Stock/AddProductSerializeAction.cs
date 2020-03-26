using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    public class AddProductSerializeAction: SerializeAction
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly StockModel _stock;

        /// <summary>
        /// contructor for AddStockSerializeAction
        /// </summary>
        /// <param name="stockRestClient"></param>
        /// <param name="cacheManager"></param>
        /// <param name="stock"></param>
        public AddProductSerializeAction(
            IStockRestClient stockRestClient,
            ICacheManager cacheManager,
            StockModel stock)
            : base("AddStock")
        {
            _stockRestClient = stockRestClient;
            _cacheManager = cacheManager;
            _stock = stock;
        }

        /// <summary>
        /// Method to add stock
        /// </summary>
        /// <returns></returns>
        protected async  override Task<object> OnPerform()
        {
            var stockContract = new Mapper().MapStock(_stock);
            var customer = JsonConvert.SerializeObject(stockContract);
            var content = new StringContent(customer, Encoding.UTF8, ApplicationJSON);
            var response = await _stockRestClient.AddProduct(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var parsedData = new DeSerializer().MapSuccess(data);
                    return parsedData.success;
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
