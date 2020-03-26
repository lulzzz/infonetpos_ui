using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IMaintenanceBussinessLogic
    {
        /// <summary>
        /// Method to change password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>success model</returns>
        Task<Success> ChangePassword(string password, string confirmPassword);


        Task<List<Report>> CloseBatch();

        Task<Error> Initialize();

        Task<bool> SetPrepayOrPostPay(bool isOn, bool isPrepay);
    }
}
