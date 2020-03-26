namespace Infonet.CStoreCommander.EntityLayer.Entities.Payment
{
    public class PaymentByFleet
    {
        public string Caption { get; set; }
        public bool AllowSwipe { get; set; }
        public FleetMessage Message { get; set; }
    }

    public class FleetMessage
    {
        public string Message { get; set; }
        public int MessageType { get; set; }
    }
}
