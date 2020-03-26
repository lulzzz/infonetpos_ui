using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock;
using System.Collections.Generic;
using System.Globalization;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    public class PriceChangeSerializeAction : SerializeAction
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly ChangePrice _changePrice;
        private readonly bool _isRegularPriceChange;
        public PriceChangeSerializeAction(IStockRestClient stockRestClient,
            ChangePrice changePrice, bool isRegularPriceChange)
            : base("PriceChange")
        {
            _stockRestClient = stockRestClient;
            _changePrice = changePrice;
            _isRegularPriceChange = isRegularPriceChange;
        }

        protected async override Task<object> OnPerform()
        {
            HttpResponseMessage response;

            var reason = JsonConvert.SerializeObject(MapChangePrice(_isRegularPriceChange));
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            if (_isRegularPriceChange)
            {
                response = await _stockRestClient.ChangeRegularPrice(content);
            }
            else
            {
                response = await _stockRestClient.ChangeSpecialPrice(content);
            }

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapStockPrice(data);
                default:
                    return await HandleExceptions(response);
            }
        }

        private ChangePriceContract MapChangePrice(bool isRegularPrice)
        {
            var changePriceContract = new ChangePriceContract();
            if (isRegularPrice)
            {
                changePriceContract = new ChangePriceContract
                {
                    stockCode = _changePrice.StockCode,
                    registerNumber = _changePrice.RegisterNumber,
                    tillNumber = _changePrice.TillNumber,
                    saleNumber = _changePrice.SaleNumber,
                    regularPrice = _changePrice.RegularPrice.ToString(CultureInfo.InvariantCulture)
                };
            }
            else
            {
                changePriceContract = new ChangePriceContract
                {
                    fromdate = _changePrice.Fromdate,
                    stockCode = _changePrice.StockCode,
                    registerNumber = _changePrice.RegisterNumber,
                    tillNumber = _changePrice.TillNumber,
                    saleNumber = _changePrice.SaleNumber,
                    regularPrice = _changePrice.RegularPrice.ToString(CultureInfo.InvariantCulture),
                    gridPrices = MapGridPriceContract(_changePrice.GridPricesContract),
                    isEndDate = _changePrice.IsEndDate,
                    perDollarChecked = _changePrice.PerDollarChecked,
                    priceType = _changePrice.PriceType,
                    todate = _changePrice.Todate

                };
            }
            return changePriceContract;
        }

        private List<GridPricesContract> MapGridPriceContract(List<GridPrices> GridPricesContract)
        {
            var gridPricesContract = new List<GridPricesContract>();

            foreach (var gridprice in GridPricesContract)
            {
                gridPricesContract.Add(new GridPricesContract
                {
                    column1 = gridprice.Column1,
                    column2 = gridprice.Column2,
                    column3 = gridprice.Column3
                });
            }
            return gridPricesContract;
        }
    }
}
