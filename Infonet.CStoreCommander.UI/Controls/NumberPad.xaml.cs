using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Model.Cash;
using Infonet.CStoreCommander.UI.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class NumberPad : UserControl
    {
        public NumberpadVM NumberpadVM { get; set; }
           = SimpleIoc.Default.GetInstance<NumberpadVM>();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Visibility BackArrowVisibility
        {
            get { return (Visibility)GetValue(BackArrowVisibilityProperty); }
            set { SetValue(BackArrowVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text),
                typeof(string),
                typeof(NumberPad),
                new PropertyMetadata(null));

        public static readonly DependencyProperty BackArrowVisibilityProperty =
            DependencyProperty.Register(nameof(BackArrowVisibility),
                typeof(Visibility),
                typeof(SecondFrameHeading),
                new PropertyMetadata(Visibility.Visible));

        public ObservableCollection<CashButtonModel> CashButtonItemSource
        {
            get { return (ObservableCollection<CashButtonModel>)GetValue(CashButtonItemSourceProperty); }
            set { SetValue(CashButtonItemSourceProperty, value); }
        }

        public static readonly DependencyProperty CashButtonItemSourceProperty =
            DependencyProperty.Register(
                nameof(CashButtonItemSource),
                typeof(ObservableCollection<CashButtonModel>),
                typeof(NumberPad),
                new PropertyMetadata(null));


        public ICommand BackNavigation
        {
            get { return (ICommand)GetValue(BackNavigationProperty); }
            set { SetValue(BackNavigationProperty, value); }
        }

        public static readonly DependencyProperty BackNavigationProperty =
            DependencyProperty.Register(
                nameof(BackNavigation),
                typeof(ICommand),
                typeof(NumberPad),
                new PropertyMetadata(null));

        public bool IsDecimalValueAllowed
        {
            get { return (bool)GetValue(IsDecimalValueAllowedProperty); }
            set { SetValue(IsDecimalValueAllowedProperty, value); }
        }

        public static readonly DependencyProperty IsDecimalValueAllowedProperty =
            DependencyProperty.Register(nameof(IsDecimalValueAllowed),
                typeof(bool),
                typeof(NumberPad),
                new PropertyMetadata(true));



        public bool IsAdditionEnabled
        {
            get { return (bool)GetValue(IsAdditionEnabledProperty); }
            set { SetValue(IsAdditionEnabledProperty, value); }
        }
        public bool IsSubtractionEnabled
        {
            get { return (bool)GetValue(IsSubtractionEnabledProperty); }
            set { SetValue(IsSubtractionEnabledProperty, value); }
        }

        public Visibility IsDollarVisible
        {
            get { return (Visibility)GetValue(IsDollarVisibleProperty); }
            set { SetValue(IsDollarVisibleProperty, value); }
        }




        public static readonly DependencyProperty IsDollarVisibleProperty =
             DependencyProperty.Register(nameof(IsDollarVisible),
                 typeof(Visibility),
                 typeof(NumberPad),
                 new PropertyMetadata(Visibility.Collapsed));



        public ICommand EnterTappedCommand
        {
            get { return (ICommand)GetValue(EnterTappedCommandProperty); }
            set { SetValue(EnterTappedCommandProperty, value); }
        }

        public static readonly DependencyProperty IsSubtractionEnabledProperty =
          DependencyProperty.Register(
              nameof(IsSubtractionEnabled),
              typeof(bool),
              typeof(NumberPad),
              new PropertyMetadata(null));

        public static readonly DependencyProperty IsAdditionEnabledProperty =
            DependencyProperty.Register(
                nameof(IsAdditionEnabled),
                typeof(bool),
                typeof(NumberPad),
                new PropertyMetadata(false));

        public static readonly DependencyProperty EnterTappedCommandProperty =
            DependencyProperty.Register(
                nameof(EnterTappedCommand),
                typeof(ICommand),
                typeof(NumberPad),
                new PropertyMetadata(null));



        public NumberPad()
        {
            this.InitializeComponent();
            this.DataContext = NumberpadVM;
            NumberpadVM.ReInitialize();
        }
    }
}
