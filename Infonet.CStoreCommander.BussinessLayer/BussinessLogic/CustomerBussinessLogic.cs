using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Customer;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Contains method for business operations regarding to Customers
    /// </summary>
    public class CustomerBussinessLogic : ICustomerBussinessLogic
    {
        private readonly ICustomerSerializeManager _serializeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager">Customer Serialize manager</param>
        public CustomerBussinessLogic(
            ICustomerSerializeManager serializeManager)
        {
            _serializeManager = serializeManager;
        }

        /// <summary>
        /// Sets the loyalty number to the customer 
        /// </summary>
        /// <param name="code">Code of the customer</param>
        /// <param name="loyalityNumber">Loyalty Number</param>
        /// <returns>True if customer is added otherwise False</returns>
        public async Task<bool> SetLoyaltyNumber(string code,
            string loyalityNumber)
        {
            var customerModel = new Customer
            {
                Code = code,
                LoyaltyNumber = loyalityNumber
            };

            return await _serializeManager.SetLoyaltyCustomer(customerModel);
        }

        /// <summary>
        /// Adds a Loyalty customer record
        /// </summary>
        /// <param name="customer">Customer Record</param>
        /// <returns>True if customer is added otherwise False</returns>
        public async Task<bool> Add(Customer customer)
        {
            return await _serializeManager.AddCustomer(customer);
        }

        /// <summary>
        /// Gets all the Customer Records
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="loyalty">Whether fetch loyalty customers or all customers</param>
        /// <returns>List of All Customers</returns>
        public async Task<List<Customer>> GetAll(int pageIndex,
            bool loyalty)
        {
            return await _serializeManager.GetAllCustomers(pageIndex, loyalty);
        }

        /// <summary>
        /// Searches for customers matching the search text
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="loyalty">Whether fetch loyalty customers or all customers</param>
        /// <returns>List of Matching Customer</returns>
        public async Task<object> Search(string searchTerm,
            int pageIndex, bool loyalty)
        {
            return await _serializeManager.SearchCustomers(searchTerm,
                pageIndex, loyalty);
        }

        /// <summary>
        /// Sets the specified customer to the ongoing sale
        /// </summary>
        /// <param name="customerCode">Customer code</param>
        /// <returns>Updated sale after the Customer is updated</returns>
        public async Task<Sale> SetCustomerForSale(string customerCode)
        {
            return await _serializeManager.SetToSale(customerCode);
        }

        public async Task<Customer> GetCustomerByCard(string customerCode, bool isLoyaltycard)
        {
            return await _serializeManager.getCustomerByCard(customerCode, isLoyaltycard);
        }
    }
}
