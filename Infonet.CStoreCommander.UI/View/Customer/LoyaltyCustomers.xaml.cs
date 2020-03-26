using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Customer;
using System;
using System.Threading;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Customer
{
    public sealed partial class LoyaltyCustomers : Page
    {
        public LoyaltyCustomersScreenVM LoyaltyCustomersScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<LoyaltyCustomersScreenVM>();

        private MSRService _msrService;

        public LoyaltyCustomers()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                LoyaltyCustomersScreenVM.ReInitialize();
            }

            this.DataContext = LoyaltyCustomersScreenVM;
            LoyaltyCustomersScreenVM.SearchTextFieldName = nameof(txtSearch);
        }

        private void OnReadCompleted(string data)
        {
            if (txtSearch.FocusState == FocusState.Unfocused)
            {
                LoyaltyCustomersScreenVM.CardNumber = data;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(FocusState.Keyboard);
        }

        private void CoreWindowKeyDown(Windows.UI.Core.CoreWindow sender,
            Windows.UI.Core.KeyEventArgs args)
        {
            Window.Current.CoreWindow.GetKeyState(args.VirtualKey);

            _msrService.ReadKey(args.VirtualKey, args.KeyStatus);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;

            Loaded -= OnLoaded;
            _msrService.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;

            Loaded -= OnLoaded;
            Loaded += OnLoaded;

            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
        }
    }
}
