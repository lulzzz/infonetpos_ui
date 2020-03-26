using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions;
using Windows.UI.Xaml.Controls;


namespace Infonet.CStoreCommander.UI.View.PumpOptions
{
    public sealed partial class Finish : Page
    {
        public FinishVM FinishVM { get; set; } =
       SimpleIoc.Default.GetInstance<FinishVM>();

        public Finish()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                FinishVM.ResetVM();
            }

        }
    }
}
