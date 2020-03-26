using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.CashManager;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.CashManager
{
    public sealed partial class CashTender : Page
    {
        public CashDropVM CashDropVM { get; set; }
          = SimpleIoc.Default.GetInstance<CashDropVM>();

        public CashTender()
        {
            this.InitializeComponent();
            DataContext = CashDropVM;
        }
    }
}
