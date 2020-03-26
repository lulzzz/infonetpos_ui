using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class BottleContract
    {
        public string product { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public int lineNumber { get; set; }
        public int defaultQuantity { get; set; }
        public string price { get; set; }
        public string imageUrl { get; set; }
        public string amount { get; set; }
    }

    public class BottleSaleContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public byte registerNumber { get; set; }
        public string amount { get; set; }
        public List<BottleContract> bottles { get; set; }
    }
}
