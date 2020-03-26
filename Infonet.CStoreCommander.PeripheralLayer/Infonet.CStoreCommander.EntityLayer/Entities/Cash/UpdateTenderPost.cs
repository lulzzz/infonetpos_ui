using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Cash
{
    public class UpdateTenderPost
    {
        public List<Tender> Tenders { get; set; }
        public string DropReason { get; set; }
    }
}
