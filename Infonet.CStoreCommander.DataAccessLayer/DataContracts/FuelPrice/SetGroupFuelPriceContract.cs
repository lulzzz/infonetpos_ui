using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice
{
    public class SetGroupFuelPriceContract
    {
        public List<FuelPriceContract> prices { get; set; }
        public int row { get; set; }
    }
}
