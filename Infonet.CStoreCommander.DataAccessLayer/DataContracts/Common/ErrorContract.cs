namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common
{
    /// <summary>
    /// Data contract for Error Message
    /// </summary>
    public class ErrorContract
    {
        /// <summary>
        /// Shut POS on error or not
        /// </summary>
        public bool shutDownPOS { get; set; }

        /// <summary>
        /// Error 
        /// </summary>
        public Error error { get; set; }
    }

    /// <summary>
    /// Class representing the Error of the API
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Type of the Message 
        /// </summary>
        public int messageType { get; set; }
    }
}
