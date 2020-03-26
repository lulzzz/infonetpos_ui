using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class TenderDataTemplate : UserControl
    {
        public ICommand OpenNumberPadCommand
        {
            get { return (ICommand)GetValue(OpenNumberPadCommandProperty); }
            set { SetValue(OpenNumberPadCommandProperty, value); }
        }

        public static readonly DependencyProperty OpenNumberPadCommandProperty =
            DependencyProperty.Register(nameof(OpenNumberPadCommand),
                typeof(ICommand),
                typeof(TenderDataTemplate), 
                new PropertyMetadata(null));


        public SaleSummaryVM SaleSummaryVM { get; set; }

        public TenderDataTemplate()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                SaleSummaryVM = DataContext as SaleSummaryVM;
            };
        }
    }
}
