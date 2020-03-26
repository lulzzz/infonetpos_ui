using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class FourDigitVisibilityConverter : IValueConverter
    {
        public object Convert(dynamic value, Type targetType, object parameter, string language)
        {
            if ((value >= 1000 && value <= 9999) || (value <= -100 && (value >= -9999)))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
