namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock.HotCategory
{
    /// <summary>
    /// Data Contract for Product
    /// </summary>
    public class HotProductContract
    {
        /// <summary>
        /// Button Id
        /// </summary>
        public int buttonId { get; set; }


        /// <summary>
        /// Stock code
        /// </summary>
        public string stockCode { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }        

        /// <summary>
        /// Default Quantity
        /// </summary>
        public string defaultQuantity { get; set; }

        /// <summary>
        /// Image URL of the Product
        /// </summary>
        public string imageUrl { get; set; }
    }
}
