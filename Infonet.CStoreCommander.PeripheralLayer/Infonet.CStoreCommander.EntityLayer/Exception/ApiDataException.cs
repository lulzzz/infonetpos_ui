using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.EntityLayer.Exception
{
    public class ApiDataException : System.Exception
    {
        public Error Error { get; set; }

        public override string Message
        {
            get
            {
                if (Error != null)
                {
                    return Error.Message;
                }
                return Message;
            }
        }
    }
}
