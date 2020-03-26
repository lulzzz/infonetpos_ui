using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IPaymentSerializeManager
    {
        Task<ExactChange> PayByExactCash();

        Task<List<ARCustomer>> SearchARCustomer(string searchTearm, int pageIndex);

        Task<List<ARCustomer>> GetAllARCustomer(int pageIndex);

        Task<CheckoutSummary> SaveARPayment(string customerCode,
            string amount);

        Task<ARCustomer> SearchARCustomer(string cardNumber);

        Task<PaymentByFleet> VerifyFleet();

        Task<CheckoutSummary> PaymentByFleet(string cardNumber, string amount, bool isSwipe);
    }
}
