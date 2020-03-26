using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IFuelPriceRestClient
    {
        Task<HttpResponseMessage> LoadPricesToDisplay();

        Task<HttpResponseMessage> SavePricesToDisplay(HttpContent content);

        Task<HttpResponseMessage> LoadPriceIncrementsAndDecrements(bool taxExempt);

        Task<HttpResponseMessage> SetPriceDecrement(HttpContent content);

        Task<HttpResponseMessage> SetPriceIncrement(HttpContent content);
    }
}
