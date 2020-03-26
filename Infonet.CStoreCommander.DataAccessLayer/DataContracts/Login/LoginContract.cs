namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Login
{
    /// <summary>
    /// Data Contract for Login API
    /// </summary>
    public class LoginContract
    {
        /// <summary>
        /// Authentication token
        /// </summary>
        public string authToken { get; set; }
        public string trainerCaption { get; set; }
    }
}
