using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Checkout.TaxExempt
{
    public sealed partial class SignatureScreen : Page
    {
        private DispatcherTimer _fetchSignatureTimer;
        public SignatureVM SignatureVM = SimpleIoc.Default.GetInstance<SignatureVM>();

        public SignatureScreen()
        {
            this.InitializeComponent();
            DataContext = SignatureVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SignatureVM.ReInitialize();

            SignatureVM.SignatureImage = null;
            if (_fetchSignatureTimer == null)
            {
                _fetchSignatureTimer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 2)
                };
            }

            _fetchSignatureTimer.Tick -= FetchSignatureTimerTick;
            _fetchSignatureTimer.Tick += FetchSignatureTimerTick;
            _fetchSignatureTimer.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (_fetchSignatureTimer != null)
            {
                _fetchSignatureTimer.Stop();
                _fetchSignatureTimer.Tick -= FetchSignatureTimerTick;
            }
        }

        private async void FetchSignatureTimerTick(object sender, object e)
        {
            var imageStream = SignatureVM.SignaturePad.Accept();
            if (!string.IsNullOrEmpty(imageStream))
            {
                SignatureVM.SignatureImage = await Helper.Base64StringToBitmap(imageStream);
            }
        }
    }
}
