using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Reports.ReportOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Reports.ReportOptions
{

    public sealed partial class SalesCount : Page
    {
        public SalesCountVM SalesCountVM { get; set; } =
            SimpleIoc.Default.GetInstance<SalesCountVM>();

        public SalesCount()
        {
            this.InitializeComponent();

            SalesCountVM.ResetVM();
        }

    }
}
