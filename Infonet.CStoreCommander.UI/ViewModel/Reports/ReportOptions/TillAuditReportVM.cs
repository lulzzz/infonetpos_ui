using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions
{
    public class TillAuditReportVM : ReportsScreenVM
    {
        private string _reportText;

        public string ReportText
        {
            get { return _reportText; }
            set
            {
                _reportText = value;
                RaisePropertyChanged(nameof(ReportText));
            }
        }


        public RelayCommand GetTillAuditReportCommand;
        public RelayCommand PrintReportCommand;


        public TillAuditReportVM(IReportsBussinessLogic reportsBussinessLogic)
            : base(reportsBussinessLogic)
        {
            InitializeCommands();
        }

        private async Task GetTillAuditReportAsync()
        {
            try
            {
                var response = await _reportsBussinessLogic.GetTillAuditReport();

                ReportText = response.ReportContent;

                if (CacheBusinessLogic.FramePriorSwitchUserNavigation == "Reports")
                {
                    CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
                    CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
                }

            }
            catch (SwitchUserException ex)
            {
                ShowNotification(ex.Error.Message,
                        SwitchUserToViewTillAuditReport,
                        SwitchUserToViewTillAuditReport,
                        ApplicationConstants.ButtonWarningColor);
            }
        }

        private void SwitchUserToViewTillAuditReport()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "Reports";
            NavigateService.Instance.NavigateToLogout();
        }

        private void InitializeCommands()
        {
            PrintReportCommand = new RelayCommand(() => PerformAction(PrintReport));
            GetTillAuditReportCommand = new RelayCommand(() => PerformAction(GetTillAuditReportAsync));
        }

        private async Task PrintReport()
        {
            var printReport = new Task(async () =>
            {
                await PrintReport(ReportType.TillAuditFile);
            });

            printReport.RunSynchronously();
        }


        internal void ReInitializeVM()
        {
            ReportText = string.Empty;
        }
    }
}
