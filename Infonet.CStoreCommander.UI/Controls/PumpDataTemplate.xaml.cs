using System.Collections;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using System.Windows.Input;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.UI.Model;
using GalaSoft.MvvmLight.Ioc;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class PumpDataTemplate : UserControl
    {
        public static IEnumerable GetItemsSource(DependencyObject obj)
        {
            return obj.GetValue(ItemsSourceProperty) as IEnumerable;
        }

        public static void SetItemsSource(DependencyObject obj, IEnumerable value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }

        public ICommand PumpInteractionCommand
        {
            get { return (ICommand)GetValue(PumpInteractionCommandProperty); }
            set { SetValue(PumpInteractionCommandProperty, value); }
        }

        public ICommand AddBasketCommand
        {
            get { return (ICommand)GetValue(AddBasketCommandProperty); }
            set { SetValue(AddBasketCommandProperty, value); }
        }
        
        public static readonly DependencyProperty AddBasketCommandProperty =
            DependencyProperty.Register(nameof(AddBasketCommand),
                typeof(ICommand),
                typeof(PumpDataTemplate),
                new PropertyMetadata(null));



        public static readonly DependencyProperty PumpInteractionCommandProperty =
            DependencyProperty.Register(
                nameof(PumpInteractionCommand),
                typeof(ICommand),
                typeof(PumpDataTemplate),
                new PropertyMetadata(null));


        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached("ItemsSource",
                typeof(IEnumerable),
                typeof(PumpDataTemplate),
                new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Setup(d as Flyout);
        }
        public HomeScreenVM HomeScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<HomeScreenVM>();

        #region ItemTemplate

        public static DataTemplate GetItemTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(ItemTemplateProperty);
        }
        public static void SetItemTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(ItemTemplateProperty, value);
        }
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.RegisterAttached("ItemTemplate", typeof(DataTemplate),
            typeof(PumpDataTemplate), new PropertyMetadata(null, ItemsTemplateChanged));
        private static void ItemsTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { Setup(d as Flyout); }

        #endregion

        private static async void Setup(Flyout m)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            var s = GetItemsSource(m);
            if (s == null)
                return;
            var t = GetItemTemplate(m);
            if (t == null)
                return;
            var c = new ItemsControl
            {
                ItemsSource = s,
                ItemTemplate = t,
            };
            var n = CoreDispatcherPriority.Normal;
            DispatchedHandler h = () => m.Content = c;
            await m.Dispatcher.RunAsync(n, h);
        }

        public PumpDetailModel PumpDetailModel { get { return this.DataContext as PumpDetailModel; } }


        public PumpDataTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

    }
}
