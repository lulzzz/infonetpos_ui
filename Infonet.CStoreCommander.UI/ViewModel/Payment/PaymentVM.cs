using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.ViewModel.Payment
{
    public class PaymentVM : VMBase
    {
        private string _selectedTab;
        private bool _isPayoutEnable;
        private bool _isArPaymentEnable;

        public bool IsARPaymentEnable
        {
            get { return _isArPaymentEnable; }
            set
            {
                _isArPaymentEnable = value;
                RaisePropertyChanged(nameof(IsARPaymentEnable));
            }
        }


        public bool IsPayoutEnable
        {
            get { return _isPayoutEnable; }
            set
            {
                _isPayoutEnable = value;
                RaisePropertyChanged(nameof(IsPayoutEnable));
            }
        }


        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                RaisePropertyChanged(nameof(SelectedTab));
            }
        }


        public RelayCommand ARTabSelected;
        public RelayCommand FleetTabSelected;
        public RelayCommand PayoutsTabSelected;

        public PaymentVM()
        {

            InitializeCommands();
            MessengerInstance.Register<SelectFleetTabMessage>(this,
                 "SelectPaymentByFleetTab", SelectFleetTab);
        }

        private void SelectFleetTab(SelectFleetTabMessage obj)
        {
            NavigateService.Instance.NavigateToFleet();
            SelectedTab = PaymentTabs.Fleet.ToString();
        }

        private void InitializeCommands()
        {
            ARTabSelected = new RelayCommand(OpenARTab);
            PayoutsTabSelected = new RelayCommand(PayoutsTab);
            FleetTabSelected = new RelayCommand(FleetTab);
        }

        internal void ResetVM()
        {
            IsARPaymentEnable = CacheBusinessLogic.OperatorCanUseCustomer;

            if (IsARPaymentEnable)
            {
                SelectedTab = PaymentTabs.AR.ToString();
                NavigateService.Instance.NavigateToAR();
            }
            else
            {
                FleetTab();
            }

            IsPayoutEnable = CacheBusinessLogic.AllowPayout;
        }

        private void FleetTab()
        {
            SelectedTab = PaymentTabs.Fleet.ToString();
            NavigateService.Instance.NavigateToFleet();
        }

        private void PayoutsTab()
        {
            SelectedTab = PaymentTabs.Payouts.ToString();
            NavigateService.Instance.NavigateToPayout();
        }

        private void OpenARTab()
        {
            SelectedTab = PaymentTabs.AR.ToString();
            NavigateService.Instance.NavigateToAR();
        }
    }
}
