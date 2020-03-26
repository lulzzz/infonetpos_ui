using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class LargePopupHeader : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(LargePopupHeader),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CancelCommandProperty =
               DependencyProperty.Register(nameof(CancelCommand),
                   typeof(ICommand),
                   typeof(LargePopupHeader),
                   new PropertyMetadata(null));

        public LargePopupHeader()
        {
            this.InitializeComponent();
        }
    }
}
