using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.ViewModel;
using MetroLog;
using System;

namespace Infonet.CStoreCommander.UI.Service
{
    public class PeripheralsService
    {
        private InfonetLog _log = InfonetLogManager.GetLogger<PeripheralsService>();

        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        public PeripheralsService()
        {
            _cacheBusinessLogic = SimpleIoc.Default.GetInstance<ICacheBusinessLogic>();
        }

        public void ClaimPrinter()
        {
            try
            {
                if (_cacheBusinessLogic.UseReceiptPrinter && _cacheBusinessLogic.UseOposReceiptPrinter)
                {
                    if (VMBase.OposPrinter == null)
                    {
                        VMBase.OposPrinter = new PeripheralLayer.OPOSPrinter(_cacheBusinessLogic.ReceiptPrinterName);
                    }
                }
                else if (_cacheBusinessLogic.UseReceiptPrinter)
                {
                    VMBase.Printer = new PeripheralLayer.Printer(_cacheBusinessLogic.ReceiptPrinterDriver);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ReleasePrinter()
        {
            VMBase.OposPrinter?.Dispose();
            VMBase.OposPrinter = null;
            _log.Info("Releasing the printer is done");
        }
    }
}
