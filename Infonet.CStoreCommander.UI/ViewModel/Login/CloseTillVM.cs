using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Logout;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.UI.Model.Logout;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infonet.CStoreCommander.UI.Messages;

namespace Infonet.CStoreCommander.UI.ViewModel.Login
{
    public class CloseTillVM : LogoutScreenVM
    {
        private IReportsBussinessLogic _reportsBussinessLogic;
        private CloseTillModel _closeTillModel;
        private CloseTillTendersModel _selectedCloseTillTenders;
        private bool _gridWith2ColumnVisible;
        private bool _gridWidthEnteredColumnVisible;
        private bool _gridWithSystemColumnVisible;
        private bool _gridWidth5ColumnVisible;
        private bool _isReportVisible;
        private Report _tillCloseReport;
        private bool _isBillCoinGridVisibles;
        private BillCoinsModel _selectedBillCoins;
        private bool _isCompletetillCloseButtonEnable;
        private bool _isExitButtonEnable;
        private bool _gridWidth2ColumnVisible;
        private bool _isBillCoinCounterEnable;


        public CloseTillMessage CloseTillMessage { get; set; }
        public bool IsBillCoinCounterEnable
        {
            get { return _isBillCoinCounterEnable; }
            set
            {
                if (value != _isBillCoinCounterEnable)
                {
                    _isBillCoinCounterEnable = value;
                    RaisePropertyChanged(nameof(IsBillCoinCounterEnable));
                }
            }
        }
        public bool GridWidth2ColumnVisible
        {
            get { return _gridWidth2ColumnVisible; }
            set
            {
                if (value != _gridWidth2ColumnVisible)
                {
                    _gridWidth2ColumnVisible = value;
                    RaisePropertyChanged(nameof(GridWidth2ColumnVisible));
                }
            }
        }
        public bool IsExitButtonEnable
        {
            get { return _isExitButtonEnable; }
            set
            {
                if (_isExitButtonEnable != value)
                {
                    _isExitButtonEnable = value;
                    RaisePropertyChanged(nameof(IsExitButtonEnable));
                }
            }
        }
        public bool IsCompleteTillCloseButtonEnable
        {
            get { return _isCompletetillCloseButtonEnable; }
            set
            {
                if (_isCompletetillCloseButtonEnable != value)
                {
                    _isCompletetillCloseButtonEnable = value;
                    RaisePropertyChanged(nameof(IsCompleteTillCloseButtonEnable));
                }
            }
        }
        public bool IsBillCoinGridVisible
        {
            get { return _isBillCoinGridVisibles; }
            set
            {
                if (_isBillCoinGridVisibles != value)
                {
                    _isBillCoinGridVisibles = value;
                    RaisePropertyChanged(nameof(IsBillCoinGridVisible));
                }
            }
        }
        public BillCoinsModel SelectedBillCoin
        {
            get { return _selectedBillCoins; }
            set
            {
                if (_selectedBillCoins != value)
                {
                    _selectedBillCoins = value;
                    RaisePropertyChanged(nameof(SelectedBillCoin));
                }
            }
        }
        public Report TillCloseReport
        {
            get { return _tillCloseReport; }
            set
            {
                if (_tillCloseReport != value)
                {
                    _tillCloseReport = value;
                    RaisePropertyChanged(nameof(TillCloseReport));
                }
            }
        }
        public bool IsReportVisible
        {
            get { return _isReportVisible; }
            set
            {
                _isReportVisible = value;
                RaisePropertyChanged(nameof(IsReportVisible));
            }
        }
        public bool GridWidth5ColumnVisible
        {
            get { return _gridWidth5ColumnVisible; }
            set
            {
                if (_gridWidth5ColumnVisible != value)
                {
                    _gridWidth5ColumnVisible = value;
                    RaisePropertyChanged(nameof(GridWidth5ColumnVisible));
                }
            }
        }
        public bool GridWithSystemColumnVisible
        {
            get { return _gridWithSystemColumnVisible; }
            set
            {
                if (_gridWithSystemColumnVisible != value)
                {
                    _gridWithSystemColumnVisible = value;
                    RaisePropertyChanged(nameof(GridWithSystemColumnVisible));
                }
            }
        }
        public bool GridWidthEnteredColumnVisible
        {
            get { return _gridWidthEnteredColumnVisible; }
            set
            {
                if (_gridWidthEnteredColumnVisible != value)
                {
                    _gridWidthEnteredColumnVisible = value;
                    RaisePropertyChanged(nameof(GridWidthEnteredColumnVisible));
                }
            }
        }
        public bool GridWith2ColumnVisible
        {
            get { return _gridWith2ColumnVisible; }
            set
            {
                if (_gridWith2ColumnVisible != value)
                {
                    _gridWith2ColumnVisible = value;
                    RaisePropertyChanged(nameof(GridWith2ColumnVisible));
                }
            }
        }
        public CloseTillTendersModel SelectedCloseTillTenders
        {
            get { return _selectedCloseTillTenders; }
            set
            {
                if (_selectedCloseTillTenders != value)
                {
                    _selectedCloseTillTenders = value;
                    RaisePropertyChanged(nameof(SelectedCloseTillTenders));
                }

                IsBillCoinGridVisible = false;
            }
        }
        public CloseTillModel CloseTillModel
        {
            get { return _closeTillModel; }
            set
            {
                if (_closeTillModel != value)
                {
                    _closeTillModel = value;
                    RaisePropertyChanged(nameof(CloseTillModel));
                }
            }
        }
        public RelayCommand BillCoinButtonPressedCommand { get; set; }
        public RelayCommand PrintReportCommand { get; set; }
        public RelayCommand TenderValueChangedCommand { get; set; }
        public RelayCommand BillCoinValueChangedCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand FinishTillCloseCommand { get; set; }

        public CloseTillVM(ILogoutBussinessLogic logoutBussinesslogic,
            IFuelPumpBusinessLogic fuelBusinessLogic,
            IReportsBussinessLogic reportsBussinessLogic) :
            base(logoutBussinesslogic, fuelBusinessLogic)
        {
            _reportsBussinessLogic = reportsBussinessLogic;
            RegisterMessages();
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            BillCoinButtonPressedCommand = new RelayCommand(BillCoinButtonPressed);
            TenderValueChangedCommand = new RelayCommand(TenderValueChanged);
            BillCoinValueChangedCommand = new RelayCommand(BillCoinValueChanged);
            ExitCommand = new RelayCommand(PerformCloseApplication);

            FinishTillCloseCommand = new RelayCommand(() =>
            {
                PerformAction(FinishTillClose);
            });

            PrintReportCommand = new RelayCommand(async () =>
            {
                await PrintTillCloseReport();
            });
        }

        private async Task FinishTillClose()
        {
            WriteToLineDisplay(CloseTillModel.LineDisplay);

            var response = await _logoutBussinesslogic.FinishTillClose(CloseTillMessage.ApiResponseForReadTankDip,
                CloseTillMessage.ApiResponseForReadTotalizer);


            var eodReport = response.Reports.FirstOrDefault(x => x.ReportName.Equals(ReportType.EodDetailsFile));
            var bankEodReport = response.Reports.FirstOrDefault(x => x.ReportName.Equals(ReportType.BankEodFile));

            var reports = new List<Report>();

            if (eodReport != null)
            {
                reports.Add(eodReport);
            }
            if (bankEodReport != null)
            {
                reports.Add(bankEodReport);
            }

            WriteToLineDisplay(response.LineDisplay);

            PerformPrint(reports);


            if (!string.IsNullOrEmpty(response.Message?.Message))
            {
                ShowNotification(response.Message.Message,
                   () =>
                   {
                       ShowReportText(response.Reports);
                   },
                () =>
                {
                    ShowReportText(response.Reports);
                },
                ApplicationConstants.ButtonWarningColor);
            }
            else
            {
                ShowReportText(response.Reports);
            }
        }

        private void ShowReportText(List<Report> reports)
        {
            TillCloseReport = reports.FirstOrDefault(x => x.ReportName.Contains(ReportType.TillCloseFile));

            IsBillCoinGridVisible = false;
            IsReportVisible = true;
            IsCompleteTillCloseButtonEnable = false;
            IsExitButtonEnable = false;
            IsBillCoinCounterEnable = false;
        }

        private async Task PrintTillCloseReport()
        {
            await PerformPrint(TillCloseReport);
            IsExitButtonEnable = true;
        }

        private void BillCoinValueChanged()
        {
            PerformAction(async () =>
            {
                var updatedBillCoin = new UpdatedBillCoin
                {
                    Amount = SelectedBillCoin.Amount,
                    Description = SelectedBillCoin.Description
                };

                var response = await _logoutBussinesslogic.UpdateTillClose(null, updatedBillCoin);
                SetCloseTillResponse(response);
            });
        }

        private void TenderValueChanged()
        {
            PerformAction(async () =>
            {
                var updatedTender = new UpdatedTender
                {
                    Entered = SelectedCloseTillTenders.Entered,
                    Name = SelectedCloseTillTenders.Tender
                };

                var response = await _logoutBussinesslogic.UpdateTillClose(updatedTender, null);
                SetCloseTillResponse(response);
            });
        }

        private void BillCoinButtonPressed()
        {
            PerformAction(async () =>
            {
                var response = await _logoutBussinesslogic.UpdateTillClose(null, null);
                SelectedCloseTillTenders = null;
                IsBillCoinGridVisible = true;

                SetCloseTillResponse(response);
                OpenCashDrawer();
            });
        }

        private void RegisterMessages()
        {
            MessengerInstance.Register<CloseTill>(this,
                "SetCloseTill",
                SetCloseTillResponse);

            MessengerInstance.Register<CloseTillMessage>(this,
               "CloseTillMessage",
               SetCloseTillValues);
        }

        private void SetCloseTillValues(CloseTillMessage message)
        {
            CloseTillMessage = message;
        }

        private async void SetCloseTillResponse(CloseTill response)
        {
            if (response != null)
            {
                response.BillCoins = response.BillCoins ?? new List<BillCoins>();
                response.Tenders = response.Tenders ?? new List<CloseTillTenders>();

                var billCoins = (from b in response.BillCoins
                                 select new BillCoinsModel
                                 {
                                     Amount = b.Amount,
                                     Description = b.Description,
                                     Value = b.Value
                                 }).ToList();

                var closeTillTenders = (from t in response.Tenders
                                        select new CloseTillTendersModel
                                        {
                                            Count = t.Count,
                                            Difference = t.Difference,
                                            Entered = t.Entered,
                                            System = t.System,
                                            Tender = t.Tender
                                        }).ToList();


                CloseTillModel = new CloseTillModel
                {
                    Total = response.Total,
                    BillCoins = new ObservableCollection<BillCoinsModel>(billCoins),
                    Tenders = new ObservableCollection<CloseTillTendersModel>(closeTillTenders),
                    ShowBillCoins = response.ShowBillCoins,
                    ShowDifferenceField = response.ShowDifferenceField,
                    ShowEnteredField = response.ShowEnteredField,
                    ShowSystemField = response.ShowSystemField,
                    LineDisplay = response.LineDisplay
                };

                SetVisibilityOfGrid();

                // Adding wait for the keyboard closing
                await Task.Delay(500);
                MessengerInstance.Send(new CloseKeyboardMessage());
            }
        }

        private void SetVisibilityOfGrid()
        {
            if (CloseTillModel.ShowDifferenceField)
            {
                HideAllGrids();
                GridWidth5ColumnVisible = true;
            }

            else if (CloseTillModel.ShowEnteredField)
            {
                HideAllGrids();
                GridWidthEnteredColumnVisible = true;
            }

            else if (CloseTillModel.ShowSystemField)
            {
                HideAllGrids();
                GridWithSystemColumnVisible = true;
            }

            else if (!CloseTillModel.ShowDifferenceField && !CloseTillModel.ShowEnteredField &&
               !CloseTillModel.ShowSystemField)
            {
                HideAllGrids();
                GridWidth2ColumnVisible = true;
            }
        }

        private void HideAllGrids()
        {
            GridWidth5ColumnVisible = false;
            GridWidthEnteredColumnVisible = false;
            GridWith2ColumnVisible = false;
            GridWithSystemColumnVisible = false;
        }

        internal void ReInitialize()
        {
            CloseTillModel = new CloseTillModel();
            SelectedCloseTillTenders = null;
            IsReportVisible = false;
            IsBillCoinGridVisible = false;
            IsExitButtonEnable = true;
            IsCompleteTillCloseButtonEnable = true;
            IsBillCoinCounterEnable = true;
        }

    }
}
