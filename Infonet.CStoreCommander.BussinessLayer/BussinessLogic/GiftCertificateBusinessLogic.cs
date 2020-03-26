using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class GiftCertificateBusinessLogic : IGiftCertificateBusinessLogic
    {
        private readonly IGiftCertificateSerializeManager _serializeManager;

        public GiftCertificateBusinessLogic(IGiftCertificateSerializeManager serializeManager)
        {
            _serializeManager = serializeManager;
        }



        /// <summary>
        /// Gets all the gift certificates 
        /// </summary>
        /// <returns></returns>
        public async Task<List<GiftCertificate>> GetGiftCertificates(decimal? amount, string tenderCode, string transactionType)
        {
            return await _serializeManager.GetGiftCertificates(amount, tenderCode, transactionType);
        }

        public async Task<List<StoreCredit>> GetStoreCredit(string transactionType, string tenderCode, string amountEntered)
        {
            return await _serializeManager.GetStoreCredits(transactionType, tenderCode,
                amountEntered);
        }

        /// <summary>
        /// Saves the provided gift certificates for the ongoing sale
        /// </summary>
        /// <param name="giftCertificates"></param>
        /// <returns></returns>
        public async Task<TenderSummary> SaveGiftCertificates(List<GiftCertificate> giftCertificates)
        {
            return await _serializeManager.SaveGiftCertificates(giftCertificates);
        }

        public async Task<TenderSummary> SaveStoreCredit(string transactionType, string tenderCode, List<StoreCredit> storeCredit)
        {
            return await _serializeManager.SaveStoreCredits(transactionType, tenderCode,
                storeCredit);
        }
    }
}
