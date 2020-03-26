using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class GetSaleBySaleNumberSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        private readonly int _saleNumber;
        private readonly int _tillNumber;

        public GetSaleBySaleNumberSerializeAction(ISaleRestClient restClient,
            int tillNumber, int saleNumber)
            : base("GetSaleBySaleNumber")
        {
            _restClient = restClient;
            _saleNumber = saleNumber;
            _tillNumber = tillNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetSaleBySaleNumber(_tillNumber, _saleNumber);
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
