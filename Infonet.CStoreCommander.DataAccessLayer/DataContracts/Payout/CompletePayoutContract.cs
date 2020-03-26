using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payout
{
    public class CompletePayoutContract
    {
        public int registerNumber { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string reasonCode { get; set; }
        public string vendorCode { get; set; }
        public string amount { get; set; }
        public List<TaxContract> taxes { get; set; }
    }
}
