using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using EntityCustomer = Infonet.CStoreCommander.EntityLayer.Entities.Customer.Customer;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Threading.Tasks;
using CustomerUIModel = Infonet.CStoreCommander.UI.Model.Customer.CustomerModel;

namespace Infonet.CStoreCommander.UI.ViewModel.Customer
{
    /// <summary>
    /// View model for Add Customer Screen
    /// </summary>
    public class AddCustomerScreenVM : VMBase
    {
        private bool _isAddingLoyaltyCustomer;

        private readonly ICustomerBussinessLogic _customerBussinessLogic;

        public CustomerUIModel Customer { get; set; }
        = new CustomerUIModel();

        public bool IsAddingLoyaltyCustomer
        {
            get { return _isAddingLoyaltyCustomer; }
            set
            {
                _isAddingLoyaltyCustomer = value;
                RaisePropertyChanged(nameof(IsAddingLoyaltyCustomer));
            }
        }

        #region Commands
        public RelayCommand SaveCustomerCommand { get; private set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public AddCustomerScreenVM(ICustomerBussinessLogic customerBussinessLogic)
        {
            _customerBussinessLogic = customerBussinessLogic;
            InitializeCommands();
            RegisterMessages();
        }

        /// <summary>
        /// Registers all the messages listened by the View Model
        /// </summary>
        private void RegisterMessages()
        {
            MessengerInstance.Register<CustomerUIModel>(this,
                "AddLoyalityNumberForCustomer", SetLoyaltyCustomer);
        }

        /// <summary>
        /// Initializes commands
        /// </summary>
        private void InitializeCommands()
        {
            SaveCustomerCommand = new RelayCommand(() => PerformAction(SaveCustomer));
        }

        /// <summary>
        /// Sets an existing customer as loyalty
        /// </summary>
        /// <param name="customer"></param>
        private void SetLoyaltyCustomer(CustomerUIModel customer)
        {
            Customer.Name = customer?.Name;
            Customer.PhoneNumber = customer?.PhoneNumber;
            Customer.Code = customer?.Code;

            IsAddingLoyaltyCustomer = false;
        }

        /// <summary>
        /// Adds customer if entered is pressed on phone number field
        /// </summary>
        /// <param name="args"></param>
        private void AddCustomer(dynamic args)
        {
            //TODO: Should create a command which will only raise on Enter click
            if (Helper.IsEnterKey(args))
            {
                PerformAction(SaveCustomer);
            }
        }

        /// <summary>
        /// Method to add new Customer 
        /// </summary>
        private async Task SaveCustomer()
        {
            var startTime = DateTime.Now;
            try
            {
                var customer = new EntityLayer.Entities.Customer.Customer
                {
                    LoyaltyNumber = Customer.LoyaltyNumber,
                    Code = Customer.Code,
                    Name = Customer.Name,
                    PhoneNumber = Customer.PhoneNumber
                };

                // Check whether we are setting Customer as loyalty for sale or adding a new one
                if (!IsAddingLoyaltyCustomer)
                {
                    await _customerBussinessLogic.SetLoyaltyNumber(
                        Customer.Code, Customer.LoyaltyNumber);
                    MessengerInstance.Send(true, "SetCustomerAsLoyalityCustomer");
                }
                else if (await _customerBussinessLogic.Add(customer))
                {
                    SelectCustomerForSale(customer);
                }
            }
            finally
            {
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Add Customer is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }
        
        /// <summary>
        /// Selects the highlighted customer for the current sale
        /// </summary>
        private void SelectCustomerForSale(EntityCustomer customer)
        {
            PerformAction(async () =>
            {
                var result = await _customerBussinessLogic.SetCustomerForSale(customer.Code);
                var sale = result.ToModel();
                NavigateService.Instance.NavigateToHome();
                MessengerInstance.Send(sale, "UpdateSale");
            });
        }

        /// <summary>
        /// Reinitializes the View model data
        /// </summary>
        internal void ReInitialize()
        {
            IsAddingLoyaltyCustomer = true;
            Customer.LoyaltyNumber = Customer.Name = Customer.Code =
                Customer.PhoneNumber = string.Empty;
        }
    }
}
