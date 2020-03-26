using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Login
{
    public sealed partial class TillClose : Page
    {
        public CloseTillVM CloseTillVM =
         SimpleIoc.Default.GetInstance<CloseTillVM>();

        public TillClose()
        {
            this.InitializeComponent();
            this.DataContext = CloseTillVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                CloseTillVM.ReInitialize();
            }
        }
    }
}
