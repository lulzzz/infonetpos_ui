using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class PaymentBussinessLogic : IPaymentBussinessLogic
    {
        private readonly IPaymentSerializeManager _seralizemanager;
        private readonly IReportsBussinessLogic _reportBusinessLogic;

        public PaymentBussinessLogic(IPaymentSerializeManager seralizemanager,
            IReportsBussinessLogic reportBusinessLogic)
        {
            _seralizemanager = seralizemanager;
            _reportBusinessLogic = reportBusinessLogic;
        }

        public async Task<List<ARCustomer>> GetAllARCustomer(int pageIndex)
        {
            return await _seralizemanager.GetAllARCustomer(pageIndex);
        }

        public async Task<ARCustomer> GetARCustomerByCustomerCode(string cardNumber)
        {
            return await _seralizemanager.SearchARCustomer(cardNumber);
        }

        public async Task<ExactChange> PayByExactChange()
        {
            var response =  await _seralizemanager.PayByExactCash();
            await _reportBusinessLogic.SaveReport(response.Report);
            return response;
        }

        public async Task<CheckoutSummary> PaymentByFleet(string cardNumber, string amount,
            bool isSwipe)
        {
            return await _seralizemanager.PaymentByFleet(cardNumber, 
                string.IsNullOrEmpty(amount) ? "0" :
               amount, isSwipe);
        }

        public async Task<CheckoutSummary> SaveARPayment(string customerCode, string amount)
        {
            return await _seralizemanager.SaveARPayment(customerCode, string.IsNullOrEmpty(amount) ? "0" :
                amount);
        }

        public async Task<List<ARCustomer>> SearchARCustomer(string searchTearm, int pageIndex)
        {
            return await _seralizemanager.SearchARCustomer(searchTearm, pageIndex);
        }

        public async Task<PaymentByFleet> VerifyFleet()
        {
            return await _seralizemanager.VerifyFleet();
        }
    }
}
