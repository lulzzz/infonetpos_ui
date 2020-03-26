using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    class TripleDigitVisibilityConveter : IValueConverter
    {
        public object Convert(dynamic value, Type targetType, object parameter, string language)
        {
            if ((value >= 100 && value <= 999) || (value <= -10 && (value >= -99)))
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
