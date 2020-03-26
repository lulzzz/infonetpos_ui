using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Sale
{
    public class ReturnSaleVM : VMBase
    {
        private readonly ISaleBussinessLogic _saleBussinessLogic;
        private int _allSaleListPageIndex = 1;
        private int _searchPageIndex = 1;
        private bool _isSearchInProgress;
        private ObservableCollection<SaleList> _sales;
        private SaleList _selectedSalemodel;
        private string _searchText;
        private bool _allowCorrection;
        private string _reasonCode;
        private bool _isReturnSaleEnabled;
        private string _selectedSaleDate;

        public bool IsReturnSaleEnabled
        {
            get { return _isReturnSaleEnabled; }
            set
            {
                _isReturnSaleEnabled = value;
                RaisePropertyChanged(nameof(IsReturnSaleEnabled));
            }
        }

        public bool AllowCorrection
        {
            get { return _allowCorrection; }
            set
            {
                _allowCorrection = value;
                RaisePropertyChanged(nameof(AllowCorrection));
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = Helper.SelectIntegers(value);
                RaisePropertyChanged(nameof(SearchText));
            }
        }

        public SaleList SelectedSaleModel
        {
            get { return _selectedSalemodel; }
            set
            {
                _selectedSalemodel = value;
                IsReturnSaleEnabled = _selectedSalemodel != null &&
                    CacheBusinessLogic.IsCurrentSaleEmpty;
                if (_selectedSalemodel != null)
                {
                    AllowCorrection = _selectedSalemodel.AllowCorrection &&
                        CacheBusinessLogic.IsCurrentSaleEmpty;
                }
                RaisePropertyChanged(nameof(SelectedSaleModel));
            }
        }

        public ObservableCollection<SaleList> Sales
        {
            get { return _sales; }
            set
            {
                _sales = value;
                RaisePropertyChanged(nameof(Sales));
            }
        }

        public RelayCommand<object> MessageItemClickedCommand { get; private set; }
        public RelayCommand ReturnSaleCommand { get; private set; }
        public RelayCommand GetAllSaleListCommand { get; private set; }
        public RelayCommand SearchBySaleNumberCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand ReturnSaleItemCommand { get; private set; }
        public RelayCommand<object> SaleDateSelectedCommand;

        private readonly IReasonListBussinessLogic _reasonListBussinessLogic;

        public string SearchTextFieldName { get; set; }

        public ReturnSaleVM(IReasonListBussinessLogic reasonListBussinessLogic,
            ISaleBussinessLogic saleBussinessLogic)
        {
            _reasonListBussinessLogic = reasonListBussinessLogic;
            _saleBussinessLogic = saleBussinessLogic;
            InitializeCommands();
        }

        private void InitializeData()
        {
            IsReturnSaleEnabled = SelectedSaleModel != null &&
                CacheBusinessLogic.IsCurrentSaleEmpty;
            _isSearchInProgress = true;
            Sales = new ObservableCollection<SaleList>();
        }

        private void SaleDateSelected(dynamic obj)
        {
            _selectedSaleDate = obj.NewDate.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            PerformAction(LoadSearchResults, SearchTextFieldName);
        }

        public void ReInitialize()
        {
            IsReturnSaleEnabled = SelectedSaleModel != null &&
                CacheBusinessLogic.IsCurrentSaleEmpty;
            AllowCorrection = false;
        }

        private void InitializeCommands()
        {
            SaleDateSelectedCommand = new RelayCommand<object>(SaleDateSelected);
            MessageItemClickedCommand = new RelayCommand<object>(ReturnSaleWithReason);
            ReturnSaleCommand = new RelayCommand(() => PerformAction(ReturnSale));
            SearchBySaleNumberCommand = new RelayCommand(SearchBySaleNumber);
            GetAllSaleListCommand = new RelayCommand(() => PerformAction(LoadAllSales, SearchTextFieldName));
            RefreshCommand = new RelayCommand(() => PerformAction(LoadMoreData, SearchTextFieldName));
            ReturnSaleItemCommand = new RelayCommand(() => PerformAction(ReturnSaleItem));
        }

        private void ReturnSaleWithReason(dynamic obj)
        {
            _reasonCode = obj.Code;
            CloseReasonPopup();
            PerformAction(ReturnSaleAsync);
        }

        /// <summary>
        /// Method to return sale items
        /// </summary>
        /// <returns></returns>
        private async Task ReturnSaleItem()
        {
            var startTime = DateTime.Now;
            try
            {
                if (SelectedSaleModel != null)
                {
                    var response = await _saleBussinessLogic.GetSaleBySaleNumber(SelectedSaleModel.TillNumber, SelectedSaleModel.SaleNumber);
                    NavigateService.Instance.NavigateToReturnSaleItem();
                    MessengerInstance.Send(response.ToModel(), "ReturnSaleItem");
                }
            }
            finally
            {
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Return Sale Item  is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }


        private async Task ReturnSale()
        {
            if (SelectedSaleModel != null)
            {
                if (SelectedSaleModel.AllowReason == true)
                {
                    PerformAction(async () => { await GetReasonListAsync(EntityLayer.ReasonType.refunds, MessageItemClickedCommand); });
                }
                else
                {
                    await ReturnSaleAsync();
                }
            }
        }


        /// <summary>
        /// method to return sale
        /// </summary>
        /// <returns></returns>
        private async Task ReturnSaleAsync()
        {
            if (SelectedSaleModel != null)
            {
                var response = await _saleBussinessLogic.ReturnSale(CacheBusinessLogic.TillNumber,
                    SelectedSaleModel.TillNumber,
                    SelectedSaleModel.SaleNumber,
                    SelectedSaleModel.AllowCorrection,
                    _reasonCode, EntityLayer.ReasonType.refunds.ToString());

                await PerformPrint(response.Report);
                MessengerInstance.Send(response.Sale.ToModel(), "UpdateSale");
                NavigateService.Instance.NavigateToHome();
            }
        }

        /// <summary>
        /// Method to get reason list
        /// </summary>
        /// <returns></returns>
        private async Task GetReasonListAsync(EntityLayer.ReasonType reasonEnum, RelayCommand<object> reasonSelectCommand)
        {
            if (!PopupService.IsPopupOpen)
            {
                PopupService.ReasonList?.Clear();
                var response = await _reasonListBussinessLogic.GetReasonListAsync(reasonEnum.ToString());

                foreach (var reason in response.Reasons)
                {
                    PopupService.ReasonList.Add(new Reasons
                    {
                        Code = reason.Code,
                        Description = reason.Description
                    });
                }

                PopupService.Title = response.ReasonTitle;
                PopupService.MessageItemClicked = reasonSelectCommand;
                PopupService.IsPopupOpen = true;
                PopupService.IsReasonPopupOpen = true;

                PopupService.CloseCommand = new RelayCommand(CloseReasonPopup);
            }
        }


        /// <summary>
        /// Searches the sale for the Search keyword specified
        /// </summary>
        private void SearchBySaleNumber()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                PerformAction(LoadAllSales, SearchTextFieldName);
            }
            else
            {
                PerformAction(LoadSearchResults, SearchTextFieldName);
            }
        }


        private async Task LoadMoreData()
        {
            if (_isSearchInProgress)
            {
                _searchPageIndex++;
                await LoadSearchResults();
            }
            else
            {
                _allSaleListPageIndex++;
                await GetSaleList();
            }
        }

        private async Task LoadAllSales()
        {
            InitializeData();
            _isSearchInProgress = false;
            var startTime = DateTime.Now;
            try
            {
                await GetSaleList();
            }
            catch (Exception)
            {
                Reset();
                throw;
            }
            finally
            {
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Load All Suspended Sales is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }

        private async Task LoadSearchResults()
        {
            var startTime = DateTime.Now;
            try
            {
                var sales = await _saleBussinessLogic.SearchSaleList(_searchPageIndex, string.IsNullOrEmpty(SearchText) ? 0 : Convert.ToInt32(SearchText, CultureInfo.InvariantCulture), _selectedSaleDate);

                // Resetting the Collection if Search is instantiated
                if (_searchPageIndex == 1)
                {
                    Sales.Clear();
                }

                PopulateSales(sales);
            }
            finally
            {
                SearchText = string.Empty;
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Search Suspended Sales  is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }

        private async Task GetSaleList()
        {
            var sales = await _saleBussinessLogic.GetSaleList(_allSaleListPageIndex);
            PopulateSales(sales);
        }

        private void PopulateSales(List<SaleList> sales)
        {
            var tempSales = new ObservableCollection<SaleList>(sales);
            Sales = new ObservableCollection<SaleList>(Sales.Concat(tempSales));
        }

        internal void Reset()
        {
            SearchText = string.Empty;
            _searchPageIndex = 1;
            _allSaleListPageIndex = 1;
            _isSearchInProgress = false;
            SelectedSaleModel = null;
            AllowCorrection = false;
            _selectedSaleDate = string.Empty;
        }
    }
}
