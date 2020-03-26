using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class QitePopup : UserControl
    {
        public QiteVM QiteVM =
            SimpleIoc.Default.GetInstance<QiteVM>();

        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
             DependencyProperty.Register(nameof(BackgroundOverlay),
                 typeof(SolidColorBrush),
                 typeof(EnvelopeNumberPopup),
                 new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        public QitePopup()
        {
            this.InitializeComponent();
            DataContext = QiteVM;            
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            validateBand.Focus(FocusState.Programmatic);
        }
    }
}
