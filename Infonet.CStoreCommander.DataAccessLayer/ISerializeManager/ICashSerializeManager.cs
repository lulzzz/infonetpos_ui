using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ICashSerializeManager
    {
        Task<CashDrawTypes> GetCashDrawTypes();

        Task<Report> CompleteCashDraw(CompleteCashDraw completeCashDraw);

        Task<List<Tender>> GetAllTenders(string transactionType,
          bool billTillClose, string dropreason);

        Task<List<CashButtons>> GetCashButtons();

        Task<UpdateTenderGet> UpdateTender(UpdateTenderPost updatedTender);

        Task<Report> CompleteCashDrop(CompleteCashDrop completeCashDrop);

        Task<bool> OpenCashDrawer(string openDrawerReason);
        
    }
}
