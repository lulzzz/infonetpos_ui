using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.UI.Service;

namespace Infonet.CStoreCommander.UI.ViewModel.Customer
{
    /// <summary>
    /// View Model class for Loyalty Customers Screen
    /// </summary>
    public class LoyaltyCustomersScreenVM : CustomersScreenVM
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoyaltyCustomersScreenVM(
            ICustomerBussinessLogic customerBussinessLogic,
            ISaleBussinessLogic saleBusinessLogic) :
            base(customerBussinessLogic, saleBusinessLogic)
        {
            InitializeCommands();
            Loyalty = true;
        }

        #region Commands
        public RelayCommand AddCustomerCommand { get; private set; }
        #endregion

        /// <summary>
        /// Initializes all the commands
        /// </summary>
        private void InitializeCommands()
        {
            AddCustomerCommand = new RelayCommand(NavigateService.Instance.NavigateToAddCustomer);
        }
    }
}
