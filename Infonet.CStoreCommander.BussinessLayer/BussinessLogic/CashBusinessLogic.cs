using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class CashBusinessLogic : ICashBusinessLogic
    {
        private readonly ICashSerializeManager _seralizeManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        public CashBusinessLogic(ICashSerializeManager seralizeManager,
             IReportsBussinessLogic reportsBusinessLogic)
        {
            _seralizeManager = seralizeManager;
            _reportsBusinessLogic = reportsBusinessLogic;
        }

        public async Task<Report> CompleteCashDraw(CompleteCashDraw completeCashDraw)
        {
            var response = await _seralizeManager.CompleteCashDraw(completeCashDraw);
            await _reportsBusinessLogic.SaveReport(response);
            return response;
        }

        public async Task<Report> CompleteCashDrop(CompleteCashDrop completeCashDrop)
        {
            var response = await _seralizeManager.CompleteCashDrop(completeCashDrop);
            await _reportsBusinessLogic.SaveReport(response);
            return response;
        }

        public async Task<List<Tender>> GetAllTenders(string transactionType, bool billTillClose, string dropreason)
        {
            return await _seralizeManager.GetAllTenders(transactionType,
                billTillClose, dropreason);
        }

        public async Task<List<CashButtons>> GetCashButtons()
        {
            return await _seralizeManager.GetCashButtons();
        }

        public async Task<CashDrawTypes> GetCashDrawType()
        {
            return await _seralizeManager.GetCashDrawTypes();
        }

        public async Task<UpdateTenderGet> UpdateTender(UpdateTenderPost updatedTender)
        {
            return await _seralizeManager.UpdateTender(updatedTender);
        }

        public async Task<bool> OpenCashDrawer(string openDrawerReason)
        {
            return await _seralizeManager.OpenCashDrawer(openDrawerReason);
        }
    }
}
