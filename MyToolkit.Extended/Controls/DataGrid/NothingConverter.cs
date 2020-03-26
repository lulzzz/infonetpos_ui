using System;
using Windows.UI.Xaml.Data;

namespace MyToolkit.Extended.Controls.DataGrid
{
    public class NothingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
    }
}
