namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.GiveX
{
    public class GiveXCardContract
    {
        public string givexCardNumber { get; set; }
        public string givexPrice { get; set; }
        public string amount { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public string stockCode { get; set; }
    }
}
