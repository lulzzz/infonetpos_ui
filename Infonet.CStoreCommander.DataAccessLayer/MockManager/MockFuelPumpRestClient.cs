using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockFuelPumpRestClient : IFuelPumpRestClient
    {
        public Task<HttpResponseMessage> AddBasket(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddManually(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddPrepay(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddPropane(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> CheckError()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ClearError()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeletePrepay(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetError()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetFuelVolume(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetHeadOfficeNotification()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> GetPumpStatus(int tillNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> InitializeFuelPump()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoadGrade(int pumpId, bool switchPrepay, int tillNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoadPrices(bool grouped)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoadPropaneGrade()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoadPropanePumps(int gradeId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> LoadTierLevel()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ReadTotalizer(int tillNumber)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ResumeAll(HttpContent content = null)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveBasePrices(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SaveGroupBasePrices(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SetBasePrice(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SetGroupBasePrice(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> StopAll(HttpContent content = null)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> StopBroadcast()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SwitchPrepay(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UncompleteDelete(int pumpId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UncompleteOverPayment(HttpContent content)
        {
            throw new NotImplementedException();
        }


        public Task<HttpResponseMessage> UncompletePrepayChange(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UncompletePrepayLoad()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateFuelPrice(int option, int counter)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateTierLevel(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> VerifyBasePrices(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> VerifyGroupBasePrices(HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
