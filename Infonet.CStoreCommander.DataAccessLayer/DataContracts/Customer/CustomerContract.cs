namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer
{
    /// <summary>
    /// Data Contract for Customer
    /// </summary>
    public class CustomerContract
    {
        public string code { get; set; }
        public string name { get; set; }
        public string loyaltyNumber { get; set; }
        public string loyaltyPoints { get; set; }
        public string phone { get; set; }
    }
}
