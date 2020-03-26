using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.UI.Model.Sale;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Infonet.CStoreCommander.UI.ViewModel.Sale
{
    public class BottleReturnsScreenVM : VMBase
    {
        private ObservableCollection<BottleModel> _bottles;
        private BottleReturnSaleModel _sale;
        private ProductDataModel _selectedBottle;
        private int _quantity;
        private bool _isCompleteSaleEnabled;
        private string _amountToDisplay;
        private bool _isPrintOn;

        private readonly IStockBussinessLogic _stockBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBussinessLogic;

        public ObservableCollection<BottleModel> Bottles
        {
            get { return _bottles; }
            set
            {
                _bottles = value;
                RaisePropertyChanged(nameof(Bottles));
            }
        }

        public RelayCommand<object> ReduceQuantityCommand { get; private set; }
        public RelayCommand<object> SetQuantityCommand { get; private set; }
        public RelayCommand<object> IncreaseQuantity { get; private set; }
        public RelayCommand<object> AddItemByCodeCommand { get; private set; }
        public RelayCommand<object> UpdateQuantityCommand { get; private set; }
        public RelayCommand CompleteSaleCommand { get; private set; }

        public BottleReturnSaleModel Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                RaisePropertyChanged(nameof(Sale));
            }
        }


        public bool IsPrintOn
        {
            get { return _isPrintOn; }
            set
            {
                _isPrintOn = value;
                RaisePropertyChanged(nameof(IsPrintOn));
            }
        }

        public ProductDataModel SelectedBottle
        {
            get { return _selectedBottle; }
            set
            {
                _selectedBottle = value;
                RaisePropertyChanged(nameof(SelectedBottle));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }

        public bool IsCompleteSaleEnabled
        {
            get { return _isCompleteSaleEnabled; }
            set
            {
                _isCompleteSaleEnabled = value;
                RaisePropertyChanged(nameof(IsCompleteSaleEnabled));
            }
        }

        public string AmountToDisplay
        {
            get { return _amountToDisplay; }
            set
            {
                _amountToDisplay = value;
                RaisePropertyChanged(nameof(AmountToDisplay));
            }
        }

        public BottleReturnsScreenVM(IStockBussinessLogic stockBussinessLogic,
            IReportsBussinessLogic reportsBussinessLogic)
        {
            _stockBusinessLogic = stockBussinessLogic;
            _reportsBussinessLogic = reportsBussinessLogic;
            InitializeCommands();
            ReInitialize();
            MessengerInstance.Register<BottleReturnSaleModel>(this,
                "CompleteBottleReturn",
                CompleteSale);
        }

        private void CompleteSale(BottleReturnSaleModel sale)
        {
            Sale = sale;
            CompleteSale();
            CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
            CacheBusinessLogic.AuthKey = CacheBusinessLogic.PreviousAuthKey;
            MessengerInstance.Send(false, "LoadAllPolicies");
        }

        public void ReInitialize()
        {
            IsPrintOn = false;
            IsCompleteSaleEnabled = false;
            AmountToDisplay = string.Empty;
            Sale = new BottleReturnSaleModel();
            LoadBottlePages();
        }

        private async void LoadBottlePages()
        {
            LoadingService.ShowLoadingStatus(true);
            var bottlePages = await _stockBusinessLogic.GetHotProductPages();

            Bottles = new ObservableCollection<BottleModel>(
                from b in bottlePages
                select new BottleModel
                {
                    PageId = b.PageId,
                    PageName = b.PageName
                });

            foreach (var bottle in Bottles)
            {
                bottle.BottleDetails = await LoadBottles(bottle.PageId);
            }
            LoadingService.ShowLoadingStatus(false);
        }

        private async Task<ObservableCollection<ProductDataModel>> LoadBottles(int pageId)
        {
            var bottles = await _stockBusinessLogic.GetBottles(pageId);

            return new ObservableCollection<ProductDataModel>(
                from b in bottles
                select new ProductDataModel
                {
                    Description = b.Description,
                    DefaultQuantity = b.DefaultQuantity,
                    ImageSource = new BitmapImage
                    {
                        UriSource = Helper.IsValidURI(b.Image) ? new Uri(b.Image) : null
                    },
                    StockCode = b.Product,
                    Price = b.Price
                });
        }

        private void InitializeCommands()
        {
            UpdateQuantityCommand = new RelayCommand<object>((s) => UpdateQuantity(s));
            ReduceQuantityCommand = new RelayCommand<object>(ReduceQuantityOfSelectedItem);
            IncreaseQuantity = new RelayCommand<object>(IncreaseQuantityOfSelectedItem);
            SetQuantityCommand = new RelayCommand<object>(OpenNumericPad);
            AddItemByCodeCommand = new RelayCommand<object>(NavigateService.Instance.NavigateToAddItemByCode);
            CompleteSaleCommand = new RelayCommand(CompleteSale);
        }

        // TODO: Use converter to get the casted value in the XAML
        private void UpdateQuantity(object s)
        {
            if (_selectedBottle != null)
            {
                var quantity = 0;
                int.TryParse(s.ToString(), out quantity);
                _selectedBottle.Quantity = quantity;
                UpdateProductInSale(_selectedBottle);
            }
            NavigateService.Instance.SecondFrameBackNavigation();
        }

        private void CompleteSale()
        {
            PerformAction(async () =>
            {
                var startTime = DateTime.Now;
                try
                {
                    var result = await _stockBusinessLogic.
                    CompleteBottleReturnSale(Sale.ToEntity());

                    WriteToLineDisplay(result.LineDisplay);

                    if (result.OpenCashDrawer)
                    {
                        base.OpenCashDrawer();
                    }

                    if (IsPrintOn)
                    {
                        PerformPrint(result.Receipt);
                    }

                    NavigateService.Instance.NavigateToHome();
                    MessengerInstance.Send(result.Sale.ToModel(), "UpdateSale");
                }
                catch (SwitchUserException ex)
                {
                    ShowNotification(ex.Error.Message,
                        SwitchUserAndCompleteSale,
                        null,
                        ApplicationConstants.ButtonWarningColor);
                }
                finally
                {
                    var endTime = DateTime.Now;
                    _log.Info(string.Format("Time Taken In Bottle Return is {0}ms", (endTime - startTime).TotalMilliseconds));
                }
            });
        }
        
        private void SwitchUserAndCompleteSale()
        {
            CacheBusinessLogic.FramePriorSwitchUserNavigation = "SwitchUserToCompleteSale";
            NavigateService.Instance.NavigateToLogout();
            MessengerInstance.Send(Sale, "SwitchUserAndCompleteBottleReturn");
        }

        private void OpenNumericPad(dynamic s)
        {
            _selectedBottle = SelectedBottle = GetProductById(s);
            NavigateService.Instance.NavigateToBottleReturnNumericKeyPad();
            MessengerInstance.Send(SelectedBottle.Quantity.ToString(), "SetQuantiyUsingNumberPad");
            Quantity = SelectedBottle.Quantity;
        }

        private ProductDataModel GetProductById(string stockCode)
        {
            if (string.IsNullOrEmpty(stockCode))
            {
                return null;
            }

            foreach (var product in Bottles)
            {
                SelectedBottle = product.BottleDetails.FirstOrDefault(x => x.StockCode == stockCode);
                if (SelectedBottle != null)
                {
                    return SelectedBottle;
                }
            }
            return null;
        }

        private void ReduceQuantityOfSelectedItem(dynamic s)
        {
            var selectedProduct = GetProductById(s);
            if (selectedProduct != null)
            {
                selectedProduct.Quantity -= selectedProduct.DefaultQuantity;
                UpdateProductInSale(selectedProduct);
            }
        }

        private void IncreaseQuantityOfSelectedItem(dynamic s)
        {
            var selectedProduct = GetProductById(s);
            if (selectedProduct != null)
            {
                selectedProduct.Quantity += selectedProduct.DefaultQuantity;
                UpdateProductInSale(selectedProduct);
            }
        }

        private void UpdateProductInSale(ProductDataModel selectedProduct)
        {
            var saleLine = Sale.SaleLines
                .FirstOrDefault(x => x.StockCode == selectedProduct.StockCode);

            if (saleLine != null)
            {
                saleLine.Quantity = selectedProduct.Quantity;
                saleLine.Amount = (decimal.Parse(saleLine.Price, NumberStyles.Any, CultureInfo.InvariantCulture) * saleLine.Quantity).ToString();
                if (saleLine.Quantity == 0)
                {
                    Sale.SaleLines.Remove(saleLine);
                }
            }
            else
            {
                Sale.SaleLines.Add(new BottleReturnSaleLineModel
                {
                    LineNumber = Sale.SaleLines.Count > 0 ?
                    Sale.SaleLines.Max(x => x.LineNumber) + 1 : 1,
                    Price = selectedProduct.Price.ToString(CultureInfo.InvariantCulture),
                    StockCode = selectedProduct.StockCode,
                    Quantity = selectedProduct.Quantity,
                    Amount = (selectedProduct.Price * selectedProduct.Quantity).ToString(CultureInfo.InvariantCulture),
                    Description = selectedProduct.Description
                });
            }

            Sale.TotalAmount = Sale.SaleLines.Sum(x => decimal.Parse(x.Amount, NumberStyles.Any, CultureInfo.InvariantCulture));
            AmountToDisplay = Sale.TotalAmount.ToString(CultureInfo.InvariantCulture);

            IsCompleteSaleEnabled = _sale.SaleLines.Count > 0;
        }
    }
}
