using GalaSoft.MvvmLight.Command;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class ViewHeading : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(ViewHeading),
                new PropertyMetadata(null));

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register(nameof(BackCommand), typeof(ICommand),
                typeof(ViewHeading),
                new PropertyMetadata(new RelayCommand(() =>
                {
                    NavigateService.Instance.NavigateToHome();
                    InputPane.GetForCurrentView().TryHide();
                })));

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register(nameof(FontSize),
                typeof(double),
                typeof(ViewHeading),
                new PropertyMetadata(32D));

        public VMBase VMBase { get; set; }

        public ViewHeading()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
        }
    }
}
