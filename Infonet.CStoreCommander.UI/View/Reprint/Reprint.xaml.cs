using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Reprint;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reprint
{
    public sealed partial class Reprint : Page
    {
        public ReprintVM ReprintVM { get; set; }
    = SimpleIoc.Default.GetInstance<ReprintVM>();

        public Reprint()
        {
            this.InitializeComponent();
            this.DataContext = ReprintVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ReprintVM.ResetVM();
            }
        }
    }
}
