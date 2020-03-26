using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class SecondFrameHeading : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ICommand PreviousFrame
        {
            get { return (ICommand)GetValue(PreviousFrameProperty); }
            set { SetValue(PreviousFrameProperty, value); }
        }

        public Visibility BackArrowVisibility
        {
            get { return (Visibility)GetValue(BackArrowVisibilityProperty); }
            set { SetValue(BackArrowVisibilityProperty, value); }
        }

        public static readonly DependencyProperty PreviousFrameProperty =
            DependencyProperty.Register(
                nameof(PreviousFrame),
                typeof(ICommand),
                typeof(SecondFrameHeading),
                new PropertyMetadata(null));


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text),
                typeof(string),
                typeof(SecondFrameHeading),
                new PropertyMetadata(null));

        public static readonly DependencyProperty BackArrowVisibilityProperty =
            DependencyProperty.Register(nameof(BackArrowVisibility),
                typeof(Visibility),
                typeof(SecondFrameHeading),
                new PropertyMetadata(Visibility.Visible));

        public SecondFrameHeading()
        {
            this.InitializeComponent();
        }
    }
}
