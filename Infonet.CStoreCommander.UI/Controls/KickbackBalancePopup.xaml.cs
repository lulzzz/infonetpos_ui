using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class KickbackBalancePopup : UserControl
    {
        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
           DependencyProperty.Register(nameof(BackgroundOverlay),
               typeof(SolidColorBrush),
               typeof(KickbackBalancePopup),
               new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        
        public KickbackVM KickbackVM { get; set; } =
        SimpleIoc.Default.GetInstance<KickbackVM>();

        private void GridLoaded(object sender, RoutedEventArgs e)
        {            
            pbText.Focus(FocusState.Programmatic);
        }

        public KickbackBalancePopup()
        {
            this.InitializeComponent();
            DataContext = KickbackVM;
        }
    }
}
