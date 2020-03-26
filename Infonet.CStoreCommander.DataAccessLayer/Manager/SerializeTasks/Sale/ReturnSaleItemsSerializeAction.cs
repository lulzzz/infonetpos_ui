using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class ReturnSaleItemsSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        private readonly int _saleNumber;
        private readonly int _tillNumber;
        private readonly List<int> _lineNumbers;
        private readonly string _reasonCode;
        private readonly string _reasonType;
        private readonly int _saleTillNumber;

        public ReturnSaleItemsSerializeAction(ISaleRestClient restClient,
            int saleNumber, int tillNumber, int saleTillNumber,
            List<int> lineNumber, string reasonCode, string reasonType)
            : base("ReturnSaleItems")
        {
            _restClient = restClient;
            _tillNumber = tillNumber;
            _saleTillNumber = saleTillNumber;
            _lineNumbers = lineNumber;
            _saleNumber = saleNumber;
            _reasonCode = reasonCode;
            _reasonType = reasonType;
        }

        protected async override Task<object> OnPerform()
        {
            var returnItems = new ReturnSaleItemContract
            {
                saleNumber = _saleNumber,
                saleLines = _lineNumbers,
                tillNumber = _tillNumber,
                saleTillNumber = _saleTillNumber,
                reasonCode = _reasonCode,
                reasonType = _reasonType
            };

            var reason = JsonConvert.SerializeObject(returnItems);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.ReturnSaleItems(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var returnedSale = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(returnedSale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
