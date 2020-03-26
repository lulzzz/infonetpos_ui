using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using System;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using Infonet.CStoreCommander.EntityLayer;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Logout and Switch user
    /// </summary>
    public class LogoutBussinessLogic : ILogoutBussinessLogic
    {
        private readonly ILogoutSerializeManager _serializeManager;
        private readonly ICacheManager _cacheManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager">Logout Serialize manager</param>
        /// <param name="cacheManager">Cache manager</param>
        public LogoutBussinessLogic(ILogoutSerializeManager serializeManager,
            ICacheManager cacheManager,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _reportsBusinessLogic = reportsBusinessLogic;
            _serializeManager = serializeManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Switches the logged in user
        /// </summary>
        /// <param name="userName">User name of the new user</param>
        /// <param name="password">Password of the new user</param>
        /// <returns>New authentication token</returns>
        public async Task<string> SwitchUserAsync(string userName,
            string password, bool isUnauthorizedAccess)
        {
            var userModel = new User
            {
                UserName = userName,
                Password = password,
                FloatAmount = Convert.ToDecimal(_cacheManager.CashFloat),
                PosId = _cacheManager.LoginPolicies.PosID,
                ShiftNumber = _cacheManager.ShiftNumber,
                TillNumber = _cacheManager.TillNumber,
                UnauthorizedAccess = isUnauthorizedAccess
            };

            return await _serializeManager.SwitchUser(userModel);
        }

        /// <summary>
        /// Logs out a user of the application
        /// </summary>
        /// <returns>True if logs out successfully otherwise False</returns>
        public async Task<bool> LogoutUser()
        {
            return await _serializeManager.LogoutUser();
        }

        public async Task<CloseTill> CloseTill()
        {
            return await _serializeManager.CloseTill();
        }

        public async Task<ValidateTillClose> ValidateClosetill()
        {
            return await _serializeManager.ValidateClosetill();
        }

        public async Task<bool> EndShift()
        {
            return await _serializeManager.EndShift();
        }

        public async Task<bool> ReadTankDip()
        {
            return await _serializeManager.ReadTankDip();
        }

        public async Task<CloseTill> UpdateTillClose(UpdatedTender updatedTender, UpdatedBillCoin updatedBillCoin)
        {
            updatedTender = updatedTender ?? new UpdatedTender();
            updatedBillCoin = updatedBillCoin ?? new UpdatedBillCoin();

            return await _serializeManager.UpdateCloseTill(updatedTender, updatedBillCoin);
        }

        public async Task<FinishTillClose> FinishTillClose(bool? readTankDip, bool? readTotalizer)
        {
            var response = await _serializeManager.FinishTillClose(readTankDip, readTotalizer);

            foreach (var report in response.Reports)
            {
                if (report.ReportName.Contains(ReportType.EodDetailsFile))
                {
                    await _reportsBusinessLogic.SaveReport(report);
                }
                else if (report.ReportName.Contains(ReportType.TillCloseFile))
                {
                    await _reportsBusinessLogic.SaveReport(report);
                }
            }

            return response;
        }
    }
}
