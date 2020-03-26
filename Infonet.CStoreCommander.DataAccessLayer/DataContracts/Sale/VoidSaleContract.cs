namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class VoidSaleContract
    {
        public string voidReason { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
    }
}
