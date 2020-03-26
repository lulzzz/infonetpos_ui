namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    /// <summary>
    /// Data contract for Sale line
    /// </summary>
    public class SaleLineContract
    {
        /// <summary>
        /// Line number
        /// </summary>
        public int lineNumber { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Stock code
        /// </summary>
        public string stockCode { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// Discount type
        /// </summary>
        public string discountType { get; set; }

        /// <summary>
        /// Discount Rate
        /// </summary>
        public string discountRate { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// Total Amount
        /// </summary>
        public string totalAmount { get; set; }

        /// <summary>
        /// Allow price change or not
        /// </summary>
        public bool allowPriceChange { get; set; }

        /// <summary>
        /// Allow quantity change or not
        /// </summary>
        public bool allowQuantityChange { get; set; }

        /// <summary>
        /// Allow Discount change or not
        /// </summary>
        public bool allowDiscountChange { get; set; }

        /// <summary>
        /// Allow Discount resaons or not
        /// </summary>
        public bool allowDiscountReason { get; set; }

        /// <summary>
        /// Allow price reason or not
        /// </summary>
        public bool allowPriceReason { get; set; }

        /// <summary>
        /// Allow Return Reason or not
        /// </summary>
        public bool allowReturnReason { get; set; }
        //Tony 03/19/2019
        public string dept { get; set; }
    }
}
