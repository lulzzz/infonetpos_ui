using Microsoft.PointOfService;
using System;
using System.Runtime.InteropServices;

namespace PeripheralsComponent
{
    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D14D"), ComVisible(true)]
    public interface IOPOSCashDrawerServer : IDisposable
    {
        bool Open();
    }

    [Guid("3B0D070F-29D2-45B6-AC0A-157A1A65D14E"), ComVisible(true)]
    public sealed class OPOSCashDrawerServer : IOPOSCashDrawerServer
    {
        private const int CLAIM_TIMEOUT_MS = 1000;

        private CashDrawer _cashDrawer;

        public OPOSCashDrawerServer(string deviceName)
        {
            PosExplorer myPosExplorer = new PosExplorer();
            DeviceCollection myDevices = myPosExplorer.GetDevices(DeviceType.CashDrawer);

            try
            {
                foreach (DeviceInfo devInfo in myDevices)
                {
                    if (devInfo.ServiceObjectName == deviceName)
                    {
                        _cashDrawer = myPosExplorer.CreateInstance(devInfo) as CashDrawer;

                        //open
                        _cashDrawer.Open();

                        //claim the printer for use
                        _cashDrawer.Claim(CLAIM_TIMEOUT_MS);

                        //make sure it is enabled
                        _cashDrawer.DeviceEnabled = true;
                    }
                }

                if (_cashDrawer == null)
                {
                    throw new Exception("No Cash Drawer Available!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No Cash Drawer Available!");
            }
        }

        public bool Open()
        {
            try
            {
                if (_cashDrawer != null && _cashDrawer.Claimed)
                {
                    _cashDrawer.OpenDrawer();
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public void Dispose()
        {
            if (_cashDrawer != null)
            {
                _cashDrawer.Close();
            }
        }
    }
}
