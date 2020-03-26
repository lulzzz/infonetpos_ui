using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System;

namespace Infonet.CStoreCommander.UI.View.Login
{
    public sealed partial class Freezed : Page
    {
        public FreezedScreenVM FreezedScreenVM =
            SimpleIoc.Default.GetInstance<FreezedScreenVM>();

        public Freezed()
        {
            this.InitializeComponent();
            DataContext = FreezedScreenVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                FreezedScreenVM.ReInitialize();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Task.Delay(500);
            Password.Focus(FocusState.Keyboard);
        }

        private void PasswordKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

        private void CoreWindowPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            FreezedScreenVM.IsUserFormVisible = true;
        }

        private void CoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            var obj = new
            {
                OriginalKey = args.VirtualKey
            };
            if (Helper.IsEnterKey(obj))
            {
                FreezedScreenVM.IsUserFormVisible = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            shiftPopup.Visibility = Visibility.Collapsed;
            errorPopup.Visibility = Visibility.Collapsed;
            LoginPopup.Visibility = Visibility.Collapsed;

            Loaded -= OnLoaded;
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.PointerPressed -= CoreWindowPointerPressed;
            Password.KeyUp -= PasswordKeyUp;
            LayoutUpdated -= OnLayoutUpdated;

            FreezedScreenVM.StartFreezeTimer();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FreezedScreenVM.StopFreezeTimer();

            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            Window.Current.CoreWindow.PointerPressed -= CoreWindowPointerPressed;
            Window.Current.CoreWindow.PointerPressed += CoreWindowPointerPressed;
            Password.KeyUp -= PasswordKeyUp;
            Password.KeyUp += PasswordKeyUp;

            Loaded -= OnLoaded;
            Loaded += OnLoaded;

            LayoutUpdated -= OnLayoutUpdated;
            LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, object e)
        {
            if (FreezedScreenVM.IsUserFormVisible && ((Storyboard)Resources["GradientAnimation"]).GetCurrentState() == ClockState.Active)
            {
                ((Storyboard)Resources["GradientAnimation"]).Stop();
            }
            else if (!FreezedScreenVM.IsUserFormVisible && ((Storyboard)Resources["GradientAnimation"]).GetCurrentState() == ClockState.Stopped)
            {
                ((Storyboard)Resources["GradientAnimation"]).Begin();
            }
        }
    }
}
