using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payout
{
    public class VendorPayoutContract
    {
        public List<Reason> reasons { get; set; }
        public List<VendorsContract> vendors { get; set; }
        public List<TaxContract> taxes { get; set; }
        public PayoutMessageContract message { get; set; }
    }

    public class VendorsContract
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class TaxContract
    {
        public string code { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
    }

    public  class PayoutMessageContract
    {
        public string message { get; set; }
        public int messageType { get; set; }
    }
}