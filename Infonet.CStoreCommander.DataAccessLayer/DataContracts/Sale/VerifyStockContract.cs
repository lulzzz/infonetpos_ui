using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class VerifyStockContract
    {
        public Error quantityMessage { get; set; }
        public Error regularPriceMessage { get; set; }
        public bool canManuallyEnterProduct { get; set; }
        public string manuallyEnterMessage { get; set; }
        public AddStockPageContract addStockPage { get; set; }
        public RestrictionPageContract restrictionPage { get; set; }
        public GiftCertificatePageContract giftCertPage { get; set; }
        public GiveXPageContract givexPage { get; set; }
        public PSInetPageContract psInetPage { get; set; }
    }

    public class GiveXPageContract
    {
        public bool openGivexPage { get; set; }
        public string stockCode { get; set; }
        public string regularPrice { get; set; }
    }
    public class PSInetPageContract
    {
        public bool openPSInetPage { get; set; }
        public string stockCode { get; set; }
        public string regularPrice { get; set; }
    }
    public class GiftCertificatePageContract
    {
        public int giftNumber;
        public bool openGiftCertPage { get; set; }
        public string stockCode { get; set; }
        public string regularPrice { get; set; }
    }

    public class RestrictionPageContract
    {
        public bool openRestrictionPage { get; set; }
        public string description { get; set; }
    }

    public class AddStockPageContract
    {
        public bool openAddStockPage { get; set; }
        public string stockCode { get; set; }
    }
}
