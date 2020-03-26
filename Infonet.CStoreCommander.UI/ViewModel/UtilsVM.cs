using System;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Utility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Infonet.CStoreCommander.UI.ViewModel
{
    public class UtilsVM : VMBase
    {
        public UtilsVM()
        {
            MessengerInstance.Register<CloseKeyboardMessage>(this, CloseVirtualKeyboard);
            MessengerInstance.Register<string>(this, "SetFocusOn", SetFocusOn);
        }

        public void CloseVirtualKeyboard(CloseKeyboardMessage message)
        {
            Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
        }

        public void SetFocusOn(string controlName)
        {
            var viewUtility = new ViewUtility();
            var textBox = viewUtility.FindChild<TextBox>(null, controlName);

            if (textBox != null)
            {
                textBox.Focus(FocusState.Keyboard);
            }
            else
            {
                var passwordBox = viewUtility.FindChild<PasswordBox>(null, controlName);
                if (passwordBox != null)
                {
                    passwordBox.Focus(FocusState.Keyboard);
                }
            }
        }
    }
}
