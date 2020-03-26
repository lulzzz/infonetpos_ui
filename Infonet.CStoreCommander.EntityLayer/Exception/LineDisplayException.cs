namespace Infonet.CStoreCommander.EntityLayer.Exception
{
    public class LineDisplayException : System.Exception
    {
        public LineDisplayException(System.Exception innerException = null) : base(innerException?.Message)
        {
        }

        public LineDisplayException(string message) : base(message) { }
    }
}
