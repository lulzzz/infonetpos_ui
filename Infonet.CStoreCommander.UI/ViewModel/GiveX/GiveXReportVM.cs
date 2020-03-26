using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.GiveX;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using System.Globalization;

namespace Infonet.CStoreCommander.UI.ViewModel.GiveX
{
    public class GiveXReportVM : VMBase
    {
        private string _reportText;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly IGiveXBussinessLogic _givexBussinessLogic;
        private ObservableCollection<GiveXReportModel> _giveXReports;
        private GiveXReport _giveXReport;
        private DateTimeOffset _selectedDate;

        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    RaisePropertyChanged(nameof(SelectedDate));

                    PerformAction(GetGiveXReport);
                }
            }
        }

        public ObservableCollection<GiveXReportModel> GiveXReports
        {
            get { return _giveXReports; }
            set
            {
                if (_giveXReports != value)
                {
                    _giveXReports = value;
                    RaisePropertyChanged(nameof(GiveXReports));
                }
            }
        }


        public string ReportText
        {
            get { return _reportText; }
            set
            {
                _reportText = value;
                RaisePropertyChanged(nameof(ReportText));
            }
        }

        public RelayCommand PrintReportCommand { get; set; }

        public GiveXReportVM(IReportsBussinessLogic reportsBusinessLogic,
            IGiveXBussinessLogic givexBussinessLogic)
        {
            _reportsBusinessLogic = reportsBusinessLogic;
            _givexBussinessLogic = givexBussinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            PrintReportCommand = new RelayCommand(async () =>
            {
                var report = _giveXReport?.CloseBatchReport;
                await PerformPrint(report);
            });
        }

        internal void ReSetVM()
        {
            SelectedDate = DateTime.Now;
            GiveXReports = new ObservableCollection<GiveXReportModel>();
        }

        private async Task GetGiveXReport()
        {
            _giveXReport = await _givexBussinessLogic.GetGiveXReport(SelectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            var tempGiveXReport = MapGiveXReport(_giveXReport);
            ReportText = _giveXReport.CloseBatchReport?.ReportContent;

            GiveXReports = new ObservableCollection<GiveXReportModel>(tempGiveXReport);
        }

        private List<GiveXReportModel> MapGiveXReport(GiveXReport giveXReports)
        {
            return (from g in giveXReports.ReportDetails
                    select new GiveXReportModel
                    {
                        BatchDate = g.BatchDate,
                        BatchTime = g.BatchTime,
                        CashOut = g.CashOut,
                        Id = g.Id.ToString(),
                        Report = g.Report
                    }).ToList();

        }
    }
}
