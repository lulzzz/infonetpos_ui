using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class KickBackNumberPopup : UserControl
    {
        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
           DependencyProperty.Register(nameof(BackgroundOverlay),
               typeof(SolidColorBrush),
               typeof(KickBackNumberPopup),
               new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }

        public static readonly DependencyProperty ClosePopupCommandProperty =
            DependencyProperty.Register(nameof(ClosePopupCommand),
                typeof(ICommand), typeof(KickBackNumberPopup),
                new PropertyMetadata(null));

        public VMBase VMBase { get; set; }
        public SaleGridVM SaleGridVM { get; set; }
        = SimpleIoc.Default.GetInstance<SaleGridVM>();

        public KickBackNumberPopup()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            pbText.Focus(FocusState.Programmatic);
        }

        private void KickBackLayoutUpdated(object sender, object e)
        {
            if (pbText.FocusState == FocusState.Unfocused
                && PopupService.PopupInstance.IsPopupOpen 
                && yesButton.FocusState == FocusState.Unfocused)
            {
                pbText.Focus(FocusState.Programmatic);
            }
        }
    }
}
