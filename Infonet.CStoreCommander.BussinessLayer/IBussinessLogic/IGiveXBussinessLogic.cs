using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IGiveXBussinessLogic
    {
        /// <summary>
        /// Method to get card balance for given card number
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns></returns>
        Task<GiveXCardBalance> CardBalance(string givexCardNumber);

        /// <summary>
        /// Method to deactivate givex card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <param name="givexPrice"></param>
        /// <returns>sale object</returns>
        Task<GivexSaleCard> DeactivateCard(string givexCardNumber, decimal givexPrice, string stockCodeForGivexCard);

        /// <summary>
        /// Method to close batch
        /// </summary>
        /// <returns>success model</returns>
        Task<Report> CloseBatch();

        /// <summary>
        /// Method to activate givex card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <param name="givexPrice"></param>
        /// <returns>sale object</returns>
        Task<GivexSaleCard> ActivateCard(string givexCardNumber, decimal givexPrice, string stockCodeForGivexCard);

        /// <summary>
        /// method to add amount in givex card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <param name="givexPrice"></param>
        /// <returns>SaleModel</returns>
        Task<GivexSaleCard> AddAmount(string givexCardNumber, decimal givexPrice, string stockCodeForGivexCard);

        /// <summary>
        /// Method to set amount in givex card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <param name="givexPrice"></param>
        /// <returns>sale model</returns>
        Task<GivexSaleCard> SetAmount(string givexCardNumber, decimal givexPrice, string stockCodeForGivexCard);

        /// <summary>
        /// method to get stock code for givex
        /// </summary>
        /// <returns>stock code</returns>
        Task<string> GetGiveXStockCode();

        Task<GiveXReport> GetGiveXReport(string date);
    }
}
