using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class AmountDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = 0M;
            if (value != null)
            {
                decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out data);
            }
            return data != 0 ? data.ToString(CultureInfo.InvariantCulture) : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var data = value.ToString();
            return data == "" ? 0 : System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }
    }
}
