using Infonet.CStoreCommander.UI.Utility;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{

    public sealed partial class LoadingPage : Page
    {
        public LoadingService LoadingService { get; set; } 
            = LoadingService.LoadingInstance;

        public LoadingPage()
        {
            this.InitializeComponent();         
        }
    }
}
