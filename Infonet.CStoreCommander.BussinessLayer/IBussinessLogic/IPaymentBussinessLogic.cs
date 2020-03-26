using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IPaymentBussinessLogic
    {
        Task<ExactChange> PayByExactChange();

        Task<List<ARCustomer>> GetAllARCustomer(int pageIndex);

        Task<List<ARCustomer>> SearchARCustomer(string searchTearm, int pageIndex);

        Task<CheckoutSummary> SaveARPayment(string customerCode, string amount);

        Task<ARCustomer> GetARCustomerByCustomerCode(string cardNumber);

        Task<PaymentByFleet> VerifyFleet();

        Task<CheckoutSummary> PaymentByFleet(string cardNumber, string amount, bool isSwipe);
    }
}
