using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Stock;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Stock
{
    public sealed partial class SearchStock : Page
    {
        public SearchStockScreenVM SearchStockScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<SearchStockScreenVM>();

        public SearchStock()
        {
            this.InitializeComponent();

            LoadViewModel();
            this.DataContext = SearchStockScreenVM;
            SearchStockScreenVM.SearchTextFieldName = nameof(txtSearchStock);

            Loaded -= OnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtSearchStock.Focus(FocusState.Keyboard);
        }

        private void LoadViewModel()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                SearchStockScreenVM.ReInitialize();
            }
        }
    }
}
