using Infonet.CStoreCommander.UI.Model.Checkout;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class StoreCreditListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            dynamic val = value;
            if (val != null)
            {
                ObservableCollection<StoreCreditModel> certificates = new ObservableCollection<StoreCreditModel>();
                foreach (var data in val)
                {
                    certificates.Add(data as StoreCreditModel);
                }

                return certificates;
            }
            return null;
        }
    }
}
