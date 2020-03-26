using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Collections.Generic;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class PaymentSerializeManager : SerializeManager, IPaymentSerializeManager
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly ICacheManager _cacheManager;

        public PaymentSerializeManager(IPaymentRestClient paymentRestClient,
            ICacheManager cacheManager)
        {
            _paymentRestClient = paymentRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<ExactChange> PayByExactCash()
        {
            var action = new ExactChangeSerializeAction(_paymentRestClient,
                _cacheManager);

            await PerformTask(action);

            return (ExactChange)action.ResponseValue;
        }

        public async Task<List<ARCustomer>> SearchARCustomer(string searchTearm, int pageIndex)
        {
            var action = new SearchARCustomerSerializeAction(_paymentRestClient,
            pageIndex, searchTearm);

            await PerformTask(action);
            var customers = (List<ARCustomerContract>)action.ResponseValue;
            return new Mapper().MapCustomers(customers);
        }

        public async Task<List<ARCustomer>> GetAllARCustomer(int pageIndex)
        {
            var action = new GetAllARCustomerSerializeAction(_paymentRestClient,
             pageIndex);

            await PerformTask(action);
            var customers = (List<ARCustomerContract>)action.ResponseValue;
            return new Mapper().MapCustomers(customers);
        }

        public async Task<CheckoutSummary> SaveARPayment(string customerCode,
            string amount)
        {
            var action = new SaveARPaymentSerializeAction(_paymentRestClient,
              _cacheManager, customerCode, amount);
            await PerformTask(action);
            return (CheckoutSummary)action.ResponseValue;
        }

        public async Task<ARCustomer> SearchARCustomer(string cardNumber)
        {
            var action = new GetARCustomerByCustomerCodeSerializeAction(_paymentRestClient, cardNumber);
            await PerformTask(action);
            var customer = (ARCustomerContract)action.ResponseValue;
            return new Mapper().MapARCustomer(customer);
        }

        public async Task<PaymentByFleet> VerifyFleet()
        {
            var action = new VerifyFleetSerializeAction(_paymentRestClient);
            await PerformTask(action);
            return (PaymentByFleet)action.ResponseValue;
        }

        public async Task<CheckoutSummary> PaymentByFleet(string cardNumber, string amount,
            bool isSwipe)
        {
            var action = new PaymentByFleetSerializeAction(_paymentRestClient,
            _cacheManager, cardNumber, amount, isSwipe);
            await PerformTask(action);
            return (CheckoutSummary)action.ResponseValue;
        }
    }
}
