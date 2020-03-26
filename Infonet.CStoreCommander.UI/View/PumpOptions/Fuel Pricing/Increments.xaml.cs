using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.PumpOptions.Fuel_Pricing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Increments : Page
    {
        PriceIncrementsVM PriceIncrementsVM = SimpleIoc.Default.GetInstance<PriceIncrementsVM>();

        public Increments()
        {
            this.InitializeComponent();
            DataContext = PriceIncrementsVM;
            PriceIncrementsVM.ReInitialize();
        }
    }
}
