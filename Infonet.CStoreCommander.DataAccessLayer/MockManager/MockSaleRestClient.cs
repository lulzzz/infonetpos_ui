using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockSaleRestClient : ISaleRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockSaleRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> GetAllSuspendedSale()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(
                    await Helper.GetOfflineResponse("SuspendedSales.json",
                    _storageInstalledFolder),
                    Encoding.UTF8,
                    "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetSaleList(int pageIndex)
        {
            if (pageIndex == 1)
            {
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = new StringContent(
                    await Helper.GetOfflineResponse("SaleList.json",
                    _storageInstalledFolder),
                    Encoding.UTF8,
                    "application/json")
                };
                return response;
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("",
                       Encoding.UTF8, "application/json")
            };
        }

        public async Task<HttpResponseMessage> InitializeNewSale()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("InitializeNewSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }


        public async Task<HttpResponseMessage> SearchSaleList(int pageIndex, int searchText, string saleDate)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("SearchSaleList.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> SuspendSale()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("SuspendSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Method to verify the stock restrictions beforing adding to sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="stockCode">Stock code</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="isReturn">Is Return</param>
        /// <returns>Http response frm the API</returns>
        public async Task<HttpResponseMessage> VerifyStock(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(
                    await Helper.GetOfflineResponse("VerifyStock.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> AddStockToSale(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(
                    await Helper.GetOfflineResponse("AddStockToSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> UnsuspendSale(int saleNumber)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("UnsuspendSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> VoidSale(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("VoidSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> ReturnSale(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("ReturnSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetSaleBySaleNumber(int tillNumber, int saleNumber)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetSaleBySaleNumber.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> ReturnSaleItems(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("RetunSaleItems.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Removes the sale line item from the sale
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="lineNumber">Line number</param>
        /// <returns>Http Response from the API</returns>
        public async Task<HttpResponseMessage> RemoveSaleLine(int tillNumber,
            int saleNumber, int lineNumber)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("InitializeNewSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Updates the existing Sale line item
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="lineNumber">Line number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="discount">Discount</param>
        /// <param name="discountType">Discount type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Price</param>
        /// <param name="reason">Reason</param>
        /// <param name="reasonType">Reason type</param>
        /// <returns>Sale model</returns>
        public async Task<HttpResponseMessage> UpdateSaleLine(int saleNumber, int tillNumber,
            int lineNumber, byte registerNumber,
            string discount, string discountType, string quantity, string price,
            string reason, string reasonType)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("InitializeNewSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Writes off the sale 
        /// </summary>
        /// <param name="content">HTTP content payload</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> WriteOff(StringContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("WriteOff.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> SetTaxExemption(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("InitializeNewSale.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> ValidateVoidSale()
        {
            throw new NotImplementedException();
        }
    }
}
