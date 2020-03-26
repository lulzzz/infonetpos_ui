using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    /// <summary>
    /// Interface for Login Screen Manager
    /// </summary>
    public interface ILoginBussinessLogic
    {
        /// <summary>
        /// Gets the tills 
        /// </summary>
        /// <returns>Tills</returns>
        Task<ActiveTills> GetTillsAsync();

        /// <summary>
        /// Gets the login policy
        /// </summary>
        /// <returns>Login Policies</returns>
        Task<LoginPolicy> GetLoginPolicyAsync();

        /// <summary>
        /// Gets the Shifts
        /// </summary>
        /// <returns>Active Shifts</returns>
        Task<ActiveShifts> GetShiftsAsync();

        /// <summary>
        /// Logs in to the POS System
        /// </summary>
        /// <returns>Authentication token used for the API calls</returns>
        Task<Login> LoginAsync();

        Task<string> GetPassword();
    }
}
