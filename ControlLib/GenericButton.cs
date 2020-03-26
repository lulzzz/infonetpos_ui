using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.ControlLib
{
    public sealed class GenericButton : Button
    {
        public double UpperHeight
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DependencyProperty UpperHeightProperty =
            DependencyProperty.Register(nameof(UpperHeight),
                typeof(double), 
                typeof(GenericButton),
                new PropertyMetadata(50D));


        public SolidColorBrush BottomBackGroundColor
        {
            get { return (SolidColorBrush)GetValue(BottomBackGroundColorProperty); }
            set { SetValue(BottomBackGroundColorProperty, value); }
        }

        public static readonly DependencyProperty BottomBackGroundColorProperty =
                 DependencyProperty.Register(nameof(BottomBackGroundColor), typeof(SolidColorBrush),
                     typeof(GenericButton), new PropertyMetadata(null));


        public GenericButton()
        {
            DefaultStyleKey = typeof(GenericButton);
        }
    }
}
