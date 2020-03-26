namespace Infonet.CStoreCommander.EntityLayer.Exception
{

    public enum PrinterError
    {
        NotFound,
        Unexpected,
        InvalidFormat,
        CannotCut
    }

    public class PrinterLayerException : System.Exception
    {

        public PrinterLayerException(PrinterError errorCode, System.Exception innerException = null) : base(GetMessage(errorCode), innerException)
        {
            ErrorCode = errorCode;
        }

        public PrinterError ErrorCode
        {
            get; private set;
        }

        public static string GetMessage(PrinterError error)
        {
            switch (error)
            {
                case PrinterError.NotFound:
                    return "No Receipt Printer Available!";
                case PrinterError.InvalidFormat:
                    return "Receipt data is in an invalid format";
                case PrinterError.CannotCut:
                    return "Cannot cut the paper";
                case PrinterError.Unexpected:
                default:
                    return "Printer Error";
            }
        }
    }
}
