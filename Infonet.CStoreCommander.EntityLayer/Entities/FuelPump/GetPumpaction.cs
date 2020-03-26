namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPump
{
    public class GetPumpAction
    {
        public string PumpId { get; set; }
        public bool IsPumpVisible { get; set; }
        public string PumpLabel { get; set; }
        public string PumpMessage { get; set; }
        public string Amount { get; set; }
    }
}
