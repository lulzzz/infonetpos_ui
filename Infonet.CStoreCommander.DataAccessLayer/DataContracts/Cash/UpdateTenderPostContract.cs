using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class UpdateTenderPostContract
    {
        public List<TenderContract> tenders { get; set; }
        public string dropReason { get; set; }

        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
    }
}
