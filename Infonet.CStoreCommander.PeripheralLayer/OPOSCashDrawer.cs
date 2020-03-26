using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class OPOSCashDrawer : IOPOSCashDrawer
    {
        private InfonetLog _log = InfonetLogManager.GetLogger<OPOSCashDrawer>();

        private IOPOSCashDrawerServer _cashDrawer;

        public OPOSCashDrawer(string deviceName)
        {
            try
            {
                _cashDrawer = new OPOSCashDrawerServer(deviceName);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new CashDrawerException("No Cash Drawer Available!");
            }
        }

        public void Dispose()
        {
            if (_cashDrawer != null)
            {
                _cashDrawer.Dispose();
            }
        }

        public bool Open()
        {
            if (_cashDrawer != null)
            {
                return _cashDrawer.Open();
            }
            return false;
        }
    }
}
