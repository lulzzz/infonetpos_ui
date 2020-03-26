using Infonet.CStoreCommander.UI.Model.FuelPump;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;

namespace Infonet.CStoreCommander.UI.Converter
{
    public class TierLevelListConverter : IValueConverter
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
                ObservableCollection<PumpTierLevelModel> pumpTierLevelModel = new ObservableCollection<PumpTierLevelModel>();
                foreach (var data in val)
                {
                    pumpTierLevelModel.Add(data as PumpTierLevelModel);
                }

                return pumpTierLevelModel;
            }
            return null;
        }
    }
}
