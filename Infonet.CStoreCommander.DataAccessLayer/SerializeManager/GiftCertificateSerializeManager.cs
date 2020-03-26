using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class GiftCertificateSerializeManager : SerializeManager,
        IGiftCertificateSerializeManager
    {
        private readonly IGiftCertificateRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public GiftCertificateSerializeManager(IGiftCertificateRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets all the gift certificates 
        /// </summary>
        /// <returns></returns>
        public async Task<List<GiftCertificate>> GetGiftCertificates(decimal? amount, string tenderCode, string transactionType)
        {
            var action = new GetGiftCertificatesSerializeAction(_restClient,
                _cacheManager, amount, tenderCode, transactionType);

            await PerformTask(action);

            return (List<GiftCertificate>)action.ResponseValue;
        }

        /// <summary>
        /// Saves the provided gift certificates for the ongoing sale
        /// </summary>
        /// <param name="giftCertificates"></param>
        /// <returns></returns>
        public async Task<TenderSummary> SaveGiftCertificates(List<GiftCertificate> giftCertificates)
        {
            var action = new SaveGiftCertificatesSerializeAction(_restClient,
                _cacheManager, giftCertificates);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }

        public async Task<List<StoreCredit>> GetStoreCredits(string transactionType, string tenderCode, string amountEntered)
        {
            var action = new GetStoreCreditSerializeAction(_restClient,
                  _cacheManager, transactionType, tenderCode, amountEntered);

            await PerformTask(action);

            return (List<StoreCredit>)action.ResponseValue;
        }

        public async Task<TenderSummary> SaveStoreCredits(
            string transactionType,
            string tenderCode,
            List<StoreCredit> giftCertificates)
        {
            var action = new SaveStoreCreditSerializeAction(_restClient,
                 _cacheManager, transactionType, tenderCode, giftCertificates);

            await PerformTask(action);

            return (TenderSummary)action.ResponseValue;
        }
    }
}
