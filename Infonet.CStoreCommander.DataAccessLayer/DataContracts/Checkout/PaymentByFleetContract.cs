using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class PaymentByFleetContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string cardNumber { get; set; }
        public string profileId { get; set; }
        public List<PromptContract> prompts { get; set; }
    }

    public class PromptContract
    {
        public string promptMessage { get; set; }
        public string promptAnswer { get; set; }
    }
}