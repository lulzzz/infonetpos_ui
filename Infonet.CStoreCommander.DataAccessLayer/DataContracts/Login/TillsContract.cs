using System.Collections.Generic;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login
{
    /// <summary>
    /// Data Contract for Active Tills API
    /// </summary>
    public class TillsContract
    {
        public string shiftDate;

        /// <summary>
        /// List of tills
        /// </summary>
        public List<Till> tills { get; set; }

        /// <summary>
        /// Shift number
        /// </summary>
        public int shiftNumber { get; set; }

        /// <summary>
        /// Cash float
        /// </summary>
        public string cashFloat { get; set; }

        /// <summary>
        /// Error error
        /// </summary>
        public Error message { get; set; }

        public bool isTrainer { get; set; }

        /// <summary>
        /// Whether shut down POS on error
        /// </summary>
        public bool shutDownPOS { get; set; }

        /// <summary>
        /// Force login on Tills 
        /// </summary>
        public bool forceTill { get; set; }
    }

    /// <summary>
    /// Data Contract for a Till
    /// </summary>
    public class Till
    {
        /// <summary>
        /// Till number
        /// </summary>
        public int tillNumber { get; set; }
    }
}
