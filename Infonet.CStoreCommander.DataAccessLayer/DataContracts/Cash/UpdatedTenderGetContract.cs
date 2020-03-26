using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class UpdatedTenderGetContract
    {
        public List<TenderContract> tenders { get; set; }
        public string tenderedAmount { get; set; }
    }
}
