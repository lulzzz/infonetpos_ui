using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Home
{
    /// <summary>
    /// New sale Serialization Action
    /// </summary>
    internal class InitializeNewSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Contrsuctor
        /// </summary>
        /// <param name="saleRestClient">Sale rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public InitializeNewSaleSerializeAction(
            ISaleRestClient saleRestClient,
            ICacheManager cacheManager) : base("InitializeNewSale")
        {
            _restClient = saleRestClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Sale</returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.InitializeNewSale();

            //response ok to continue
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var newSale = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(newSale);
                default:
                    return await HandleExceptions(response);
            }
        }

    }
}
