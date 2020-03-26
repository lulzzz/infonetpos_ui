using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class CancelTendersContract
    {
        public bool success { get; set; }
        public LineDisplayContract customerDisplay { get; set; }
    }
}
