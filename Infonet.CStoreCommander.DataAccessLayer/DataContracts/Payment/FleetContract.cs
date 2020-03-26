namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment
{
    public class FleetContract
    {
        public string caption { get; set; }
        public bool allowSwipe { get; set; }
        public FleetMessageContract message { get; set; }
    }

    public class FleetMessageContract
    {
        public string message { get; set; }
        public int messageType { get; set; }
    }
}