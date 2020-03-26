using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class SalesSummary : Page
    {
        public SaleSummaryVM SaleSummaryVM { get; set; } =
               SimpleIoc.Default.GetInstance<SaleSummaryVM>();

        private MSRService _msrService;
        private ValueSet table = null;

        public SalesSummary()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            


        }

        private async void PrintReceipt(string text, string ImagePath)
        {
            // create a ValueSet from the datacontext
            table = new ValueSet();
            table.Add("PrintText", text);
            //table.Add("PrintText", "TEST:\r\n");
            if (ImagePath != null)
                table.Add("ImgPath", ImagePath);
            else
                table.Add("ImgPath", null);
            //table.Add("ImgPath", null);
            // launch the fulltrust process and for it to connect to the app service            
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
            else
            {
                MessageDialog dialog = new MessageDialog("This feature is only available on Windows 10 Desktop SKU");
                await dialog.ShowAsync();
            }
        }

        private async void PrintReceipt_AppServiceConnected(object sender, EventArgs e)
        {
            // send the ValueSet to the fulltrust process
            AppServiceResponse response = await App.Connection.SendMessageAsync(table);

            // check the result
            MessageDialog dialog;
            object result = null;
            response.Message.TryGetValue("RESPONSE", out result);
            if (result == null)
            {
                dialog = new MessageDialog("Print error.");
                await dialog.ShowAsync();
            }
            else
            {
                if (result.ToString() != "SUCCESS")
                {
                    dialog = new MessageDialog(result.ToString());
                    await dialog.ShowAsync();
                }
            }

            // no longer need the AppService connection
            App.AppServiceDeferral.Complete();
        }

        private void ResetVM()
        {
            SaleSummaryVM.ReInitializeVM();
        }

        private void OnReadCompleted(string data)
        {
            SaleSummaryVM.CardNumber = data;
        }

        private void CoreWindowKeyDown(Windows.UI.Core.CoreWindow sender,
            Windows.UI.Core.KeyEventArgs args)
        {
            Window.Current.CoreWindow.GetKeyState(args.VirtualKey);
            _msrService.ReadKey(args.VirtualKey, args.KeyStatus);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            SaleSummaryVM.PrintReceiptEvent -= PrintReceipt;
            App.AppServiceConnected -= PrintReceipt_AppServiceConnected;
            _msrService.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            SaleSummaryVM.PrintReceiptEvent -= PrintReceipt;
            SaleSummaryVM.PrintReceiptEvent += PrintReceipt;
            App.AppServiceConnected -= PrintReceipt_AppServiceConnected;
            App.AppServiceConnected += PrintReceipt_AppServiceConnected;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ResetVM();
            }

            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
        }
    }
}
