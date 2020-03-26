using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class ExtendedSplashScreen : Page
    {
        public ExtendedSplashScreenVM ExtendedSplashScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<ExtendedSplashScreenVM>();

        public ExtendedSplashScreen()
        {
            this.InitializeComponent();
            this.DataContext = ExtendedSplashScreenVM;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            confirmationPopup.Visibility = Visibility.Collapsed;
            ErrorPopup.Visibility = Visibility.Collapsed;
        }
    }
}
