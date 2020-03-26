using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockReportRestClient : IReportRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockReportRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }
        public async Task<HttpResponseMessage> GetReceiptHeader()
        {
            return new HttpResponseMessage();
        }
        public async Task<HttpResponseMessage> GetAllDepartment()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetAllDepartments.json",
                _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetAllShift()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetAllShifts.json",
                _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetAllTill()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetAllTills.json",
                 _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetFlashReport()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("FlashReport.json",
                  _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> GetKickBackReport(double kickBackBalancePoints)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetShiftCountReport(string departmentId,
            int tillNumber, int shiftNumber)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("SaleCountReport.json",
                _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetTillAuditReport()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("TillAuditReport.json",
               _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
