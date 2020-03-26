using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class TaxExemptVerification
    {
        public bool ProcessSiteReturn { get; set; }

        public bool ProcessSiteSale { get; set; }

        public bool ProcessSiteSaleRemoveTax { get; set; }

        public bool ProcessAite { get; set; }

        public bool ProcessQite { get; set; }

        public Error ConfirmMessage { get; set; }

        public string TreatyName { get; set; }

        public string TreatyNumber { get; set; }
    }
}
