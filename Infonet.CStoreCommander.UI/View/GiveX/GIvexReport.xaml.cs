using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.GiveX;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.GiveX
{
    public sealed partial class GivexReport : Page
    {
        public GiveXReportVM GiveXReportVM { get; set; }
          = SimpleIoc.Default.GetInstance<GiveXReportVM>();

        public GivexReport()
        {
            this.InitializeComponent();
        }
    }
}
