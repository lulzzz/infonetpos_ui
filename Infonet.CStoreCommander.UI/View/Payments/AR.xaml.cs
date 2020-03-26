using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Payment;
using System;
using System.Threading;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Payments
{
    public sealed partial class AR : Page
    {
        public ARVM ARVM { get; set; } = SimpleIoc.Default.GetInstance<ARVM>();

        private MSRService _msrService;

        public AR()
        {
            this.InitializeComponent();
            this.DataContext = ARVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ARVM.ReInitialize();
            }

            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DetachEvents();
        }

        private void OnReadCompleted(string data)
        {
            ARVM.CardNumber = data;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtCustomerSearch.Focus(FocusState.Keyboard);
        }

        private void CustomersListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ARVM.SelectedARCustomer != null)
            {
                amount.Focus(FocusState.Keyboard);
            }
        }

        private void AmountKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
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
            DetachEvents();
        }

        private void DetachEvents()
        {
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;

            amount.KeyUp -= AmountKeyUp;
            CustomersList.SelectionChanged -= CustomersListSelectionChanged;
            this.Loaded -= OnLoaded;

            _msrService.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;

            amount.KeyUp -= AmountKeyUp;
            amount.KeyUp += AmountKeyUp;

            CustomersList.SelectionChanged -= CustomersListSelectionChanged;
            CustomersList.SelectionChanged += CustomersListSelectionChanged;

            this.Loaded -= OnLoaded;
            this.Loaded += OnLoaded;

            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
        }
    }
}




