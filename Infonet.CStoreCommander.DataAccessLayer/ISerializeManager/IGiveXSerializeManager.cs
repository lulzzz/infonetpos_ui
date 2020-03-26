using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IGiveXSerializeManager
    {
        /// <summary>
        /// Method to get givex card balance 
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns>GiveXCardBalanceModel</returns>
        Task<GiveXCardBalance> GetCardBalance(string givexCardNumber);


        /// <summary>
        /// Method to deactivate givex Card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>SaleModel</returns>
        Task<GivexSaleCard> DeactivateGivexCard(GiveXCard givexCard);

        /// <summary>
        /// Method to close batch
        /// </summary>
        /// <returns>success model</returns>
        Task<Report> CloseBatch();

        /// <summary>
        /// Method to activate givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns></returns>
        Task<GivexSaleCard> ActivateGivexCard(GiveXCard givexCard);


        /// <summary>
        /// Method to add amount in givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>sale model</returns>
        Task<GivexSaleCard> AddAmount(GiveXCard givexCard);

        /// <summary>
        /// Method to set amount in givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>sale model</returns>
        Task<GivexSaleCard> SetAmount(GiveXCard givexCard);

        /// <summary>
        /// method to get stock code for givex card
        /// </summary>
        /// <returns>stock code</returns>
        Task<string> GetStockCode();

        Task<GiveXReport> GetGiveXReport(string date);
    }
}
