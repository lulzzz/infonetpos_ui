using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Customer;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    /// <summary>
    /// Customer Business Logic
    /// </summary>
    public interface ICustomerBussinessLogic
    {
        /// <summary>
        /// Method to add new loyalty customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> Add(Customer customer);

        /// <summary>
        /// Method to get all customers
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="loyalty">Whether fetch loyalty customers or all customers</param>
        /// <returns>List of All Customer</returns>
        Task<List<Customer>> GetAll(int pageIndex, bool loyalty);

        /// <summary>
        /// Method to search for customers matching the search text
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="loyalty">Whether fetch loyalty customers or all customers</param>
        /// <returns>List of Matching Customer</returns>
        Task<object> Search(string searchTerm, 
            int pageIndex, bool loyalty);

        /// <summary>
        /// Method to set a particular customer for a sale
        /// </summary>
        /// <param name="customerCode">Code of the Selected customer</param>
        /// <returns>Sale model</returns>
        Task<Sale> SetCustomerForSale(string customerCode);

        Task<bool> SetLoyaltyNumber(string code, string loyalityNumber);

        Task<Customer> GetCustomerByCard(string customerCode, bool isLoyaltycard);
    }
}
