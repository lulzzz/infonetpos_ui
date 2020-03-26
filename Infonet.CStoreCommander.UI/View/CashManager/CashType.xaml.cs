using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.CashManager;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.CashManager
{
    public sealed partial class CashType : Page
    {
        public CashDrawVM CashDrawVM { get; set; }
         = SimpleIoc.Default.GetInstance<CashDrawVM>();

        public CashType()
        {
            this.InitializeComponent();
            this.DataContext = CashDrawVM;
        }
    }
}
