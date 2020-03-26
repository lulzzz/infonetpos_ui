using System;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface IOPOSCashDrawer : IDisposable
    {
        bool Open();
    }
}
