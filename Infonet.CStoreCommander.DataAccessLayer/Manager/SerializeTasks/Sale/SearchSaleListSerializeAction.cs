using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class SearchSaleListSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        private readonly int _saleNumber;
        private readonly int _pageIndex;
        private readonly string _date;

        public SearchSaleListSerializeAction(ISaleRestClient restClient, int pageIndex, int saleNumber,
            string date)
            : base("SearchSaleList")
        {
            _restClient = restClient;
            _saleNumber = saleNumber;
            _pageIndex = pageIndex;
            _date = date;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.SearchSaleList(_pageIndex, _saleNumber, _date);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleList = new DeSerializer().MapSaleList(data);
                    return new Mapper().MapSaleList(saleList);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
