using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class ProductQuantityVisibilityConverter : IValueConverter
    {
        public object Convert(dynamic value, Type targetType, dynamic parameter, string language)
        {
            if (value != 0)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
