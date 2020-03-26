using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class MessagePopup : Page
    {
        public string Heading
        {
            get { return (string)GetValue(HeadingProperty); }
            set { SetValue(HeadingProperty, value); }
        }

        public static readonly DependencyProperty HeadingProperty =
           DependencyProperty.Register(
               nameof(Heading),
               typeof(string),
               typeof(MessagePopup),
               new PropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(
                nameof(CancelCommand),
                typeof(ICommand),
                typeof(MessagePopup),
                new PropertyMetadata(null));     


        public SaleGridVM SaleGridVM { get; set; } =
         SimpleIoc.Default.GetInstance<SaleGridVM>();

        public MessagePopup()
        {
            this.InitializeComponent();
            this.DataContext = SaleGridVM;
            txtMessage.KeyUp -= TxtMessageKeyUp;
            txtMessage.KeyUp += TxtMessageKeyUp;

            Unloaded -= OnUnLoaded;
            Unloaded += OnUnLoaded;
        }

        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            txtMessage.KeyUp -= TxtMessageKeyUp;
        }

        private void TxtMessageKeyUp(object sender, KeyRoutedEventArgs e)
        {
           if(Helper.IsEnterKey(e))
            {
                txtMessage.IsEnabled = false;
                txtMessage.IsEnabled = true;
            }
        }
    }
}
