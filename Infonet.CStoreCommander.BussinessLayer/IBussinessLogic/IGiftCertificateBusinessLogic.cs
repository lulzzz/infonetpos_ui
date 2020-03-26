using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IGiftCertificateBusinessLogic
    {
        /// <summary>
        /// Gets all the gift certificates 
        /// </summary>
        /// <returns></returns>
        Task<List<GiftCertificate>> GetGiftCertificates(decimal? amount,string tenderCode, string transactionType);

        /// <summary>
        /// Saves the provided gift certificates for the ongoing sale
        /// </summary>
        /// <param name="giftCertificates"></param>
        /// <returns></returns>
        Task<TenderSummary> SaveGiftCertificates(List<GiftCertificate> giftCertificates);

        Task<List<StoreCredit>> GetStoreCredit(string transactionType, 
            string tenderCode, string amountEntered);

        Task<TenderSummary> SaveStoreCredit(string transactionType,
            string tenderCode,
            List<StoreCredit> storeCredit);
    }
}
