using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class UpdateSaleItemSerializeAction : SerializeAction
    {
        private readonly string _discount;
        private readonly string _discountType;
        private readonly int _lineNumber;
        private readonly string _price;
        private readonly string _quantity;
        private readonly string _reason;
        private readonly string _reasonType;
        private readonly byte _registerNumber;
        private readonly int _saleNumber;
        private readonly int _tillNumber;
        private readonly ISaleRestClient _saleRestClient;

        public UpdateSaleItemSerializeAction(ISaleRestClient saleRestClient, int tillNumber, int saleNumber,
            int lineNumber, byte registerNumber, string discount, string discountType, string quantity,
            string price, string reason, string reasonType)
            : base("UpdateSaleLine")
        {
            if (discount.Contains("%"))
            {
                discount = discount.Replace("%", "");
            }
            if (discount.Contains("$"))
            {
                discount = discount.Replace("$", "");
            }

            _saleRestClient = saleRestClient;
            _tillNumber = tillNumber;
            _saleNumber = saleNumber;
            _lineNumber = lineNumber;
            _registerNumber = registerNumber;
            _discount = discount;
            _discountType = discountType;
            _quantity = quantity;
            _price = price;
            _reason = reason;
            _reasonType = reasonType;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _saleRestClient.UpdateSaleLine(_saleNumber, _tillNumber, _lineNumber,
                _registerNumber, _discount, _discountType, _quantity, _price, _reason, _reasonType);
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