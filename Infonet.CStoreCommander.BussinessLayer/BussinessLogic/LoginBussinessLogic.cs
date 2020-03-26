using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Login
    /// </summary>
    public class LoginBussinessLogic : ILoginBussinessLogic
    {
        private readonly ILoginSerializeManager _serializeManager;
        private readonly ICacheBusinessLogic _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager"></param>
        public LoginBussinessLogic(ILoginSerializeManager serializeManager,
            ICacheBusinessLogic cacheManager)
        {
            _serializeManager = serializeManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Returns the login policies
        /// </summary>
        /// <returns>Login policy model</returns>
        public async Task<LoginPolicy> GetLoginPolicyAsync()
        {
            var policies = await _serializeManager.GetLoginPolicy();
            _cacheManager.LoginPolicies = policies;
            return policies;
        }

        /// <summary>
        /// Returns Active shifts available for login
        /// </summary>
        /// <returns>Model of Active shifts</returns>
        public async Task<ActiveShifts> GetShiftsAsync()
        {
            var shifts =  await _serializeManager.GetActiveShifts();

            _cacheManager.UseShiftForTheDay = shifts.IsShiftUsedForDay;

            if (shifts.ForceShift)
            {
                _cacheManager.ShiftNumber = shifts.Shifts.FirstOrDefault().ShiftNumber;
                _cacheManager.ShiftDate = shifts.Shifts.FirstOrDefault().ShiftDate;
            }

            return shifts;
        }

        /// <summary>
        /// Returns Active tills available for login
        /// </summary>
        /// <returns>Model of Available tills</returns>
        public async Task<ActiveTills> GetTillsAsync()
        {
            var tills = await _serializeManager.GetTills();

            _cacheManager.CashFloat = tills.CashFloat.ToString(CultureInfo.InvariantCulture);

            if (tills.ForceTill)
            {
                _cacheManager.TillNumber = tills.Tills.FirstOrDefault();
                _cacheManager.ShiftNumber = tills.ShiftNumber;
                _cacheManager.ShiftDate = tills.ShiftDate;
                _cacheManager.CashFloat = tills.CashFloat.ToString(CultureInfo.InvariantCulture);
            }

            return tills;
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <returns>Authentication token</returns>
        public async Task<Login> LoginAsync()
        {
            var login = await _serializeManager.Login();
            _cacheManager.AuthKey = login.AuthToken;
            _cacheManager.TrainerCaption = login.TrainerCaption;
            return login;
        }

        public async Task<string> GetPassword()
        {
            var password = await _serializeManager.GetPassword();
            _cacheManager.Password = password;
            return password;
        }
    }
}
