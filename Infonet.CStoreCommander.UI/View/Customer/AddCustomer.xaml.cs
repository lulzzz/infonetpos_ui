using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Customer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Customer
{
    public sealed partial class AddCustomer : Page
    {
        public AddCustomerScreenVM AddCustomerScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<AddCustomerScreenVM>();

        public AddCustomer()
        {
            this.InitializeComponent();
            DataContext = AddCustomerScreenVM;


            ReinitializeVM();
        }


        private void ReinitializeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                AddCustomerScreenVM.ReInitialize();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            txtCode.KeyUp -= CodeKeyUp;
            txtCode.KeyUp += CodeKeyUp;
            txtLoyalityNumber.KeyUp -= LoyalityKeyUp;
            txtLoyalityNumber.KeyUp += LoyalityKeyUp;
            txtName.KeyUp -= NameKeyUp;
            txtName.KeyUp += NameKeyUp;
            txtPhone.KeyUp -= PhoneKeyUp;
            txtPhone.KeyUp += PhoneKeyUp;
            Loaded -= AddCustomerLoaded;
            Loaded += AddCustomerLoaded;
        }

        private void AddCustomerLoaded(object sender, RoutedEventArgs e)
        {
            txtCode.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Loaded -= AddCustomerLoaded;
            txtCode.KeyUp -= CodeKeyUp;
            txtLoyalityNumber.KeyUp -= LoyalityKeyUp;
            txtName.KeyUp -= NameKeyUp;
            txtPhone.KeyUp -= PhoneKeyUp;
        }


        private void PhoneKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                btnSave.Focus(FocusState.Pointer);
            }
        }

        private void NameKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtPhone.Focus(FocusState.Keyboard);
            }
        }

        private void LoyalityKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtName.Focus(FocusState.Keyboard);
            }
        }

        private void CodeKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtLoyalityNumber.Focus(FocusState.Keyboard);
            }
        }
    }
}
