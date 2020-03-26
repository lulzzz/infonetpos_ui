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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class CarwashPopup : UserControl
    {
        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush) GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
            DependencyProperty.Register(nameof(BackgroundOverlay),
                typeof(SolidColorBrush),
                typeof(CarwashPopup),
                new PropertyMetadata((SolidColorBrush) Application.Current.Resources["LightGray"]));

        public ICommand ClosePopupCommand
        {
            get { return (ICommand) GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }

        public static readonly DependencyProperty ClosePopupCommandProperty =
            DependencyProperty.Register(nameof(ClosePopupCommand),
                typeof(ICommand), typeof(CarwashPopup),
                new PropertyMetadata(null));

        public VMBase VMBase { get; set; }

        public SaleGridVM SaleGridVM { get; set; }
            = SimpleIoc.Default.GetInstance<SaleGridVM>();

        public CarwashPopup()
        {
            this.InitializeComponent();
            pbText.MaxLength = 5;
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            pbText.Focus(FocusState.Programmatic);
            yesButton.IsEnabled = false;
        }

        private void pbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                yesButton.IsEnabled = true;
            }
            else
            {
                yesButton.IsEnabled = false;
            }
        }

        private void PbText_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            //  throw new NotImplementedException();
            if (Helper.IsEnterKey(e))
            {
                if (yesButton.IsEnabled)
                {
                    SaleGridVM.CheckCarwashCode();
                }
            }
        }
    }
}