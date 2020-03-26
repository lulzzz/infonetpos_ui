using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Fuel_Pricing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasePrice : Page
    {
        public BasePricesVM BasePricesVM { get; set; }
      = SimpleIoc.Default.GetInstance<BasePricesVM>();

        public BasePrice()
        {
            this.InitializeComponent();
            DataContext = BasePricesVM;
            BasePricesVM.ReInitialize();
        }
    }
}
