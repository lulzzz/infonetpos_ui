using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reports.ReportOptions
{
    public sealed partial class FlashReport : Page
    {
        public FlashReportVM FlashReportVM { get; set; }
           = SimpleIoc.Default.GetInstance<FlashReportVM>();

        public FlashReport()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                FlashReportVM.ReInitalizeVM();
            }
        }
    }
}
