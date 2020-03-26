using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Customer;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ICustomerSerializeManager
    {
        Task<bool> AddCustomer(Customer customer);

        Task<List<Customer>> GetAllCustomers(int pageSize, bool loyalty);

        Task<object> SearchCustomers(string searchTerm, int pageIndex,
            bool loyalty);

        Task<Sale> SetToSale(string customerCode);

        Task<bool> SetLoyaltyCustomer(Customer customer);

        Task<Customer> getCustomerByCard(string customerCode, bool isLoyaltycard);
    }
}
