using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Model.Customer;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.UI.ViewModel.Customer
{
    /// <summary>
    /// View Model class for Customers Screen
    /// </summary>
    public class CustomersScreenVM : VMBase
    {
        private const string _cashSale = "Cash Sale";

        private EntityLayer.Entities.Customer.Customer _selectedCustomer;
        private EntityLayer.Entities.Customer.Customer _previousSelectedCustomer;
        private ObservableCollection<EntityLayer.Entities.Customer.Customer> _customers;

        private string _searchText;
        private bool _isSelectCustomerEnabled;
        private bool _isSearchInProgress;
        private int _searchPageIndex = 1;
        private int _allCustomersPageIndex = 1;
        private string _cardNumber;
        private bool _isTaxEcemptionVisible;
        private string _taxExemptionNumber;

        public string TaxExemptionNumber
        {
            get { return _taxExemptionNumber; }
            set
            {
                _taxExemptionNumber = value;
                RaisePropertyChanged(nameof(TaxExemptionNumber));
            }
        }

        public bool IsTaxExemptionVisible
        {
            get { return _isTaxEcemptionVisible; }
            set
            {
                _isTaxEcemptionVisible = value;
                RaisePropertyChanged(nameof(IsTaxExemptionVisible));
            }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                PerformAction(async () =>
                {
                    await SearchCustomerByCode(new Helper().GetTrack2Data(_cardNumber));
                });
            }
        }

        private readonly ICustomerBussinessLogic _customerBusinessLogic;
        private readonly ISaleBussinessLogic _saleBusinessLogic;

        public string SearchTextFieldName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomersScreenVM(
            ICustomerBussinessLogic customerBussinessLogic,
            ISaleBussinessLogic saleBusinessLogic)
        {
            _customerBusinessLogic = customerBussinessLogic;
            _saleBusinessLogic = saleBusinessLogic;
            _isSearchInProgress = true;

            InitializeCommands();
            RegisterMessages();
        }

        #region Commands
        public RelayCommand SelectCustomerForSaleCommand { get; private set; }
        public RelayCommand SelectCashCustomerForSaleCommand { get; private set; }
        public RelayCommand<object> SearchCommand { get; private set; }
        public RelayCommand LoadAllCustomersCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand CustomerSelectedCommand { get; private set; }
        public RelayCommand OpenTaxExemptionPopupCommand { get; private set; }
        public RelayCommand<object> EnterPressedOnTaxExemptionNumberCommand { get; private set; }
        public RelayCommand SetTaxExemptionCommand { get; private set; }

        #endregion

        protected bool Loyalty { get; set; }

        public EntityLayer.Entities.Customer.Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                IsSelectCustomerEnabled = _selectedCustomer != null;

                RaisePropertyChanged(nameof(SelectedCustomer));
            }
        }

        public ObservableCollection<EntityLayer.Entities.Customer.Customer> Customers
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

        public bool IsSelectCustomerEnabled
        {
            get { return _isSelectCustomerEnabled; }
            set
            {
                _isSelectCustomerEnabled = value;
                RaisePropertyChanged(nameof(IsSelectCustomerEnabled));
            }
        }

        /// <summary>
        /// Registers all the messages listened by View model
        /// </summary>
        private void RegisterMessages()
        {
            MessengerInstance.Register<bool>(this,
    "SetCustomerAsLoyalityCustomer", SelectLoyaltyCustomerForSale);
        }

        /// <summary>
        /// Initializes the Commands
        /// </summary>
        private void InitializeCommands()
        {
            Customers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>();
            SearchCommand = new RelayCommand<object>(Search);
            SelectCustomerForSaleCommand = new RelayCommand(ChecksForSelectCustomerForSale);
            SelectCashCustomerForSaleCommand = new RelayCommand(SelectCashCustomerForSale);
            LoadAllCustomersCommand = new RelayCommand(LoadAllCustomers);
            RefreshCommand = new RelayCommand(LoadMoreData);
            CustomerSelectedCommand = new RelayCommand(CustomerTapped);
            OpenTaxExemptionPopupCommand = new RelayCommand(OpenTaxExemptionPopup);
            EnterPressedOnTaxExemptionNumberCommand = new RelayCommand<object>(EnterPressedOnTaxExemptionNumber);
            SetTaxExemptionCommand = new RelayCommand(SetTaxExemption);
        }

        private void SetTaxExemption()
        {
            PerformAction(async () =>
            {
                CloseTaxExemptionPopup();
                var saleResponse = await _saleBusinessLogic.SetTaxExemption(TaxExemptionNumber);
                MessengerInstance.Send(saleResponse.ToModel(), "UpdateSale");
                NavigateService.Instance.NavigateToHome();
            });
        }

        private void EnterPressedOnTaxExemptionNumber(dynamic args)
        {
            if (Helper.IsEnterKey(args))
            {
                SetTaxExemption();
            }
        }

        private void OpenTaxExemptionPopup()
        {
            if (!PopupService.PopupInstance.IsPopupOpen)
            {
                PopupService.PopupInstance.IsTaxExemptionPopupOpen = true;
                PopupService.PopupInstance.IsPopupOpen = true;

                PopupService.PopupInstance.Title = ApplicationConstants.TaxExemptionNumber;

                TaxExemptionNumber = string.Empty;

                PopupService.PopupInstance.CloseCommand = new RelayCommand(() =>
                {
                    CloseTaxExemptionPopup();
                });
            }
        }

        private void CloseTaxExemptionPopup()
        {
            PopupService.PopupInstance.IsTaxExemptionPopupOpen = false;
            PopupService.PopupInstance.IsPopupOpen = false;
        }

        /// <summary>
        /// A customer is tapped from the list of customers
        /// </summary>
        private void CustomerTapped()
        {
            if (_selectedCustomer == null)
            {
                return;
            }

            if (_previousSelectedCustomer == _selectedCustomer)
            {
                ChecksForSelectCustomerForSale();
            }
            _previousSelectedCustomer = _selectedCustomer;
        }

        /// <summary>
        /// Selects loyalty customer for ongoing sale
        /// </summary>
        /// <param name="isCustomerLoyaltySet">Is loyalty customer set or not</param>
        private void SelectLoyaltyCustomerForSale(bool isCustomerLoyaltySet)
        {
            if (isCustomerLoyaltySet)
            {
                SelectCustomerForSale();
            }
        }

        private async Task SearchCustomerByCode(string cardNumber)
        {
            var response = await _customerBusinessLogic.GetCustomerByCard(cardNumber, Loyalty);
            if (response != null)
            {
                SelectedCustomer = response;
                ChecksForSelectCustomerForSale();
            }
            else
            {
                Customers.Clear();
                var tempCustomers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(new List<EntityLayer.Entities.Customer.Customer> { response });
                Customers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(Customers.Concat(tempCustomers));
            }
        }

        /// <summary>
        /// Selects Cash sale customer for the ongoing sale
        /// </summary>
        private void SelectCashCustomerForSale()
        {
            SelectedCustomer = new EntityLayer.Entities.Customer.Customer
            {
                Code = _cashSale
            };
            SelectCustomerForSale();
        }

        /// <summary>
        /// Loads more data from the API if user scrolls to the bottom of the Grid
        /// </summary>
        private async void LoadMoreData()
        {
            // Check whether to load search results or load all results
            if (_isSearchInProgress)
            {
                _searchPageIndex++;
                await LoadSearchResults();
            }
            else
            {
                _allCustomersPageIndex++;
                await LoadAllCustomersResults();
            }
        }

        private void ChecksForSelectCustomerForSale()
        {
            if (Loyalty && string.IsNullOrEmpty(SelectedCustomer.LoyaltyNumber))
            {
                var customerUIModel = new CustomerModel
                {
                    Code = SelectedCustomer.Code,
                    Name = SelectedCustomer.Name,
                    PhoneNumber = SelectedCustomer.PhoneNumber
                };

                ShowConfirmationMessage(ApplicationConstants.LoyalityNumberConfirmation,
                   () =>
                   {
                       NavigateService.Instance.NavigateToAddCustomer();
                       MessengerInstance.Send<CustomerModel>(customerUIModel, "AddLoyalityNumberForCustomer");
                   },
                   SelectCustomerForSale, SelectCustomerForSale);
            }
            else
            {
                SelectCustomerForSale();
            }
        }

        /// <summary>
        /// Selects the highlighted customer for the current sale
        /// </summary>
        private void SelectCustomerForSale()
        {
            PerformAction(async () =>
            {
                if (SelectedCustomer != null)
                {
                    var result = await _customerBusinessLogic.SetCustomerForSale(SelectedCustomer.Code);
                    var sale = result.ToModel();
                    NavigateService.Instance.NavigateToHome();
                    MessengerInstance.Send(sale, "UpdateSale");
                }
            });
        }

        /// <summary>
        /// Searches the customer for the Search keyword specified
        /// </summary>
        private async void Search(dynamic args)
        {
            if (!Helper.IsEnterKey(args))
            {
                return;
            }

            // Eliminating track1 over here
            if (Helper.IsEnterKey(args) && SearchText.IndexOf('?') != -1 &&
                SearchText.IndexOf('?') == SearchText.LastIndexOf('?'))
            {
                return;
            }

            if (string.IsNullOrEmpty(SearchText))
            {
                _allCustomersPageIndex = 1;
                LoadAllCustomers();
            }
            else
            {
                LoadSearchResults();
            }
        }

        private async Task LoadSearchResults()
        {
            PerformAction(async () =>
            {
                try
                {
                    _isSearchInProgress = true;
                    SearchText = new Helper().GetTrack2Data(SearchText);
                    var response = await _customerBusinessLogic.Search(
                        SearchText, _searchPageIndex, Loyalty);
                    try
                    {
                        if (response != null && response.GetType() == typeof(List<EntityLayer.Entities.Customer.Customer>))
                        {
                            var customers = (List<EntityLayer.Entities.Customer.Customer>)response;
                            if (customers != null && customers.Count > 0)
                            {
                                // Resetting the Collection if Search is instantiated again
                                if (_searchPageIndex == 1)
                                {
                                    Customers.Clear();
                                }
                                var tempCustomers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(customers);
                                Customers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(Customers.Concat(tempCustomers));
                                SelectedCustomer = Customers.FirstOrDefault();
                            }
                        }
                        else if (response != null && response.GetType() == typeof(EntityLayer.Entities.Sale.Sale))
                        {
                            var sale = ((EntityLayer.Entities.Sale.Sale)response).ToModel();
                            NavigateService.Instance.NavigateToHome();
                            MessengerInstance.Send(sale, "UpdateSale");
                        }
                    }
                    catch
                    {
                        SearchText = string.Empty;
                    }
                }
                catch (Exception)
                {
                    SearchText = string.Empty;
                    throw;
                }
            }, SearchTextFieldName);
        }

        /// <summary>
        /// Loads all the customers
        /// </summary>
        private async void LoadAllCustomers()
        {
            _isSearchInProgress = false;

            await LoadAllCustomersResults();
        }

        private async Task LoadAllCustomersResults()
        {
            PerformAction(async () =>
            {
                var customers = await _customerBusinessLogic.GetAll(
                    _allCustomersPageIndex, Loyalty);
                if (customers != null && customers.Count > 0)
                {
                    // Resetting the Collection if Search is instantiated again
                    if (_allCustomersPageIndex == 1)
                    {
                        Customers.Clear();
                    }
                    var tempCustomers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(customers);
                    Customers = new ObservableCollection<EntityLayer.Entities.Customer.Customer>(Customers.Concat(tempCustomers));
                }
            }, SearchTextFieldName);
        }

        public void ReInitialize()
        {
            _isSearchInProgress = true;
            _allCustomersPageIndex = 1;
            _searchPageIndex = 1;
            TaxExemptionNumber = SearchText = string.Empty;
            SelectedCustomer = null;
            Customers.Clear();
            IsTaxExemptionVisible = CacheBusinessLogic.TaxExemption;
        }
    }
}
