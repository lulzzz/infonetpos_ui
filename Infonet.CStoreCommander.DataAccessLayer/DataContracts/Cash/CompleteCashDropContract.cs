using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class CompleteCashDropContract
    {
        public string envelopeNumber { get; set; }
        public string dropReason { get; set; }
        public int tillNumber { get; set; }
        public short registerNumber { get; set; }
        public List<TenderContract> tenders { get; set; }
    }
}
