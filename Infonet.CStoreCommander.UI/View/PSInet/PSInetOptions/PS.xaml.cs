using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.PSInet;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.PSInet.PSInetOptions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PS : Page
    {
        public PaymentSourceVM PaymentSourceVM { get; set; } = SimpleIoc.Default.GetInstance<PaymentSourceVM>();
        private MSRService _msrService;
        
        public PS()
        {
            this.InitializeComponent();
            this.DataContext = PaymentSourceVM;
        }
        private void OnReadCompleted(string data)
        {
            if (CardNumberBox.FocusState == FocusState.Unfocused)
            {
                PaymentSourceVM.CardNumber = data;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.CoreWindow.KeyDown-= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            Loaded -= OnLoaded;
            Loaded += OnLoaded;
            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
            CardNumberBox.Focus(FocusState.Programmatic);
            


        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CardNumberBox.Focus(FocusState.Keyboard);
            


        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            _msrService.Stop();

            
        }
        private void CoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Window.Current.CoreWindow.GetKeyState(args.VirtualKey);
            _msrService.ReadKey(args.VirtualKey, args.KeyStatus);
            


        }
    }
}
