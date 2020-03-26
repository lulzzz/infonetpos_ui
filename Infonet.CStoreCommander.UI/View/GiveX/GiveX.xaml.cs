using System;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.GiveX;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.GiveX
{
    public sealed partial class GiveX : Page
    {
        public GiveXVM GiveXVM { get; set; }
          = SimpleIoc.Default.GetInstance<GiveXVM>();

        public GiveX()
        {
            this.InitializeComponent();
            DataContext = GiveXVM;
            LoadViewModel();

            Loaded -= OnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtCard.Focus(FocusState.Keyboard);
        }

        private void LoadViewModel()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                GiveXVM.ReInitialize();
            }
        }
    }
}
