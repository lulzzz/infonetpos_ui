using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class AddBottleReturnSaleSerializeAction : SerializeAction
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly ICacheManager _cacheManager;

        private readonly BottleReturnSale _sale;

        /// <summary>
        /// contructor for AddBottleReturnSaleSerializeAction
        /// </summary>
        /// <param name="stockRestClient">Stock Rest Client</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="sale">Bottle Return Sale</param>
        public AddBottleReturnSaleSerializeAction(
            IStockRestClient stockRestClient,
            ICacheManager cacheManager,
            BottleReturnSale sale)
            : base("AddBottleReturnSale")
        {
            _stockRestClient = stockRestClient;
            _cacheManager = cacheManager;
            _sale = sale;
        }

        /// <summary>
        /// Method to Complete a Bottle Return Sale
        /// </summary>
        /// <returns></returns>
        protected override async Task<object> OnPerform()
        {
            _sale.SaleNumber = _cacheManager.SaleNumber;
            _sale.TillNumber = _cacheManager.TillNumberForSale;
            _sale.Register = _cacheManager.RegisterNumber;

            var saleContract = new Mapper().MapBottleReturnSale(_sale);
            var sale = JsonConvert.SerializeObject(saleContract);
            var content = new StringContent(sale, Encoding.UTF8, ApplicationJSON);
            var response = await _stockRestClient.AddBottleReturnSale(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapBottleReturn(data);
                    return result;
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
