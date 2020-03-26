using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class ReturnSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        private readonly int _saleNumber;
        private readonly int _tillNumber;
        public readonly int _saleTillNumber;
        private readonly bool _isCorrection;
        private readonly string _reasonCode;
        private readonly string _reason;

        public ReturnSaleSerializeAction(ISaleRestClient restClient,
            int saleNumber, int tillNumber, int saleTillNumber,
            bool isCorrection, string reasonCode, string reason)
            : base("ReturnSale")
        {
            _restClient = restClient;
            _saleNumber = saleNumber;
            _tillNumber = tillNumber;
            _saleTillNumber = saleTillNumber;
            _isCorrection = isCorrection;
            _reason = reason;
            _reasonCode = reasonCode;
        }

        protected async override Task<object> OnPerform()
        {
            var returnSale = new ReturnSaleContract
            {
                isCorrection = _isCorrection,
                saleNumber = _saleNumber,
                tillNumber = _tillNumber,
                saleTillNumber = _saleTillNumber,
                reasonCode = _reasonCode,
                reasonType = _reason
            };

            var reason = JsonConvert.SerializeObject(returnSale);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.ReturnSale(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var returnedSale = new DeSerializer().MapGivexSaleContract(data);
                    return new Mapper().MapGivexSaleContract(returnedSale);
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
