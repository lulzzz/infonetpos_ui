namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class AddFuelManuallyContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public int pumpId { get; set; }
        public string amount { get; set; }
        public bool isCashSelected { get; set; }
        public string grade { get; set; }
    }
}
