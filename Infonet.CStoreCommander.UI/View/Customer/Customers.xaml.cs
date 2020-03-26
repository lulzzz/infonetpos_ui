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
using Windows.UI.Xaml.Input;

namespace Infonet.CStoreCommander.UI.View.Customer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Customers : Page
    {
        public CustomersScreenVM CustomersScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<CustomersScreenVM>();

        private MSRService _msrService;

        public Customers()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                CustomersScreenVM.ReInitialize();
            }

            this.DataContext = CustomersScreenVM;
            CustomersScreenVM.SearchTextFieldName = nameof(txtCustomerSearchTextBox);

            txtCustomerSearchTextBox.KeyDown -= CheckForTrack1;
            txtCustomerSearchTextBox.KeyDown += CheckForTrack1;
        }

        private void CheckForTrack1(object sender, KeyRoutedEventArgs e)
        {
            // If only one question mark is present and enter is pressed then treat it as handled as track2 needs to be received
            if (e.Key == VirtualKey.Enter && txtCustomerSearchTextBox.Text.IndexOf('?') != -1 &&
                txtCustomerSearchTextBox.Text.IndexOf('?') == txtCustomerSearchTextBox.Text.LastIndexOf('?'))
            {
                e.Handled = true;
            }
        }

        private void OnReadCompleted(string data)
        {
            if (txtCustomerSearchTextBox.FocusState == FocusState.Unfocused)
            {
                CustomersScreenVM.CardNumber = data;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtCustomerSearchTextBox.Focus(FocusState.Keyboard);
        }

        private void CustomersLayoutUpdated(object sender, object e)
        {
            if (txtCustomerSearchTextBox.FocusState == FocusState.Unfocused
                && !PopupService.PopupInstance.IsPopupOpen)
            {
                DataGrid.Focus(FocusState.Programmatic);
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
