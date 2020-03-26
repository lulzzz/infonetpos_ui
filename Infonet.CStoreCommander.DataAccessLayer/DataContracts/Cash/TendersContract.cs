using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class TendersContract
    {
        public List<TenderContract> tenders { get; set; }

        public TendersContract()
        {
            tenders = new List<TenderContract>();
        }
    }
}
