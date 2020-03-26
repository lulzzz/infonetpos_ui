using System;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class LoginScreen : Page
    {
        public LoginScreenVM LoginScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<LoginScreenVM>();
        private DispatcherTimer _cursorTimer;
        private bool _focusOnUserName = true;
        private bool _focusOnPassword = true;

        public LoginScreen()
        {
            this.InitializeComponent();
            this.DataContext = LoginScreenVM;
            LoginScreenVM.ResetVM();
            LoginScreenVM.UserFieldName = nameof(User);
        }

        private void SetTimer()
        {
            if (_cursorTimer == null)
            {
                _cursorTimer = new DispatcherTimer();
                _cursorTimer.Interval = new TimeSpan(0, 0, 1);
                _cursorTimer.Tick -= CursorTimerTick;
                _cursorTimer.Tick += CursorTimerTick;
                _cursorTimer.Start();
            }
        }

        private void CursorTimerTick(object sender, object e)
        {
            if(User.FocusState != FocusState.Unfocused)
            {
                _focusOnUserName = true;
                _focusOnPassword = false;
            }
            else if(Password.FocusState != FocusState.Unfocused)
            {
                _focusOnUserName = false;
                _focusOnPassword = true;
            }

             if (_focusOnUserName)
            {
                User.Focus(FocusState.Pointer);
            }
            else if(_focusOnPassword)
            {
                Password.Focus(FocusState.Pointer);
            }
        }

        private void TillFloatKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                btnLogin.Focus(FocusState.Pointer);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Password.KeyUp -= FocusOnTills;
            Password.KeyUp += FocusOnTills;
            User.KeyUp -= FocusOnPassword;
            User.KeyUp += FocusOnPassword;
            TillFloat.KeyUp -= TillFloatKeyUp;
            TillFloat.KeyUp += TillFloatKeyUp;
            SetTimer();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Password.KeyUp -= FocusOnTills;
            User.KeyUp -= FocusOnPassword;
            TillFloat.KeyUp -= TillFloatKeyUp;

            _cursorTimer.Tick -= CursorTimerTick;
            _cursorTimer.Stop();

            //TODO: Ipsit- Patch for Hiding Popups
            shiftPopup.Visibility = Visibility.Collapsed;
            errorPopup.Visibility = Visibility.Collapsed;
        }

        private void FocusOnPassword(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                Password.Focus(FocusState.Keyboard);
            }
        }

        private void FocusOnTills(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

    }
}