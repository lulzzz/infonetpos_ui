using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class GstPstTaxExemptPopup : UserControl
    {
        public AiteVM AiteVM =
            SimpleIoc.Default.GetInstance<AiteVM>();

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

        public GstPstTaxExemptPopup()
        {
            this.InitializeComponent();
            DataContext = AiteVM;
        }
    }
}
