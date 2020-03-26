using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reports
{
    public class ReportsScreenVM : VMBase
    {
        private string _selectedTab;
        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));
            }
        }

        public RelayCommand SalesCountSelected;
        public RelayCommand FlashSelected;
        public RelayCommand TillAuditSelected;

        protected IReportsBussinessLogic _reportsBussinessLogic;

        public ReportsScreenVM(IReportsBussinessLogic reportsBussinessLogic)
        {
            _reportsBussinessLogic = reportsBussinessLogic;
            InitializeCommands();
            MessengerInstance.Register<bool>
                (this, "SelectTillAuditTab", SelectTillAuditTab);
        }

        private void SelectTillAuditTab(bool obj)
        {
            OpenTillAuditTab();
        }

        private void InitializeCommands()
        {
            SalesCountSelected = new RelayCommand(OpenSalesCountTab);
            TillAuditSelected = new RelayCommand(OpenTillAuditTab);
            FlashSelected = new RelayCommand(OpenFlashTab);
        }


        private void OpenFlashTab()
        {
            SelectedTab = ReportTabs.Flash.ToString();
            NavigateService.Instance.NavigateToFlashReport();
        }

        private void OpenTillAuditTab()
        {
            SelectedTab = ReportTabs.TillAudit.ToString();
            NavigateService.Instance.NavigateToTillAuditReport();
        }

        private void OpenSalesCountTab()
        {
            SelectedTab = ReportTabs.SalesCount.ToString();
            NavigateService.Instance.NavigateToSaleCountReport();
        }

        internal void ResetVM()
        {
            SelectedTab = ReportTabs.SalesCount.ToString();
            NavigateService.Instance.NavigateToSaleCountReport();
        }

        protected async Task PrintReport(string reportName)
        {
            try
            {
                var reportContent = await _reportsBussinessLogic.GetReceipt(reportName);
               PerformPrint(reportContent);
            }
            catch (PrinterLayerException)
            {
                ShowNotification(ApplicationConstants.NoPrinterFound,
                   null,
                   null,
                   ApplicationConstants.ButtonWarningColor);
            }
        }
    }
}
