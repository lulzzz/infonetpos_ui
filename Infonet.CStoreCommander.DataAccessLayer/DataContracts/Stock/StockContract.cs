using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock
{
    public class StockContract
    {
        public string alternateCode { get; set; }
        public string stockCode { get; set; }
        public string description { get; set; }
        public string regularPrice { get; set; }
        public List<string> taxCodes { get; set; }
    }
}
