using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Customer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class TaxExemptionPopup : Page
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

        public CustomersScreenVM CustomersScreenVM { get; set; }
     = SimpleIoc.Default.GetInstance<CustomersScreenVM>();

        public TaxExemptionPopup()
        {
            this.InitializeComponent();
            this.DataContext = CustomersScreenVM;
        }


        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            taxTextBox.Focus(FocusState.Programmatic);
        }
    }
}
