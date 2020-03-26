using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Cash
{
    public class UpdateTenderGet
    {
        public List<Tender> Tenders { get; set; }
        public string TenderedAmount { get; set; }
    }
}
