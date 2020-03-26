using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reprint;
using Infonet.CStoreCommander.UI.Model.Reprint;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Reprint
{
    public class ReprintVM : VMBase
    {
        private readonly IReprintBusinessLogic _reprintBusinessLogic;
        protected readonly IReportsBussinessLogic _reportsBussinessLogic;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;
        private int _selectedReportIndex;
        private bool _isDateEnabled;
        private string _selectedReportType;
        private DateTimeOffset _selectedDate;
        private List<CloseBatchSalesModel> _localCloseBatchSales;
        private List<PayAtPumpSaleModel> _localPayAtPumpSales;
        private List<PayInsideSalesModel> _localPayInsideSales;
        private List<PaymentSalesModel> _localPaymentSales;
        private ObservableCollection<ReportTypesModel> _reportTypes;
        private ObservableCollection<PayAtPumpSaleModel> _payAtPumpSales;
        private ObservableCollection<PayInsideSalesModel> _payInsideSales;
        private ObservableCollection<PaymentSalesModel> _paymentSales;
        private ObservableCollection<CloseBatchSalesModel> _closeBatchSales;
        private bool _isPaymentSaleVisible;
        private bool _isPayAtPumpSaleVisible;
        private bool _isPayInsideSaleVisible;
        private bool _isCloseBatchSaleVisible;
        private string _reportContent;
        private CloseBatchSalesModel _selectedCloseBatchSale;
        private PaymentSalesModel _selectedPaymentSale;
        private PayInsideSalesModel _selectedPayInsideSale;
        private PayAtPumpSaleModel _selectedPayAtPumpSale;
        private bool _isPrintEnable;
        private string _searchSaleNumber;
        private bool _isSearchBoxVisible;
        private List<Report> _localListOfReports;
        private bool _isTenderDetailsEnable;

        public bool IsTenderDetailsEnable
        {
            get { return _isTenderDetailsEnable; }
            set
            {
                if (_isTenderDetailsEnable != value)
                {
                    _isTenderDetailsEnable = value;
                    RaisePropertyChanged(nameof(IsTenderDetailsEnable));
                }
            }
        }



        public bool IsSearchBoxVisible
        {
            get { return _isSearchBoxVisible; }
            set
            {
                _isSearchBoxVisible = value;
                RaisePropertyChanged(nameof(IsSearchBoxVisible));
            }
        }
        public string SearchSaleNumber
        {
            get { return _searchSaleNumber; }
            set
            {
                _searchSaleNumber = Helper.SelectIntegers(value);
                RaisePropertyChanged(nameof(SearchSaleNumber));
            }
        }
        public bool IsPrintEnable
        {
            get { return _isPrintEnable; }
            set
            {
                _isPrintEnable = value;
                RaisePropertyChanged(nameof(IsPrintEnable));
            }
        }
        public PayAtPumpSaleModel SelectedPayAtPumpSale
        {
            get { return _selectedPayAtPumpSale; }
            set
            {
                if (_selectedPayAtPumpSale != value)
                {
                    _selectedPayAtPumpSale = value;
                    RaisePropertyChanged(nameof(SelectedPayAtPumpSale));
                    if (SelectedPayAtPumpSale != null)
                    {
                        GetReport(SelectedPayAtPumpSale.SaleNumber);
                    }
                }
            }
        }
        public PayInsideSalesModel SelectedPaymentInsideSale
        {
            get { return _selectedPayInsideSale; }
            set
            {
                if (_selectedPayInsideSale != value)
                {
                    _selectedPayInsideSale = value;
                    RaisePropertyChanged(nameof(SelectedPaymentInsideSale));
                    if (SelectedPaymentInsideSale != null)
                    {
                        GetReport(SelectedPaymentInsideSale.SaleNumber);
                    }
                }
            }
        }
        public PaymentSalesModel SelectedPaymentSale
        {
            get { return _selectedPaymentSale; }
            set
            {
                if (_selectedPaymentSale != value)
                {
                    _selectedPaymentSale = value;
                    RaisePropertyChanged(nameof(SelectedPaymentSale));
                    if (SelectedPaymentSale != null)
                    {
                        GetReport(SelectedPaymentSale.SaleNumber);
                    }
                }
            }
        }
        public CloseBatchSalesModel SelectedCloseBatchSale
        {
            get { return _selectedCloseBatchSale; }
            set
            {
                if (_selectedCloseBatchSale != value)
                {
                    _selectedCloseBatchSale = value;
                    RaisePropertyChanged(nameof(SelectedCloseBatchSale));

                    if (SelectedCloseBatchSale != null)
                    {
                        ReportContent = SelectedCloseBatchSale.Report;
                    }
                }
            }
        }
        public string ReportContent
        {
            get { return _reportContent; }
            set
            {
                _reportContent = value;
                IsPrintEnable = !string.IsNullOrEmpty(_reportContent);
                RaisePropertyChanged(nameof(ReportContent));
            }
        }
        public bool IsCloseBatchSaleVisible
        {
            get { return _isCloseBatchSaleVisible; }
            set
            {
                if (value != _isCloseBatchSaleVisible)
                {
                    _isCloseBatchSaleVisible = value;
                    RaisePropertyChanged(nameof(IsCloseBatchSaleVisible));
                }
            }
        }
        public bool IsPayInsideSaleVisible
        {
            get { return _isPayInsideSaleVisible; }
            set
            {
                if (value != _isPayInsideSaleVisible)
                {
                    _isPayInsideSaleVisible = value;
                    RaisePropertyChanged(nameof(IsPayInsideSaleVisible));
                }
            }
        }
        public bool IsPayAtPumpSaleVisible
        {
            get { return _isPayAtPumpSaleVisible; }
            set
            {
                if (_isPayAtPumpSaleVisible != value)
                {
                    _isPayAtPumpSaleVisible = value;
                    RaisePropertyChanged(nameof(IsPayAtPumpSaleVisible));
                }
            }
        }
        public bool IsPaymentSaleVisible
        {
            get { return _isPaymentSaleVisible; }
            set
            {
                if (_isPaymentSaleVisible != value)
                {
                    _isPaymentSaleVisible = value;
                    RaisePropertyChanged(nameof(IsPaymentSaleVisible));
                }
            }
        }
        public ObservableCollection<CloseBatchSalesModel> CloseBatchSales
        {
            get { return _closeBatchSales; }
            set
            {
                _closeBatchSales = value;
                RaisePropertyChanged(nameof(CloseBatchSales));
            }
        }
        public ObservableCollection<PaymentSalesModel> PaymentSales
        {
            get { return _paymentSales; }
            set
            {
                _paymentSales = value;
                RaisePropertyChanged(nameof(PaymentSales));
            }
        }
        public ObservableCollection<PayInsideSalesModel> PayInsideSales
        {
            get { return _payInsideSales; }
            set
            {
                _payInsideSales = value;
                RaisePropertyChanged(nameof(PayInsideSales));
            }
        }
        public ObservableCollection<PayAtPumpSaleModel> PayAtPumpSales
        {
            get { return _payAtPumpSales; }
            set
            {
                _payAtPumpSales = value;
                RaisePropertyChanged(nameof(PayAtPumpSales));
            }
        }
        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    RaisePropertyChanged(nameof(SelectedDate));

                    if (!string.IsNullOrEmpty(_selectedReportType))
                    {
                        PerformAction(GetReportSales);
                    }
                }
            }
        }
        public bool IsDateEnabled
        {
            get { return _isDateEnabled; }
            set
            {
                _isDateEnabled = value;
                RaisePropertyChanged(nameof(IsDateEnabled));
            }
        }
        public int SelectedReportIndex
        {
            get { return _selectedReportIndex; }
            set
            {
                if (value > -1 && _selectedReportIndex != value)
                {
                    var selectedReport = ReportTypes.ElementAt(value);

                    ResetSelectedReports();

                    if (selectedReport != null)
                    {
                        if (selectedReport.IsReportEnabled)
                        {
                            _selectedReportType = selectedReport.ReportType;
                            IsDateEnabled = selectedReport.IsDateEnabled;
                            if (_selectedReportType == "PayInside_HistoricalSale" || _selectedReportType == "PayAtPump_HistoricalSale")
                            {
                                SelectedDate = DateTime.Parse(CacheBusinessLogic.ShiftDate, CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                SelectedDate = DateTime.Now;
                            };
                            SearchSaleNumber = string.Empty;
                            IsTenderDetailsEnable = selectedReport.ReportType.Equals("PayInside_HistoricalSale") ||
                            selectedReport.ReportType.Equals("PayInside_CurrentSale") ||
                            selectedReport.ReportType.Equals("Payments_ArPay") ||
                            selectedReport.ReportType.Equals("Payments_FleetCard");
                        }
                        else
                        {
                            ShowNotification(ApplicationConstants.ReprintDisabled,
                                null,
                                null,
                                ApplicationConstants.ButtonWarningColor);
                            ReportContent = string.Empty;
                            RaisePropertyChanged(nameof(SelectedReportIndex));
                            return;
                        }
                        ReportContent = string.Empty;
                    }
                }
                _selectedReportIndex = value;
                RaisePropertyChanged(nameof(SelectedReportIndex));
            }
        }

        private void ResetSelectedReports()
        {
            SelectedPayAtPumpSale = null;
            SelectedPaymentInsideSale = null;
            SelectedPaymentSale = null;
            SelectedCloseBatchSale = null;
        }

        public ObservableCollection<ReportTypesModel> ReportTypes
        {
            get { return _reportTypes; }
            set
            {
                _reportTypes = value;
                RaisePropertyChanged(nameof(ReportTypes));
            }
        }

        public RelayCommand<object> SearchBySaleNumberCommand { get; set; }
        public RelayCommand PrintReportCommand { get; set; }
        public RelayCommand TenderDetailsCommand { get; set; }

        public ReprintVM(IReprintBusinessLogic reprintBusinessLogic,
            IReportsBussinessLogic reportsBussinessLogic,
            ICacheBusinessLogic cacheBusinessLogic
            )
        {
            _reprintBusinessLogic = reprintBusinessLogic;
            _reportsBussinessLogic = reportsBussinessLogic;
            _cacheBusinessLogic = CacheBusinessLogic;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SearchBySaleNumberCommand = new RelayCommand<object>(SearchBySaleNumber);
            PrintReportCommand = new RelayCommand(async () =>
           {
               await PerformPrint(ReportContent.Split('\n').ToList());
           });

            TenderDetailsCommand = new RelayCommand(async () =>
           {
               await PerformPrint(_localListOfReports);
           });
        }

        private void SearchBySaleNumber(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                if (IsPayAtPumpSaleVisible)
                {
                    var searchedList = (from p in _localPayAtPumpSales
                                        where p.SaleNumber.Contains(SearchSaleNumber)
                                        select p).ToList();

                    PayAtPumpSales = new ObservableCollection<PayAtPumpSaleModel>(searchedList);
                }
                else if (IsPayInsideSaleVisible)
                {
                    var searchedList = (from p in _localPayInsideSales
                                        where p.SaleNumber.Contains(SearchSaleNumber)
                                        select p).ToList();

                    PayInsideSales = new ObservableCollection<PayInsideSalesModel>(searchedList);
                }
                else if (IsPaymentSaleVisible)
                {
                    var searchedList = (from p in _localPaymentSales
                                        where p.SaleNumber.Contains(SearchSaleNumber)
                                        select p).ToList();

                    PaymentSales = new ObservableCollection<PaymentSalesModel>(searchedList);
                }
            }
        }


        private async Task GetReportSales()
        {
            var timer = new Stopwatch();
            timer.Restart();

            try
            {
                var response = await _reprintBusinessLogic.GetReprintReportSale(_selectedReportType,
                    SelectedDate.Date.ToString(CultureInfo.InvariantCulture));

                if (response.IsCloseBatchSale)
                {
                    IsSearchBoxVisible = false;
                    MapCloseBatchSale(response.CloseBatchSales);
                    ShowCloseBacthSale();
                    var report = new Report
                    {
                        ReportContent = ReportContent,
                        ReportName = "CloseBatch.txt",
                        Copies = 0
                    };

                    await _reportsBussinessLogic.SaveReport(report);
                }
                else if (response.IsPayAtPumpSale)
                {
                    IsSearchBoxVisible = true;
                    MapPayAtPumpSales(response.PayAtPumpSales);
                    ShowPayAtPumpSale();
                }
                else if (response.IsPayInsideSale)
                {
                    IsSearchBoxVisible = true;
                    MapPayInsideSales(response.PayInsideSales);
                    ShowPayInsideSale();
                }
                else if (response.IsPaymentSale)
                {
                    IsSearchBoxVisible = true;
                    MapPaymentSale(response.PaymentSales);
                    ShowPaymentSale();
                }
            }
            catch (Exception)
            {
                IsCloseBatchSaleVisible = false;
                IsPaymentSaleVisible = false;
                IsPayAtPumpSaleVisible = false;
                IsPayInsideSaleVisible = false;
                throw;
            }
            finally
            {
                timer.Stop();
                Log.Info(string.Format("Time taken in getting report sales is {0}ms ", timer.ElapsedMilliseconds));
            }
        }

        private void GetReport(string saleNumber)
        {
            PerformAction(async () =>
            {
                ReportContent = string.Empty;

                _localListOfReports = await _reprintBusinessLogic.GetReprintReport(saleNumber,
                    SelectedDate.Date.ToString(CultureInfo.InvariantCulture), _selectedReportType);
                if (_cacheBusinessLogic.RECEIPT_TYPE != "DEFAULT" && _localListOfReports != null && _localListOfReports.Count > 0)
                {
                    _localListOfReports = Helper.ReceiptLabelMapping(_localListOfReports, _cacheBusinessLogic.RECEIPT_TYPE);
                }
                if (_localListOfReports?.Count >= 1)
                {
                    ReportContent = _localListOfReports.FirstOrDefault().ReportContent;
                }
            });
        }

        private void ShowPaymentSale()
        {
            HideAllSales();
            IsPaymentSaleVisible = true;
        }

        private void HideAllSales()
        {
            IsPaymentSaleVisible = IsPayInsideSaleVisible = IsPayAtPumpSaleVisible =
            IsCloseBatchSaleVisible = false;
        }

        private void ShowPayInsideSale()
        {
            HideAllSales();
            IsPayInsideSaleVisible = true;
        }

        private void ShowPayAtPumpSale()
        {
            HideAllSales();
            IsPayAtPumpSaleVisible = true;
        }

        private void ShowCloseBacthSale()
        {
            HideAllSales();
            IsCloseBatchSaleVisible = true;
        }

        private void MapPaymentSale(List<PaymentSales> paymentSales)
        {
            _localPaymentSales = (from p in paymentSales
                                  select new PaymentSalesModel
                                  {
                                      Amount = p.Amount,
                                      SaleNumber = p.SaleNumber,
                                      SoldOn = p.SoldOn,
                                      Time = p.Time
                                  }).ToList();

            PaymentSales = new ObservableCollection<PaymentSalesModel>(_localPaymentSales);
        }

        private void MapPayInsideSales(List<PayInsideSales> payInsideSales)
        {
            _localPayInsideSales = (from p in payInsideSales
                                    select new PayInsideSalesModel
                                    {
                                        Amount = p.Amount,
                                        Customer = p.Customer,
                                        SaleNumber = p.SaleNumber,
                                        SoldOn = p.SoldOn,
                                        Time = p.Time
                                    }).ToList();

            PayInsideSales = new ObservableCollection<PayInsideSalesModel>(_localPayInsideSales);
        }

        private void MapPayAtPumpSales(List<PayAtPumpSale> payAtPumpSales)
        {
            _localPayAtPumpSales = (from p in payAtPumpSales
                                    select new PayAtPumpSaleModel
                                    {
                                        Amount = p.Amount,
                                        Date = p.Date,
                                        Grade = p.Grade,
                                        Pump = p.Pump,
                                        SaleNumber = p.SaleNumber,
                                        Time = p.Time,
                                        Volume = p.Volume
                                    }).ToList();

            PayAtPumpSales = new ObservableCollection<PayAtPumpSaleModel>(_localPayAtPumpSales);
        }

        private void MapCloseBatchSale(List<CloseBatchSales> closeBatchSales)
        {
            _localCloseBatchSales = (from c in closeBatchSales
                                     select new CloseBatchSalesModel
                                     {
                                         BatchNumber = c.BatchNumber,
                                         Date = c.Date,
                                         Report = c.Report,
                                         TerminalId = c.TerminalId,
                                         Time = c.Time
                                     }).ToList();

            CloseBatchSales = new ObservableCollection<CloseBatchSalesModel>(_localCloseBatchSales);
        }

        private async Task GetReportNames()
        {
            var response = await _reprintBusinessLogic.GetReprintReportName();

            foreach (var report in response)
            {
                ReportTypes.Add(new ReportTypesModel
                {
                    IsReportEnabled = report.IsEnabled,
                    ReportName = report.Name.Replace("_", " "),
                    ReportType = report.ReportType,
                    IsDateEnabled = report.DateEnabled
                });
            }
        }

        internal void ResetVM()
        {
            _selectedReportType = string.Empty;
            ReportContent = string.Empty;
            SelectedReportIndex = -1;
            ReportTypes = new ObservableCollection<ReportTypesModel>();
            PerformAction(GetReportNames);
            SelectedDate = DateTime.Now;
            SelectedCloseBatchSale = null;
            SelectedPayAtPumpSale = null;
            SelectedPaymentInsideSale = null;
            SelectedPaymentSale = null;
            HideAllSales();
            IsPayAtPumpSaleVisible = false;
            SearchSaleNumber = string.Empty;
            IsDateEnabled = false;
            IsSearchBoxVisible = false;
            IsTenderDetailsEnable = false;
        }
    }
}
