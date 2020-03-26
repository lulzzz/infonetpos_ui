using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class SaveStoreCreditContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string transactionType { get; set; }
        public string tenderCode { get; set; }
        public List<StoreCreditsContract> storeCredits { get; set; }

    }

    public class StoreCreditsContract
    {
        public string number { get; set; }
        public string amountEntered { get; set; }
    }
}
