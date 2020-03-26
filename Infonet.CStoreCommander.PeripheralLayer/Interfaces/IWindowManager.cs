using System;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer.Interfaces
{
    public interface IWindowManager : IDisposable
    {
        Task<string> GetCurrentUserName();

        Task ShutDownSystem();

        Task LogOffSystem();

        Task InvokeKeyBoard();
    }
}
