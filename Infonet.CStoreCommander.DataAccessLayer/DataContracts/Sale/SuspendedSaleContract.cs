using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class SuspendedSaleContract
    {
        public List<SuspendedSale> suspendedSale { get; set; }
    }

    public class SuspendedSale
    {
        public int saleNumber { get; set; }
        public string customer { get; set; }
        public int tillNumber { get; set; }
        public LineDisplayContract customerDisplayText { get; set; }
    }
}
