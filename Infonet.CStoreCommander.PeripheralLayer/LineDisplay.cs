using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class LineDisplay : ILineDisplay
    {
        private InfonetLog _log = InfonetLogManager.GetLogger<LineDisplay>();

        private ILineDisplayServer _lineDisplay;

        public LineDisplay(string portNumber)
        {
            try
            {
                _lineDisplay = new LineDisplayServer(portNumber);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException("No Customer Display Available!");
            }
        }

        public void Clear()
        {
            try
            {
                _lineDisplay.Clear();
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException("No Customer Display Available!");
            }
        }

        public void DisplayText(string text, bool clear)
        {
            try
            {
                _lineDisplay.DisplayText(text, clear);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
                throw new LineDisplayException("No Customer Display Available!");
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
