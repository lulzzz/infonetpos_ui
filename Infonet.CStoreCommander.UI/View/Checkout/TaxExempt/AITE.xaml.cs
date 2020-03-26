using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Checkout.TaxExempt
{
    public sealed partial class AITE : Page
    {
        public AiteVM AiteVM =
            SimpleIoc.Default.GetInstance<AiteVM>();

        public AITE()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            DataContext = AiteVM;

            Loaded -= OnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            BarCode.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Loaded -= OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                AiteVM.ReInitialize();
            }
        }
    }
}
