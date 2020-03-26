using Infonet.CStoreCommander.UI.ViewModel;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;

namespace Infonet.CStoreCommander.UI.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddItemByCode : Page
    {
        public HotCategoriesScreenVM HotCategoriesScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<HotCategoriesScreenVM>();

        public AddItemByCode()
        {
            this.InitializeComponent();
            DataContext = HotCategoriesScreenVM;
        }
    }
}
