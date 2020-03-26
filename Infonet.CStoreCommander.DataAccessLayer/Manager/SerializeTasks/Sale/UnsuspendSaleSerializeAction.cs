using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class UnsuspendSaleSerializeAction : SerializeAction
    {
        private readonly int _saleNumber;
        private readonly ISaleRestClient _saleRestClient;

        public UnsuspendSaleSerializeAction(ISaleRestClient saleRestClient, int saleNumber)
            : base("UnsuspendSale")
        {
            _saleNumber = saleNumber;
            _saleRestClient = saleRestClient;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _saleRestClient.UnsuspendSale(_saleNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSales = new DeSerializer().MapSale(data);
                    return  new Mapper().MapSale(suspendedSales);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
