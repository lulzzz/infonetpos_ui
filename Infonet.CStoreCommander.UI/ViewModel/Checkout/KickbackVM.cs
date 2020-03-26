using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Checkout
{
    public class KickbackVM : VMBase
    {
        private string _cardNumber;
        private string _kickbackBalance;
        private bool _isPrintEnable;

        public bool IsPrintEnable
        {
            get { return _isPrintEnable; }
            set { Set(nameof(IsPrintEnable), ref _isPrintEnable, value); }
        }


        public string KickBackBalance
        {
            get { return _kickbackBalance; }
            set { Set(nameof(KickBackBalance), ref _kickbackBalance, value); }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set { Set(nameof(CardNumber), ref _cardNumber, value); }
        }

        public RelayCommand GetKickbackBalanceCommand { get; set; }
        public RelayCommand CloseKickbackBalancePopupCommand { get; set; }
        public RelayCommand<object> KickbackBalanceEnterCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }

        private readonly IKickBackBusinessLogic _kickBackBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;

        public KickbackVM(IKickBackBusinessLogic kickBackBusinessLogic,
            IReportsBussinessLogic reportBusinessLogic)
        {
            _kickBackBusinessLogic = kickBackBusinessLogic;
            _reportsBussinessLogic = reportBusinessLogic;
            InitializeCommand();
        }

        private void ResetData()
        {
            IsPrintEnable = false;
            CardNumber = string.Empty;
            KickBackBalance = null;
        }

        private void InitializeCommand()
        {
            GetKickbackBalanceCommand = new RelayCommand(GetKickbackBalance);
            CloseKickbackBalancePopupCommand = new RelayCommand(() =>
            {
                CloseKickbackPopup();
                ResetData();
            });

            KickbackBalanceEnterCommand = new RelayCommand<object>(KickbackBalanceEnter);
            PrintCommand = new RelayCommand(Print);
        }

        private void KickbackBalanceEnter(object s)
        {
            if (Helper.IsEnterKey(s))
            {
                GetKickbackBalance();
            }
        }

        private void Print()
        {
            double points = 0D;
            CloseKickbackPopup();

            if (double.TryParse(KickBackBalance, out points))
            {
                PerformAction(async () =>
                {
                    try
                    {
                        await _reportsBussinessLogic.GetKickBackReport(points);
                        PrintKickBackReport();
                    }
                    finally
                    {
                        ResetData();
                        CacheBusinessLogic.LastPrintReport = ReportType.KickBackReport;
                    }
                });
            }
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

        private void PrintKickBackReport()
        {
            var printReport = new Task(async () =>
            {
                await PrintReport(ReportType.KickBackReport);
            });

            printReport.RunSynchronously();
        }

        private void CloseKickbackPopup()
        {
            PopupService.IsKickbackBalancePopupOpen = false;
            PopupService.IsPopupOpen = false;
        }

        private void GetKickbackBalance()
        {
            PerformAction(async () =>
            {
                try
                {
                    CloseKickbackPopup();
                    var response = await _kickBackBusinessLogic.CheckKickBackbalance(CardNumber);
                    KickBackBalance = response.BalancePoint;
                    IsPrintEnable = true;
                    OpenKickBackPopup();
                }
                catch (Exception ex)
                {
                    ResetData();
                    throw;
                }
            });

        }

        private void OpenKickBackPopup()
        {
            PopupService.IsPopupOpen = true;
            PopupService.IsKickbackBalancePopupOpen = true;
        }
    }
}
