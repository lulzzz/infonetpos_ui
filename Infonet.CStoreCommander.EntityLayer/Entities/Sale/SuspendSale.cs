namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class SuspendedSale
    {
        public int SaleNumber { get; set; }

        public string Customer { get; set; }

        public int Till { get; set; }

        public LineDisplayModel LineDisplay { get; set; }
    }
}
