using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    /// <summary>
    /// Data Contract for New sale
    /// </summary>
    public class SaleContract
    {
        /// <summary>
        /// Till number
        /// </summary>
        public int tillNumber { get; set; }

        /// <summary>
        /// Sale number
        /// </summary>
        public int saleNumber { get; set; }

        /// <summary>
        /// Total Amount
        /// </summary>
        public string totalAmount { get; set; }

        /// <summary>
        /// Customer code 
        /// </summary>
        public string customer { get; set; }

        public bool enableExactChange { get; set; }

        /// <summary>
        /// Sale lines 
        /// </summary>
        public List<SaleLineContract> saleLines { get; set; }

        /// <summary>
        /// Taxes
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// gets or sets sale line errors
        /// </summary>
        public List<ErrorContract> saleLineErrors { get; set; }

        public bool enableWriteOffButton { get; set; }

        public LineDisplayContract customerDisplayText { get; set; }

        public bool hasCarwashProducts { get; set; }
    }
}
