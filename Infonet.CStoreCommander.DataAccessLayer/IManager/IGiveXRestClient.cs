using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IGiveXRestClient
    {
        /// <summary>
        /// Method to check balance ofr a card number
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetCardBalance(string givexCardNumber);

        /// <summary>
        /// method to deactivate givex
        /// </summary>
        /// <param name="content"></param>
        /// <returns>api resonse for deactivate givex card</returns>
        Task<HttpResponseMessage> DeactivateGivexcard(HttpContent content);

        /// <summary>
        /// Method to close batch
        /// </summary>
        /// <param name="content"></param>
        /// <returns>api response </returns>
        Task<HttpResponseMessage> CloseBatch(HttpContent content);

        /// <summary>
        /// Method to activate givex card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>api response</returns>
        Task<HttpResponseMessage> ActivateGivexCard(HttpContent content);

        /// <summary>
        /// Method to add amouynt in givex card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>api response message</returns>
        Task<HttpResponseMessage> AddAmount(HttpContent content);

        /// <summary>
        /// Method to adjust amount in givex card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>api response</returns>
        Task<HttpResponseMessage> SetAmount(HttpContent content);

        /// <summary>
        /// Method to get stock code for givex
        /// </summary>
        /// <returns>api response</returns>
        Task<HttpResponseMessage> getGivexStockCode();

        Task<HttpResponseMessage> GetGivexReport(string date);
    }
}
