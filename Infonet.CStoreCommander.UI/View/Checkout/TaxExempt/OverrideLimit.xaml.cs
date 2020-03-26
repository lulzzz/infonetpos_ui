using System;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace Infonet.CStoreCommander.UI.View.Checkout.TaxExempt
{
    public sealed partial class OverrideLimit : Page
    {
        public OverrideLimitVM OverrideLimitVM =
            SimpleIoc.Default.GetInstance<OverrideLimitVM>();

        public OverrideLimit()
        {
            this.InitializeComponent();
            DataContext = OverrideLimitVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                OverrideLimitVM.ReInitialize();
            }

            document.KeyDown += OnkeyPressedDocument;
            details.KeyDown += OnkeyPressedDetails;
        }

        private void OnkeyPressedDetails(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                details.IsEnabled = false;
                details.IsEnabled = true;
            }
        }

        private void OnkeyPressedDocument(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                document.IsEnabled = false;
                document.IsEnabled = true;
            }
        }
    }
}

