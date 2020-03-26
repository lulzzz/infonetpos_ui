namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login
{
    /// <summary>
    /// Data Contract for User
    /// </summary>
    public class UserContract
    {
        /// <summary>
        /// Username
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Till number
        /// </summary>
        public int tillNumber { get; set; }

        /// <summary>
        /// Float Amount
        /// </summary>
        public string floatAmount { get; set; }

        /// <summary>
        /// Cash bonus Float
        /// </summary>
        public string cashBonusFloat { get; set; }

        /// <summary>
        /// POS Id
        /// </summary>
        public int posId { get; set; }

        /// <summary>
        /// Shift number
        /// </summary>
        public int shiftNumber { get; set; }

        /// <summary>
        /// Whether Switching user for Unauthorized access or not
        /// </summary>
        public bool unauthorizedAccess { get; set; }
    }
}
