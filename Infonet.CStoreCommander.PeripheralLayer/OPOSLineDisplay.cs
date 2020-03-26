using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class OPOSLineDisplay : IOPOSLineDisplay
    {
        private InfonetLog _log = InfonetLogManager.GetLogger<OPOSLineDisplay>();

        private IOPOSLineDisplayServer _lineDisplay;

        public OPOSLineDisplay(string deviceName)
        {
            try
            {
                _lineDisplay = new OPOSLineDisplayServer(deviceName);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException("No Customer Display Available!");
            }
        }

        public bool Clear()
        {
            try
            {
                return _lineDisplay.Clear();
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException(ex.Message);
            }
        }

        public bool DisplayText(string text)
        {
            try
            {
                return _lineDisplay.DisplayText(text);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException(ex.Message);
            }
        }

        public bool DisplayTextAt(int row, int columnm, string text)
        {
            try
            {
                return _lineDisplay.DisplayTextAt(row, columnm, text);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException(ex.Message);
            }
        }

        public void Dispose()
        {
            if (_lineDisplay != null)
            {
                _lineDisplay.Dispose();
            }
        }
    }
}
