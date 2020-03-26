using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.GiveX;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.GiveX
{
    public sealed partial class GiveXReportGrid : Page
    {
        public GiveXReportVM GiveXReportVM { get; set; }
        = SimpleIoc.Default.GetInstance<GiveXReportVM>();

        public GiveXReportGrid()
        {
            this.InitializeComponent();
            this.DataContext = GiveXReportVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                GiveXReportVM.ReSetVM();
            }
        }
    }
}
