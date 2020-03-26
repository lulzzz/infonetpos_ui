using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class SystemBusinessLogic : ISystemBusinessLogic
    {
        private readonly ISystemSerializeManager _serializeManager;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        public SystemBusinessLogic(ISystemSerializeManager serializeManager,
            ICacheBusinessLogic cacheBusinessLogic)
        {
            _serializeManager = serializeManager;
            _cacheBusinessLogic = cacheBusinessLogic;
        }

        public async Task GetAndSaveRegisterSettings()
        {
            var register = await _serializeManager.GetRegisterSettings(_cacheBusinessLogic.RegisterNumber);

            _cacheBusinessLogic.UseReceiptPrinter = register.Receipt.UseReceiptPrinter;
            _cacheBusinessLogic.UseOposReceiptPrinter = register.Receipt.UseOposReceiptPrinter;
            _cacheBusinessLogic.ReceiptPrinterDriver = register.Receipt.ReceiptDriver;
            _cacheBusinessLogic.ReceiptPrinterName = register.Receipt.Name;

            _cacheBusinessLogic.CustomerDisplayName = register.CustomerDisplay.Name;
            _cacheBusinessLogic.CustomerDisplayPort = register.CustomerDisplay.Port;
            _cacheBusinessLogic.UseCustomerDisplay = register.CustomerDisplay.UseCustomerDisplay;
            _cacheBusinessLogic.UseOposCustomerDisplay = register.CustomerDisplay.UseOposCustomerDisplay;

            _cacheBusinessLogic.CashDrawerName = register.CashDrawer.Name;
            _cacheBusinessLogic.CashDrawerOpenCode = register.CashDrawer.OpenCode;
            _cacheBusinessLogic.UseCashDrawer = register.CashDrawer.UseCashDrawer;
            _cacheBusinessLogic.UseOposCashDrawer = register.CashDrawer.UseOposCashDrawer;
        }
    }
}
