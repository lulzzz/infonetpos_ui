using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Ackroo;
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
using Windows.UI.Core;
using Infonet.CStoreCommander.UI.Utility;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class AckrooAccoutBalance : UserControl
    {
        private MSRService _msrService;
        public AckrooAccoutBalance()
        {
            this.InitializeComponent();
            this.DataContext = AckrooTenderVM;
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            //Loaded -= OnLoaded;
            //Loaded += OnLoaded;
            _msrService = new MSRService();
            _msrService.Start(null);
            //_msrService.ReadCompleted -= OnReadCompleted;
            //_msrService.ReadCompleted += OnReadCompleted;
        }

        private void OnReadCompleted(string data)
        {
            throw new NotImplementedException();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Window.Current.CoreWindow.GetKeyState(args.VirtualKey);
            _msrService.ReadKey(args.VirtualKey, args.KeyStatus);
        }

        public AckrooTenderVM AckrooTenderVM { get; set; }
        = SimpleIoc.Default.GetInstance<AckrooTenderVM>();
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //var obj = sender;
            AckrooTenderVM.SelectedItemChaged();
        }
    }
}
