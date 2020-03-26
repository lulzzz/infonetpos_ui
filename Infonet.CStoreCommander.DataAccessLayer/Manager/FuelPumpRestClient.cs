using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class FuelPumpRestClient : IFuelPumpRestClient
    {
        private readonly ICacheManager _cacheManager;

        public FuelPumpRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> InitializeFuelPump()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.InitializeFuelPump, _cacheManager.TillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetHeadOfficeNotification()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetHeadOfficeNotification, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPumpStatus(int tillNumber)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.GetPumpStatus, tillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadGrade(int pumpId, bool switchPrepay, int tillNumber)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.LoadGrades, pumpId, switchPrepay, tillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ResumeAll(HttpContent content = null)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ResumeAll, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> StopAll(HttpContent content = null)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.StopAll, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> AddPrepay(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddPrepay, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> DeletePrepay(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.DeletePrepay, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SwitchPrepay(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SwitchPrepay, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UpdateFuelPrice(int option, int counter)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.UpdateFuelPrice, option, counter);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> AddBasket(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddBasket, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.GetPumpAction, pumpId, isStopPressed, isResumePressed);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadTierLevel()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.LoadTierLevel, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UpdateTierLevel(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.UpdateTierLevel, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadPropaneGrade()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.LoadPropaneGrade, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadPropanePumps(int gradeId)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.LoadPropanePumps, gradeId);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> AddPropane(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddPropane, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> AddManually(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddManually, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadPrices(bool grouped)
        {
            var client = new HttpRestClient();
            return await client.GetAsync(grouped ? Urls.GroupFuelPrices : Urls.FuelPrices, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ReadTotalizer(int tillNumber)
        {
            var client = new HttpRestClient(new TimeSpan(0, 10, 0));
            var url = string.Format(Urls.ReadTotalizer, tillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetGroupBasePrice(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SetGroupBasePrice, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveGroupBasePrices(HttpContent content)
        {
            var client = new HttpRestClient(new TimeSpan(0, 10, 0));
            return await client.PostAsync(Urls.SaveGroupBasePrices, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetBasePrice(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SetBasePrice, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> VerifyBasePrices(HttpContent content)
        {
            var client = new HttpRestClient(new TimeSpan(0, 10, 0));
            return await client.PostAsync(Urls.VerifyBasePrices, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> VerifyGroupBasePrices(HttpContent content)
        {
            var client = new HttpRestClient(new TimeSpan(0, 10, 0));
            return await client.PostAsync(Urls.VerifyGroupBasePrices, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveBasePrices(HttpContent content)
        {
            var client = new HttpRestClient(new TimeSpan(0, 10, 0));
            return await client.PostAsync(Urls.SaveBasePrices, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetError()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetError, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ClearError()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.ClearError, _cacheManager.AuthKey);
        }
        public async Task<HttpResponseMessage> UncompleteOverPayment(HttpContent content)
        {
            var client = new HttpRestClient();

            return await client.PostAsync(Urls.UncompleteOverPayment, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UncompletePrepayChange(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.UncompletePrepayChange, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UncompletePrepayLoad()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.UncompletePrepayLoad, _cacheManager.TillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UncompleteDelete(int pumpId)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.UncompleteDelete, pumpId, _cacheManager.SaleNumber, _cacheManager.TillNumber);
            return await client.DeleteAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> StopBroadcast()
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.StopBroadcast, null, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CheckError()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.CheckError, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetFuelVolume(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GetFuelVolume, content, _cacheManager.AuthKey);
        }
    }
}
