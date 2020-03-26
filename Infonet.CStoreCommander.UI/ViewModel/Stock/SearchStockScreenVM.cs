using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Infonet.CStoreCommander.UI.ViewModel.Stock
{
    public class SearchStockScreenVM : VMBase
    {
        private StockModel _selectedStockItem;
        private StockModel _previousSelectedStockItem;
        private ObservableCollection<StockModel> _stockItems;
        private string _searchText;
        private bool _isSelectStockEnabled;
        private bool _isSearchInProgress;
        private int _searchPageIndex = 1;
        private int _allStockItemsPageIndex = 1;
        private bool _isAddStockEnabled;
        private Stopwatch _tracker;

        #region Commands
        public RelayCommand AddStockForSaleCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand AddStockCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand StockSelectedCommand { get; private set; }
        #endregion

        private readonly IStockBussinessLogic _stockBussinessLogic;
        private readonly ISaleBussinessLogic _saleBussinessLogic;

        private VerifyStock _verifyStockModel;

        public string SearchTextFieldName { get; set; }

        public bool IsAddStockEnabled
        {
            get { return _isAddStockEnabled; }
            set
            {
                _isAddStockEnabled = value;
                RaisePropertyChanged(nameof(IsAddStockEnabled));
            }
        }

        public StockModel SelectedStockItem
        {
            get
            {
                return _selectedStockItem;
            }
            set
            {
                _selectedStockItem = value;
                IsSelectStockEnabled = _selectedStockItem != null;
                RaisePropertyChanged(nameof(SelectedStockItem));
            }
        }

        public ObservableCollection<StockModel> StockItems
        {
            get { return _stockItems; }
            set
            {
                _stockItems = value;
                RaisePropertyChanged(nameof(StockItems));
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
            }
        }

        public bool IsSelectStockEnabled
        {
            get
            {
                return _isSelectStockEnabled;
            }
            set
            {
                _isSelectStockEnabled = value;
                RaisePropertyChanged(nameof(IsSelectStockEnabled));
            }
        }

        /// <summary>
        /// Constructor for Customer View Model
        /// </summary>
        public SearchStockScreenVM(
            IStockBussinessLogic stockBussinessLogic,
            ISaleBussinessLogic saleBussinessLogic)
        {
            _stockBussinessLogic = stockBussinessLogic;
            _saleBussinessLogic = saleBussinessLogic;
            _tracker = new Stopwatch();
            MessengerInstance.Register<string>(this, "AddStockToSale", AddStockToSale);

            ReInitialize();
        }

        /// <summary>
        /// Message Receiver which adds the specified stock code to the 
        /// Sale
        /// </summary>
        /// <param name="stockCode"></param>
        private void AddStockToSale(string stockCode)
        {
            SelectedStockItem = new StockModel
            {
                StockCode = stockCode
            };

            VerifyAndAddStockForSale();
        }

        /// <summary>
        /// Reinitializes the data of the VM
        /// </summary>
        public void ReInitialize()
        {
            InitializeData();
            InitializeCommands();
            LoadAllStockItems();
        }

        /// <summary>
        /// Method to Initialize required data
        /// </summary>
        private void InitializeData()
        {
            SearchText = string.Empty;
            _isSearchInProgress = false;
            IsAddStockEnabled = CacheBusinessLogic.OperatorCanAddStock;
        }

        /// <summary>
        /// Initializes the Commands
        /// </summary>
        private void InitializeCommands()
        {
            StockItems = new ObservableCollection<StockModel>();
            SearchCommand = new RelayCommand(Search);
            AddStockForSaleCommand = new RelayCommand(VerifyAndAddStockForSale);
            AddStockCommand = new RelayCommand(AddStock);
            RefreshCommand = new RelayCommand(LoadMoreData);
            StockSelectedCommand = new RelayCommand(StockTapped);
        }

        /// <summary>
        /// A stock is tapped from the list of stock items
        /// </summary>
        private void StockTapped()
        {
            if (_selectedStockItem == null)
            {
                return;
            }

            if (_previousSelectedStockItem == _selectedStockItem)
            {
                VerifyAndAddStockForSale();
            }
            _previousSelectedStockItem = _selectedStockItem;
        }

        private void VerifyAndAddStockForSale()
        {
            MessengerInstance.Send(new AddStockToSaleMessage
            {
                StockCode = SelectedStockItem.StockCode,
                Quantity = 1,
                IsManuallyAdded = true
            }, "VerifyAndAddStockForSale");
        }

        private void LoadMoreData()
        {
            if (_isSearchInProgress)
            {
                _searchPageIndex++;
                LoadSearchResults();
            }
            else
            {
                _allStockItemsPageIndex++;
                LoadAllStockItems();
            }
        }

        private void AddStock()
        {
            Reset();
            NavigateService.Instance.NavigateToAddStock();
        }

        /// <summary>
        /// Searches the customer for the Search keyword specified
        /// </summary>
        private void Search()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                _allStockItemsPageIndex = 1;
                LoadAllStockItems();
            }
            else
            {
                _tracker.Restart();
                LoadSearchResults();
            }
        }

        private void LoadSearchResults()
        {
            PerformAction(async () =>
            {
                try
                {
                    _isSearchInProgress = true;
                    var stockItems = await _stockBussinessLogic.SearchStockItems(SearchText,
                        _searchPageIndex);
                    if (stockItems != null && stockItems.Count > 0)
                    {
                        if (_searchPageIndex == 1)
                        {
                            StockItems.Clear();
                        }

                        var tempStockItems = new ObservableCollection<StockModel>();
                        foreach (var stockItem in stockItems)
                        {
                            tempStockItems.Add(stockItem);
                        }

                        StockItems = new ObservableCollection<StockModel>(StockItems.Concat(tempStockItems));
                        SelectedStockItem = StockItems.FirstOrDefault();
                    }
                }
                finally
                {
                    _tracker.Stop();
                    Log.Info(string.Format("Time taken by stock search is {0}ms", _tracker.Elapsed.TotalMilliseconds));
                    SearchText = string.Empty;
                }
            }, SearchTextFieldName);
        }

        /// <summary>
        /// Loads all the Stock items
        /// </summary>
        private void LoadAllStockItems()
        {
            PerformAction(async () =>
            {
                _isSearchInProgress = false;
                var stockItems = await _stockBussinessLogic.GetStockItems(_allStockItemsPageIndex);
                if (stockItems != null && stockItems.Count > 0)
                {
                    if (_allStockItemsPageIndex == 1)
                    {
                        StockItems.Clear();
                    }

                    var tempStockItems = new ObservableCollection<StockModel>();
                    foreach (var stockItem in stockItems)
                    {
                        tempStockItems.Add(stockItem);
                    }

                    StockItems = new ObservableCollection<StockModel>(StockItems.Concat(tempStockItems));
                }
            }, SearchTextFieldName);
        }

        public void Reset()
        {
            _isSearchInProgress = false;
            _searchPageIndex = 1;
        }
    }
}
