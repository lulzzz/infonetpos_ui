using Infonet.CStoreCommander.UI.ViewModel;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class SetQuantityForHotCategories : Page
    {
        public HotCategoriesScreenVM HotCategoriesScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<HotCategoriesScreenVM>();

        public SetQuantityForHotCategories()
        {
            this.InitializeComponent();
            DataContext = HotCategoriesScreenVM;
        }
    }
}
