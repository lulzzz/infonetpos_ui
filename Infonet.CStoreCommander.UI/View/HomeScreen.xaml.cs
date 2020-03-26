using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class HomeScreen : Page
    {
        public HomeScreenVM HomeScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<HomeScreenVM>();

        public HomeScreen()
        {
            this.InitializeComponent();
            this.DataContext = HomeScreenVM;
            NavigateService.Instance.FirstFrame = frmFirst;
            NavigateService.Instance.SecondFrame = frmSecond;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                HomeScreenVM.ResetVM();
            }

            SetupEventsForFreeze();
        }

        private void SetupEventsForFreeze()
        {
            if (!HomeScreenVM.AreFreezeEventsAttached)
            {
                Window.Current.CoreWindow.PointerPressed -= UserInteractedWithMouse;
                Window.Current.CoreWindow.PointerPressed += UserInteractedWithMouse;
                Window.Current.CoreWindow.PointerMoved -= UserInteractedWithMouse;
                Window.Current.CoreWindow.PointerMoved += UserInteractedWithMouse;
                Window.Current.CoreWindow.PointerReleased -= UserInteractedWithMouse;
                Window.Current.CoreWindow.PointerReleased += UserInteractedWithMouse;
                Window.Current.CoreWindow.KeyUp -= UserInteractedWithKeyboard;
                Window.Current.CoreWindow.KeyUp += UserInteractedWithKeyboard;

                HomeScreenVM.AreFreezeEventsAttached = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            AlertPopup.Visibility = Visibility.Collapsed;
            ReasonPopup.Visibility = Visibility.Collapsed;
            ConfirmationPopup.Visibility = Visibility.Collapsed;
            CheckoutOptionsOpen.Visibility = Visibility.Collapsed;
            ReturnsPopupOpen.Visibility = Visibility.Collapsed;
            EnvelopePopup.Visibility = Visibility.Collapsed;
            TreatyNumberPopup.Visibility = Visibility.Collapsed;
            MessagePopup.Visibility = Visibility.Collapsed;
            QitePopup.Visibility = Visibility.Collapsed;
            purchaseOrderPopup.Visibility = Visibility.Collapsed;
            pumpOptionsPopup.Visibility = Visibility.Collapsed;
            popupWithTextBoxFleet.Visibility = Visibility.Collapsed;
            taxExemptionNumberpopup.Visibility = Visibility.Collapsed;
            FNGTRPopup.Visibility = Visibility.Collapsed;
            KickbackBalancePopup.Visibility = Visibility.Collapsed;
            KickbackNumberPopup.Visibility = Visibility.Collapsed;
        }

        private void UserInteractedWithKeyboard(CoreWindow sender, KeyEventArgs args)
        {
            HomeScreenVM.UserActivityHappenedSinceInSeconds = 0;
            HomeScreenVM.StartFreezeTimer();
        }

        private void UserInteractedWithMouse(CoreWindow sender, PointerEventArgs args)
        {
            HomeScreenVM.UserActivityHappenedSinceInSeconds = 0;
            HomeScreenVM.StartFreezeTimer();
        }
    }
}
