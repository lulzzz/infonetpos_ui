using Infonet.CStoreCommander.UI.ViewModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class Products : Page
    {
        public HotCategoriesScreenVM HotCategoriesScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<HotCategoriesScreenVM>();

        public ICommand AddItemByCode
        {
            get { return (ICommand)GetValue(AddItemByCodeProperty); }
            set { SetValue(AddItemByCodeProperty, value); }
        }

        public ICommand DecreaseQuantityCommand
        {
            get { return (ICommand)GetValue(DecreaseQuantityCommandProperty); }
            set { SetValue(DecreaseQuantityCommandProperty, value); }
        }

        public ICommand IncreaseQuantityCommand
        {
            get { return (ICommand)GetValue(IncreaseQuantityCommandProperty); }
            set { SetValue(IncreaseQuantityCommandProperty, value); }
        }

        public static readonly DependencyProperty AddItemByCodeProperty =
                DependencyProperty.Register(
                    nameof(AddItemByCode),
                    typeof(ICommand),
                    typeof(Products),
                    new PropertyMetadata(null));


        public static readonly DependencyProperty DecreaseQuantityCommandProperty =
            DependencyProperty.Register(nameof(DecreaseQuantityCommand), typeof(ICommand), typeof(Products), new PropertyMetadata(null));


        public static readonly DependencyProperty IncreaseQuantityCommandProperty =
               DependencyProperty.Register(nameof(IncreaseQuantityCommand), typeof(ICommand), typeof(Products), new PropertyMetadata(null));


        public Products()
        {
            this.InitializeComponent();
            DataContext = HotCategoriesScreenVM;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}
