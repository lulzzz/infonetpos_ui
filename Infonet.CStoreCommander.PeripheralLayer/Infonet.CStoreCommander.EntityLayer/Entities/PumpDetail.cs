using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.EntityLayer.Entities
{
    public class PumpDetail
    {
        public int PumpNumber { get; set; }
        public string Status { get; set; }
        public Uri Source { get; set; }
        public SolidColorBrush CriticalLevelColor { get; set; }
            = new SolidColorBrush(Color.FromArgb(255, 105, 121, 126));
    }
}
