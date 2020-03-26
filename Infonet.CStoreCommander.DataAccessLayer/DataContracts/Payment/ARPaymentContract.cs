
namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment
{
    public class ARPaymentContract
    {
        public string customerCode { get; set; }
        public string amount { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public bool isReturnMode { get; set; }
    }
}
