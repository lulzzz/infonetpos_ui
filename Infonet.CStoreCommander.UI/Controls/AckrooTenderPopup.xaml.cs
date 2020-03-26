using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel;
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
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class AckrooTenderPopup : UserControl
    {
        private MSRService _msrService;
        private string _logoFile;
        
        public AckrooTenderPopup()
        {
            this.InitializeComponent();
            this.DataContext = AckrooTenderVM;
            Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
            Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;
            Loaded -= OnLoaded;
            Loaded += OnLoaded;
            _msrService = new MSRService();
            _msrService.Start(null);
            _msrService.ReadCompleted -= OnReadCompleted;
            _msrService.ReadCompleted += OnReadCompleted;
            CardNumberBox.Focus(FocusState.Programmatic);
            
        }
        private async Task GetLogoFilePath()
        {
            Uri uri = new Uri("ms-appx:///Images/Others/Mr-Gas-New-Card-WEB.png");
            StorageFile stFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            _logoFile= stFile.Path;
        }
        private void LoadImage()
        {
            //BitmapImage bmp = new BitmapImage();
            //ctlImage.Source = bmp;
            //bmp.UriSource = new Uri("ms-appx:///Images/Others/Mr-Gas-New-Card-WEB.png", UriKind.RelativeOrAbsolute);


            //if(_logoFile==null)
            //{
            //    Uri uri = new Uri("ms-appx:///Images/Others/Mr-Gas-New-Card-WEB.png");
            //    StorageFile stFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            //    _logoFile = stFile.Path;
            //}

            //var ofile = await Windows.Storage.StorageFile.GetFileFromPathAsync(_logoFile);
            //IRandomAccessStream stream = await ofile.OpenAsync(FileAccessMode.Read);
            //BitmapImage bmp = new BitmapImage();
            //await bmp.SetSourceAsync(stream);

            //ctlImage.Source = bmp;

        }
        private void OnReadCompleted(string data)
        {
            if (CardNumberBox.FocusState == FocusState.Unfocused)
            {
                AckrooTenderVM.CardNumber = data;
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

        
    }
}
