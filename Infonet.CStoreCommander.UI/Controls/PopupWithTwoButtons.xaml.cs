using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Infonet.CStoreCommander.UI.ViewModel;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class PopupWithTwoButtons : UserControl
    {
        #region Properties
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string HeadingText
        {
            get { return (string)GetValue(HeadingTextProperty); }
            set { SetValue(HeadingTextProperty, value); }
        }

        public string FirstButtonText
        {
            get { return (string)GetValue(FirstButtonTextProperty); }
            set { SetValue(FirstButtonTextProperty, value); }
        }

        public string SecondButtonText
        {
            get { return (string)GetValue(SecondButtonTextProperty); }
            set { SetValue(SecondButtonTextProperty, value); }
        }

        public SolidColorBrush FirstButtonBottomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(FirstButtonBottomBackgroundColorProperty); }
            set { SetValue(FirstButtonBottomBackgroundColorProperty, value); }
        }

        public SolidColorBrush SecondButtonBottomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(SecondButtonBottomBackgroundColorProperty); }
            set { SetValue(SecondButtonBottomBackgroundColorProperty, value); }
        }

        public SolidColorBrush BackgroundOverlay
        {
            get { return (SolidColorBrush)GetValue(BackgroundOverlayProperty); }
            set { SetValue(BackgroundOverlayProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOverlayProperty =
             DependencyProperty.Register(nameof(BackgroundOverlay),
                 typeof(SolidColorBrush),
                 typeof(PopupWithTwoButtons),
                 new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));

        public bool IsThirdButtonVisible
        {
            get { return (bool)GetValue(IsThirdButtonVisibleProperty); }
            set { SetValue(IsThirdButtonVisibleProperty, value); }
        }

        public SolidColorBrush ThirdButtonBottomBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(ThirdButtonBottomBackgroundColorProperty); }
            set { SetValue(ThirdButtonBottomBackgroundColorProperty, value); }
        }

       public static readonly DependencyProperty ThirdButtonBottomBackgroundColorProperty =
            DependencyProperty.Register(nameof(ThirdButtonBottomBackgroundColor),
                typeof(SolidColorBrush), 
                typeof(SolidColorBrush), 
                new PropertyMetadata((SolidColorBrush)Application.Current.Resources["LightGray"]));


        public string ThirdButtonText
        {
            get { return (string)GetValue(ThirdButtonTextProperty); }
            set { SetValue(ThirdButtonTextProperty, value); }
        }

        public static readonly DependencyProperty ThirdButtonTextProperty =
             DependencyProperty.Register(nameof(ThirdButtonText),
                 typeof(string),
                 typeof(string),
                 new PropertyMetadata(null));


        public static readonly DependencyProperty IsThirdButtonVisibleProperty =
            DependencyProperty.Register(nameof(IsThirdButtonVisible),
                typeof(bool),
                typeof(PopupWithTwoButtons),
                new PropertyMetadata(false));



        #endregion

        #region Dependency Property



        public static readonly DependencyProperty ClosePopupCommandProperty =
               DependencyProperty.Register(
                   nameof(ClosePopupCommand),
                   typeof(ICommand),
                   typeof(PopupWithTwoButtons),
                   new PropertyMetadata(null));


        public static readonly DependencyProperty DescriptionProperty =
          DependencyProperty.Register(nameof(Description),
              typeof(string),
              typeof(PopupWithTwoButtons),
              new PropertyMetadata(null));

        public static readonly DependencyProperty SecondButtonBottomBackgroundColorProperty =
                  DependencyProperty.Register(nameof(SecondButtonBottomBackgroundColor),
                      typeof(SolidColorBrush),
                      typeof(PopupWithTwoButtons),
                      new PropertyMetadata(null));


        public static readonly DependencyProperty FirstButtonBottomBackgroundColorProperty =
            DependencyProperty.Register(nameof(FirstButtonBottomBackgroundColor),
                typeof(SolidColorBrush),
                typeof(PopupWithTwoButtons),
                new PropertyMetadata(null));


        public static readonly DependencyProperty SecondButtonTextProperty =
                 DependencyProperty.Register(nameof(SecondButtonText),
                     typeof(string),
                     typeof(PopupWithTwoButtons),
                     new PropertyMetadata(null));


        public static readonly DependencyProperty FirstButtonTextProperty =
             DependencyProperty.Register(nameof(FirstButtonText),
                 typeof(string),
                 typeof(PopupWithTwoButtons),
                 new PropertyMetadata(null));


        public static readonly DependencyProperty HeadingTextProperty =
                DependencyProperty.Register(nameof(HeadingText),
                    typeof(string),
                    typeof(PopupWithTwoButtons),
                    new PropertyMetadata(null));
        #endregion

        #region Commands      
        public ICommand ClosePopupCommand
        {
            get { return (ICommand)GetValue(ClosePopupCommandProperty); }
            set { SetValue(ClosePopupCommandProperty, value); }
        }
        #endregion


        public VMBase VMBase { get; set; }


        public PopupWithTwoButtons()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                VMBase = DataContext as VMBase;
            };
            this.Loaded -= PopupWithTwoButtonsLoaded;
            this.Loaded += PopupWithTwoButtonsLoaded;
        }

        private void PopupWithTwoButtonsLoaded(object sender, RoutedEventArgs e)
        {
            ConfirmButton.Focus(FocusState.Pointer);
        }
    }
}
