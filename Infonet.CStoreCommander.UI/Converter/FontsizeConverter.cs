using System;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class FontsizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var quantity = (int)value;

            if (quantity <= 99 && quantity >= -9)
            {
                return (double)57;
            }
            else if ((quantity >= 100 && quantity <= 999) || (quantity >= -10 && (quantity <= -99)))
            {
                return (double)47;
            }
            else
            {
                return (double)33;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
