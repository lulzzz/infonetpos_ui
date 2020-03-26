using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class PumpOptions : UserControl
    {
        public HomeScreenVM HomeScreenVM { get; set; } =
           SimpleIoc.Default.GetInstance<HomeScreenVM>();

        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public string HeadingText
        {
            get { return (string)GetValue(HeadingTextProperty); }
            set { SetValue(HeadingTextProperty, value); }
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

       public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(nameof(CancelCommand), 
                typeof(ICommand),
                typeof(PumpOptions),
                new PropertyMetadata(null));



        public static readonly DependencyProperty HeadingTextProperty =
             DependencyProperty.Register(nameof(HeadingText),
                 typeof(string),
                 typeof(PumpOptions),
                 new PropertyMetadata(null));

        public static readonly DependencyProperty BackgroundOverlayProperty =
            DependencyProperty.Register(nameof(BackgroundOverlay),
                typeof(SolidColorBrush),
                typeof(PumpOptions),
                new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        public VMBase VMBase { get; set; }

        public PumpOptions()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
        }
    }
}
