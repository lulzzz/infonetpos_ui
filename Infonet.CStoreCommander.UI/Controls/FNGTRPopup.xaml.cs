using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class FNGTRPopup : Page
    {
        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
           DependencyProperty.Register(nameof(BackgroundOverlay),
               typeof(SolidColorBrush),
               typeof(PopupWithTwoButtons),
               new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));


        public VMBase VMBase { get; set; }
        public SiteVM SiteVM { get; set; } =
        SimpleIoc.Default.GetInstance<SiteVM>();

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            pbText.Focus(FocusState.Programmatic);
        }
        public FNGTRPopup()
        {
            this.InitializeComponent(); this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
            DataContext = SiteVM;
        }
    }
}
