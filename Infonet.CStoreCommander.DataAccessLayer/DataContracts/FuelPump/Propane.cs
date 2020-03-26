namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class PropaneGradesContract
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string shortname { get; set; }
        public string stockCode { get; set; }
    }

    public class LoadPumpsContract
    {
        public int id { get; set; }
        public string name { get; set; }
        public int positionId { get; set; }
    }

    public class AddPropane
    {
        public int gradeId { get; set; }
        public int pumpId { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public bool isAmount { get; set; }
        public string propaneValue { get; set; }
    }
}
