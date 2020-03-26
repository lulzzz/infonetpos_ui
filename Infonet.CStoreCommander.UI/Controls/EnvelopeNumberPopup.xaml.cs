using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class EnvelopeNumberPopup : UserControl
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
                typeof(EnvelopeNumberPopup),
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
                 typeof(EnvelopeNumberPopup),
                 new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));



        #endregion

        #region Dependency Property



        public static readonly DependencyProperty ClosePopupCommandProperty =
               DependencyProperty.Register(
                   nameof(ClosePopupCommand),
                   typeof(ICommand),
                   typeof(EnvelopeNumberPopup),
                   new PropertyMetadata(null));


        public static readonly DependencyProperty SecondButtonBottomBackgroundColorProperty =
                  DependencyProperty.Register(nameof(SecondButtonBottomBackgroundColor),
                      typeof(SolidColorBrush),
                      typeof(EnvelopeNumberPopup),
                      new PropertyMetadata(null));


        public static readonly DependencyProperty FirstButtonBottomBackgroundColorProperty =
            DependencyProperty.Register(nameof(FirstButtonBottomBackgroundColor),
                typeof(SolidColorBrush),
                typeof(EnvelopeNumberPopup),
                new PropertyMetadata(null));
        #endregion

        #region Commands      
        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }
        #endregion


        public SaleGridVM SaleGridVM { get; set; }
        = SimpleIoc.Default.GetInstance<SaleGridVM>();

        public EnvelopeNumberPopup()
        {
            this.InitializeComponent();
            this.DataContext = SaleGridVM;

         //   SaleGridVM.ReInitialize();

            Loaded -= UpdatedLayout;
            Loaded += UpdatedLayout;
        }

        private void UpdatedLayout(object sender, object e)
        {
            EnvelopeNumber.Focus(FocusState.Keyboard);
        }
    }
}
