using System;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface IOPOSLineDisplay: IDisposable
    {
        bool Clear();

        bool DisplayText(string text);

        bool DisplayTextAt(int row, int columnm, string text);
    }
}
