using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    /// <summary>
    /// Interface for Login Serialization manager
    /// </summary>
    public interface ILoginSerializeManager
    {
        /// <summary>
        /// Gets the active shifts
        /// </summary>
        /// <returns>Active shifts</returns>
        Task<ActiveShifts> GetActiveShifts();

        /// <summary>
        /// Gets the Active tills
        /// </summary>
        /// <returns>Active tills</returns>
        Task<ActiveTills> GetTills();

        /// <summary>
        /// Gets the Login policies 
        /// </summary>
        /// <returns>Login policies</returns>
        Task<LoginPolicy> GetLoginPolicy();

        /// <summary>
        /// Logs in to POS 
        /// </summary>
        /// <returns>Authentication token</returns>
        Task<Login> Login();

        Task<string> GetPassword();
    }
}
