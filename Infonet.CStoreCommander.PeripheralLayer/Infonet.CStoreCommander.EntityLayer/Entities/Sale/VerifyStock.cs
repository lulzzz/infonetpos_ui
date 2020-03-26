using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class VerifyStock
    {
        public Error QuantityMessage { get; set; }

        public Error RegularPriceMessage { get; set; }

        public bool CanManuallyEnterProduct { get; set; }

        public string ManuallyEnterMessage { get; set; }

        public AddStockPage AddStockPage { get; set; }

        public RestrictionPage RestrictionPage { get; set; }

        public GiftCertificatePage GiftCertificatePage { get; set; }

        public GiveXPage GiveXPage { get; set; }
        public PSInetPage PSInetPage { get; set; }
    }

    public class GiveXPage
    {
        public bool OpenGiveXPage { get; set; }

        public string StockCode { get; set; }

        public string RegularPrice { get; set; }
    }
    public class PSInetPage
    {
        public bool OpenPSInetPage { get; set; }
        public string StockCode { get; set; }

        public string RegularPrice { get; set; }
    }
    public class GiftCertificatePage
    {
        public bool OpenGiftCertificatePage { get; set; }

        public string StockCode { get; set; }

        public string RegularPrice { get; set; }

        public int GiftNumber { get; set; }
    }

    public class RestrictionPage
    {
        public bool OpenRestrictionPage { get; set; }

        public string Description { get; set; }
    }

    public class AddStockPage
    {
        public bool OpenAddStockPage { get; set; }

        public string StockCode { get; set; }
    }
}
