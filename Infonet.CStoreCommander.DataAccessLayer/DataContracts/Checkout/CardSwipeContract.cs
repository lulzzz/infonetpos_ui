
namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class CardSwipeContract
    {
        public string cardNumber { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public string transactionType { get; set; }

    }
}
