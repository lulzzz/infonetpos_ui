using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.Prepay
{
    public sealed partial class PrepayNumberPad : Page
    {
        public PrepayVM PrepayVM { get; set; }
      = SimpleIoc.Default.GetInstance<PrepayVM>();

        public PrepayNumberPad()
        {
            this.InitializeComponent();

            DataContext = PrepayVM;
        }
    }
}
