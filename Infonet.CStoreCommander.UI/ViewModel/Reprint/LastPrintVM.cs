using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reprint
{
    public class LastPrintVM : VMBase
    {
        private readonly IReportsBussinessLogic _reportsBussinessLogic;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;
        private string _reportContent;
        private bool _isReprintEnable;

        public bool IsReprintEnable
        {
            get { return _isReprintEnable; }
            set
            {
                _isReprintEnable = value;
                RaisePropertyChanged(nameof(IsReprintEnable));
            }
        }

        public string ReportContent
        {
            get { return _reportContent; }
            set
            {
                _reportContent = value;
                IsReprintEnable = !string.IsNullOrEmpty(_reportContent);
                RaisePropertyChanged(nameof(ReportContent));
            }
        }

        public RelayCommand PrintReportCommand { get; set; }

        public LastPrintVM(IReportsBussinessLogic reportsBussinessLogic,
            ICacheBusinessLogic cacheBusinessLogic
            )            
        {
            _reportsBussinessLogic = reportsBussinessLogic;
            _cacheBusinessLogic = cacheBusinessLogic;
            PrintReportCommand = new RelayCommand(async () =>
            {
                await PerformPrint(ReportContent.Split('\n')?.ToList());
            });
            
        }

       

        internal void ResetVM()
        {
            ReportContent = string.Empty;
            PerformAction(GetLastReport);
        }

        public async Task GetLastReport()
        {
            var lastPrintReport = CacheBusinessLogic.LastPrintReport;
            try
            {
                var lastReport = await _reportsBussinessLogic.GetReceipt(lastPrintReport);
                if (_cacheBusinessLogic.RECEIPT_TYPE.ToUpper() != "DEFAULT" && lastPrintReport != null && lastReport.Count > 0)
                {
                    switch (_cacheBusinessLogic.RECEIPT_TYPE.ToLower())
                    {
                        case "en-ar":
                            lastReport = Helper.En_Ar_List(lastReport);
                            break;
                    }
                }
                ReportContent = string.Join("\n", lastReport.ToArray());
            }
            catch (FileNotFoundException)
            {
                ShowNotification(ApplicationConstants.NoReportFound,
                null,
                null, ApplicationConstants.ButtonWarningColor);
            }
        }
    }
}
