using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reports.ReportOptions
{
    public sealed partial class TillAuditReport : Page
    {
        public TillAuditReportVM TillAuditReportVM { get; set; }
     = SimpleIoc.Default.GetInstance<TillAuditReportVM>();

        public TillAuditReport()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                TillAuditReportVM.ReInitializeVM();
            }
        }
    }
}
