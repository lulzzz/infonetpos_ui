using GalaSoft.MvvmLight;
using MyToolkit.Extended.Controls;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class BarCodeTextbox : UserControl
    {
        private BarCodeContext _dataContext;

        public ICommand EnterPressedCommand
        {
            get { return (ICommand)GetValue(EnterPressedCommandProperty); }
            set { SetValue(EnterPressedCommandProperty, value); }
        }

        public static readonly DependencyProperty EnterPressedCommandProperty =
            DependencyProperty.Register(
                nameof(EnterPressedCommand),
                typeof(ICommand),
                typeof(BarCodeTextbox),
                new PropertyMetadata(null));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text),
                typeof(string),
                typeof(BarCodeTextbox),
                new PropertyMetadata(null));

        public BarCodeTextbox()
        {
            this.InitializeComponent();
            _dataContext = new BarCodeContext(TextBox, this)
            {
                Text = Text
            };
            TextBox.DataContext = _dataContext;
        }
    }

    internal class BarCodeContext : ViewModelBase
    {
        private string _text;
        private CustomTextBox _textBox;
        private BarCodeTextbox _barCodeTextBox;

        public BarCodeContext(CustomTextBox textBox, BarCodeTextbox barCodeTextBox)
        {
            _textBox = textBox;
            _barCodeTextBox = barCodeTextBox;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != null)
                {
                    _text = value;
                    _barCodeTextBox.Text = value;
                    RaisePropertyChanged(nameof(Text));
                }
            }
        }
    }
}
