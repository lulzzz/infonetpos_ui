namespace Infonet.CStoreCommander.EntityLayer.Exception
{
    public class CashDrawerException : System.Exception
    {
        public CashDrawerException(System.Exception innerException = null) : base(innerException?.Message)
        {
        }

        public CashDrawerException(string message) : base(message) { }
    }
}
