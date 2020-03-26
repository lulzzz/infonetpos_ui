using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ILogoutSerializeManager
    {
       /// <summary>
       /// Method to switch user
       /// </summary>
       /// <param name="userModel"></param>
       /// <returns>auth token</returns>
        Task<string> SwitchUser(User userModel);

        Task<bool> LogoutUser();

        Task<CloseTill> CloseTill();

        Task<ValidateTillClose> ValidateClosetill();

        Task<bool> EndShift();

        Task<bool> ReadTankDip();

        Task<CloseTill> UpdateCloseTill(UpdatedTender updatedTender,
            UpdatedBillCoin updatedBillCoin);

        Task<FinishTillClose> FinishTillClose(bool? readTankDip, bool? readTotalizer);
    }
}
