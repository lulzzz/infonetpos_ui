using Infonet.CStoreCommander.UI.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class NotificationPopup : UserControl
    {
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public string Continue
        {
            get { return (string)GetValue(ContinueProperty); }
            set { SetValue(ContinueProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public SolidColorBrush ButtonBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }

        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
            DependencyProperty.Register(nameof(BackgroundOverlay),
                typeof(SolidColorBrush),
                typeof(NotificationPopup),
                new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));



        public static readonly DependencyProperty ButtonBackgroundColorProperty =
            DependencyProperty.Register(
                nameof(ButtonBackgroundColor),
                typeof(SolidColorBrush),
                typeof(NotificationPopup),
                new PropertyMetadata(null));





        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title),
                typeof(string),
                typeof(NotificationPopup),
                new PropertyMetadata(null));



        public static readonly DependencyProperty ContinueProperty =
            DependencyProperty.Register(
                nameof(Continue),
                typeof(string),
                typeof(NotificationPopup),
                new PropertyMetadata(null));


        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(NotificationPopup),
                new PropertyMetadata(null));

        public VMBase VMBase { get; set; }

        public NotificationPopup()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
        }
        
    }
}
