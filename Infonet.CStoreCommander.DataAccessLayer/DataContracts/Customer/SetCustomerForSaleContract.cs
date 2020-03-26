namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer
{
    public class SetCustomerForSaleContract
    {
        public string code { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public byte registerNumber { get; set; }
    }
}
