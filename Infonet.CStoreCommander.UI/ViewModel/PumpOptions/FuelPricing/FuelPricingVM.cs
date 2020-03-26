using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Messages;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;

namespace Infonet.CStoreCommander.UI.ViewModel.FuelPricing
{
    public class FuelPricingVM : VMBase
    {
        private string _selectedTab;

        public string SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    ChangeToSelectePage();
                    RaisePropertyChanged(nameof(SelectedTab));
                }
            }
        }

        public bool IsPriceIncrementEnabled => CacheBusinessLogic.IsPriceIncrementEnabled;
        public bool IsTaxExemptionPricesEnabled => CacheBusinessLogic.IsTaxExemptionPricesEnabled;
        public bool IsFuelPriceDisplayUsed => CacheBusinessLogic.IsFuelPriceDisplayUsed;

        public RelayCommand BasePricingSelectedCommand { get; set; }
        public RelayCommand IncrementsSelectedCommand { get; set; }
        public RelayCommand TaxExemptIncrementsSelectedCommand { get; set; }
        public RelayCommand PricesToDisplaySelectedCommand { get; set; }
        public RelayCommand BackPageCommand { get; set; }

        public FuelPricingVM()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            BasePricingSelectedCommand = new RelayCommand(() =>
            {
                SelectedTab = FuelPricingTabs.BasePricing.ToString();
            });

            IncrementsSelectedCommand = new RelayCommand(() =>
             {
                 if (CacheBusinessLogic.AreFuelPricesSaved)
                 {
                     SelectedTab = FuelPricingTabs.Increments.ToString();
                 }
             });

            TaxExemptIncrementsSelectedCommand = new RelayCommand(() =>
            {
                if (CacheBusinessLogic.AreFuelPricesSaved)
                {
                    SelectedTab = FuelPricingTabs.Differences.ToString();
                }
            });

            PricesToDisplaySelectedCommand = new RelayCommand(() =>
            {
                if (CacheBusinessLogic.AreFuelPricesSaved)
                {
                    SelectedTab = FuelPricingTabs.PricesToDisplay.ToString();
                }
            });

            BackPageCommand = new RelayCommand(() =>
            {
                if (!CacheBusinessLogic.AreFuelPricesSaved)
                {
                    return;
                }

                if (CacheBusinessLogic.IsFuelOnlySystem)
                {
                    MessengerInstance.Send<FuelOnlySystemMessage>(new FuelOnlySystemMessage());
                }
                NavigateService.Instance.NavigateToHome();
            });
        }

        public void ResetVM()
        {
            SelectedTab = FuelPricingTabs.BasePricing.ToString();
        }

        private void ChangeToSelectePage()
        {
            switch (SelectedTab)
            {
                case nameof(FuelPricingTabs.BasePricing):
                    NavigateService.Instance.NavigateToBasePricing();
                    break;
                case nameof(FuelPricingTabs.Increments):
                    VMBase.LoadFuelPrices = false;
                    NavigateService.Instance.NavigateToIncrements();
                    break;
                case nameof(FuelPricingTabs.Differences):
                    VMBase.LoadFuelPrices = false;
                    NavigateService.Instance.NavigateToDifferences();
                    break;
                case nameof(FuelPricingTabs.PricesToDisplay):
                    NavigateService.Instance.NavigateToPricesToDisplay();
                    break;
            }
        }
    }
}
