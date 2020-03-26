using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IMaintenanceSeralizeManager
    {
        /// <summary>
        /// Method to change password 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Success> ChangePassword(string password, string confirmPassword);

        Task<List<Report>> CloseBatch();

        Task<Error> Initialize();

        Task<bool> SetPrepayOrPostPay(bool isOn, bool isPrepay);
    }
}
