using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Model.Reports;
using Infonet.CStoreCommander.UI.Utility;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions
{
    public class ReportVM : ReportsScreenVM
    {
        private SaleCountModel _saleCount;
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

        public RelayCommand GetReportCommand;
        public RelayCommand PrintReportCommand;

        public ReportVM(IReportsBussinessLogic reportsBussinessLogic)
            : base(reportsBussinessLogic)
        {
            InitializeCommands();
            MessengerInstance.Register<SaleCountModel>(this, "RunReport", RunReport);
        }


        private void InitializeCommands()
        {
            GetReportCommand = new RelayCommand(() => PerformAction(GetReportAsync));
            PrintReportCommand = new RelayCommand(() => PerformAction(PrintReport));
        }

        private async Task PrintReport()
        {
            var printReport = new Task(async () =>
            {
                await PrintReport(ReportType.SalesCountFile);
            });

            printReport.RunSynchronously();
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


        private void RunReport(SaleCountModel saleCount)
        {
            _saleCount = saleCount;
            PerformAction(GetReportAsync);
        }

        private async Task GetReportAsync()
        {
            var response = await _reportsBussinessLogic.GetSaleCountReport(_saleCount.ShiftNumber,
                _saleCount.TillNumber, _saleCount.departmentID);

            ReportText = response.ReportContent;
        }

        internal void ReInitializeVM()
        {
            _saleCount = new SaleCountModel
            {
                departmentID = "0",
                ShiftNumber = 0,
                TillNumber = 0
            };

            ReportText = string.Empty;
        }

    }
}
