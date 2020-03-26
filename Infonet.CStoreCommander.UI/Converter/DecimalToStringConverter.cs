using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (decimal)value;
            return data.ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
