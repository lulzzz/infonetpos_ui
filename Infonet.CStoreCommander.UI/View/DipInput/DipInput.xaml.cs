using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.DipInputs;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.DipInput
{
    public sealed partial class DipInput : Page
    {
        public DipInputVM DipInputVM { get; set; }
    = SimpleIoc.Default.GetInstance<DipInputVM>();

        public DipInput()
        {
            this.InitializeComponent();
            this.DataContext = DipInputVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                DipInputVM.ReInitialize();
            }
        }
    }
}
