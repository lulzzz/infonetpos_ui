using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using PeripheralsComponent;
using System;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.PeripheralLayer
{
    public class WindowManager : IWindowManager
    {
        public IWindowsManager _windowsManager;

        public void Initialize()
        {
            try
            {
                _windowsManager = new WindowsManager();
            }
            catch (Exception ex)
            {
                throw new WindowsManagerException(WindowsManagerError.Unexpected, ex);
            }
        }

        public Task<string> GetCurrentUserName()
        {
            try
            {
                Initialize();
                var userName = _windowsManager.GetCurrentUserName();
                userName = userName.Substring(userName.IndexOf(@"\") + 1);

                return Task.FromResult(userName);
            }
            catch (Exception ex)
            {
                throw new WindowsManagerException(WindowsManagerError.FailedToGetUserName, ex);
            }
        }

        public Task ShutDownSystem()
        {
            try
            {
                Initialize();

                _windowsManager.ShutDownSystem();

                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                throw new WindowsManagerException(WindowsManagerError.FailedToShutDownSystem, ex);
            }
        }

        public void Dispose()
        {
            if (_windowsManager != null)
            {
                _windowsManager = null;
            }
        }

        public Task LogOffSystem()
        {
            try
            {
                Initialize();

                _windowsManager.WindowsLogOff();

                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                throw new WindowsManagerException(WindowsManagerError.FailedToShutDownSystem, ex);
            }
        }

        public Task InvokeKeyBoard()
        {
            try
            {
                Initialize();

                _windowsManager.InvokeKeyboard();

                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult<bool>(false);
            }
        }
    }
}
