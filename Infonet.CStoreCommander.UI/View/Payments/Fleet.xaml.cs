using GalaSoft.MvvmLight.Ioc;
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
    public sealed partial class Fleet : Page
    {
        public FleetVM FleetVM { get; set; }
 = SimpleIoc.Default.GetInstance<FleetVM>();

        private MSRService _msrService;

        public Fleet()
        {
            this.InitializeComponent();

            DataContext = FleetVM;
            FleetVM.ResetVM();
        }

        private void OnReadCompleted(string data)
        {
            FleetVM.CardNumber = data;
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
            _msrService.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;

            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            txtCardNumber.Focus(FocusState.Programmatic);
        }

        private void CardNumberKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtAmountFleet.Focus(FocusState.Pointer);
            }
        }
    }
}
