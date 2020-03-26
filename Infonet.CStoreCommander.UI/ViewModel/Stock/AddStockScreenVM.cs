using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Model.Stock;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Stock
{
    /// <summary>
    /// View model for Add stock screen
    /// </summary>
    public class AddStockScreenVM : VMBase
    {
        private bool _isNotReceivedFromAddSale;

        private readonly IStockBussinessLogic _stockBussinessLogic;

        private string _stockCode;
        private string _description;
        private string _regularPrice;
        private bool _isAddButtonEnabled;

        public bool IsAddButtonEnabled
        {
            get { return _isAddButtonEnabled; }
            set
            {
                _isAddButtonEnabled = value;
                RaisePropertyChanged(nameof(IsAddButtonEnabled));
            }
        }


        public string RegularPrice
        {
            get { return _regularPrice; }
            set
            {
                _regularPrice = Helper.SelectDecimalValue(value, _regularPrice);
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                     !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(RegularPrice));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                    !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                IsAddButtonEnabled = !string.IsNullOrEmpty(_stockCode) &&
                    !string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_regularPrice);
                RaisePropertyChanged(nameof(StockCode));
            }
        }

        public ObservableCollection<TaxCodes> TaxCodes { get; set; }
            = new ObservableCollection<TaxCodes>();

        public RelayCommand AddStockCommand { get; set; }
        public RelayCommand<object> RegularPriceCompletedCommand;
        public RelayCommand BackCommand { get; set; }

        public bool IsNotReceivedFromAddSale
        {
            get { return _isNotReceivedFromAddSale; }
            set
            {
                _isNotReceivedFromAddSale = value;
                RaisePropertyChanged(nameof(IsNotReceivedFromAddSale));
            }
        }

        /// <summary>
        /// Constructor for Add stock screen
        /// </summary>
        public AddStockScreenVM(IStockBussinessLogic stockBussinessLogic)
        {
            _stockBussinessLogic = stockBussinessLogic;

            MessengerInstance.Register<string>(this, "AddSale", AddStockForSale);
        }

        /// <summary>
        /// Reinitializes the data for the VM
        /// </summary>
        public void ReInitialize()
        {
            PerformAction(GetAllTaxes);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddStockCommand = new RelayCommand(() => PerformAction(AddStock));
            RegularPriceCompletedCommand = new RelayCommand<object>(RegularPriceCompleted);
            BackCommand = new RelayCommand(()=>
            {
                if(!IsNotReceivedFromAddSale)
                {
                    SoundService.Instance.PlaySoundFile(SoundTypes.stocknotFound);
                }
                NavigateService.Instance.NavigateToHome();
                MessengerInstance.Send(new SetFocusOnGridMessage());
            });

            StockCode = string.Empty;
            Description = string.Empty;
            RegularPrice = string.Empty;
            IsNotReceivedFromAddSale = true;
        }

        /// <summary>
        /// This method sets stock code received from add Sale line API and 
        /// Pre-populates the stock code
        /// </summary>
        /// <param name="stockCode">Stock code returned from the API</param>
        private void AddStockForSale(string stockCode)
        {
            StockCode = stockCode;
            IsNotReceivedFromAddSale = false;
        }
        /// <summary>
        /// Method to add stock on press of enter key on regular price field
        /// </summary>
        /// <param name="args"></param>
        private void RegularPriceCompleted(object args)
        {
            if (Helper.IsEnterKey(args))
            {
                MessengerInstance.Send(new CloseKeyboardMessage());
            }
        }
        /// <summary>
        /// Method to add stock
        /// </summary>
        /// <returns></returns>
        private async Task AddStock()
        {
            decimal regularPrice;
            var stock = new EntityLayer.Entities.Stock.StockModel
            {
                Description = Description,
                RegularPrice = decimal.TryParse(RegularPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out regularPrice) ? regularPrice : 0.00M,
                StockCode = StockCode
            };

            stock.TaxCodes.TaxCode.AddRange
                (TaxCodes.Where(x => x.IsChecked == true).Select(x => x.TaxCode).ToList());

            if (await _stockBussinessLogic.AddProductAsync(stock))
            {
                if (IsNotReceivedFromAddSale)
                {
                    NavigateService.Instance.NavigateToHome();
                }
                else
                {
                    MessengerInstance.Send(new AddStockToSaleMessage
                    {
                        StockCode = stock.StockCode,
                        Quantity = 1,
                        IsManuallyAdded = true
                    }, "VerifyAndAddStockForSale");
                }
            }
        }

        /// <summary>
        /// Method to get all taxes
        /// </summary>
        /// <returns></returns>
        private async Task GetAllTaxes()
        {
            TaxCodes = new ObservableCollection<TaxCodes>();

            var taxCodes = await _stockBussinessLogic.GetAllTaxesAsync();
            foreach (var taxcode in taxCodes.TaxCode)
            {
                TaxCodes.Add(new TaxCodes
                {
                    TaxCode = taxcode,
                    IsChecked = false
                });
            }
        }
    }
}
