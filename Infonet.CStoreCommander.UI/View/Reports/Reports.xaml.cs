using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Reports;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reports
{
    public sealed partial class Reports : Page
    {
        public ReportsScreenVM ReportsScreenVM { get; set; }
      = SimpleIoc.Default.GetInstance<ReportsScreenVM>();

        public Reports()
        {
            this.InitializeComponent();
            NavigateService.Instance.ReportsFrame = frmReports;
            ReInitializeVM();
        }

        private void ReInitializeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ReportsScreenVM.ResetVM();
            }
        }
    }
}
