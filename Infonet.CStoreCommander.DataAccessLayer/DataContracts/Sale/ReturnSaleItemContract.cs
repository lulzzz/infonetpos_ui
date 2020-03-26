using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class ReturnSaleItemContract
    {
        public int saleTillNumber { get; set; }
        public int tillNumber { get; set; }
        public List<int> saleLines { get; set; }
        public int saleNumber { get; set; }
        public string reasonCode { get; set; }
        public string reasonType { get; set; }
    }
}
