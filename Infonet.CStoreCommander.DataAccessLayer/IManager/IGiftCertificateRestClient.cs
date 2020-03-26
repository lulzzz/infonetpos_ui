using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IGiftCertificateRestClient
    {
        /// <summary>
        /// Gets the gift certificates
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetGiftCertificates(HttpContent content);

        /// <summary>
        /// Saves the gift certificates 
        /// </summary>
        /// <param name="content">HTTP Content</param>
        /// <returns></returns>
        Task<HttpResponseMessage> SaveGiftCertificates(HttpContent content);

        /// <summary>
        /// Method to Get Store Credits
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetStoreCredits(HttpContent content);

        /// <summary>
        /// Method to Save Store Credit
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SaveStoreCredits(HttpContent content);
    }
}
