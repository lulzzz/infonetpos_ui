using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class PurchaseOrderPopup : Page
    {
        #region Properties


        public ICommand EnterPressedCommand
        {
            get { return (ICommand)GetValue(EnterPressedCommandProperty); }
            set { SetValue(EnterPressedCommandProperty, value); }
        }

        public static readonly DependencyProperty EnterPressedCommandProperty =
            DependencyProperty.Register(
                nameof(EnterPressedCommand),
                typeof(ICommand),
                typeof(PurchaseOrderPopup),
                new PropertyMetadata(null));


        public SolidColorBrush FirstButtonBottomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(FirstButtonBottomBackgroundColorProperty); }
            set { SetValue(FirstButtonBottomBackgroundColorProperty, value); }
        }

        public SolidColorBrush SecondButtonBottomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(SecondButtonBottomBackgroundColorProperty); }
            set { SetValue(SecondButtonBottomBackgroundColorProperty, value); }
        }

        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
             DependencyProperty.Register(nameof(BackgroundOverlay),
                 typeof(SolidColorBrush),
                 typeof(PurchaseOrderPopup),
                 new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));



        #endregion

        #region Dependency Property



        public static readonly DependencyProperty ClosePopupCommandProperty =
               DependencyProperty.Register(
                   nameof(ClosePopupCommand),
                   typeof(ICommand),
                   typeof(PurchaseOrderPopup),
                   new PropertyMetadata(null));


        public static readonly DependencyProperty SecondButtonBottomBackgroundColorProperty =
                  DependencyProperty.Register(nameof(SecondButtonBottomBackgroundColor),
                      typeof(SolidColorBrush),
                      typeof(PurchaseOrderPopup),
                      new PropertyMetadata(null));


        public static readonly DependencyProperty FirstButtonBottomBackgroundColorProperty =
            DependencyProperty.Register(nameof(FirstButtonBottomBackgroundColor),
                typeof(SolidColorBrush),
                typeof(PurchaseOrderPopup),
                new PropertyMetadata(null));
        #endregion

        #region Commands      
        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }
        #endregion

        public SaleSummaryVM SaleSummaryVM { get; set; }
       = SimpleIoc.Default.GetInstance<SaleSummaryVM>();

        public PurchaseOrderPopup()
        {
            this.InitializeComponent();
            this.DataContext = SaleSummaryVM;
            Loaded -= UpdatedLayout;
            Loaded += UpdatedLayout;
        }

        private void UpdatedLayout(object sender, object e)
        {
            purchaseOrderNumber.Focus(FocusState.Keyboard);
        }
    }
}
