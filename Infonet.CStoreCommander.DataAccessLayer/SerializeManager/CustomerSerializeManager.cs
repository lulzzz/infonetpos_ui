using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Customer;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class CustomerSerializeManager : SerializeManager,
        ICustomerSerializeManager
    {
        private readonly ICustomerRestClient _customerRestClient;
        private readonly ICacheManager _cacheManager;

        public CustomerSerializeManager(ICustomerRestClient customerRestClient,
            ICacheManager cacheManager)
        {
            _customerRestClient = customerRestClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method for calling add customer of AddCustomerSerializeAction
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<bool> AddCustomer(
            Customer customer)
        {
            var action = new AddCustomerSerializeAction(_customerRestClient, _cacheManager, customer);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        public async Task<List<Customer>> GetAllCustomers(int pageSize,
            bool loyalty)
        {
            var action = new GetAllCustomersSerializeAction(_customerRestClient,
                pageSize, loyalty);

            await PerformTask(action);

            return new Mapper().MapCustomers((List<CustomerContract>)action.ResponseValue);
        }

        public async Task<object> SearchCustomers(string searchTerm,
            int pageIndex, bool loyalty)
        {
            var action = new SearchCustomersSerializeAction(_customerRestClient,
                searchTerm, pageIndex, loyalty);

            await PerformTask(action);

            return (object)action.ResponseValue;
        }

        public async Task<Sale> SetToSale(string customerCode)
        {
            var action = new SetCustomerForSaleSerializeAction(_customerRestClient,
                _cacheManager, customerCode);

            await PerformTask(action);

            var saleContract = ((SaleContract)action.ResponseValue);
            return new Mapper().MapSale(saleContract);
        }

        public async Task<bool> SetLoyaltyCustomer(Customer customer)
        {
            var action = new SetLoyalityCustomerSerializeAction(_customerRestClient, customer);
            await PerformTask(action);
            return ((bool)action.ResponseValue);
        }

        public async Task<Customer> getCustomerByCard(string cardNumber, bool isLoyaltycard)
        {
            var action = new GetCustomerByCardSerializeAction(_customerRestClient, cardNumber,
                isLoyaltycard, _cacheManager.SaleNumber, _cacheManager.TillNumberForSale);
            await PerformTask(action);
            return new Mapper().MapCustomeFromContract((CustomerContract)action.ResponseValue);
        }
    }
}
