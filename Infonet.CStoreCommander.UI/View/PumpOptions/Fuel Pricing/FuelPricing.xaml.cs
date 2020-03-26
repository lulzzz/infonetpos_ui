using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.FuelPricing;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Fuel_Pricing
{
    public sealed partial class FuelPricing : Page
    {
        public FuelPricingVM FuelPricingVM { get; set; } =
                  SimpleIoc.Default.GetInstance<FuelPricingVM>();

        public FuelPricing()
        {
            this.InitializeComponent();
            FuelPricingVM.ResetVM();
            NavigateService.Instance.FuelPricingFrame = frmFuelPricing;
        }
    }
}
