using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payment;
using Infonet.CStoreCommander.UI.Model.Payment;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Payment
{
    public class ARVM : VMBase
    {
        #region Private variables
        private bool _isSearchInProgress;
        private bool _isCompleteButtonEnabled;
        private bool _isSelectedCustomerNotEmpty;
        private int _searchPageIndex = 1;
        private int _allCustomersPageIndex = 1;
        private string _searchText;
        private string _enteredAmount;
        private string _cardNumber;

        private ObservableCollection<ARCustomerModel> _customers;
        private ARCustomerModel _selectedARCustomer;

        private readonly IPaymentBussinessLogic _paymentBussinessLogic;
        #endregion

        public bool IsSelectedCustomerNotEmpty
        {
            get { return _isSelectedCustomerNotEmpty; }
            set
            {
                _isSelectedCustomerNotEmpty = value;
                RaisePropertyChanged(nameof(IsSelectedCustomerNotEmpty));
            }
        }

        public bool IsCompleteButtonEnabled
        {
            get { return _isCompleteButtonEnabled; }
            set
            {
                _isCompleteButtonEnabled = value;
                RaisePropertyChanged(nameof(IsCompleteButtonEnabled));
            }
        }

        public string EnteredAmount
        {
            get { return _enteredAmount; }
            set
            {
                _enteredAmount = Helper.SelectAllDecimalValue(value, _enteredAmount);
                RaisePropertyChanged(nameof(EnteredAmount));
            }
        }

        public ARCustomerModel SelectedARCustomer
        {
            get { return _selectedARCustomer; }
            set
            {
                _selectedARCustomer = value;
                IsSelectedCustomerNotEmpty = SelectedARCustomer != null;
                IsCompleteButtonEnabled = IsSelectedCustomerNotEmpty;
                RaisePropertyChanged(nameof(SelectedARCustomer));
            }
        }

        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                _cardNumber = value;
                PerformAction(async () =>
                {
                    await SearchCustomerByCode(_cardNumber);
                });
            }
        }

        public ObservableCollection<ARCustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged(nameof(Customers));
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
            }
        }

        public RelayCommand LoadAllARCustomer { get; set; }
        public RelayCommand<object> SearchCommand { get; private set; }
        public RelayCommand SearchCustomerCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand CompletePaymentCommand { get; private set; }
        public RelayCommand<object> AmountEnteredCommand { get; private set; }

        public ARVM(IPaymentBussinessLogic paymentBussinessLogic)
        {
            _paymentBussinessLogic = paymentBussinessLogic;
            Customers = new ObservableCollection<ARCustomerModel>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            RefreshCommand = new RelayCommand(LoadMoreData);
            SearchCommand = new RelayCommand<object>(Search);
            SearchCustomerCommand = new RelayCommand(Search);
            LoadAllARCustomer = new RelayCommand(() => PerformAction(GetAllCustomers));
            CompletePaymentCommand = new RelayCommand(() => PerformAction(CompletePayment));
            AmountEnteredCommand = new RelayCommand<object>((args) => AmountEntered(args));
        }

        private void AmountEntered(object args)
        {
            if (Helper.IsEnterKey(args) && IsCompleteButtonEnabled)
            {
                PerformAction(CompletePayment);
            }
        }

        private async Task CompletePayment()
        {
            var timer = new Stopwatch();
            timer.Restart();

            try
            {
                var response = await _paymentBussinessLogic.SaveARPayment(SelectedARCustomer.Code,
                   EnteredAmount);
                NavigateService.Instance.NavigateToSaleSummary();
                MessengerInstance.Send<CheckoutSummary>(response, "PaymentByAR");
            }
            catch (Exception)
            {
                SelectedARCustomer = null;
                throw;
            }
            finally
            {
                timer.Stop();
                Log.Info(string.Format("Time taken in save AR Payment is {0}ms ", timer.ElapsedMilliseconds));
            }
        }

        private async Task SearchCustomerByCode(string cardNumber)
        {
            var response = await _paymentBussinessLogic.GetARCustomerByCustomerCode(cardNumber);
            Customers.Clear();
            MapCustomerWithUIModel(new List<ARCustomer> { response });
            SelectedARCustomer = Customers.FirstOrDefault();
        }

        private void LoadMoreData()
        {
            // Check whether to load search results or load all results
            if (_isSearchInProgress)
            {
                _searchPageIndex++;
                PerformAction(LoadSearchResults);
            }
            else
            {
                _allCustomersPageIndex++;
                PerformAction(LoadAllCustomers);
            }
        }

        private void Search(object args)
        {
            if (Helper.IsEnterKey(args))
            {
                Search();
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                _allCustomersPageIndex = 1;
                PerformAction(LoadAllCustomers);
            }
            else
            {
                PerformAction(LoadSearchResults);
            }
        }

        private async Task LoadAllCustomers()
        {
            _isSearchInProgress = false;
            await GetAllCustomers();
        }

        private async Task LoadSearchResults()
        {
            _isSearchInProgress = true;
            var customers = await _paymentBussinessLogic.SearchARCustomer(
                SearchText, _searchPageIndex);

            if (customers?.Count > 0)
            {
                // Resetting the Collection if Search is instantiated again
                if (_searchPageIndex == 1)
                {
                    Customers.Clear();
                }
            }

            MapCustomerWithUIModel(customers);
            SelectedARCustomer = Customers?.Count > 0 ? Customers.FirstOrDefault() : null;
        }

        private async Task GetAllCustomers()
        {
            var response = await _paymentBussinessLogic.GetAllARCustomer(_allCustomersPageIndex);
            if (response != null && response.Count > 0)
            {
                // Resetting the Collection if Search is instantiated again
                if (_allCustomersPageIndex == 1)
                {
                    Customers.Clear();
                }
                MapCustomerWithUIModel(response);
            }
        }

        private void MapCustomerWithUIModel(List<ARCustomer> customers)
        {
            var tempCustomerList = new List<ARCustomerModel>();
            foreach (var customer in customers)
            {
                tempCustomerList.Add(new ARCustomerModel
                {
                    Balance = customer.Balance,
                    Code = customer.Code,
                    CreditLimit = customer.CreditLimit,
                    Name = customer.Name,
                    Phone = customer.Phone
                });
            }

            Customers = new ObservableCollection<ARCustomerModel>(Customers.Concat(tempCustomerList));
        }

        public void ReInitialize()
        {
            _isSearchInProgress = false;
            _allCustomersPageIndex = 1;
            _searchPageIndex = 1;
            SearchText = string.Empty;
            EnteredAmount = string.Empty;
            Customers.Clear();
            EnteredAmount = string.Empty;
            SelectedARCustomer = null;
        }
    }
}
