using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Cash
{
    public class CompleteCashDrop
    {
        public string EnvelopeNumber { get; set; }
        public string DropReason { get; set; }
        public int TillNumber { get; set; }
        public short RegisterNumber { get; set; }
        public List<Tender> Tenders { get; set; }
    }
}
