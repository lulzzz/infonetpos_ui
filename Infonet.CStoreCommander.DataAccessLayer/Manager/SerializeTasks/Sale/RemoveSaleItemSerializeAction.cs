using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class RemoveSaleItemSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly int _tillNumber;
        private readonly int _saleNumber;
        private readonly int _lineNumber;

        public RemoveSaleItemSerializeAction(
            ISaleRestClient saleRestClient,
                int tillNumber, int saleNumber, int lineNumber)
            : base("RemoveSaleLine")
        {
            _saleRestClient = saleRestClient;
            _tillNumber = tillNumber;
            _saleNumber = saleNumber;
            _lineNumber = lineNumber;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _saleRestClient.RemoveSaleLine(
                _tillNumber, _saleNumber, _lineNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var sale = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(sale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
