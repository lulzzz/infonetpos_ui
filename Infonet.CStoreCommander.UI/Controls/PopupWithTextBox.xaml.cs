using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using MyToolkit.Extended.Controls;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{

    public sealed partial class PopupWithTextBox : Page
    {
        public PasswordRevealMode PasswordRevealMode
        {
            get { return (PasswordRevealMode)GetValue(PasswordRevealModeProperty); }
            set { SetValue(PasswordRevealModeProperty, value); }
        }

        public static readonly DependencyProperty PasswordRevealModeProperty =
            DependencyProperty.Register(nameof(PasswordRevealMode),
                typeof(PasswordRevealMode), typeof(PopupWithTextBox), new PropertyMetadata(PasswordRevealMode.Visible));


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

        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }

        public static readonly DependencyProperty ClosePopupCommandProperty =
            DependencyProperty.Register(nameof(ClosePopupCommand),
                typeof(ICommand), typeof(PopupWithTextBox),
                new PropertyMetadata(null));

        public NumericKeyType NumericKeyType
        {
            get { return (NumericKeyType)GetValue(NumericKeyTypeProperty); }
            set
            {
                SetValue(NumericKeyTypeProperty, value);
            }
        }

        public static readonly DependencyProperty NumericKeyTypeProperty =
            DependencyProperty.Register(nameof(NumericKeyType),
                typeof(bool), typeof(CustomTextBox),
                new PropertyMetadata(NumericKeyType.None));

        public InputScopeNameValue InputScope
        {
            get { return (InputScopeNameValue)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register(nameof(InputScope), typeof(InputScopeNameValue), typeof(CustomTextBox),
                new PropertyMetadata(InputScopeNameValue.AlphanumericFullWidth));


        public VMBase VMBase { get; set; }
        public FleetTenderVM FleetTenderVM { get; set; } =
        SimpleIoc.Default.GetInstance<FleetTenderVM>();

        public PopupWithTextBox()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            pbText.Focus(FocusState.Programmatic);
            pwdText.Focus(FocusState.Programmatic);
        }
    }
}
