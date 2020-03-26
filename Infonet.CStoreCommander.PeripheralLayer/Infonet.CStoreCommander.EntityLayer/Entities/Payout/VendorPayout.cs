using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Payout
{
    public class VendorPayout
    {
        public List<Reasons> Reasons { get; set; }
        public List<Vendors> Vendors { get; set; }
        public List<Tax> Taxes { get; set; }
        public PayoutMessage Message { get; set; }
    }

    public class Vendors
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Tax
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
    }

    public class PayoutMessage
    {
        public string ActualMessage { get; set; }
        public int MessageType { get; set; }
    }
}
