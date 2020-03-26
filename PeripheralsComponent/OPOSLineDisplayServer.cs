using Microsoft.PointOfService;
using System;
using System.Runtime.InteropServices;

namespace PeripheralsComponent
{
    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D14B"), ComVisible(true)]
    public interface IOPOSLineDisplayServer : IDisposable
    {
        bool Clear();

        bool DisplayText(string text);

        bool DisplayTextAt(int row, int columnm, string text);
    }

    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D14C"), ComVisible(true)]
    public sealed class OPOSLineDisplayServer : IOPOSLineDisplayServer
    {
        private const int CLAIM_TIMEOUT_MS = 1000;

        private LineDisplay _lineDisplay;

        public OPOSLineDisplayServer(string deviceName)
        {
            PosExplorer myPosExplorer = new PosExplorer();
            DeviceCollection myDevices = myPosExplorer.GetDevices(DeviceType.LineDisplay);

            foreach (DeviceInfo devInfo in myDevices)
            {
                if (devInfo.ServiceObjectName == deviceName)
                {
                    _lineDisplay = myPosExplorer.CreateInstance(devInfo) as LineDisplay;

                    //open
                    _lineDisplay.Open();

                    //claim the printer for use
                    _lineDisplay.Claim(CLAIM_TIMEOUT_MS);

                    //make sure it is enabled
                    _lineDisplay.DeviceEnabled = true;
                }
            }

            if (_lineDisplay == null)
            {
                throw new Exception("No Customer Display Available!");
            }
        }

        public bool Clear()
        {
            try
            {
                if (_lineDisplay != null && _lineDisplay.Claimed)
                {
                    _lineDisplay.ClearText();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DisplayText(string text)
        {
            try
            {
                if (_lineDisplay != null && _lineDisplay.Claimed)
                {
                    _lineDisplay.DisplayText(text);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DisplayTextAt(int row, int columnm, string text)
        {
            try
            {
                if (_lineDisplay != null && _lineDisplay.Claimed)
                {
                    _lineDisplay.DisplayTextAt(row, columnm, text);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (_lineDisplay != null && _lineDisplay.Claimed)
            {
                _lineDisplay.Close();
            }
        }
    }
}
