using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Infonet.CStoreCommander.UI.View.Ackroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AckrooCardActivation : Page
    {
        public AckrooVM AKVM { get; set; } = SimpleIoc.Default.GetInstance<AckrooVM>();
        private MSRService _msrService;
        public AckrooCardActivation()
        {
            this.InitializeComponent();
            this.DataContext = AKVM;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            Loaded -= OnLoaded;
            Loaded += OnLoaded;
            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
            CardNumberBox.Focus(FocusState.Programmatic);
            CardNumberBox.KeyUp -= FocusOnAckrooAmount;
            CardNumberBox.KeyUp += FocusOnAckrooAmount;


        }

        private void FocusOnAckrooAmount(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                AckrooAmount.Focus(FocusState.Keyboard);
            }
        }

        private void OnReadCompleted(string data)
        {
            if (CardNumberBox.FocusState == FocusState.Unfocused)
            {
                AKVM.CardNumber = data;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CardNumberBox.Focus(FocusState.Keyboard);
        }

        private void CoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Window.Current.CoreWindow.GetKeyState(args.VirtualKey);
            _msrService.ReadKey(args.VirtualKey, args.KeyStatus);
        }
    }
}
