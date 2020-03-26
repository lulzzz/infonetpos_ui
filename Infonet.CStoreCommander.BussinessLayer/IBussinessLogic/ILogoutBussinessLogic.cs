using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface ILogoutBussinessLogic
    {
        /// <summary>
        /// Method to switch current user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> SwitchUserAsync(string userName, string password, bool isUnauthorizedAccess);

        /// <summary>
        /// Method to logout user
        /// </summary>
        /// <returns></returns>
        Task<bool> LogoutUser();

        Task<CloseTill> CloseTill();

        Task<ValidateTillClose> ValidateClosetill();

        Task<bool> EndShift();

        Task<bool> ReadTankDip();

        Task<CloseTill> UpdateTillClose(UpdatedTender updatedTender, UpdatedBillCoin updatedBillCoin);

        Task<FinishTillClose> FinishTillClose(bool? readTankDip, bool? readTotalizer);
    }
}
