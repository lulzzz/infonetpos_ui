namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class DeletePrepayContract
    {
        public int activePump { get; set; }
        public int shiftNumber { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
    }

    public class AddPrepayContract
    {
        public int activePump { get; set; }
        public int shiftNumber { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public string amount { get; set; }
        public string fuelGrade { get; set; }
        public bool isAmountCash { get; set; }
    }

    public class SwitchPrepayContract
    {
        public int activePump { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int newPumpId { get; set; }
    }


    public class AddBasketContract
    {
        public int activePump { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public string basketValue { get; set; }
    }

    public class GetPumpActionContract
    {
        public string pumpId { get; set; }
        public bool isPumpVisible { get; set; }
        public string pumpLabel { get; set; }
        public string pumpMessage { get; set; }
        public string amount { get; set; }

    }

}