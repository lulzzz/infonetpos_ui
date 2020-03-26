namespace Infonet.CStoreCommander.EntityLayer.Exception
{
    public enum WindowsManagerError
    {
        FailedToGetUserName,
        Unexpected,
        FailedToShutDownSystem
    }

    public class WindowsManagerException : System.Exception
    {
        public WindowsManagerException(WindowsManagerError errorCode, System.Exception innerException = null)
            : base(GetMessage(errorCode), innerException)
        {
            ErrorCode = errorCode;
        }

        public WindowsManagerError ErrorCode
        {
            get; private set;
        }

        private static string GetMessage(WindowsManagerError error)
        {
            switch (error)
            {
                case WindowsManagerError.FailedToGetUserName:
                    return "Failed To Get User Name.";
                case WindowsManagerError.FailedToShutDownSystem:
                    return "Failed To Shut Down System.";
                case WindowsManagerError.Unexpected:
                default:
                    return "Unexpected Error";
            }
        }
    }
}
