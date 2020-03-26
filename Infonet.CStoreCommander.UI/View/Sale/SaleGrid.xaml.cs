using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using MyToolkit.Controls;
using MyToolkit.Extended.Controls;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class SaleGrid : Page
    {
        public SaleGridVM SaleGridVM { get; set; } =
             SimpleIoc.Default.GetInstance<SaleGridVM>();

        private DateTime _startTime;
        private InfonetLog _log;
        private DispatcherTimer _timer;
        private bool _editingStockCode;
        

        public SaleGrid()
        {
            NavigationCacheMode = NavigationCacheMode.Enabled;
            this.InitializeComponent();
            
            _log = InfonetLogManager.GetLogger(GetType());

            _startTime = DateTime.Now;

            Loaded -= OnLoaded;
            Loaded += OnLoaded;

            if (!SaleGridVM.AreScanEventsAttached)
            {
                SaleGridVM.AreScanEventsAttached = true;
                Window.Current.CoreWindow.KeyDown -= CoreWindowKeyDown;
                Window.Current.CoreWindow.KeyDown += CoreWindowKeyDown;

                _timer = new DispatcherTimer();
                _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Tick -= TimerTick;
                _timer.Tick += TimerTick;
                _timer.Start();
            }
        }

       

       

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var myGrid = (DataGrid)this.FindName("GridSale");
            myGrid.Visibility = Visibility.Visible;

            var endTime = DateTime.Now;
            _log.Info("------------==========================--------------------------");
            _log.Info(string.Format("Time taken in Sale Grid loading is {0}ms ", (endTime - _startTime).TotalMilliseconds));
            _log.Info("------------==========================--------------------------");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SaleGridVM.StockStream = string.Empty;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                SaleGridVM.ReInitialize();
            }
        }

        private void TimerTick(object sender, object e)
        {
            var element = FocusManager.GetFocusedElement();
            if (element != null && element.GetType() == typeof(CustomTextBox))
            {
                var textBox = element as CustomTextBox;
                _editingStockCode = textBox?.NumericKeyType == NumericKeyType.StockCodeNumber;
            }
            else
            {
                _editingStockCode = false;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SaleGridVM.StockStream = string.Empty;
        }

        private void CoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Enter)
            {
                var element = FocusManager.GetFocusedElement();
                if (element != null && element.GetType() == typeof(CustomTextBox))
                {
                    var textBox = element as CustomTextBox;
                    SaleGridVM.StockStream = textBox.Text;
                }
            }

            if (_editingStockCode)
            {
                char? key = Helper.ToChar(args.VirtualKey, false);
                if (key.HasValue)
                {
                    SaleGridVM.StockStream += key;
                }
                else if (args.VirtualKey == VirtualKey.Space || args.VirtualKey == VirtualKey.Delete || args.VirtualKey == VirtualKey.Back )
                {
                    var element = FocusManager.GetFocusedElement();
                    if (element != null && element.GetType() == typeof(CustomTextBox))
                    {
                        var textBox = element as CustomTextBox;
                        SaleGridVM.StockStream = textBox.Text;
                    }
                }

            }
        }
    }
}
