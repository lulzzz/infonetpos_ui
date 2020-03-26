using Infonet.CStoreCommander.UI.ViewModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class EmergencyPopup : UserControl
    {
        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
             DependencyProperty.Register(nameof(BackgroundOverlay),
                 typeof(SolidColorBrush),
                 typeof(PopupWithTwoButtons),
                 new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        public HomeScreenVM HomeScreenVM;

        public EmergencyPopup()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                HomeScreenVM = DataContext as HomeScreenVM;
            };
        }
    }
}
