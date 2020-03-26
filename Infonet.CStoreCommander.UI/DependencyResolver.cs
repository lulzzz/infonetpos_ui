using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.BussinessLogic;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager;
using Infonet.CStoreCommander.DataAccessLayer.MockManager;
using Infonet.CStoreCommander.DataAccessLayer.SerializeManager;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.ViewModel.Ackroo;
using Infonet.CStoreCommander.UI.ViewModel.CashManager;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Infonet.CStoreCommander.UI.ViewModel.Common;
using Infonet.CStoreCommander.UI.ViewModel.Customer;
using Infonet.CStoreCommander.UI.ViewModel.DipInputs;
using Infonet.CStoreCommander.UI.ViewModel.FuelPricing;
using Infonet.CStoreCommander.UI.ViewModel.GiveX;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using Infonet.CStoreCommander.UI.ViewModel.Payment;
using Infonet.CStoreCommander.UI.ViewModel.PSInet;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.FuelPricing;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions.PropaneGrade;
using Infonet.CStoreCommander.UI.ViewModel.Reports;
using Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions;
using Infonet.CStoreCommander.UI.ViewModel.Reprint;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Infonet.CStoreCommander.UI.ViewModel.SettingsMenu;
using Infonet.CStoreCommander.UI.ViewModel.SettingsMenu.Maintenance;
using Infonet.CStoreCommander.UI.ViewModel.Stock;
using Infonet.CStoreCommander.UI.ViewModel.TierLevelVM;

namespace Infonet.CStoreCommander.UI
{
    /// <summary>
    /// Class for Resolving the dependencies
    /// </summary>
    public class DependencyResolver
    {
        private const bool MockedData = false;

        /// <summary>
        /// Registers and resolves all the dependencies required
        /// </summary>
        /// <returns></returns>
        public void RegisterDependencies()
        {
            SimpleIoc.Default.Register<ExtendedSplashScreenVM>();
            SimpleIoc.Default.Register<HomeScreenVM>();
            SimpleIoc.Default.Register<HotCategoriesScreenVM>();
            SimpleIoc.Default.Register<LoginScreenVM>();
            SimpleIoc.Default.Register<LogoutScreenVM>();
            SimpleIoc.Default.Register<CustomersScreenVM>();
            SimpleIoc.Default.Register<AddCustomerScreenVM>();
            SimpleIoc.Default.Register<AddStockScreenVM>();
            SimpleIoc.Default.Register<LoyaltyCustomersScreenVM>();
            SimpleIoc.Default.Register<SearchStockScreenVM>();
            SimpleIoc.Default.Register<SaleGridVM>();
            SimpleIoc.Default.Register<BottleReturnsScreenVM>();
            SimpleIoc.Default.Register<MaintenanceVM>();
            SimpleIoc.Default.Register<SwitchUserVM>();
            SimpleIoc.Default.Register<UnsuspendSaleVM>();
            SimpleIoc.Default.Register<ReturnSaleVM>();
            SimpleIoc.Default.Register<ReturnSaleItemVM>();
            SimpleIoc.Default.Register<GiveXVM>();
            SimpleIoc.Default.Register<NumberpadVM>();
            SimpleIoc.Default.Register<SaleSummaryVM>();
            SimpleIoc.Default.Register<PaymentVM>();
            SimpleIoc.Default.Register<PriceCheckVM>();
            SimpleIoc.Default.Register<ReportsScreenVM>();
            SimpleIoc.Default.Register<SalesCountVM>();
            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<FlashReportVM>();
            SimpleIoc.Default.Register<FreezedScreenVM>();
            SimpleIoc.Default.Register<TillAuditReportVM>();
            SimpleIoc.Default.Register<TaxExemptionVM>();
            SimpleIoc.Default.Register<CashDrawVM>();
            SimpleIoc.Default.Register<CashDropVM>();
            SimpleIoc.Default.Register<TaxExemptionVM>();
            SimpleIoc.Default.Register<AiteVM>();
            SimpleIoc.Default.Register<OverLimitVM>();
            SimpleIoc.Default.Register<SiteVM>();
            SimpleIoc.Default.Register<OverrideLimitVM>();
            SimpleIoc.Default.Register<QiteVM>();
            SimpleIoc.Default.Register<ARVM>();
            SimpleIoc.Default.Register<GiftCertificateVM>();
            SimpleIoc.Default.Register<CouponVM>();
            SimpleIoc.Default.Register<GiveXTenderVM>();
            SimpleIoc.Default.Register<UtilsVM>();
            SimpleIoc.Default.Register<MasterPageVM>();
            SimpleIoc.Default.Register<StoreCreditVM>();
            SimpleIoc.Default.Register<DipInputVM>();
            SimpleIoc.Default.Register<TierLevelVM>();
            SimpleIoc.Default.Register<FuelPricingVM>();
            SimpleIoc.Default.Register<PayoutVM>();
            SimpleIoc.Default.Register<FleetVM>();
            SimpleIoc.Default.Register<ReprintVM>();
            SimpleIoc.Default.Register<LastPrintVM>();
            SimpleIoc.Default.Register<VendorCouponVM>();
            SimpleIoc.Default.Register<FleetTenderVM>();
            SimpleIoc.Default.Register<PrepayVM>();
            SimpleIoc.Default.Register<PropaneGradeVM>();
            SimpleIoc.Default.Register<AddManualVM>();
            SimpleIoc.Default.Register<BasePricesVM>();
            SimpleIoc.Default.Register<CloseTillVM>();
            SimpleIoc.Default.Register<ErrorsVM>();
            SimpleIoc.Default.Register<PricesToDisplayVM>();
            SimpleIoc.Default.Register<PriceIncrementsVM>();
            SimpleIoc.Default.Register<FinishVM>();
            SimpleIoc.Default.Register<SignatureVM>();
            SimpleIoc.Default.Register<GiveXReportVM>();
            SimpleIoc.Default.Register<KickbackVM>();
            SimpleIoc.Default.Register<FuelDiscountVM>();
            SimpleIoc.Default.Register<AckrooTenderVM>();
            SimpleIoc.Default.Register<AckrooVM>();
            SimpleIoc.Default.Register<PaymentSourceVM>();

            SimpleIoc.Default.Register<ICacheBusinessLogic, CacheBusinessLogic>();
            SimpleIoc.Default.Register<ILoginBussinessLogic, LoginBussinessLogic>();
            SimpleIoc.Default.Register<ICustomerBussinessLogic, CustomerBussinessLogic>();
            SimpleIoc.Default.Register<IStockBussinessLogic, StockBussinessLogic>();
            SimpleIoc.Default.Register<IReasonListBussinessLogic, ReasonListBussinessLogic>();
            SimpleIoc.Default.Register<ISaleBussinessLogic, SaleBussinessLogic>();
            SimpleIoc.Default.Register<IPolicyBussinessLogic, PolicyBussinessLogic>();
            SimpleIoc.Default.Register<IMaintenanceBussinessLogic, MaintenanceBussinessLogic>();
            SimpleIoc.Default.Register<ILogoutBussinessLogic, LogoutBussinessLogic>();
            SimpleIoc.Default.Register<IGiveXBussinessLogic, GiveXBussinessLogic>();
            SimpleIoc.Default.Register<IReportsBussinessLogic, ReportsBussinessLogic>();
            SimpleIoc.Default.Register<ICheckoutBusinessLogic, CheckoutBusinessLogic>();
            SimpleIoc.Default.Register<IPaymentBussinessLogic, PaymentBussinessLogic>();
            SimpleIoc.Default.Register<ICashBusinessLogic, CashBusinessLogic>();
            SimpleIoc.Default.Register<IGiftCertificateBusinessLogic, GiftCertificateBusinessLogic>();
            SimpleIoc.Default.Register<IThemeBusinessLogic, ThemeBusinessLogic>();
            SimpleIoc.Default.Register<IDipInputBusinessLogic, DipInputBusinessLogic>();
            SimpleIoc.Default.Register<IMessageBusinessLogic, MessageBusinessLogic>();
            SimpleIoc.Default.Register<IPayoutBusinessLogic, PayoutBusinessLogic>();
            SimpleIoc.Default.Register<IReprintBusinessLogic, ReprintBusinessLogic>();
            SimpleIoc.Default.Register<IFuelPumpBusinessLogic, FuelPumpBusinessLogic>();
            SimpleIoc.Default.Register<ISystemBusinessLogic, SystemBusinessLogic>();
            SimpleIoc.Default.Register<ISoundBusinessLogic, SoundBusinessLogic>();
            SimpleIoc.Default.Register<IFuelPriceBusinessLogic, FuelPriceBusinessLogic>();
            SimpleIoc.Default.Register<IKickBackBusinessLogic, KickBackBusinessLogic>();
            SimpleIoc.Default.Register<ICarwashBusinessLogic, CarwashBusinessLogic>();
            SimpleIoc.Default.Register<ICarwashSerializeManager, CarwashSerializeManager>();
            SimpleIoc.Default.Register<ICarwashRestClient, CarwashRestClient>();
   

            SimpleIoc.Default.Register<ICacheManager, CacheManager>();
            SimpleIoc.Default.Register<IStorageService, StorageService>();

            SimpleIoc.Default.Register<ILoginSerializeManager, LoginSerializeManager>();
            SimpleIoc.Default.Register<ICustomerSerializeManager, CustomerSerializeManager>();
            SimpleIoc.Default.Register<ISaleSerializeManager, SaleSerializeManager>();
            SimpleIoc.Default.Register<IStockSerializeManager, StockSerializeManager>();
            SimpleIoc.Default.Register<IReasonListSerializeManager, ReasonListSerializeManager>();
            SimpleIoc.Default.Register<IPolicySerializeManager, PolicySeralizeManager>();
            SimpleIoc.Default.Register<IMaintenanceSeralizeManager, MaintenanceSeralizeManager>();
            SimpleIoc.Default.Register<ILogoutSerializeManager, LogoutSerializeManager>();
            SimpleIoc.Default.Register<IGiveXSerializeManager, GiveXSerializeManager>();
            SimpleIoc.Default.Register<IReportSerializeManager, ReportsSerializeManager>();
            SimpleIoc.Default.Register<ICheckoutSerializeManager, CheckoutSerializeManager>();
            SimpleIoc.Default.Register<IPaymentSerializeManager, PaymentSerializeManager>();
            SimpleIoc.Default.Register<ICashSerializeManager, CashSerializeManager>();
            SimpleIoc.Default.Register<IGiftCertificateSerializeManager, GiftCertificateSerializeManager>();
            SimpleIoc.Default.Register<IThemeSerializeManager, ThemeSerializeManager>();
            SimpleIoc.Default.Register<IDipInputSerializeManager, DipInputSerializeManager>();
            SimpleIoc.Default.Register<IMessageSerializeManager, MessageSerializeManager>();
            SimpleIoc.Default.Register<IPayoutSerializeManager, PayoutSerializeManager>();
            SimpleIoc.Default.Register<IReprintSerializeManager, ReprintSerializeManager>();
            SimpleIoc.Default.Register<IFuelPumpSerializeManager, FuelPumpSerializeManager>();
            SimpleIoc.Default.Register<ISystemSerializeManager, SystemSerializeManager>();
            SimpleIoc.Default.Register<ISoundSerializeManager, SoundSerializeManager>();
            SimpleIoc.Default.Register<IFuelPriceSerializeManager, FuelPriceSerializeManager>();
            SimpleIoc.Default.Register<IKickBackSerializeManager, KickBackSerializeManager>();
            SimpleIoc.Default.Register<IFuelDiscountBusinessLogic, FuelDiscountBusinessLogic>();
            SimpleIoc.Default.Register<IFuelDiscountRestClient, FuelDiscountRestClient>();
            SimpleIoc.Default.Register<IFuelDiscountSerializeManager, FuelDiscountSerializeManager>();
            SimpleIoc.Default.Register<IPaymentSourceBusinessLogic, PaymentSourceBusinessLogic>();
            SimpleIoc.Default.Register<IPaymentSourceRestClient, PaymentSourceRestClient>();
            SimpleIoc.Default.Register<IPaymentSourceSerializeManager, PaymentSourceSerializeManager>();
            SimpleIoc.Default.Register<IAckrooBusinessLogic, AckrooBusinessLogic>();
            SimpleIoc.Default.Register<IAckrooRestClient, AckrooRestClient>();
            SimpleIoc.Default.Register<IAckrooSerializeManager, AckrooSerializeManager>();

            ResolveRestClients();
        }

        private void ResolveRestClients()
        {
            if (MockedData)
            {
                SimpleIoc.Default.Register<ILoginRestClient, MockLoginRestClient>();
                SimpleIoc.Default.Register<ISaleRestClient, MockSaleRestClient>();
                SimpleIoc.Default.Register<ICustomerRestClient, MockCustomerRestClient>();
                SimpleIoc.Default.Register<IStockRestClient, MockStockRestClient>();
                SimpleIoc.Default.Register<IReasonListRestClient, MockReasonListRestClient>();
                SimpleIoc.Default.Register<IPolicyRestClient, MockPolicyRestClient>();
                SimpleIoc.Default.Register<IMaintenanceRestClient, MockMaintenanceRestClient>();
                SimpleIoc.Default.Register<ILogoutRestClient, MockLogoutRestClient>();
                SimpleIoc.Default.Register<IGiveXRestClient, MockGiveXRestClient>();
                SimpleIoc.Default.Register<IReportRestClient, MockReportRestClient>();
                SimpleIoc.Default.Register<ICheckoutRestClient, MockCheckoutRestClient>();
                SimpleIoc.Default.Register<IPaymentRestClient, MockPaymentRestClient>();
                SimpleIoc.Default.Register<ICashRestClient, MockCashRestClient>();
                SimpleIoc.Default.Register<IDipInputRestClient, MockDipInputRestClient>();
                SimpleIoc.Default.Register<IMessageRestClient, MockMessageRestClient>();
                SimpleIoc.Default.Register<IPayoutRestClient, MockPayoutRestClient>();
                SimpleIoc.Default.Register<IReprintRestClient, MockReprintRestClient>();
                SimpleIoc.Default.Register<IFuelPumpRestClient, MockFuelPumpRestClient>();
            }
            else
            {
                SimpleIoc.Default.Register<ILoginRestClient, LoginRestClient>();
                SimpleIoc.Default.Register<ISaleRestClient, SaleRestClient>();
                SimpleIoc.Default.Register<ICustomerRestClient, CustomerRestClient>();
                SimpleIoc.Default.Register<IStockRestClient, StockRestClient>();
                SimpleIoc.Default.Register<IReasonListRestClient, ReasonListRestClient>();
                SimpleIoc.Default.Register<IPolicyRestClient, PolicyRestClient>();
                SimpleIoc.Default.Register<IMaintenanceRestClient, MaintenanceRestClient>();
                SimpleIoc.Default.Register<ILogoutRestClient, LogoutRestClient>();
                SimpleIoc.Default.Register<IGiveXRestClient, GiveXRestClient>();
                SimpleIoc.Default.Register<IReportRestClient, ReportRestClient>();
                SimpleIoc.Default.Register<ICheckoutRestClient, CheckoutRestClient>();
                SimpleIoc.Default.Register<IPaymentRestClient, PaymentRestClient>();
                SimpleIoc.Default.Register<ICashRestClient, CashRestClient>();
                SimpleIoc.Default.Register<IGiftCertificateRestClient, GiftCertificateRestClient>();
                SimpleIoc.Default.Register<IThemeRestClient, ThemeRestClient>();
                SimpleIoc.Default.Register<IDipInputRestClient, DipInputRestClient>();
                SimpleIoc.Default.Register<IMessageRestClient, MessageRestClient>();
                SimpleIoc.Default.Register<IPayoutRestClient, PayoutRestClient>();
                SimpleIoc.Default.Register<IReprintRestClient, ReprintRestClient>();
                SimpleIoc.Default.Register<IFuelPumpRestClient, FuelPumpRestClient>();
                SimpleIoc.Default.Register<ISystemRestClient, SystemRestClient>();
                SimpleIoc.Default.Register<ISoundRestClient, SoundsRestClient>();
                SimpleIoc.Default.Register<IFuelPriceRestClient, FuelPriceRestClient>();
                SimpleIoc.Default.Register<IKickBackRestClient, KickBackRestClient>();
            }
        }
    }
}
