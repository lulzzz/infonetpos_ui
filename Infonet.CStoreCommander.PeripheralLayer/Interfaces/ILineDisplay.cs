using System;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface ILineDisplay : IDisposable
    {
        void Clear();

        void DisplayText(string text, bool clear);
    }
}
