using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class GroupFuelPricesPayLoadContract
    {
        public int tillNumber { get; set; }
        public bool isReadTotalizerChecked { get; set; }
        public bool isPricesToDisplayChecked { get; set; }
        public bool isReadTankDipChecked { get; set; }
        public List<FuelPriceContract> groupFuelPrices { get; set; }
    }
}
