using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.HotCategory;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace Infonet.CStoreCommander.UI.ViewModel
{
    public class HotCategoriesScreenVM : VMBase
    {
        private readonly InfonetLog _log = InfonetLogManager.GetLogger<HotCategoriesScreenVM>();
        private readonly IStockBussinessLogic _stockBussinessLogic;
        private int _quantity;
        private ObservableCollection<HotProductModel> _hotCategories;
        private HotProductModel _selectedHotCategory;

        public RelayCommand OpenStockSearchCommand { get; set; }
        public RelayCommand OpenPriceCheckPageCommand { get; set; }
        public RelayCommand<object> ReduceQuantityCommand { get; private set; }
        public RelayCommand<object> SetQuantityCommand { get; private set; }
        public RelayCommand<object> IncreaseQuantity { get; private set; }
        public RelayCommand<object> UpdateQuantityCommand { get; set; }
        public RelayCommand<int> CategoryChangedCommand { get; private set; }
        public RelayCommand OpenReprintPopupCommand { get; set; }

        public ObservableCollection<HotProductModel> HotCategories
        {
            get { return _hotCategories; }
            set
            {
                _hotCategories = value;
                RaisePropertyChanged(nameof(HotCategories));
            }
        }

        public ProductDataModel SelectedProduct { get; set; }
            = new ProductDataModel();

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public HotProductModel SelectedHotCategory
        {
            get
            {
                return _selectedHotCategory;
            }
            set
            {
                if (_selectedHotCategory != value)
                {
                    _selectedHotCategory = value;
                    RaisePropertyChanged(nameof(SelectedHotCategory));
                }
            }
        }

        public HotCategoriesScreenVM(IStockBussinessLogic stockBussinessLogic)
        {
            _stockBussinessLogic = stockBussinessLogic;

            HotCategories = new ObservableCollection<HotProductModel>();
            InitializeCommands();

            LoadHotCategories(null);
            MessengerInstance.Register<ObservableCollection<SaleLineModel>>(this,
                "SyncHotProducts",
                UpdateQuantities);
            MessengerInstance.Register<LoadHotCategoriesMessage>(this, LoadHotCategories);
        }

        private void UpdateQuantities(ObservableCollection<SaleLineModel> saleLines)
        {
            foreach (var x in HotCategories)
            {
                foreach (var hx in x.ProductDetails)
                {
                    hx.Quantity = (int)saleLines.Where(y => y.Code == hx.StockCode).
                        Sum(z => double.Parse(z.Quantity, CultureInfo.InvariantCulture));
                }
            }

            var selectedHotProduct = SelectedHotCategory?.PageId;
            if (selectedHotProduct.HasValue && selectedHotProduct.Value != 0)
            {
                SelectedHotCategory = HotCategories.FirstOrDefault(x => x.PageId == selectedHotProduct);
            }
        }

        private void InitializeCommands()
        {
            OpenPriceCheckPageCommand = new RelayCommand(NavigateService.Instance.NavigateToPriceCheckPage);
            OpenStockSearchCommand = new RelayCommand(NavigateService.Instance.NavigateToStockSearch);
            ReduceQuantityCommand = new RelayCommand<object>(ReduceQuantityOfSelectedItem);
            IncreaseQuantity = new RelayCommand<object>(IncreaseQuantityOfSelectedItem);
            SetQuantityCommand = new RelayCommand<object>(OpenNumericPad);
            UpdateQuantityCommand = new RelayCommand<object>(UpdateQuantity);
            CategoryChangedCommand = new RelayCommand<int>(CategoryChanged);
            OpenReprintPopupCommand = new RelayCommand(OpenReprintPopup);
        }

        private void OpenReprintPopup()
        {
            ShowConfirmationMessage(ApplicationConstants.Reprint,
                NavigateService.Instance.NavigateToReprint,
                ReprintLastReport, null, ApplicationConstants.ButtonFooterColor,
                ApplicationConstants.ButtonFooterColor, ApplicationConstants.ReprintText,
                ApplicationConstants.LastPrint, true,
                Helper.IfFileExists(CacheBusinessLogic.LastPrintReport));
        }

        private void ReprintLastReport()
        {
            var lastPrintReport = CacheBusinessLogic.LastPrintReport;

            if (!string.IsNullOrEmpty(CacheBusinessLogic.LastPrintReport))
            {
                NavigateService.Instance.NavigateToLastPrint();
            }
        }


        private void UpdateQuantity(object obj)
        {
            var quantity = Convert.ToInt32(obj, CultureInfo.InvariantCulture);
            MessengerInstance.Send(new AddStockToSaleMessage
            {
                StockCode = SelectedProduct.StockCode,
                Quantity = (quantity - SelectedProduct.Quantity),
                IsManuallyAdded = false
            }, "VerifyAndAddStockForSale");
        }

        private void CategoryChanged(int pageId)
        {
            SelectedHotCategory = HotCategories.FirstOrDefault(x => x.PageId == pageId);
            if (SelectedHotCategory?.ProductDetails == null || SelectedHotCategory?.ProductDetails.Count == 0)
            {
                LoadHotProducts(pageId);
            }
        }

        private async void LoadHotCategories(LoadHotCategoriesMessage message)
        {
            LoadingService.ShowLoadingStatus(true);
            try
            {
                var hotProductPages = await _stockBussinessLogic.GetHotProductPages();

                var categories = new ObservableCollection<HotProductModel>(
                    from h in hotProductPages
                    select new HotProductModel
                    {
                        PageId = h.PageId,
                        PageName = h.PageName
                    });

                HotCategories = new ObservableCollection<HotProductModel>(categories);

                // Load for First Category
                if (HotCategories.Count > 0)
                {
                    LoadHotProducts(HotCategories.FirstOrDefault().PageId);
                }
            }
            catch (UserNotAuthorizedException)
            {
                NavigateService.Instance.NavigateToLogin();
            }
            finally
            {
                OperationsCompletedInLogin++;
                if (IsSwitchUserStarted)
                {
                    OperationsCompletedInSwitchUser++;
                }
                LoadingService.ShowLoadingStatus(false);
            }
        }

        private async void LoadHotProducts(int pageId)
        {
            try
            {
                var hotProducts = await _stockBussinessLogic.GetHotProducts(pageId);

                var productDetails = new ObservableCollection<ProductDataModel>();

                foreach (var product in hotProducts)
                {
                    var productDetail = new ProductDataModel
                    {
                        DefaultQuantity = Convert.ToInt32(product.DefaultQuantity, CultureInfo.InvariantCulture),
                        Description = product.Description,
                        ImageSource = new BitmapImage
                        {
                            UriSource = Helper.IsValidURI(product.Image) ? new Uri(product.Image) :
                            null
                        },
                        StockCode = product.StockCode
                    };

                    productDetails.Add(productDetail);
                }

                HotCategories.FirstOrDefault(x => x.PageId == pageId).ProductDetails = productDetails;

                MessengerInstance.Send(new RequestForSyncHotProductsMessage());
            }
            finally
            {
                OperationsCompletedInLogin++;
                if (IsSwitchUserStarted)
                {
                    OperationsCompletedInSwitchUser++;
                }
            }
        }

        private void ReduceQuantityOfSelectedItem(dynamic s)
        {
            if (s != "0")
            {
                var selectedProduct = GetProductById(s);
                MessengerInstance.Send(new AddStockToSaleMessage
                {
                    StockCode = selectedProduct.StockCode,
                    Quantity = -selectedProduct.DefaultQuantity,
                    IsManuallyAdded = false
                }, "VerifyAndAddStockForSale");
            }
        }

        // TODO: Remove Dynamic keyword and use converter in XAML to get data through Models
        private void IncreaseQuantityOfSelectedItem(dynamic s)
        {
            if (s != "0")
            {
                var selectedProduct = GetProductById(s);
                MessengerInstance.Send(new AddStockToSaleMessage
                {
                    StockCode = selectedProduct.StockCode,
                    Quantity = selectedProduct.DefaultQuantity,
                    IsManuallyAdded = false
                }, "VerifyAndAddStockForSale");
            }
        }

        private void OpenNumericPad(dynamic s)
        {
            SelectedProduct = GetProductById(s);
            NavigateService.Instance.NavigateToNumericKeyPad();
            MessengerInstance.Send(SelectedProduct.Quantity.ToString(), "SetQuantiyUsingNumberPad");
            Quantity = SelectedProduct.Quantity;
        }

        private ProductDataModel GetProductById(string stockCode)
        {
            foreach (var product in HotCategories)
            {
                SelectedProduct = product.ProductDetails.FirstOrDefault(x => x.StockCode == stockCode);
                if (SelectedProduct != null)
                {
                    break;
                }
            }
            return SelectedProduct;
        }
    }
}
