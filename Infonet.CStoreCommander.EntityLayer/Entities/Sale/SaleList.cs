namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class SaleList
    {
        public int TillNumber { get; set; }
        public int SaleNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TotalAmount { get; set; }
        public bool AllowCorrection { get; set; }
        public bool AllowReason { get; set; }
    }
}