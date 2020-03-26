using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class BoolToPassswordVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var showPassword = (bool)value;
            return showPassword ? PasswordRevealMode.Visible :
                PasswordRevealMode.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
