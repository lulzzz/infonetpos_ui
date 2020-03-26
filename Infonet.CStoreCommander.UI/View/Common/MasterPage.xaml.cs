using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Common
{
    public sealed partial class MasterPage : Page
    {
        public MasterPageVM MasterPageVM { get; set; } =
            SimpleIoc.Default.GetInstance<MasterPageVM>();

        public MasterPage()
        {
            this.InitializeComponent();
            NavigateService.Instance.MasterFrame = MasterFrame;
            this.Loaded -= MasterPageLoaded;
            this.Loaded += MasterPageLoaded;
        }

        private void MasterPageLoaded(object sender, RoutedEventArgs e)
        {
            NavigateService.Instance.NavigateToExtendedScreen();
        }
    }
}
