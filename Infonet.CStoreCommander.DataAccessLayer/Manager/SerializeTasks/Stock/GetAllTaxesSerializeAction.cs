using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    internal class GetAllTaxesSerializeAction : SerializeAction
    {
        private readonly InfonetLog _log;
        private readonly IStockRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stockRestClient">Stock Rest Client</param>
        /// <param name="cacheManager">Cache manager</param>
        public GetAllTaxesSerializeAction(IStockRestClient stockRestClient, ICacheManager cacheManager)
            : base("GetAllTaxes")
        {
            _restClient = stockRestClient;
            _cacheManager = cacheManager;
            _log = InfonetLogManager.GetLogger<GetAllTaxesSerializeAction>();
        }

        /// <summary>
        /// Method to get all taxes 
        /// </summary>
        /// <returns></returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetAllTaxes();
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    //validate json
                    var taxCodes = new DeSerializer().MapTaxes(data);
                    return new Mapper().MapTaxCodes(taxCodes);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
