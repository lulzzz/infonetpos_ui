namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login
{
    /// <summary>
    /// Data contract for Login Policies
    /// </summary>
    public class LoginPolicyContract
    {
        /// <summary>
        /// Policies
        /// </summary>
        public LoginPolicy policies { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string message { get; set; }
    }

    /// <summary>
    /// Class representing Login Policy
    /// </summary>
    public class LoginPolicy
    {
        /// <summary>
        /// Pos Id
        /// </summary>
        public int posId { get; set; }

        /// <summary>
        /// Use Windows Login or not
        /// </summary>
        public bool windowsLogin { get; set; }

        /// <summary>
        /// Use shifts or not
        /// </summary>
        public bool useShifts { get; set; }

        /// <summary>
        /// Provides Till float or not
        /// </summary>
        public bool provideTillFloat { get; set; }

        /// <summary>
        /// Provides Cash bonus Float or not
        /// </summary>
        public bool autoShiftPick { get; set; }

        /// <summary>
        /// Use predefined till number or not
        /// </summary>
        public bool usePredefinedTillNumber { get; set; }

        /// <summary>
        /// Langauge on which POS will work
        /// </summary>
        public string posLanguage { get; set; }

        public string keypadFormat { get; set; }
    }
}