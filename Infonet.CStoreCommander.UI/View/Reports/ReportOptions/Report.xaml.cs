using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reports.ReportOptions
{
    public sealed partial class Report : Page
    {
        public ReportVM ReportVM { get; set; } =
              SimpleIoc.Default.GetInstance<ReportVM>();

        public Report()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ReportVM.ReInitializeVM();
            }
        }
    }
}
