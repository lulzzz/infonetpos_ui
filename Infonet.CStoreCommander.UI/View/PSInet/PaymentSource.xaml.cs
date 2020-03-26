using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.View.PSInet.PSInetOptions;
using Infonet.CStoreCommander.UI.ViewModel.PSInet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.PSInet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentSource : Page
    {
        private PrintHelper printHelper;
        public PaymentSourceVM PYSVM { get; set; } = SimpleIoc.Default.GetInstance<PaymentSourceVM>();
        public PaymentSource()
        {
            this.InitializeComponent();
            PYSVM.PrintEvent += PYSVM_PrintEvent;
            NavigateService.Instance.PaymentSourceFrame = frmPmtSource;
            ReInitalizeVM();
        }

        private async void PYSVM_PrintEvent()
        {
            //Task.Run(async () => { await printHelper.ShowPrintUIAsync(); }).RunSynchronously();

            await printHelper.ShowPrintUIAsync();

            //var task = Task.Run(async () => { await printHelper.ShowPrintUIAsync(); });
            //task.Wait();
            //NavigateService.Instance.NavigateToHome();


        }

        private void ReInitalizeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PYSVM.ResetVM();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Initalize common helper class and register for printing
            if (printHelper == null)
            {
                printHelper = new PrintHelper(this);
                printHelper.RegisterForPrinting();
                // Initialize print content for this scenario
                printHelper.PreparePrintContent(new Receipt());
            }
            
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (printHelper != null)
            {
                printHelper.UnregisterForPrinting();
               
            }

        }
    }
}
