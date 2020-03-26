using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class GetSaleListSerializeAction : SerializeAction
    {
        private ISaleRestClient _saleRestClient;
        private readonly int _pageIndex;

        public GetSaleListSerializeAction(ISaleRestClient saleRestClient,
            int pageIndex)
            : base("GetSaleList")
        {
            _saleRestClient = saleRestClient;
            _pageIndex = pageIndex;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _saleRestClient.GetSaleList(_pageIndex);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleListContract = new DeSerializer().MapSaleList(data);
                    return new Mapper().MapSaleList(saleListContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
