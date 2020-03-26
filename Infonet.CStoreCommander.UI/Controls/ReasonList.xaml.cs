using Infonet.CStoreCommander.UI.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class ReasonList : UserControl
    {
        public string Heading
        {
            get { return (string)GetValue(HeadingProperty); }
            set { SetValue(HeadingProperty, value); }
        }

        public ObservableCollection<string> ItemSource
        {
            get { return (ObservableCollection<string>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }
        public ICommand ItemClicked
        {
            get { return (ICommand)GetValue(ItemClickedProperty); }
            set { SetValue(ItemClickedProperty, value); }
        }

        public static readonly DependencyProperty ItemClickedProperty =
             DependencyProperty.Register(
                 nameof(ItemClicked),
                 typeof(ICommand),
                 typeof(ReasonList),
                 new PropertyMetadata(null));


        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                nameof(ItemSource),
                typeof(ObservableCollection<string>),
                typeof(ReasonList),
                new PropertyMetadata(null));

        public static readonly DependencyProperty HeadingProperty =
            DependencyProperty.Register(
                nameof(Heading),
                typeof(string),
                typeof(ReasonList),
                new PropertyMetadata(null));

        public VMBase VMBase { get; set; }
        public ReasonList()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };

            this.Loaded -= UnselectReasonList;
            this.Loaded += UnselectReasonList;

            messages.SelectionChanged -= MessageSelected;
            messages.SelectionChanged += MessageSelected;
        }

        private void UnselectReasonList(object sender, RoutedEventArgs e)
        {
            UnselectAllItem();
        }

        private void MessageSelected(object sender, SelectionChangedEventArgs e)
        {
            UnselectAllItem();
            SelectItem(e);
        }

        private void SelectItem(SelectionChangedEventArgs e)
        {
            var item = messages.ContainerFromItem(e.AddedItems.FirstOrDefault());
            var selectedItem = item as ListViewItem;

            if (selectedItem != null)
            {
                var grid = selectedItem.ContentTemplateRoot as Grid;
                var symbolIcon = grid.FindName("symbolIcon") as Grid;
                var messageContainer = grid.FindName("messageContainer") as Grid;
                var message = grid.FindName("message") as TextBlock;
                message.Foreground = new SolidColorBrush(Colors.White);
                messageContainer.Background = (SolidColorBrush)Application.Current.Resources["BackgroundColor1Dark"];
                symbolIcon.Background = (SolidColorBrush)Application.Current.Resources["LightGreen"];
            }
        }

        private void UnselectAllItem()
        {
            foreach (var messageListItem in messages.Items)
            {
                var itemContainer = messages.ContainerFromItem(messageListItem);
                var item = itemContainer as ListViewItem;
                if (item != null)
                {
                    var grid = item.ContentTemplateRoot as Grid;
                    var symbolIconContainer = grid.FindName("symbolIcon") as Grid;
                    var messageContainer = grid.FindName("messageContainer") as Grid;
                    var message = grid.FindName("message") as TextBlock;
                    message.Foreground = (SolidColorBrush)Application.Current.Resources["LabelTextForegroundColor"];
                    messageContainer.Background = (SolidColorBrush)Application.Current.Resources["BackgroundColor2"];
                    symbolIconContainer.Background = (SolidColorBrush)Application.Current.Resources["BackgroundColor1Dark"];
                }
            }
        }
    }
}
