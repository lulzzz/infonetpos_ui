namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class ReturnSaleContract
    {
        public int saleTillNumber { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public bool isCorrection { get; set; }
        public string reasonCode { get; set; }
        public string reasonType { get; set; }
    }
}