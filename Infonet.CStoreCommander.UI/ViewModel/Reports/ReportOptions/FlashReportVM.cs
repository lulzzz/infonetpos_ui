using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.UI.Model.Reports;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions
{
    public class FlashReportVM : ReportsScreenVM
    {
        private FlashReportTotalModel _flashReportTotalModel;
        private ObservableCollection<FlashReportModel> _flashReport;
        private string _reportText;
        private string _tillNumber;

        public string TillNumber
        {
            get { return _tillNumber; }
            set
            {
                _tillNumber = value;
                RaisePropertyChanged(nameof(TillNumber));
            }
        }


        public FlashReportTotalModel FlashReportTotalModel
        {
            get { return _flashReportTotalModel; }
            set
            {
                _flashReportTotalModel = value;
                RaisePropertyChanged(nameof(FlashReportTotalModel));
            }
        }

        public ObservableCollection<FlashReportModel> FlashReport
        {
            get { return _flashReport; }
            set
            {
                _flashReport = value;
                RaisePropertyChanged(nameof(FlashReport));
            }
        }

        public RelayCommand PrintReportCommand;
        public RelayCommand GetReportCommand;

        public FlashReportVM(IReportsBussinessLogic reportsBussinessLogic)
            : base(reportsBussinessLogic)
        {
            InitalizeCommands();
            InitalizeData();
        }

        private void InitalizeData()
        {
            FlashReportTotalModel = new FlashReportTotalModel();
        }

        private void InitalizeCommands()
        {
            PrintReportCommand = new RelayCommand(() => PerformAction(PrintReport));
            GetReportCommand = new RelayCommand(() => PerformAction(GetFlashReport));
        }

        private async Task GetFlashReport()
        {
            var response = await _reportsBussinessLogic.GetFlashReport();

            if (response?.Totals != null)
            {
                PopulatTotals(response.Totals);
            }
            if (response?.Report != null)
            {
                _reportText = response.Report.ReportContent;
            }
            if (response?.Departments != null)
            {
                PopulateDepartments(response.Departments);
            }
        }

        private async Task PrintReport()
        {
            var printReport = new Task(async () =>
            {
                await PrintReport(ReportType.FlasReportFile);
            });
            printReport.RunSynchronously();
        }

        private void PopulateDepartments(List<Departments> departments)
        {
            var tempDepartmentList = new ObservableCollection<FlashReportModel>();

            foreach (var department in departments)
            {
                tempDepartmentList.Add(new FlashReportModel
                {
                    Department = department.Department,
                    Description = department.Description,
                    NetSales = department.NetSales
                });
            }

            FlashReport = tempDepartmentList;
        }

        private void PopulatTotals(Totals totals)
        {
            FlashReportTotalModel.Charges = totals.Charges;
            FlashReportTotalModel.InvoiceDiscount = totals.InvoiceDiscount;
            FlashReportTotalModel.LineDiscounts = totals.LineDiscount;
            FlashReportTotalModel.ProductSales = totals.ProductSales;
            FlashReportTotalModel.Refunded = totals.Refunded;
            FlashReportTotalModel.SalesAfterDiscount = totals.SalesAfterDiscount;
            FlashReportTotalModel.Taxes = totals.Taxes;
            FlashReportTotalModel.TotalsReceipts = totals.TotalsReceipts;
        }

        internal void ReInitalizeVM()
        {
            _reportText = string.Empty;
            TillNumber = CacheBusinessLogic.TillNumber.ToString();
        }
    }
}
