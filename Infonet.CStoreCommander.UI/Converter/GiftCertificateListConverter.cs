using Infonet.CStoreCommander.UI.Model.Checkout;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class GiftCertificateListConverter : IValueConverter
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
                ObservableCollection<GiftCertificateModel> certificates = new ObservableCollection<GiftCertificateModel>();
                foreach (var data in val)
                {
                    certificates.Add(data as GiftCertificateModel);
                }

                return certificates;
            }
            return null;
        }
    }
}
