using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IGiftCertificateSerializeManager
    {
        /// <summary>
        /// Gets all the gift certificates 
        /// </summary>
        /// <returns></returns>
        Task<List<GiftCertificate>> GetGiftCertificates(decimal? amount, string tenderCode, string transactionType);

        /// <summary>
        /// Saves the provided gift certificates for the ongoing sale
        /// </summary>
        /// <param name="giftCertificates"></param>
        /// <returns></returns>
        Task<TenderSummary> SaveGiftCertificates(List<GiftCertificate> giftCertificates);


        Task<List<StoreCredit>> GetStoreCredits(string transactionType, string tenderCode,
            string amountEntered);

        Task<TenderSummary> SaveStoreCredits(string transactionType,
            string tenderCode,
            List<StoreCredit> giftCertificates);
    }
}
