using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.TierLevelVM;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.TierLevel
{
    public sealed partial class TierLevel : Page
    {
        public TierLevelVM TierLevelVM { get; set; } =
                   SimpleIoc.Default.GetInstance<TierLevelVM>();

        public TierLevel()
        {
            this.InitializeComponent();
            this.DataContext = TierLevelVM;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                TierLevelVM.Reset();
            }
        }
    }
}
