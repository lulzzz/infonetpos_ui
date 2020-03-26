using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace PeripheralsComponent
{

    [Guid("A8CF568F-F770-5938-B50B-F7080CAB81F3"), ComVisible(true)]
    public interface IWindowsManager
    {
        bool ShutDownSystem();

        string GetCurrentUserName();

        bool WindowsLogOff();

        void InvokeKeyboard();
    }

    public sealed class WindowsManager : IWindowsManager
    {

        /// <summary>
        /// The registry key which holds the keyboard settings.
        /// </summary>
        private static readonly RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\TabletTip\\1.7");


        public string GetCurrentUserName()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }

        public bool ShutDownSystem()
        {
            try
            {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public bool WindowsLogOff()
        {
            return ExitWindowsEx(0, 0);
        }



        private static void KillTabTip()
        {
            // Kill the previous process so the registry change will take effect.
            var processlist = Process.GetProcesses();

            foreach (var process in processlist.Where(process => process.ProcessName == "TabTip"))
            {
                process.Kill();
                break;
            }
        }

        public void InvokeKeyboard()
        {
            //KillTabTip();
            //string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
            //string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
            //Process.Start(keyboardPath);


            const string keyName = "HKEY_CURRENT_USER\\Software\\Microsoft\\TabletTip\\1.7";

            var regValue = (int)Registry.GetValue(keyName, "KeyboardLayoutPreference", 0);
            var regShowNumericKeyboard = regValue == 1;

            // Note: Remove this if do not want to control docked state.
            var dockedRegValue = (int)Registry.GetValue(keyName, "EdgeTargetDockedState", 1);
            var restoreDockedState = dockedRegValue == 0;

            //if (numericKeyboard && regShowNumericKeyboard == false)
            //{
                // Set the registry so it will show the number pad via the thumb keyboard.
              //  Registry.SetValue(keyName, "KeyboardLayoutPreference", 2, RegistryValueKind.DWord);

                // Kill the previous process so the registry change will take effect.
             //   KillTabTip();
            //}
            //else if (numericKeyboard == false && regShowNumericKeyboard)
            //{
            //    // Set the registry so it will NOT show the number pad via the thumb keyboard.
            //    Registry.SetValue(keyName, "KeyboardLayoutPreference", 0, RegistryValueKind.DWord);

            //    // Kill the previous process so the registry change will take effect.
            //    KillTabTip();
            //}

            // Note: Remove this if do not want to control docked state.
            //if (restoreDockedState)
            //{
                // Set the registry so it will show as docked at the bottom rather than floating.
               Registry.SetValue(keyName, "EdgeTargetDockedState", 2, RegistryValueKind.DWord);

                // Kill the previous process so the registry change will take effect.
                KillTabTip();
          //  }

            Process.Start("c:\\Program Files\\Common Files\\Microsoft Shared\\ink\\TabTip.exe");
        }
    }
}

