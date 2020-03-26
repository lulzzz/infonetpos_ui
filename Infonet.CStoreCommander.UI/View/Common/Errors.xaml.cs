using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Common
{
    public sealed partial class Errors : Page
    {
        public ErrorsVM ErrorsVM { get; set; } =
        SimpleIoc.Default.GetInstance<ErrorsVM>();

        public Errors()
        {
            this.InitializeComponent();
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ErrorsVM.ResetVM();
            }
        }
    }
}
