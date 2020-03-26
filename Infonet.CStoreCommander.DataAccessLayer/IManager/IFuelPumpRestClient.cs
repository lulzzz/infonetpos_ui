using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IFuelPumpRestClient
    {
        Task<HttpResponseMessage> InitializeFuelPump();

        Task<HttpResponseMessage> GetHeadOfficeNotification();

        Task<HttpResponseMessage> GetPumpStatus(int tillNumber);

        Task<HttpResponseMessage> LoadGrade(int pumpId, bool switchPrepay, int tillNumber);

        Task<HttpResponseMessage> ResumeAll(HttpContent content = null);

        Task<HttpResponseMessage> StopAll(HttpContent content = null);

        Task<HttpResponseMessage> AddPrepay(HttpContent content);

        Task<HttpResponseMessage> DeletePrepay(HttpContent content);

        Task<HttpResponseMessage> SwitchPrepay(HttpContent content);

        Task<HttpResponseMessage> UpdateFuelPrice(int option, int counter);

        Task<HttpResponseMessage> LoadPrices(bool grouped);

        Task<HttpResponseMessage> ReadTotalizer(int tillNumber);

        Task<HttpResponseMessage> AddBasket(HttpContent content);

        Task<HttpResponseMessage> GetPumpAction(int pumpId, bool isStopPressed, bool isResumePressed);

        Task<HttpResponseMessage> LoadTierLevel();

        Task<HttpResponseMessage> UpdateTierLevel(HttpContent content);

        Task<HttpResponseMessage> LoadPropaneGrade();

        Task<HttpResponseMessage> LoadPropanePumps(int gradeId);

        Task<HttpResponseMessage> AddPropane(HttpContent content);

        Task<HttpResponseMessage> AddManually(HttpContent content);

        Task<HttpResponseMessage> SetGroupBasePrice(HttpContent content);

        Task<HttpResponseMessage> SaveGroupBasePrices(HttpContent content);

        Task<HttpResponseMessage> SetBasePrice(HttpContent content);

        Task<HttpResponseMessage> VerifyBasePrices(HttpContent content);

        Task<HttpResponseMessage> VerifyGroupBasePrices(HttpContent content);

        Task<HttpResponseMessage> SaveBasePrices(HttpContent content);

        Task<HttpResponseMessage> GetError();

        Task<HttpResponseMessage> ClearError();

        Task<HttpResponseMessage> UncompletePrepayLoad();

        Task<HttpResponseMessage> UncompletePrepayChange(HttpContent content);

        Task<HttpResponseMessage> UncompleteOverPayment(HttpContent content);

        Task<HttpResponseMessage> UncompleteDelete(int pumpId);

        Task<HttpResponseMessage> CheckError();

        Task<HttpResponseMessage> StopBroadcast();

        Task<HttpResponseMessage> GetFuelVolume(HttpContent content);
    }
}
